#ifndef STRUTTURE_E_PROTOTIPI_H
#define STRUTTURE_E_PROTOTIPI_H


//Strutture e prototipi pubblici di tutto il programma
#include <p18F2553.h>
#include "usb.h"
#include "usb_function_hid.h"
#include "HardwareProfile.h"     //Contiene la configurazione hardware (non l'inizializzazione)
#include <delay.h>               //Libreria Delay di Laurtec
#include <delays.h>              //Libreria Delay del C18
#include <spi.h>                 //Libreria SPI del C18
#include "FRAM.h"                //Libreria mio sulla FRAM
#include "intEEPROM.h"           //Libreria LAURTEC per la EEPROM interna
#include <adc.h>                 //Libreria ADC del C18
#include "L3G4200_gyro.h"        //Libreria mia giroscopio
#include <timers.h>              //Libreria timers del C18. Necessaria per i timer e per il PWM
#include <math.h>                //Funzioni matematiche

/*** STRUTTURE **********************************************/
typedef struct{                 //Definisco la struttura standard delle letture dei sensori
    unsigned int acc;
    unsigned int baro;
    unsigned int analog_aux;
    int p;
    int q;
    int r;
}StrSensori;

typedef struct{                 //Definisco la struttura standard dei dati elaborati
    float x;
    float v;
    float a;
    float p_ang;
    float v_ang;
    float a_ang;
    float rotazione_rad;   
}StrStato;

typedef struct{                //Definisco la struttura di taratura
    float baro;
    float acc;
    float P0;        //Aggiunti dalla versione firmware 0.10
    float Q0;
    float R0;
}StrZero;

typedef struct{
	unsigned char byte_verifica;
	unsigned int alto;
	unsigned int basso;
}StrValoriPWM;

typedef struct{
	//int P0;  //Rimossi perchè la taratura non avviene più una tantum ma a ogni accensione
	//int Q0;
	//int R0;
	int low_battery_value;
	float servo_zero;
	float servo_min;
	float servo_max;
	float trasferenza_rad_ms;
	unsigned char programma;
	float rotazione_per_programma_1;
	//float Kp;   //Guadagni del PID
	//float Ki;
	//float Kd;	
}StrEeprom;

/******* PROTOTIPI GLOBALI ***********************************/
void Principale(void);
StrValoriPWM ruota(float);
int ADC_get_sample(unsigned char);
StrZero autodiagnosi_e_calibrazione(void);
void gestione_errori (unsigned char);
void recupera_eeprom(void);
//StrSensori leggi_sensori(void);
void leggi_sensori(StrSensori *p_dati);
void azzera_variabili(void);
//StrStato osservatore(StrSensori);   //per vecchia versione senza puntatori
void osservatore(StrSensori *pnt_dati_da_elaborare,StrStato *pnt_stato);
void riempimento_buffer(void);
void memorizzazione(void);
float da_ADC_a_metri(unsigned int, float);
float da_ADC_a_ms2(unsigned int, float);
float controllore(void);
void missione_compiuta(void);
void simula_dati (void);

#endif//





