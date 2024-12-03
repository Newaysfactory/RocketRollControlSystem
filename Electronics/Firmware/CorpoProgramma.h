#ifndef CORPOPROGRAMMA_H
#define CORPOPROGRAMMA_H

/***** VARIABILI ESTERNE *****************************************/
//Le variabili esterne sono dichiarate in main.c
extern unsigned char pwm_state;
extern unsigned int pwm_alto;
extern unsigned int pwm_basso;
extern StrValoriPWM PWM;        //Dichiaro  PWM come variabile di tipo ValoriPWM...

/***** VARIABILI VISIBILI IN QUESTO FILE ************************/
unsigned char stato_macchina = 0,        //stato macchina
              stato_memorizzazione = 0,  //se 1 -> memorizzazione ON, se 0 -> memorizzazione OFF
              simulazione = 0,           //Flag di simulazione volo
              programma = 0,             //Programma che il computer eseguirà
              contatore_controllore = 0; //Contatore per far girare il controllore a una frazione della frequenza del programma principale
              
unsigned int i;

StrEeprom eeprom;      //Struttura in cui verrà riversato il contenuto della EEPROM

unsigned short long indirizzo_fram = 100;   //Variabile con l'indirizzo della FRAM

StrZero taratura;       //Dichiaro taratura come struttura di tipo StrZero definita in "Strutture_e_prototipi.h"
StrSensori dati_raw;    //Dichiaro dati_raw come variabile di tipo StrSensori
StrStato stato;         //Dichiaro "stato" come variabile di tipo StrStato

//float min_rad,           //Valori recuperati dalla EEPROM di massime deflessioni del servo
//      max_rad,
//      centro_ms,                //Valore recuperato dalla EEPROM di posizione centrale servo in ms*10000
//      trasferenza_rad_ms,       //Valore recuperato dalla EEPROM della traferenza moltiplicata per 10000
//      rotazione_per_programma_1;//Rotazione fissa del programma 1

//Per il controllore      
float errore_del_setpoint = 0,      //Errore
	  old_error = 0,  //Errore precedente
	  I = 0;          //Azione integrale (va memorizzata)          
float P = 0,          //Componenti proporzionale e derivativa del PID
	  D = 0,
	  i_inst = 0,  //Parte integale istantanea
	  Out = 0;    //Uscita del regolatore
	  
float Kp = 0,
      Ki = 0,
      Kd = 0;

//Variabili e puntatori per il buffer memoria
#pragma udata usb4                     // Metto nella sezione usb4 unificata (vedi file linker modificato)
char buffer_mem[DIM_BUF_MEMORIA];      // Definisce il buffer per la memorizzazione dati. Bisogna accedervi tramite puntatore 
#pragma udata                          // Return to default section
char *pnt_buffer_mem = &buffer_mem[0];    // Definisco il puntatore al buffer memoria
char *pnt_inizio_buffer_mem = &buffer_mem[0];   //Inizializza il puntatore al primo elemento del buffer circolare (che viene definito al decollo) 
 
#endif