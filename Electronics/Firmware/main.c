/********************************************************************
 FileName:      main.c
 Dependencies:  See INCLUDES section
 Processor:		PIC18F2553
 Hardware:		Mio
 Complier:  	Microchip C18
 Owner:         Adriano Arcadipane
********************************************************************/
#ifndef MAIN_C
#define MAIN_C

/** INCLUDES *******************************************************/
#include "Strutture_e_prototipi.h"

//*************CONFIGURATION*****************************************
        #pragma config PLLDIV   = 2           // (8 MHz crystal)
        #pragma config CPUDIV   = OSC1_PLL2   //[Primary Oscillator Src: /2][96 MHz PLL Src: /3] 
        #pragma config USBDIV   = 2           // Clock source from 96MHz PLL/2
        #pragma config FOSC     = HSPLL_HS
        #pragma config FCMEN    = OFF
        #pragma config IESO     = OFF
        #pragma config PWRT     = OFF
        #pragma config BOR      = ON
        #pragma config BORV     = 3
        #pragma config VREGEN   = ON           //USB Voltage Regulator
        #pragma config WDT      = OFF
        #pragma config WDTPS    = 32768
        #pragma config MCLRE    = ON
        #pragma config LPT1OSC  = OFF
        #pragma config PBADEN   = OFF          //PORTB 4:0 are digital I/O
//      #pragma config CCP2MX   = ON
        #pragma config STVREN   = ON
        #pragma config LVP      = OFF
//      #pragma config ICPRT    = OFF          // Dedicated In-Circuit Debug/Programming
        #pragma config XINST    = OFF          // Extended Instruction Set
        #pragma config CP0      = OFF
        #pragma config CP1      = OFF
//      #pragma config CP2      = OFF
//      #pragma config CP3      = OFF
        #pragma config CPB      = OFF
//      #pragma config CPD      = OFF
        #pragma config WRT0     = OFF
        #pragma config WRT1     = OFF
//      #pragma config WRT2     = OFF
//      #pragma config WRT3     = OFF
        #pragma config WRTB     = OFF          // Boot Block Write Protection
        #pragma config WRTC     = OFF
//      #pragma config WRTD     = OFF
        #pragma config EBTR0    = OFF
        #pragma config EBTR1    = OFF
//      #pragma config EBTR2    = OFF
//      #pragma config EBTR3    = OFF
        #pragma config EBTRB    = OFF

/** GLOBAL VARIABLES ******************************************************/
//VARIABILI PER LA USB
#pragma udata
#pragma udata USB_VARIABLES=0x500
unsigned char ReceivedDataBuffer[64];
unsigned char ToSendDataBuffer[64];
#pragma code
#pragma udata

//VARIABILI NORMALI
USB_HANDLE USBOutHandle = 0;
USB_HANDLE USBInHandle = 0;
BOOL blinkStatusValid = TRUE;

unsigned char pwm_state = 0;
unsigned int pwm_alto,         
             pwm_basso;
StrValoriPWM PWM;           //Dichiaro PWM come variabile di tipo ValoriPWM...

/** PRIVATE PROTOTYPES *********************************************/
static void InitializeSystem(void);
void ProcessIO(void);
void YourHighPriorityISRCode();
void YourLowPriorityISRCode();
void USBCBSendResume(void);
void Principale(void);
void gestione_errori (unsigned char);
void azzera_ToSendDataBuffer(void);

/** VARIABILI ESTERNE **********************************************/

#include "VectorRemapping.h"     //Rimappatura ISV in caso di bootloader

/**ROUTINE DI INTERRUPT*********************************************/
#pragma code
	//These are your actual interrupt handling routines.
	#pragma interrupt YourHighPriorityISRCode
	void YourHighPriorityISRCode()
	{
        if (PIR1bits.TMR1IF) {        //Overflow del timer1 (PWM)
            if (pwm_state == 0){
                SERVO = 1;
                pwm_state = 1;
                WriteTimer1(pwm_alto);
            }
            else {
                SERVO = 0;
                pwm_state = 0;
                WriteTimer1(pwm_basso);
            }
        PIR1bits.TMR1IF = 0;
        }

		//Check which interrupt flag caused the interrupt.
		//Service the interrupt
		//Clear the interrupt flag
		//Etc.
        if (PIR2bits.USBIF){
        #if defined(USB_INTERRUPT)
	        USBDeviceTasks();
        #endif
        }
	}//This return will be a "retfie fast", since this is in a #pragma interrupt section 

	#pragma interruptlow YourLowPriorityISRCode
	void YourLowPriorityISRCode()
	{
		//Check which interrupt flag caused the interrupt.
		//Service the interrupt
		//Clear the interrupt flag
		//Etc.
	
	}	//This return will be a "retfie", since this is in a #pragma interruptlow section 
#endif

/** DECLARATIONS ***************************************************/
#pragma code


// * Function:        void main(void)
//********************************************************************
void main(void){   

    InitializeSystem();  //Inizializza il sistema
    delay_ms(150);       //Pausa per stabilizzare i sistemi
    
//Se la porta USB non è connessa all'accensione il sistema esegue
//la parte principale del programma
    if (PORTCbits.RC6 == 0) {
        Principale();           //Contenuta in CorpoProgramma.c
    }
//Altrimenti passa alla programma di comunicazione
    USBDeviceAttach(); 
    PIE1bits.TMR1IE = 1;     //Disabilita l'interrupt per il timer 1
    
    while(1)
    {
		// Application-specific tasks.
		// Application related code may be added here, or in the ProcessIO() function.
        ProcessIO();        
    }//end while
}//end main


/********************************************************************
 * Function:        static void InitializeSystem(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        InitializeSystem is a centralize initialization
 *                  routine. All required USB initialization routines
 *                  are called from here.
 *
 *                  User application initialization routine should
 *                  also be called from here.                  
 *
 * Note:            None
 *******************************************************************/
static void InitializeSystem(void){

    //Setto il clock per la libreria delay
    setQuartz (48);

    //Pins
    //La selezione dei canali come analogici o digitali verrà fatta
    //tramite la libreria ADC
    PORTA = 0b0000000;    //Tutte le porte basse
    PORTB = 0b0000000;   
    PORTC = 0b0000000;
    PORTE = 0b0000000;

    TRISA = 0b11111111;          //PORTA A tutti ingressi (ci sono i canali analogici)
    TRISB = 0b0000001;           //Tutte le porte B sono uscite tranne lo SPI IN
    TRISCbits.TRISC7 = OUTPUT;    //Lo SPI out è un'uscita
    TRISCbits.TRISC6 = INPUT;    //L'USB sense è input
    TRISCbits.TRISC0 = OUTPUT;   //Led è un uscita
    TRISCbits.TRISC2 = OUTPUT;   //Il servo è un uscita
    
    
    //SPI
    OpenSPI(SPI_FOSC_16,MODE_00,SMPEND);   //Attiva e configura la SPI a 3MHz
    CS_MEMORIA = 1;                        //Tutti i chip select sono alti
    CS_GYRO = 1;
    CS_AUX = 1;    

    //ADC
    OpenADC(ADC_FOSC_16 & ADC_RIGHT_JUST & ADC_20_TAD,   //il 12 fa in modo che siano analogici solo
            ADC_CH2 & ADC_INT_OFF & ADC_VREFPLUS_VDD & ADC_VREFMINUS_VSS,   //i canali 0, 1, 2, 3
            11);


    //USB
    USBOutHandle = 0;
    USBInHandle = 0;  
    USBDeviceInit();	//usb_device.c.  Initializes USB module SFRs and firmware
    					//variables to known states. 

    //L3G4200 GYROSCOPE
      //Attiva il giroscopio settando il bit PD e seleziona DATA RATE e
      //il primo filtro passa basso 
    gyro_write_register(CTRL_REG1,0b10111111);  
      //Setta il filtro passa alto
    gyro_write_register(CTRL_REG2,0b00001000);
      //Setta la modalità BDU che blocca l'aggiornamento se non si è letto anche l'LSB
      //e seleziona il fondoscala di 500°/s
    gyro_write_register(CTRL_REG4,0b10010000);
      //Disattiva il filtro passa alto e attiva secondo il passa basso
    gyro_write_register(CTRL_REG5,0b00000011);
    
    
    //FRAM
    fram_write_register(0b11000000);   //Toglie le protezioni software inizializzando la FRAM
   

    //TIMERS
    //Timer0 per generare il clock di base per l'elaborazione e acquisizione
    OpenTimer0 (TIMER_INT_OFF   & //Interrupt disabled
                T0_16BIT        & //16-bit mode
                T0_SOURCE_INT   & //Internal clock source (TOSC)
                T0_EDGE_FALL    & //INUTILIZZATO External clock on falling edge
                T0_PS_1_2);       //1:2 prescale
    WriteTimer0 (TIMER0_START_VALUE);    //Azzera il timer al valore stabilito (vedi HardwareProfile)
    T0CONbits.TMR0ON = 0;             //Spegne il timer            

    //Timer1 per generare il PWM tramite continui interrupt
    OpenTimer1( TIMER_INT_OFF   & //Interrupt disabilitati, verranno abilitati al momento giusto
                T1_16BIT_RW     & //16-bit mode
                T1_SOURCE_INT   & //Internal clock source (TOSC)
                T1_PS_1_4       & //1:4 prescale
                T1_OSC1EN_OFF   & //Disable Timer1 oscillator
                T1_SYNC_EXT_OFF); //Don’t sync external clock input
    T1CONbits.TMR1ON = 0;             //Spegne il timer
    
    //INTERRUPT
    RCONbits.IPEN = 1;     //Priorità abilitate
    INTCONbits.GIEH = 0;   //Disabilita interrupt ad alta priorità, verranno attivati al momento opportuno
    INTCONbits.GIEL = 0;   //Disabilita interrupt a bassa priorità
    IPR1bits.TMR1IP = 1;   //L'overflow del Timer1 è ad alta priorità
    PIE1bits.TMR1IE = 1;   //Abilita l'interrupt da Timer1 per la generazione del PWM 
       
    INTCONbits.TMR0IF = 0; //Pulisce gli interrupt flag per i timer
    PIR1bits.TMR1IF = 0;   



    //EEPROM INTERNA
    if (readIntEEPROM (16) == 0xFF){    //Se la memoria interna è vergine, la riempie con i valori predefiniti
	                                     //Utilizza la cella di memoria 16 perchè è quella in cui è memorizzato
	                                     //il programma da eseguire, che mai sarà 255.
        unsigned char verifica = 0;     //Dichiara una variabile utile alla verifica della corretta scrittura
        
        //Vedere HardwareProfile.h per i valori
        //Se la scrittura riesce il risultato della funzione write è 1. Questo risultato
        //vine invertito per mezzo di ~ e sommato alla variabile di verifica. Se tutto
        //è andato bene alla fine verifica sarà zero.
        
        //Valori di taratura del giroscopio aboliti dalla versione 0.10
        //da ora in poi vengono impostati all'avvio.
        //verifica = verifica + !writeIntEEPROM ((char)(P0_SU_EEPROM >> 8),0);                     //zero rate level asse X giroscopio MSB
        //verifica = verifica + !writeIntEEPROM ((char) P0_SU_EEPROM,1);                           //"" LSB    
        //verifica = verifica + !writeIntEEPROM ((char)(Q0_SU_EEPROM >> 8),2);                     //zero rate level asse Y giroscopio MSB           
        //verifica = verifica + !writeIntEEPROM ((char) Q0_SU_EEPROM,3);                           //"" LSB    
        //verifica = verifica + !writeIntEEPROM ((char)(R0_SU_EEPROM >> 8),4);                     //zero rate level asse Y giroscopio MSB
        //verifica = verifica + !writeIntEEPROM ((char) R0_SU_EEPROM,5);                           //"" LSB     
        verifica = verifica + !writeIntEEPROM ((char)(LOW_BATTERY_VALUE_SU_EEPROM >> 8),6);      //MSB valore minimo batteria           
        verifica = verifica + !writeIntEEPROM ((char) LOW_BATTERY_VALUE_SU_EEPROM,7);            //LSB valore minimo batteria
        verifica = verifica + !writeIntEEPROM ((char)(SERVO_ZERO_SU_EEPROM >> 8),8);             //MSB posizione centrale servo
        verifica = verifica + !writeIntEEPROM ((char) SERVO_ZERO_SU_EEPROM,9);                   //LSB posizione centrale servo
        verifica = verifica + !writeIntEEPROM ((char)(MIN_ROTAZIONE_RAD_SU_EEPROM >> 8),10);  //MSB max posizione oraria servo
        verifica = verifica + !writeIntEEPROM ((char) MIN_ROTAZIONE_RAD_SU_EEPROM,11);        //LSB max posizione oraria servo
        verifica = verifica + !writeIntEEPROM ((char)(MAX_ROTAZIONE_RAD_SU_EEPROM >> 8),12);  //MSB max posizione antioraria servo
        verifica = verifica + !writeIntEEPROM ((char) MAX_ROTAZIONE_RAD_SU_EEPROM,13);        //LSB max posizione antioraria servo
        verifica = verifica + !writeIntEEPROM ((char)(T_TAU_DELTA_SU_EEPROM >> 8),14);        //MSB Trasferenza tra tempo pwm alto e rotazione alette
        verifica = verifica + !writeIntEEPROM ((char) T_TAU_DELTA_SU_EEPROM,15); 
        verifica = verifica + !writeIntEEPROM ((char) PROGRAMMA_SU_EEPROM,16);                        //Programma da eseguire
        verifica = verifica + !writeIntEEPROM ((char)(ROTAZIONE_PER_PROGRAMMA_1_SU_EEPROM >> 8),17);  //MSB rotazione in rad*10000 da mantenere durante il programma 1
        verifica = verifica + !writeIntEEPROM ((char) ROTAZIONE_PER_PROGRAMMA_1_SU_EEPROM,18);        //LSB rotazione in rad*10000 da mantenere durante il programma 1
        verifica = verifica + !writeIntEEPROM ((char) (KP_SU_EEPROM >> 8),19);                               //MSB azione proporzionale * 10000
        verifica = verifica + !writeIntEEPROM ((char) KP_SU_EEPROM,20);                               //LSB azione proporzionale * 10000   
        verifica = verifica + !writeIntEEPROM ((char) (KI_SU_EEPROM >> 8),21);                               //MSB azione Integrale * 10000
        verifica = verifica + !writeIntEEPROM ((char) KI_SU_EEPROM,22);                               //LSB azione Integrale * 10000         
        verifica = verifica + !writeIntEEPROM ((char) (KD_SU_EEPROM >> 8),23);                               //MSB azione derivativa * 10000
        verifica = verifica + !writeIntEEPROM ((char) KD_SU_EEPROM,24);                               //LSB azione derivativa * 10000         
        //Se "verifica" è diverso da zero chiama la routine di gestione errori con codice 1
        if (verifica != 0) gestione_errori(1);
    }//fine IF


    //ALTRO
    LED = 1;            //Accende il led

}//end InitializeSystem



/********************************************************************
 * Function:        void ProcessIO(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        This function is a place holder for other user
 *                  routines. It is a mixture of both USB and
 *                  non-USB tasks.
 *
 * Note:            None
 *******************************************************************/
void ProcessIO(void){   

    // User Application USB tasks
    if((USBDeviceState < CONFIGURED_STATE)||(USBSuspendControl==1)) return;
    
    if(!HIDRxHandleBusy(USBOutHandle))				//Check if data was received from the host.
    {        
	    azzera_ToSendDataBuffer();                  //Cancella l'intero array da inviare             
        switch(ReceivedDataBuffer[0])				//Look at the data the host sent, to see what kind of application specific command it sent.
        {
//            case 128:{  // PING e Toggle LEDs command
//	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];   //ping comando
//		        LED = !LED;                                   //Inverte il led
//		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
//                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
//                }
//            }
//            break;
                
                
            case 129:{  // Legge EEPROM interna
	            unsigned char iii;
	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];   //ping comando
		        ToSendDataBuffer[1] = 1;                       //bit di conferma
		        for (iii = 2; iii < 27; iii++){                //Riversa la EEPROM interna nel buffer in uscita
			        ToSendDataBuffer[iii] = readIntEEPROM(iii - 2);
			    }
		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                }
            }
            break;
            
            case 130:{  // Scrive EEPROM interna
	            unsigned char verifica = 0;
	            unsigned char iii;
	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];   //ping comando
		        ToSendDataBuffer[1] = 1;                       //Operazine riuscita. Se qualcosa va storto viene messo a 0 più giù
		        for (iii = 1; iii < 26; iii++){                //Riversa il buffer nella EEPROM
			        writeIntEEPROM(ReceivedDataBuffer[iii],(iii - 1));
			    }
		        if (verifica != 0) ToSendDataBuffer[1] = 0;                             //Se qualcosa va male ritorna zero
		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                }
            }
            break;
            
            case 131:{  // Cancella FRAM
	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];   //ping comando
		        ToSendDataBuffer[1] = 1;                       //Operazione riuscita. In realtà non c'è un vero controllo                                
		        fram_erase();	        
				if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                }
            }
            break;
            
            case 132:{  // Legge i sensori e la batteria. ATTENZIONE! In questo comando l'indianità è invertita
	            StrSensori sensori;
	            unsigned char batt, iii;
	            unsigned char *pnt_ai_dati = (unsigned char*)&sensori.acc;     //Punta al primo byte della struttura
	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];   //ping comando
	            leggi_sensori(&sensori);                       //legge i sensori
	            batt = ADC_get_sample(BATTERIA);               //Legge il balore della batteria
	            for(iii = 2; iii < 14; iii++){
		            ToSendDataBuffer[iii] = *pnt_ai_dati;      //Trasferisce il contenuto della struttura dati nel buffer
                    pnt_ai_dati ++;
		        }
	            ToSendDataBuffer[14] = LOBYTE(batt);           //Chiude con il valore della batteria
	            ToSendDataBuffer[15] = HIBYTE(batt);
	            
		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                }
            }
            break;
            
            case 133:{  // Scarica i dati del volo
	           // unsigned char *pnt_send_buffer = &ToSendDataBuffer[0];          //Crea un puntatore al primo indirizzo del buffer
	            unsigned char iii;
	            unsigned short long indirizzo_da_leggere = 0;
	            unsigned short long ultimo_indirizzo;
	            unsigned int pacchetto = 0;
	            unsigned int pacchetti_da_inviare = 2048;                       //La memoria è composta da 131071 byte, quindi servono 2048 
	                                                                            //...pacchetti da 64 byte
	            LED = 0;                                                        //Spegne il led      
	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];   //ping comando
	            
	            //Se il byte USED del registro memoria è 0 il banco è pieno e legge il tutto
		        if (fram_read(0) == 0){
			                                                              
			        ToSendDataBuffer[1] = 0;                                     //Riporta il segnale di memoria piena
			        //Invia in modo che il computer si prepari ad accogliere i dati
			        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                        USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);    //Invia i dati
                    }//END IF invio dati
                    delay_ms(40);                                                            //Attende che il computer sia pronto 
				    
				    for (pacchetto = 0; pacchetto < pacchetti_da_inviare; pacchetto++){
					    unsigned char *pnt_send_buffer = &ToSendDataBuffer[0];          //Crea un puntatore al primo indirizzo del buffer
					    CS_MEMORIA = 0;
					    WriteSPI(READ); 
					    WriteSPI((char) (indirizzo_da_leggere >> 16));  //Invia l'indirizzo in tre pezzi
                        WriteSPI((char) (indirizzo_da_leggere >> 8));            
                        WriteSPI((char)  indirizzo_da_leggere);
					    for(iii = 0; iii < 64; iii++){
						    *pnt_send_buffer = ReadSPI();     //Legge byte per byte le informazioni che la FRAM manda in sequenza
						    pnt_send_buffer++;		    
						}
						CS_MEMORIA = 1;
					    if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                               USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                        }//END IF invio dati
                        indirizzo_da_leggere = indirizzo_da_leggere + 64;          //Incrementa l'indirizzo da leggere
                        Delay100TCYx(250);  //Ritardo in multipli di 100 cicli di clock
					}//END FOR pacchetto
				}//END IF
							    
			    else{                                          //See la memoria è vuota riporta 0 ed esce
				    ToSendDataBuffer[1] = 255;
				    if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                        USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                    }//END IF invio dati
				}//END ELSE
				
			 LED = 1;              //Riaccende il led  				
            }//END case 133
            break;
            
            // Invia all'USB un numero a virgola mobile scomposto in quattro byte.
            //ciò serve a controllare che il programma lato PC lo ricomponga correttamente
//            case 134:{  
//	            unsigned float numero_con_virgola = -12345.6789;    //Scelgo il numero da inviare
//	            unsigned char *puntatore;                          //Dichiara il puntatore di tipo char
//	            puntatore = (unsigned char*)&numero_con_virgola;   //Punta al numero con virgola effttuando la conversione (altrimenti non potrei puntare a un float con un puntatore char)
//	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];       //ping comando
//	            ToSendDataBuffer[1] = 1;                           //Il bit di conferma deve esserci sempre per compatibilità
//	            ToSendDataBuffer[2] = *puntatore;                  //Posiziona il primo byte nel buffer
//	            puntatore ++;                                      //Incrementa il puntatore
//	            ToSendDataBuffer[3] = *puntatore;
//	            puntatore ++;
//	            ToSendDataBuffer[4] = *puntatore;
//	            puntatore ++;
//	            ToSendDataBuffer[5] = *puntatore;                  //Posizione l'ultimo byte
//		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
//                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
//                }
//            }
//            break;
            
            
//            case 135:{  // Scrive dati farlocchi sulla FRAM (registro memoria compreso) fino all'indirizzo 100000
//	            unsigned short long indirizzo, ultimo_indirizzo = 100000;
//	            unsigned char dato_farlocco = 0;
//	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];   //ping comando
//	            ToSendDataBuffer[1] = 1;
//	            //Riempie il registro memoria
//		        fram_write(0,0);                     //byte USED
//		        fram_write(1,0);                     //Primo indirizzo in tre pezzi
//		        fram_write(2,0);
//		        fram_write(3,100);         
//		        fram_write(4,0x01); //Ultimo indirizzo (100000)
//		        fram_write(5,0x86);
//		        fram_write(6,0xA0); 
//		        fram_write_enable();                      //Attiva la scrittura
//		        
//                CS_MEMORIA = 0; 
//                WriteSPI(WRITE);                          //Invia il comando di scrittura
//                WriteSPI(0);              //Invia l'indirizzo inizio scrittira in tre pezzi (100)
//                WriteSPI(0);
//                WriteSPI(100);  
//		        for(indirizzo = 100; indirizzo <= ultimo_indirizzo; indirizzo++){
//			        WriteSPI(dato_farlocco);
//			        dato_farlocco++;
//			    }
//			    CS_MEMORIA = 1;
//		        
//		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
//                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
//                }
//            }
//            break;
            
            case 136:{  // legge byte FRAM all'indirizzo voluto e lo invia tramite USB
	            unsigned short long indirizzo;
	            ToSendDataBuffer[0] = ReceivedDataBuffer[0];                            //ping comando 
	            indirizzo = (((unsigned short long) ReceivedDataBuffer[1]) << 16)|                     //Ricompone l'indirizzo
		                    (((unsigned int) ReceivedDataBuffer[2])) << 8 |
		                    ((ReceivedDataBuffer[3]));  
		        ToSendDataBuffer[1] = fram_read(indirizzo);                             //Legge il byte
		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                }
            }
            break;
            
/*FUNZIONA MALE
            case 137:{  // Attiva servocomando
	            ToSendDataBuffer[0] = ReceivedDataBuffer[0]; //ping comando
	            ToSendDataBuffer[1] = ReceivedDataBuffer[1]; //Ritorna lo stato del servo
	            switch (ReceivedDataBuffer[1]) {
		            //caso 1: attiva la generazione del PWM per il servo o, se già attiva, aggiorna i valori
		            case 1:{
			            recupera_eeprom();         //Aggiorna la struttura della EEPROM perchè potrebbe essere cambiata
			            PWM = ruota(0);            //Ruota le alette di un angolo nullo
			            INTCONbits.GIEH = 0;       //Semmai fossero già abilitati, disabilita gli interrupt per aggiornare il PWM
			            pwm_alto = PWM.alto;       //Aggiorna i segnali PWM
                        pwm_basso = PWM.basso;			            
			            T1CONbits.TMR1ON = 1;      //Accende il timer per l'interrupt del servo
			            INTCONbits.GIEH = 1;       //Abilita tutti gli interrupt ad alta priorità 		            
			        }//fine case 1
			        break;
			        
			        case 0:{
				        INTCONbits.GIEH = 0;       //Disattiva gli interrupt da timer 0
				        T1CONbits.TMR1ON = 1;      //Ferma il timer
				    }//fine case 0
		            break;
		        }//end switch
		        //invia tramite USB
		        if(!HIDTxHandleBusy(USBInHandle)){                                      //Se non è occupato
                    USBInHandle = HIDTxPacket(HID_EP,(BYTE*)&ToSendDataBuffer[0],64);   //Invia i dati
                }
            }//fine case 137
            break;
*/
        }//FINE switch principale
        //Re-arm the OUT endpoint for the next packet
        USBOutHandle = HIDRxPacket(HID_EP,(BYTE*)&ReceivedDataBuffer,64);
    }

    
}//end ProcessIO





// ******************************************************************************************************
// ************** USB Callback Functions ****************************************************************
// ******************************************************************************************************
// The USB firmware stack will call the callback functions USBCBxxx() in response to certain USB related
// events.  For example, if the host PC is powering down, it will stop sending out Start of Frame (SOF)
// packets to your device.  In response to this, all USB devices are supposed to decrease their power
// consumption from the USB Vbus to <2.5mA each.  The USB module detects this condition (which according
// to the USB specifications is 3+ms of no bus activity/SOF packets) and then calls the USBCBSuspend()
// function.  You should modify these callback functions to take appropriate actions for each of these
// conditions.  For example, in the USBCBSuspend(), you may wish to add code that will decrease power
// consumption from Vbus to <2.5mA (such as by clock switching, turning off LEDs, putting the
// microcontroller to sleep, etc.).  Then, in the USBCBWakeFromSuspend() function, you may then wish to
// add code that undoes the power saving things done in the USBCBSuspend() function.

// The USBCBSendResume() function is special, in that the USB stack will not automatically call this
// function.  This function is meant to be called from the application firmware instead.  See the
// additional comments near the function.

/******************************************************************************
 * Function:        void USBCBSuspend(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        Call back that is invoked when a USB suspend is detected
 * Note:            None
 *****************************************************************************/
void USBCBSuspend(void)
{
	//Example power saving code.  Insert appropriate code here for the desired
	//application behavior.  If the microcontroller will be put to sleep, a
	//process similar to that shown below may be used:
	
	//ConfigureIOPinsForLowPower();
	//SaveStateOfAllInterruptEnableBits();
	//DisableAllInterruptEnableBits();
	//EnableOnlyTheInterruptsWhichWillBeUsedToWakeTheMicro();	//should enable at least USBActivityIF as a wake source
	//Sleep();
	//RestoreStateOfAllPreviouslySavedInterruptEnableBits();	//Preferrably, this should be done in the USBCBWakeFromSuspend() function instead.
	//RestoreIOPinsToNormal();									//Preferrably, this should be done in the USBCBWakeFromSuspend() function instead.

	//IMPORTANT NOTE: Do not clear the USBActivityIF (ACTVIF) bit here.  This bit is 
	//cleared inside the usb_device.c file.  Clearing USBActivityIF here will cause 
	//things to not work as intended.	
	

    #if defined(__C30__)
        //This function requires that the _IPL level be something other than 0.
        //  We can set it here to something other than 
        #ifndef DSPIC33E_USB_STARTER_KIT
        _IPL = 1;
        USBSleepOnSuspend();
        #endif
    #endif
}


/******************************************************************************
 * Function:        void _USB1Interrupt(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        This function is called when the USB interrupt bit is set
 *					In this example the interrupt is only used when the device
 *					goes to sleep when it receives a USB suspend command
 *
 * Note:            None
 *****************************************************************************/
#if 0
void __attribute__ ((interrupt)) _USB1Interrupt(void)
{
    #if !defined(self_powered)
        if(U1OTGIRbits.ACTVIF)
        {
            IEC5bits.USB1IE = 0;
            U1OTGIEbits.ACTVIE = 0;
            IFS5bits.USB1IF = 0;
        
            //USBClearInterruptFlag(USBActivityIFReg,USBActivityIFBitNum);
            USBClearInterruptFlag(USBIdleIFReg,USBIdleIFBitNum);
            //USBSuspendControl = 0;
        }
    #endif
}
#endif

/******************************************************************************
 * Function:        void USBCBWakeFromSuspend(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        The host may put USB peripheral devices in low power
 *					suspend mode (by "sending" 3+ms of idle).  Once in suspend
 *					mode, the host may wake the device back up by sending non-
 *					idle state signalling.
 *					
 *					This call back is invoked when a wakeup from USB suspend 
 *					is detected.
 *
 * Note:            None
 *****************************************************************************/
void USBCBWakeFromSuspend(void)
{
	// If clock switching or other power savings measures were taken when
	// executing the USBCBSuspend() function, now would be a good time to
	// switch back to normal full power run mode conditions.  The host allows
	// a few milliseconds of wakeup time, after which the device must be 
	// fully back to normal, and capable of receiving and processing USB
	// packets.  In order to do this, the USB module must receive proper
	// clocking (IE: 48MHz clock must be available to SIE for full speed USB
	// operation).
}

/********************************************************************
 * Function:        void USBCB_SOF_Handler(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        The USB host sends out a SOF packet to full-speed
 *                  devices every 1 ms. This interrupt may be useful
 *                  for isochronous pipes. End designers should
 *                  implement callback routine as necessary.
 *
 * Note:            None
 *******************************************************************/
void USBCB_SOF_Handler(void)
{
    // No need to clear UIRbits.SOFIF to 0 here.
    // Callback caller is already doing that.
}

/*******************************************************************
 * Function:        void USBCBErrorHandler(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        The purpose of this callback is mainly for
 *                  debugging during development. Check UEIR to see
 *                  which error causes the interrupt.
 *
 * Note:            None
 *******************************************************************/
void USBCBErrorHandler(void)
{
    // No need to clear UEIR to 0 here.
    // Callback caller is already doing that.

	// Typically, user firmware does not need to do anything special
	// if a USB error occurs.  For example, if the host sends an OUT
	// packet to your device, but the packet gets corrupted (ex:
	// because of a bad connection, or the user unplugs the
	// USB cable during the transmission) this will typically set
	// one or more USB error interrupt flags.  Nothing specific
	// needs to be done however, since the SIE will automatically
	// send a "NAK" packet to the host.  In response to this, the
	// host will normally retry to send the packet again, and no
	// data loss occurs.  The system will typically recover
	// automatically, without the need for application firmware
	// intervention.
	
	// Nevertheless, this callback function is provided, such as
	// for debugging purposes.
}


/*******************************************************************
 * Function:        void USBCBCheckOtherReq(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        When SETUP packets arrive from the host, some
 * 					firmware must process the request and respond
 *					appropriately to fulfill the request.  Some of
 *					the SETUP packets will be for standard
 *					USB "chapter 9" (as in, fulfilling chapter 9 of
 *					the official USB specifications) requests, while
 *					others may be specific to the USB device class
 *					that is being implemented.  For example, a HID
 *					class device needs to be able to respond to
 *					"GET REPORT" type of requests.  This
 *					is not a standard USB chapter 9 request, and 
 *					therefore not handled by usb_device.c.  Instead
 *					this request should be handled by class specific 
 *					firmware, such as that contained in usb_function_hid.c.
 *
 * Note:            None
 *******************************************************************/
void USBCBCheckOtherReq(void)
{
    USBCheckHIDRequest();
}//end


/*******************************************************************
 * Function:        void USBCBStdSetDscHandler(void)
 *
 * PreCondition:    None
 *
 * Input:           None
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        The USBCBStdSetDscHandler() callback function is
 *					called when a SETUP, bRequest: SET_DESCRIPTOR request
 *					arrives.  Typically SET_DESCRIPTOR requests are
 *					not used in most applications, and it is
 *					optional to support this type of request.
 *
 * Note:            None
 *******************************************************************/
void USBCBStdSetDscHandler(void)
{
    // Must claim session ownership if supporting this request
}//end


/*******************************************************************
 * Function:        void USBCBInitEP(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        This function is called when the device becomes
 *                  initialized, which occurs after the host sends a
 * 					SET_CONFIGURATION (wValue not = 0) request.  This 
 *					callback function should initialize the endpoints 
 *					for the device's usage according to the current 
 *					configuration.
 *
 * Note:            None
 *******************************************************************/
void USBCBInitEP(void)
{
    //enable the HID endpoint
    USBEnableEndpoint(HID_EP,USB_IN_ENABLED|USB_OUT_ENABLED|USB_HANDSHAKE_ENABLED|USB_DISALLOW_SETUP);
    //Re-arm the OUT endpoint for the next packet
    USBOutHandle = HIDRxPacket(HID_EP,(BYTE*)&ReceivedDataBuffer,64);
}

/********************************************************************
 * Function:        void USBCBSendResume(void)
 * PreCondition:    None
 * Input:           None
 * Output:          None
 * Side Effects:    None
 * Overview:        The USB specifications allow some types of USB
 * 					peripheral devices to wake up a host PC (such
 *					as if it is in a low power suspend to RAM state).
 *					This can be a very useful feature in some
 *					USB applications, such as an Infrared remote
 *					control	receiver.  If a user presses the "power"
 *					button on a remote control, it is nice that the
 *					IR receiver can detect this signalling, and then
 *					send a USB "command" to the PC to wake up.
 *					
 *					The USBCBSendResume() "callback" function is used
 *					to send this special USB signalling which wakes 
 *					up the PC.  This function may be called by
 *					application firmware to wake up the PC.  This
 *					function will only be able to wake up the host if
 *                  all of the below are true:
 *					
 *					1.  The USB driver used on the host PC supports
 *						the remote wakeup capability.
 *					2.  The USB configuration descriptor indicates
 *						the device is remote wakeup capable in the
 *						bmAttributes field.
 *					3.  The USB host PC is currently sleeping,
 *						and has previously sent your device a SET 
 *						FEATURE setup packet which "armed" the
 *						remote wakeup capability.   
 *
 *                  If the host has not armed the device to perform remote wakeup,
 *                  then this function will return without actually performing a
 *                  remote wakeup sequence.  This is the required behavior, 
 *                  as a USB device that has not been armed to perform remote 
 *                  wakeup must not drive remote wakeup signalling onto the bus;
 *                  doing so will cause USB compliance testing failure.
 *                  
 *					This callback should send a RESUME signal that
 *                  has the period of 1-15ms.
 *
 * Note:            This function does nothing and returns quickly, if the USB
 *                  bus and host are not in a suspended condition, or are 
 *                  otherwise not in a remote wakeup ready state.  Therefore, it
 *                  is safe to optionally call this function regularly, ex: 
 *                  anytime application stimulus occurs, as the function will
 *                  have no effect, until the bus really is in a state ready
 *                  to accept remote wakeup. 
 *
 *                  When this function executes, it may perform clock switching,
 *                  depending upon the application specific code in 
 *                  USBCBWakeFromSuspend().  This is needed, since the USB
 *                  bus will no longer be suspended by the time this function
 *                  returns.  Therefore, the USB module will need to be ready
 *                  to receive traffic from the host.
 *
 *                  The modifiable section in this routine may be changed
 *                  to meet the application needs. Current implementation
 *                  temporary blocks other functions from executing for a
 *                  period of ~3-15 ms depending on the core frequency.
 *
 *                  According to USB 2.0 specification section 7.1.7.7,
 *                  "The remote wakeup device must hold the resume signaling
 *                  for at least 1 ms but for no more than 15 ms."
 *                  The idea here is to use a delay counter loop, using a
 *                  common value that would work over a wide range of core
 *                  frequencies.
 *                  That value selected is 1800. See table below:
 *                  ==========================================================
 *                  Core Freq(MHz)      MIP         RESUME Signal Period (ms)
 *                  ==========================================================
 *                      48              12          1.05
 *                       4              1           12.6
 *                  ==========================================================
 *                  * These timing could be incorrect when using code
 *                    optimization or extended instruction mode,
 *                    or when having other interrupts enabled.
 *                    Make sure to verify using the MPLAB SIM's Stopwatch
 *                    and verify the actual signal on an oscilloscope.
 *******************************************************************/
void USBCBSendResume(void)
{
    static WORD delay_count;
    
    //First verify that the host has armed us to perform remote wakeup.
    //It does this by sending a SET_FEATURE request to enable remote wakeup,
    //usually just before the host goes to standby mode (note: it will only
    //send this SET_FEATURE request if the configuration descriptor declares
    //the device as remote wakeup capable, AND, if the feature is enabled
    //on the host (ex: on Windows based hosts, in the device manager 
    //properties page for the USB device, power management tab, the 
    //"Allow this device to bring the computer out of standby." checkbox 
    //should be checked).
    if(USBGetRemoteWakeupStatus() == TRUE) 
    {
        //Verify that the USB bus is in fact suspended, before we send
        //remote wakeup signalling.
        if(USBIsBusSuspended() == TRUE)
        {
            USBMaskInterrupts();
            
            //Clock switch to settings consistent with normal USB operation.
            USBCBWakeFromSuspend();
            USBSuspendControl = 0; 
            USBBusIsSuspended = FALSE;  //So we don't execute this code again, 
                                        //until a new suspend condition is detected.

            //Section 7.1.7.7 of the USB 2.0 specifications indicates a USB
            //device must continuously see 5ms+ of idle on the bus, before it sends
            //remote wakeup signalling.  One way to be certain that this parameter
            //gets met, is to add a 2ms+ blocking delay here (2ms plus at 
            //least 3ms from bus idle to USBIsBusSuspended() == TRUE, yeilds
            //5ms+ total delay since start of idle).
            delay_count = 3600U;        
            do
            {
                delay_count--;
            }while(delay_count);
            
            //Now drive the resume K-state signalling onto the USB bus.
            USBResumeControl = 1;       // Start RESUME signaling
            delay_count = 1800U;        // Set RESUME line for 1-13 ms
            do
            {
                delay_count--;
            }while(delay_count);
            USBResumeControl = 0;       //Finished driving resume signalling

            USBUnmaskInterrupts();
        }
    }
}


/*******************************************************************
 * Function:        BOOL USER_USB_CALLBACK_EVENT_HANDLER(
 *                        USB_EVENT event, void *pdata, WORD size)
 *
 * PreCondition:    None
 *
 * Input:           USB_EVENT event - the type of event
 *                  void *pdata - pointer to the event data
 *                  WORD size - size of the event data
 *
 * Output:          None
 *
 * Side Effects:    None
 *
 * Overview:        This function is called from the USB stack to
 *                  notify a user application that a USB event
 *                  occured.  This callback is in interrupt context
 *                  when the USB_INTERRUPT option is selected.
 *
 * Note:            None
 *******************************************************************/
BOOL USER_USB_CALLBACK_EVENT_HANDLER(USB_EVENT event, void *pdata, WORD size)
{
    switch(event)
    {
        case EVENT_TRANSFER:
            //Add application specific callback task or callback function here if desired.
            break;
        case EVENT_SOF:
            USBCB_SOF_Handler();
            break;
        case EVENT_SUSPEND:
            USBCBSuspend();
            break;
        case EVENT_RESUME:
            USBCBWakeFromSuspend();
            break;
        case EVENT_CONFIGURED: 
            USBCBInitEP();
            break;
        case EVENT_SET_DESCRIPTOR:
            USBCBStdSetDscHandler();
            break;
        case EVENT_EP0_REQUEST:
            USBCBCheckOtherReq();
            break;
        case EVENT_BUS_ERROR:
            USBCBErrorHandler();
            break;
        case EVENT_TRANSFER_TERMINATED:
            //Add application specific callback task or callback function here if desired.
            //The EVENT_TRANSFER_TERMINATED event occurs when the host performs a CLEAR
            //FEATURE (endpoint halt) request on an application endpoint which was 
            //previously armed (UOWN was = 1).  Here would be a good place to:
            //1.  Determine which endpoint the transaction that just got terminated was 
            //      on, by checking the handle value in the *pdata.
            //2.  Re-arm the endpoint if desired (typically would be the case for OUT 
            //      endpoints).
            break;
        default:
            break;
    }      
    return TRUE; 
}//FINE

//Azzera l'intero array da inviare via USB
void azzera_ToSendDataBuffer(void){
    unsigned iii = 0;
    for(iii = 0; iii < 64; iii++){
	    ToSendDataBuffer[iii] = 0;
	}
}



/** EOF main.c *************************************************/
#endif
