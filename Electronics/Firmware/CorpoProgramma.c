/********************************************************************
 FileName:      CorpoProgramma.c
 Dependencies:  See INCLUDES section
 Processor:		PIC18F2553
 Hardware:		Mio
 Complier:  	Microchip C18
 Owner:         Adriano Arcadipane

Questo file contiene tutte la parte principale del programma
********************************************************************/
#include "Strutture_e_prototipi.h"
#include "CorpoProgramma.h"

/****** CODICE ******************************************************/
void Principale(void){ 
    LED = 1;
    delay_ms(300);                                //Pausa per avviare e stabilizzare tutti i sistemi
    recupera_eeprom();                            //Recupera il contenuto della EEPROM e la mette nelle variabili oppurtune
    taratura = autodiagnosi_e_calibrazione();     //Se qualcosa non va entra in errore, se è tutto ok ritorna la calibrazione di barometro, accelerometro e giroscopio
    stato_macchina = 0;                           //Lo stato macchina è a zero (attesa del lancio)
    stato_memorizzazione = 0;                     //Memorizzazione non attiva
    azzera_variabili();                           //Azzera le variabili importanti (lo stato) 
    LED = 0;                                      //Spegne il led per risparmiare energia
    INTCONbits.GIEH = 1;                          //Abilita tutti gli interrupt ad alta priorità 
    T0CONbits.TMR0ON = 1;                         //Accende il timer per il ciclo principale
    T1CONbits.TMR1ON = 1;                         //Accende il timer per l'interrupt del servo 
    //simulazione = 1;

    while(1){
        if (INTCONbits.TMR0IF == 1){                  //Quando il timer0 va in overflow
            INTCONbits.TMR0IF = 0;                    //Pulisce il bit di flag
            WriteTimer0 (TIMER0_START_VALUE);         //Azzera il timer al valore stabilito (vedi HardwareProfile)
            //Gli interrupt sono disabilitati mentre avviene l'assegnazione di PWM_basso e PWM_alto perchè
            //se avvenisse tra le due istruzioni si avrebbero errori.
            INTCONbits.GIEH = 0;  
            pwm_alto = PWM.alto;                      //Aggiorna i segnali PWM
            pwm_basso = PWM.basso;
            INTCONbits.GIEH = 1;                      //Riabilita gli interrupt
            leggi_sensori(&dati_raw);                 //Legge tutti i sensori e li mette nella struttura dati_raw
            if (simulazione == 1) simula_dati();      //Se la simulazione è attiva, cambia i dati raccolti dai sensori
            osservatore(&dati_raw,&stato);            //Osservatori di Kalman per ricostruire lo stato
            memorizzazione();                         //Se è il caso memorizza su FRAM
            riempimento_buffer();                     //Riempie il buffer memoria
            
            switch(eeprom.programma){
	            
	            //codice 0 -> controllo normale
	            case 0:{ 
                    //Il blocco del controllore viene eseguito ogni tot cicli perchè l'attuatore non sarebbe altrimenti in grado
                    //di seguire un controllo così veloce.
                    //Se siamo in attesa del lancio tiene le alette ruotate a zero, appena viene rilevato il decollo
                    //inizia a controllare. Si evita di cominciare subito il controllo perchè altrimenti il termine
                    //integrale del PID potrebe crescere a causa della deriva del giroscopio.
	                if (contatore_controllore >= DIVISORE_FREQUENZA_CONTROLLO){
		                if (stato_macchina == 0){
			                stato.rotazione_rad = 0;
			            }
			            else{
                            stato.rotazione_rad = controllore();     //Decide di quanto ruotare le alette sulla base dei dati acquisiti
                        }         
                        PWM = ruota(stato.rotazione_rad);        //Converte la rotazione in rad in una valori PWM. L'algoritmo filtra eventuali sovrarotazioni.
                        contatore_controllore = 0;               //Riporta a zero il contatore
                    }
                    else contatore_controllore++;
                 }
                 break; //Fine case 0 
                 
                 //Codice 1 -> Tiene le alette ruotate di un angolo fisso. Questo angolo è contenuto ai byte 17 e 18 della
                 //EEPROM interna
                 case 1:{
	                 stato.rotazione_rad = eeprom.rotazione_per_programma_1;
	                 PWM = ruota(stato.rotazione_rad);      //Aggiorna l'onda rettangolare da mandare al servo
	             }
	             break; //Fine case 1  
	             
	             //Codice 2 -> Demo interazione giroscopio-servo diretto (senza filto di kalman)
	             case 2:{
		             float coeff_ang;
		             LED = 1;                               //Tieni acceso il led
		             coeff_ang = eeprom.servo_max / 0.64;    //Trovo il coefficiente di proporzionalità tra V angolare e angolo
		             stato.rotazione_rad = coeff_ang * (dati_raw.r - taratura.R0) * CONVERTI_DIGIT_RAD_S;   //Trovo la rotazione in radianti
		             
		             PWM = ruota (stato.rotazione_rad);
		         }
		         break; //Fine case 2
		         
		         //Codice 3 -> Demo interazione giroscopio-servo con filtro di Kalman attivo
	             case 3:{
		             BUZZER = 0;                            //Tiene il buzzer spento
		             LED = 1;                               //Tieni acceso il led
		             stato.rotazione_rad = controllore();     //Decide di quanto ruotare le alette sulla base dei dati acquisiti
		             PWM = ruota (stato.rotazione_rad);
		         }
		         break; //Fine case 3
		         
		         //Codice 4 -> Tiene le alette ruotate di un angolo fisso, poi a metà volo (condizione sulla velocità)
		         //ruota le alette al contrario per simulare una manovra.
		         case 4:{
			         static char condizione = 0;  //flag che diventa 1 una volta avvenuto l'evento
			         //All'inizio la rotazione è quella impostata sulla EEPROM
			         //Se il razzo è partito, è in freanata (acc < 0) e la velocità scende sotto i 55 m/s si è ragginta la condizione
			         if ((stato_macchina >= 1) && (stato.a <= 0) && (stato.v <= 55)){
				         condizione = 1;
				     }
				     if (condizione == 0){  //Prima che avvenga la condizone
					   stato.rotazione_rad = eeprom.rotazione_per_programma_1; //prima che la condizione sopraggiunga ruotale normalmente
					 }
					 else {                 //A condizione avvenuta
			           stato.rotazione_rad = - eeprom.rotazione_per_programma_1;  //Inverte il segno della rotazione
			         }      
			         
			         PWM = ruota(stato.rotazione_rad);      //Aggiorna l'onda rettangolare da mandare al servo
			     }
		         break; //Fine case 4    
		         
		         //Se si prova a dare un comando che non esiste va in errore con codice 9
		         default:{                  //E' l'equivalente dell' ELSE
			         gestione_errori(9);
		         }
		         break; //Fine case else
		         
           }//Fine select case
//**********************************************************************************	       
	       /*unsigned int kkk;
	       extern unsigned char ToSendDataBuffer[64];
	       fram_write(5,17);
	       for(kkk = 0; kkk<64; kkk++){
		       ToSendDataBuffer[kkk] = gyro_read_register(WHO_AM_I);        
		   } 
		   */
//**********************************************************************************
	       /*unsigned char kkk,jjj;
	       extern unsigned char ToSendDataBuffer[64];
	       INTCONbits.GIEH = 0;
	       for(jjj = 0; jjj < 255; jjj++){
			   fram_write(0,jjj);
			   for(kkk=1;kkk<64;kkk++){
		           ToSendDataBuffer[kkk] = fram_read(0);
		           delay_ms(1);
		       }   	
		   delay_ms(1);			
           }  */ 
//**********************************************************************************	          
           /*ruota(0);                           
           delay_ms(1000);
           delay_ms(1000);
           delay_ms(1000);
           ruota(0.1396);               //ruota di 8° in verso orario
           delay_ms(1000);
           ruota(-0.52);                //ruota di 30° in verso antiorario
           delay_ms(1000);*/
       
       
//**********************************************************************************

            switch(stato_macchina){
	            
                //In attesa del lancio
                case 0:{   
	                static unsigned char var_bip = 0;      //Variabile per creare il bip al lancio
	                //Fa un doppio bip ogni secondo fino alla partenza.
	                var_bip++;
	                if ((var_bip < 56)/* || ((var_bip > 75)&&(var_bip < 131))*/) BUZZER = 1; else BUZZER = 0; 
	                         
	                //if (stato.v > 10){  //Dopo i primi due voli ho cambiato il criterio
		            //di rilevazione del decollo
		            if (stato.a > 25){    
	                    pnt_inizio_buffer_mem = pnt_buffer_mem;      //Imposto l'inizio del buffer circolare
	                    stato_macchina++;                            //Incremento lo stato macchina di 2
	                    stato_macchina++;               //per saltare direttamente al case 2
	                    stato_memorizzazione = 1;                    //Attiva la memorizzazione
	                    BUZZER = 0;                                  //Spegne il buzzer che potrebbe essere rimasto acceso
	                }             
	                break;
                } //FINE case0
                
                //In volo
                //NON UTILIZZATO! SI VA DIRETTAMENTE DALLO STATO 0 ALLO STATO 2!!!
                case 1:{           
	                if (stato.v <= 0){  //quando la velocità diventa negativa siamo all'apogeo
		                stato_macchina++;       //vai al prossimo livello
		            }
	                break;
	            }
	            
	            //Quando finisce la memoria dati scrive il registro memoria e va alla fine
	            case 2:{           
	                if (stato_memorizzazione == 0){  //quando la routine memorizzazione ha posto lo stato_memorizzazione a 0 perchè è finita la memoria...  
		                //Scrive il registro memoria sulla FRAM		                
		                fram_write (0,0);                             //Scrive il byte "USED"
		                fram_write (4,(char)(indirizzo_fram >> 16));          //Scrive l'ultimo indirizzo usato in tre pezzi
		                fram_write (5,(char)(indirizzo_fram >> 8));      
		                fram_write (6,(char)(indirizzo_fram));
		                fram_write (7,(char)(((unsigned int)(taratura.baro*10))>>8)); //Scrive il valore di taratura del barometro moltiplicato per 10
		                fram_write (8,(char)((unsigned int)(taratura.baro*10)));      
		                fram_write (9,(char)(((unsigned int)(taratura.acc*10))>>8));  //Scrive il valore di taratura del barometro moltiplicato per 10
		                fram_write (10,(char)((unsigned int)(taratura.acc*10)));    
		                fram_write (11,(char)(((signed int)(taratura.P0*10))>>8));  //Scrive il valore di taratura del giroscopio (asse X) moltiplicato per 10
		                fram_write (12,(char)((signed int)(taratura.P0*10)));  
		                fram_write (13,(char)(((signed int)(taratura.Q0*10))>>8));  //Scrive il valore di taratura del giroscopio (asse Y) moltiplicato per 10
		                fram_write (14,(char)((signed int)(taratura.Q0*10)));  
		                fram_write (15,(char)(((signed int)(taratura.R0*10))>>8));  //Scrive il valore di taratura del giroscopio (asse Z) moltiplicato per 10
		                fram_write (16,(char)((signed int)(taratura.R0*10)));    
		                
		                PWM = ruota (0.0);     //Calcola la poszizione centrale del servocomando
		                pwm_alto = PWM.alto;   //Aggiorna immediatamente i segnali PWM
                        pwm_basso = PWM.basso;
                        delay_ms(250);         //Attende che ci sia un interrupt che aggiorni la posizione del servo
		                INTCONbits.GIEH = 0;   //Disabilita gli interrupt ad alta priorità per fermare il PWM (disabilita così il servo)
		                stato_macchina++;      //Aumenta lo stato macchina
		                missione_compiuta();   //Vai al loop infinito     
		            }
	                break;
	            }
                    
            } //FINE Switch
        } //FINE if
    }  //FINE while(1)
} //FINE Principale


void missione_compiuta(void){
	unsigned int jjj = 0;
	LED = 1;
	//Fai due minuti di pausa
	for(jjj = 0; jjj < 120; jjj++){
	    delay_ms(600);
	    LED = 1;
	}
	//Bippa all'infinito per ritrovare il modello
	while(1){
		LED = 0;
	    BUZZER = 1;
	    delay_ms(200);
	    BUZZER = 0;
	    delay_ms(200);
	    BUZZER = 1;
	    delay_ms(200);
	    delay_ms(200);
	    delay_ms(200);
	    delay_ms(200);
	    delay_ms(200);
	    BUZZER = 0;
	    delay_ms(200);
	    delay_ms(200);
	    delay_ms(200);
	    delay_ms(200);
	    delay_ms(200);
	}
}//FINE missione_compiuta


//Funzione che ruota il servo alla posizione voluta espressa in radianti. Gli angoli
//sono calcolati a partire dalla posizione centrale impostata tramite PC.
//Viene eseguito anche un doppio controllo sull'entità di tale spostamento che evita
//le rotture meccaniche.
//Controllo 1: Controllo dell'input con i valori minimi e massimi recuperati dalla EEPROM
//             affinchè non si abbia lo stallo delle alette. Se è entro il range restituisce
//             0, se oltre setta il valore limite di deflessione consentito e restituisce 1.
//Controllo 2: Controlla che il valore calcolato della parte alta del duty_cycle sia entro 
//             il range [0,9  2,1]ms per evitare rotture del servocomando. Ciò è necessario
//             come ultima spiaggia qualora il calcolo dia un risultato assurdo per qualche
//             motivo. In questo caso restituisce 2

StrValoriPWM ruota (float spostamento_in_rad){
	StrValoriPWM valori_di_ritorno;
	spostamento_in_rad = -spostamento_in_rad;    //Il segno va invertito perchè mi sono accorto che il servo gira al contrario
	valori_di_ritorno.byte_verifica = 0;        //Pone a zero il byte di verifica
	if(spostamento_in_rad < eeprom.servo_min){  //Se siamo fuori range
		spostamento_in_rad = eeprom.servo_min;  //Imposta la deflessione limite
		valori_di_ritorno.byte_verifica = 1;   //Restituisce codice di controllo 1
	}
	if(spostamento_in_rad > eeprom.servo_max){
		spostamento_in_rad = eeprom.servo_max;      //Imposta la deflessione limite
		valori_di_ritorno.byte_verifica = 1;   //Restituisce codice di controllo 1
	}
	//valori_di_ritorno.alto = ((eeprom.servo_zero + (eeprom.trasferenza_rad_ms * spostamento_in_rad))/3.333333E-4);
	//valori_di_ritorno.alto = 65535 - valori_di_ritorno.alto;
	valori_di_ritorno.alto = (65536 - ((eeprom.servo_zero + (eeprom.trasferenza_rad_ms * spostamento_in_rad))/3.333333E-4));
	valori_di_ritorno.basso = 71072 - valori_di_ritorno.alto;
	if(valori_di_ritorno.alto > 62836){                   //Controlla di non uscire fuori dal range del servo
		valori_di_ritorno.alto = 62836;
		valori_di_ritorno.basso = 8236;
		valori_di_ritorno.byte_verifica = 2;               //Restituisce codice di controllo 2
    }   
    if(valori_di_ritorno.alto < 59235){
		valori_di_ritorno.alto = 59235;
		valori_di_ritorno.basso = 11836;
		valori_di_ritorno.byte_verifica = 2;               //Restituisce codice di controllo 2
    }   
    return (valori_di_ritorno);
}//FINE setta_duty





//ESEGUE L'AUTODIAGNOSI DEL SISTEMA CONTROLLANDO CHE I SENSORI
//SIANO INTEGRI E CHE LA BATTERIA NON SIA SCARICA.
//SE è TUTTO OK RITORNA LA CALIBRAZIONE DI ACCELEROMETRO E BAROMETRO, ALTRIMENTI
//ENTRA IN ERRORE.
StrZero autodiagnosi_e_calibrazione(void){
    StrZero temp;      //Dichiaro una struttura di taratura di nome "temp"   
    short long V_batt = 0;
    unsigned int iii = 0;
    temp.baro = 0;   //Azzera le variabili della struttura
    temp.acc = 0;
    temp.P0 = 0;
    temp.Q0 = 0;
    temp.R0 = 0;
    
    for(iii = 0; iii < 2048; iii++){                         //Raccoglie molti sample in un'unica variabile
        V_batt = V_batt + ADC_get_sample( BATTERIA );
        temp.baro = temp.baro + ADC_get_sample( BAROMETRO );
        temp.acc = temp.acc + ADC_get_sample( ACCELEROMETRO );
        temp.P0 = temp.P0 + gyro_get_axis(ASSE_X);
        temp.Q0 = temp.Q0 + gyro_get_axis(ASSE_Y);
        temp.R0 = temp.R0 + gyro_get_axis(ASSE_Z);
        delay_ms(1);                     //C'è una pausa tra un sample e l'altro
    }
    
    V_batt = V_batt /2048;               //Divide per il numero di campioni raccolti per trovare il valor medio               
    temp.baro = temp.baro / 2048;     
    temp.acc = temp.acc / 2048;                             
    temp.P0 = temp.P0 / 2048;   
    temp.Q0 = temp.Q0 / 2048;
    temp.R0 = temp.R0 / 2048;

    if (V_batt < eeprom.low_battery_value) gestione_errori(2);                          
    if (temp.baro < 2303 || temp.baro > 3510) gestione_errori(3);                 
    if (temp.P0 < -150 || temp.P0 > 150) gestione_errori(4);   //La velocità angolare da fermo deve essere entro +/- 2,6°/s
    if (temp.Q0 < -150 || temp.Q0 > 150) gestione_errori(5);
    if (temp.R0 < -150 || temp.R0 > 150) gestione_errori(6);
    if (temp.acc < 1848 || temp.acc > 2248) gestione_errori(7);  
    
    return(temp);
}//FINE autodiagnosi





//Legge tutti i sensori esistenti e li mette nella struttura dati. bisogna passare
//l'indirizzo della struttura dati, ad esempio
//  leggi_sensori(&dati_raw);
//(vedi la struttura dati di tipo StruDati definita all'inizio)
void leggi_sensori(StrSensori *p_dati){
	p_dati->acc = ADC_get_sample(ACCELEROMETRO);
    p_dati->baro = ADC_get_sample(BAROMETRO);
    p_dati->analog_aux = ADC_get_sample(ANALOG_AUX);
    p_dati->p = gyro_get_axis(ASSE_X);
    p_dati->q = gyro_get_axis(ASSE_Y);
    p_dati->r = gyro_get_axis(ASSE_Z);
}
//Altra versione meno efficiente senza puntatori
/*StrSensori leggi_sensori(void){
    StrSensori dati_temp;
    dati_temp.baro = ADC_get_sample(BAROMETRO);
    dati_temp.analog_aux = ADC_get_sample(ANALOG_AUX);
    dati_temp.p = gyro_get_axis(ASSE_X);
    dati_temp.q = gyro_get_axis(ASSE_Y);
    dati_temp.r = gyro_get_axis(ASSE_Z);
    return dati_temp;
}*/



//ELABORA I DATI RACCOLTI DAI SENSORI
void osservatore(StrSensori *pnt_dati_da_elaborare,StrStato *pnt_stato){
    float acc_in_ms2,
          baro_in_metri,
          p_calibrato,
          p_in_rad_s,
          x_meno,
          v_meno,
          a_meno,
          v_ang_meno,
          a_ang_meno;


 //SEZIONE MOTO VERTICALE
 
    baro_in_metri = da_ADC_a_metri(pnt_dati_da_elaborare->baro , taratura.baro);
    acc_in_ms2 = da_ADC_a_ms2(pnt_dati_da_elaborare->acc , taratura.acc);      //Azzero i dati dell'accelerometro con la lettura iniziale

    //Kalman per la traiettoria verticale. Vedi HardwareProfile per i PHI e per i K
    x_meno = pnt_stato->x  +  pnt_stato->v * PHI_VERTICAL_12  +  pnt_stato->a * PHI_VERTICAL_13;
    v_meno =                  pnt_stato->v                    +  pnt_stato->a * PHI_VERTICAL_23;
    a_meno =                                                     pnt_stato->a;

    pnt_stato->x = x_meno + K11_VERTICAL*(baro_in_metri - x_meno) + K12_VERTICAL*(acc_in_ms2 - a_meno);
    pnt_stato->v = v_meno + K21_VERTICAL*(baro_in_metri - x_meno) + K22_VERTICAL*(acc_in_ms2 - a_meno);
    pnt_stato->a = a_meno + K31_VERTICAL*(baro_in_metri - x_meno) + K32_VERTICAL*(acc_in_ms2 - a_meno);


 //SEZIONE ROLLIO
    p_calibrato = pnt_dati_da_elaborare->r - taratura.R0;  //slitto la lettura del giroscopio del valore di taratura
    p_in_rad_s = p_calibrato * CONVERTI_DIGIT_RAD_S;     //Converte la velocità angolare in rad/s. Il fattore dipende dal fondoscala del giroscopio
    
    //Kalman per il rollio. Vedi HardwareProfile per i PHI e per i K
    v_ang_meno = pnt_stato->v_ang + pnt_stato->a_ang * PHI_ROLL_11;
    a_ang_meno =                    pnt_stato->a_ang;

    pnt_stato->v_ang = v_ang_meno + K1_ROLL * (p_in_rad_s - v_ang_meno);
    pnt_stato->a_ang = a_ang_meno + K2_ROLL * (p_in_rad_s - v_ang_meno);

}//FINE osservatore*/

//Vecchia versione senza puntatori
/*StrStato osservatore(StrSensori dati_da_elaborare){
    float baro_azzerato,
          x_meno,
          v_meno,
          a_meno;
    StrStato stato_temp;
    
    baro_azzerato = dati_da_elaborare.baro - baro_zero;       //Azzera i dati del barometro con la lettura iniziale
    
    //Kalman per la traiettoria verticale. Vedi HardwareProfile per i PHI e per i K
    x_meno = stato_temp.x  +  stato_temp.v * PHI_12  +  stato_temp.a * PHI_13;
    v_meno =                  stato_temp.v           +  stato_temp.a * PHI_23;
    a_meno =                                            stato_temp.a;

    stato_temp.x = x_meno + K1*(baro_azzerato - x_meno);
    stato_temp.v = v_meno + K2*(baro_azzerato - x_meno);
    stato_temp.a = a_meno + K3*(baro_azzerato - x_meno);

    //Kalman per il giroscopio
    return(stato_temp);
}//FINE osservatore*/


//Controllore PID
float controllore (void){
	//Variabili dichiarate al primo ingresso nella funzione
	      
	errore_del_setpoint = SETPOINT - stato.v_ang;   //Computa l'errore
	P = errore_del_setpoint * Kp;                            //Computa l'azione proporzionale
	if (P < LOWER_P_LIMIT) P = LOWER_P_LIMIT;   
	if (P > UPPER_P_LIMIT) P = UPPER_P_LIMIT;
	
	if (Ki > 0){                   //Se è presente l'azione integrale la calcola
        i_inst = errore_del_setpoint * Ki;       //Calcola l'integrale istantaneo
	    //ANTI WINDUP! L'azione integrale è aggiornata solo se l'uscita non è in saturazione.
	    if ((Out > LOWER_TOTAL_LIMIT) && (Out < UPPER_TOTAL_LIMIT)){
	        I = I + i_inst;                          
	    }
	    if (I < LOWER_I_LIMIT) I = LOWER_I_LIMIT;   
     	if (I > UPPER_I_LIMIT) I = UPPER_I_LIMIT;
	}
	else{
		I = 0;
	}
	 
	if (Kd > 0){                        //Se è presente l'azione derivativa la calcola
		D = Kd * (errore_del_setpoint - old_error);   
		old_error = errore_del_setpoint;
		if (D < LOWER_D_LIMIT) D = LOWER_D_LIMIT;   
     	if (D > UPPER_D_LIMIT) D = UPPER_D_LIMIT;
	}
	else {
		D = 0;
	}
	
	Out = P + I + D;   //mette insieme le tre azioni e controlla
	if (Out < LOWER_TOTAL_LIMIT) Out = LOWER_TOTAL_LIMIT;   
    if (Out > UPPER_TOTAL_LIMIT) Out = UPPER_TOTAL_LIMIT;

return(Out);
}//FINE controllore



//RIEMPIE IL BUFFER MEMORIA. In ingresso (implicito) ci sono le strutture dati.
//Per agire sul buffer sfrutta i puntatori globali.
void riempimento_buffer(void){
	unsigned char iii;
	//Dichiaro il puntatore alle strutture di tipo char, così scrivo il tutto byte per byte
	unsigned char *pnt_alla_struttura; 
	
	//Mette nel buffer il contenuto della struttura DATI_RAW
	pnt_alla_struttura = (unsigned char*)&dati_raw.acc; //Punta al primo byte della struttura (converto il puntatore a char)
	for(iii = 0; iii < 12; iii++){
		*pnt_buffer_mem = *pnt_alla_struttura;  //Riempie l'elemento
		pnt_buffer_mem ++;                      //incrementa i puntatori
		pnt_alla_struttura ++;
	}
	
	//Mette nel buffer il contenuto della struttura STATO
	pnt_alla_struttura = (unsigned char*)&stato.x; //Punta al primo byte della struttura (converto il puntatore a char)
	for(iii = 0; iii < 28; iii++){
		*pnt_buffer_mem = *pnt_alla_struttura;  //Riempie l'elemento
		pnt_buffer_mem ++;                      //incrementa i puntatori
		pnt_alla_struttura ++;
	}
    //Se il buffer è arrivato alla fine, azzera il puntatore.
    if (pnt_buffer_mem >= &buffer_mem[0] + DIM_BUF_MEMORIA) pnt_buffer_mem = &buffer_mem[0];
}//FINE riempimento_buffer

//MEMORIZZA LE INFORMAZIONI DEL BUFFER 
void memorizzazione(void){
    //Se la memorizzazione è attivata e se il buffer è pieno scrive il buffer sulla memoria
    //cominciando dall'indirizzo marcato come inizio dall'apposito puntatore
    if(stato_memorizzazione && (pnt_buffer_mem == pnt_inizio_buffer_mem) ){

        //Indirizzo FRAM a 24bit. Dichiarato come STATIC. Parte da 100 perchè le celle precedenti sono
        //occupate dal registro memoria  
        unsigned int iii;
        LED = ~LED;              //Inverte il led all'inizio e alla fine della memorizzazione per far vedere che il programma gira
        fram_write_enable();                    //Abilita la scrittura sulla FRAM
        CS_MEMORIA = 0;
        WriteSPI(WRITE);                        //Invia il comando di scrittura
        WriteSPI((char) (indirizzo_fram >> 16));              //Invia l'indirizzo in tre pezzi
        WriteSPI((char) (indirizzo_fram >> 8));
        WriteSPI((char) (indirizzo_fram));
        for(iii = 0; iii < DIM_BUF_MEMORIA; iii++){   //Scrive tutti i dati in maniera sequenziale
            WriteSPI(*pnt_buffer_mem);
            pnt_buffer_mem++;
            if (pnt_buffer_mem >= &buffer_mem[0] + DIM_BUF_MEMORIA) pnt_buffer_mem = &buffer_mem[0];   //Se il puntatore è alla fine del buffer lo riporta all'inizio
        }//fine FOR
        CS_MEMORIA = 1;
        indirizzo_fram = indirizzo_fram + DIM_BUF_MEMORIA;   //Incrementa l'indirizzo per la prossima scrittura
        if (indirizzo_fram >= 130900) {
            stato_memorizzazione = 0;  //Se la memoria è terminata smette di memorizzare
        }    
        if (pnt_buffer_mem != pnt_inizio_buffer_mem) gestione_errori(10);  //SOLO PER DEBUG!
        LED = ~LED;              //Inverte il led all'inizio e alla fine della memorizzazione per far vedere che il programma gira
    }//fine IF
}//fine memorizzazione




//ESEGUE UN TRILLO SEGUITO DAL CODICE DELL'ERRORE. PER GLI ERRORI GRAVI IL CIRCUITO ENTRA
//IN UN CICLO DI BIP INFINITI, PER ALTRI IL PROGRAMMA PROSEGUE DOPO UNA SOLA SEGNALAZIONE
//ACUSTICA
void gestione_errori (unsigned char codice_errore){
while(1){                       //esegui all'infinito
    unsigned char iii;
    for (iii = 1; iii <= 80; iii++){    //Trillo fastidioso per segnalare lo stato di errore
      BUZZER = ~BUZZER;
      delay_ms(16);
    }
    delay_ms(400);
    for (iii = 1; iii <= codice_errore; iii++){     //numero di bip corti pari al codice di errore
      BUZZER = 1;
      delay_ms(150);     //Durata bip
      BUZZER = 0;
      delay_ms(100);     //Intervallo tra due bip
    }
    delay_ms(700);
  //per codice 2 (batt scarica) torna al programma principale
  if (codice_errore == 2) break;  
  }  
}//FINE gestione_errori

//LEGGE IL CANALE ANALOGICO
int ADC_get_sample(unsigned char canale){
    SetChanADC( canale ); // Set channel
    ConvertADC();         // Start conversion
    while( BusyADC() );   // Wait for completion
    return( ReadADC());   // Read result
}//FINE ADC_get_sample

//Recupera i dati dalla eeprom, li ricompone e li
//mette nelle rispettive variabili globali
void recupera_eeprom(void){
	//P0, Q0 ed R0 fissi vengono aboliti dalla versione 0.10
//    eeprom.P0 = ((int)readIntEEPROM(0) << 8) | readIntEEPROM(1);
//    eeprom.Q0 = ((int)readIntEEPROM(2) << 8) | readIntEEPROM(3);
//    eeprom.R0 = ((int)readIntEEPROM(4) << 8) | readIntEEPROM(5);
    eeprom.low_battery_value = ((int)readIntEEPROM(6) << 8) | readIntEEPROM(7);
    //Per i seguenti c'è una divisione per 10000 perchè erano stati memorizzati moltiplicati per 10000
    eeprom.servo_zero = ((int)readIntEEPROM(8) << 8) | readIntEEPROM(9);
    eeprom.servo_zero = eeprom.servo_zero/10000;
    eeprom.servo_min = ((int)readIntEEPROM(10) << 8) | readIntEEPROM(11);
    eeprom.servo_min = eeprom.servo_min/10000;
    eeprom.servo_max = ((int)readIntEEPROM(12) << 8) | readIntEEPROM(13);
    eeprom.servo_max = eeprom.servo_max/10000;
    eeprom.trasferenza_rad_ms = ((int)readIntEEPROM(14) << 8) | readIntEEPROM(15);
    eeprom.trasferenza_rad_ms = eeprom.trasferenza_rad_ms/10000;
    eeprom.programma = (int)readIntEEPROM(16);
    eeprom.rotazione_per_programma_1 = ((int)readIntEEPROM(17) << 8) | readIntEEPROM(18);
    eeprom.rotazione_per_programma_1 = eeprom.rotazione_per_programma_1/10000;
    Kp = ((int)readIntEEPROM(19) << 8) | readIntEEPROM(20);
    Kp = Kp/10000;
    Ki = ((int)readIntEEPROM(21) << 8) | readIntEEPROM(22);
    Ki = Ki/10000;
    Kd = ((int)readIntEEPROM(23) << 8) | readIntEEPROM(24);
    Kd = Kd/10000;
} //FINE recupera_eeprom

//Azzera le variabili che necessitano di essere azzerate
void azzera_variabili(void){
	unsigned int iii = 0;
    stato.x = 0;            //Azzera i dati elaborati perchè saranno presi come
    stato.v = 0;         //punti di partenza dal Kalman
    stato.a = 0;
    stato.p_ang = 0;
    stato.v_ang = 0;
    stato.a_ang = 0;
    stato.rotazione_rad = 0;
    for (iii = 0; iii <= DIM_BUF_MEMORIA; iii++){   //Azzera l'intero buffer circolare
	    buffer_mem[iii] = 0;
    }
    
    PWM.alto = 60990;       //Pone i valori del PWM a un valore compatibile con il servo (1,5ms).
    PWM.basso = 10082;
    PWM.byte_verifica = 0;
} //FINE azzera_variabili



//Restituisce il valore in metri corrispondente a una quota in ADC. Per calcolarlo usa
//anche la lettura ADC della quota zero(Sensor_zero)
float da_ADC_a_metri(unsigned int lettura_ADC_baro, float lettura_ADC_baro_quota_zero){
    float quota_in_metri;
    quota_in_metri = 44345.9 - 44345.9 * pow(((389.12 + lettura_ADC_baro)/(389.12 + lettura_ADC_baro_quota_zero)),0.18092998);
    return quota_in_metri;
}//FINE da_ADC_a_metri




//Restituisce l'accelerazione in metri al secondo quadro data la lettura ADC dell'accelerometro
//e la lettura in condizioni di riposo in verticale.
//Il - serve perchè l'accelerometro è montato capovolto
float da_ADC_a_ms2(unsigned int lettura_ADC_acc, float lettura_ADC_acc_in_verticale){
    return( -(lettura_ADC_acc - lettura_ADC_acc_in_verticale) * 0.2993774414);
}//FINE da_ADC_a_ms2	

//Simula un volo reale sovrascrivendo i dati dei sensori. I dati sono approssimati
//con funzioni polinominali e provengono dal volo del PAC3 di David Shultz del 27Oct 2001
//i dati sono espressi in valori ADC e nelle funzioni si inserisce un numero intero (sarebbero
//i sample). Ad esempio 200 è 1s perchè i dati sono stati ricavati a 200Hz
void simula_dati(void){
	static int istante = 0;
	//Solo all'inizio aggiorna i valori di calibrazione
	if (istante == 0){     
		taratura.baro = 3353.1;
		taratura.acc = 2003.1;
		taratura.P0 = 0;
		taratura.Q0 = 0;
		taratura.R0 = 0;
	}	
	//impone la quota lineare in valori ADC
	dati_raw.baro = -0.2265*istante+3353;

    //trova l'accelerazione in valori ADC. Supposta costante per i primi 4 secondi
		if (istante < 400){
			dati_raw.acc = 1840;  //Simula la fase di spinta
		}
		else {
			dati_raw.acc = taratura.acc + 10;  //Simula la fase inerziale
	    }   
	istante++;	//Aggiorna l'istante per il prossimo passo
}//fine simula_dati	
	
	
