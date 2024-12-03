/********************************************************************
 FileName:      HardwareProfile.h
 Dependencies:  See INCLUDES section
 Processor:		PIC18F2553
 Hardware:		Mio
 Complier:  	Microchip C18
 Owner:         Adriano Arcadipane

 In questo file .h si espleta la configurazione hardware
 assegnando le porte e la configurazione del sistema
*******************************************************************
 File Description:

********************************************************************/

#ifndef HARDWARE_PROFILE_H
#define HARDWARE_PROFILE_H


    /*******************************************************************/
    /******** USB stack hardware selection options *********************/
    /*******************************************************************/
    //This section is the set of definitions required by the MCHPFSUSB
    //  framework.  These definitions tell the firmware what mode it is
    //  running in, and where it can find the results to some information
    //  that the stack needs.
    //These definitions are required by every application developed with
    //  this revision of the MCHPFSUSB framework.  Please review each
    //  option carefully and determine which options are desired/required
    //  for your application.

    //The PICDEM FS USB Demo Board platform supports the USE_SELF_POWER_SENSE_IO
    //and USE_USB_BUS_SENSE_IO features.  Uncomment the below line(s) if
    //it is desireable to use one or both of the features.
    //#define USE_SELF_POWER_SENSE_IO
    //#define tris_self_power     TRISAbits.TRISA2    // Input
    #if defined(USE_SELF_POWER_SENSE_IO)
    #define self_power          PORTAbits.RA2
    #else
    #define self_power          1
    #endif

    //#define USE_USB_BUS_SENSE_IO
    //#define tris_usb_bus_sense  TRISAbits.TRISA1    // Input
    #if defined(USE_USB_BUS_SENSE_IO)
    #define USB_BUS_SENSE       PORTAbits.RA1
    #else
    #define USB_BUS_SENSE       1
    #endif


    //Uncomment the following line to make the output HEX of this  
    //  project work with the MCHPUSB Bootloader    
    //#define PROGRAMMABLE_WITH_USB_MCHPUSB_BOOTLOADER
	
    //Uncomment the following line to make the output HEX of this 
    //  project work with the HID Bootloader
    //#define PROGRAMMABLE_WITH_USB_HID_BOOTLOADER		

    /*******************************************************************/
    /*******************************************************************/
    /*******************************************************************/
    /******** Application specific definitions *************************/
    /*******************************************************************/
    /*******************************************************************/
    /*******************************************************************/

    /** Board definition ***********************************************/
    //These defintions will tell the main() function which board is
    //  currently selected.  This will allow the application to add
    //  the correct configuration bits as wells use the correct
    //  initialization functions for the board.  These defitions are only
    //  required in the stack provided demos.  They are not required in
    //  final application design.
    //#define DEMO_BOARD PICDEM_FS_USB
    //#define PICDEM_FS_USB
    //#define CLOCK_FREQ 48000000


    /** ASSEGNAZIONE PIN **********************************************/
 //   #define mInitAllLEDs()      LATB &= 0xF0; TRISB &= 0xF0;
       
    #define CS_MEMORIA          PORTBbits.RB2   //CS per la memoria, il giroscopio e per il canale ausiliario
    #define CS_GYRO             PORTBbits.RB3
    #define CS_AUX              PORTBbits.RB4
    #define BUZZER              PORTBbits.RB5
    #define LED                 PORTCbits.RC0
    #define ANALOG_AUX          ADC_CH0         //canali analogici 
    #define ACCELEROMETRO       ADC_CH1
    #define BAROMETRO           ADC_CH2
    #define BATTERIA            ADC_CH3
    #define SERVO               PORTCbits.RC2   //Uscita servo
  
    /** I/O pin definitions ********************************************/
    #define INPUT 1       //Così è più facile ricordare tutta la storia
    #define OUTPUT 0

    #define HIBYTE(i)   ((unsigned char)(((int)(i)) >> 8))
    #define LOBYTE(i)   ((unsigned char)(((int)(i)) & 0xFF)) 

    /** VALORI PREDEFINITI E DI TARATURA *******************************/
    //Forzo i valori da scrivere sulla EEPROM a essere interi, altrimenti
    //quando spezzo il valore per scriverlo avvengono errori con numeri < 256
    #define P0_SU_EEPROM  (int)-200                      //Valori a riposo sui tre assi del giroscopio
    #define Q0_SU_EEPROM  (int)24
    #define R0_SU_EEPROM  (int)-24
    #define LOW_BATTERY_VALUE_SU_EEPROM  (int)1848         //corrispondenti a 7 volt
    #define SERVO_ZERO_SU_EEPROM (int)14000                //valore in ms moltiplicato per 10000 del pwm alto a cui corrisponde il centraggio del servo. Esempio: 1,5ms 0 15000
    #define MIN_ROTAZIONE_RAD_SU_EEPROM   (int)(-1745)     //valore in radianti moltiplicato per 10000. Esempio -10° = -0,1745rad -> -1745
    #define MAX_ROTAZIONE_RAD_SU_EEPROM   (int)(1745)      //valore in radianti moltiplicato per 10000. Esempio 10° = 0,1745rad -> 1745
    #define T_TAU_DELTA_SU_EEPROM         (int)17190        //Trasferenza tra rotazione delle alette e variazione del tempo pwm alto per 10000. Esempio:0,764 -> 7640
    #define PROGRAMMA_SU_EEPROM  (char)0                   //Programma da eseguire. Il predefinito è lo zero (normale controllo del rollio)
    #define ROTAZIONE_PER_PROGRAMMA_1_SU_EEPROM (int)0     //Rotazione predefinita per il programma 1(rotazione fissa con registrazione dati)
    #define KP_SU_EEPROM (int)700                          //Guadagno azione proporzionale * 10000  (0.07 = 700)
    #define KI_SU_EEPROM (int)160                          //Guadagno azione integrale * 10000  (0.0016 = 160)
    #define KD_SU_EEPROM (int)0                            //Guadagno azione derivativa * 10000

    #define DIM_BUF_MEMORIA 600             //dimensione del buffer per la memorizzazione (multiplo del blocco di dati, cioè 40)
    
    //Fattore di conversione del giroscopio che dipende dal fondoscala:
    //151.844E-6 @250dps di fondoscala
    //305.43E-6  @500dps di fondoscala
    //1221.73E-6 @2000dps di fondoscala
    #define CONVERTI_DIGIT_RAD_S 305.43E-6  

    //PARAMETRI CHE DIPENDONO DALLA FREQUENZA DI CAMPIONAMENTO
    #define F_CAMPIONAMENTO   100    //Frequenza di campionamento e dell'aggiornamento dello stato
        #define T        (float)(1)/(F_CAMPIONAMENTO)              //Periodo tra un campione e l'altro
        //TIMER0_START_VALUE con il prescaler impostato a 1/1
        //#define TIMER0_START_VALUE  (int)(65535-T/8.3333333E-8)    //MODIFICARE SE SI CAMBIANO IMPOSTAZIONI DI TIMER0 O IL FOSC
        //TIMER0_START_VALUE con il prescaler impostato a 1/2
        #define TIMER0_START_VALUE  (int)(65535-T/16.666E-8)    //MODIFICARE SE SI CAMBIANO IMPOSTAZIONI DI TIMER0 O IL FOSC
    //PARAMETRI KALMAN. Cambiare soltanto i K. I PHI si cambiano in automatico
        #define PHI_VERTICAL_12   T                   //Elementi della matrice PHI per il moto verticale
        #define PHI_VERTICAL_13   (T)*(T)/(2)
        #define PHI_VERTICAL_23   T
        #define PHI_ROLL_11       T                   //Elemento della matrice PHI per il moto di rollio

    #define K11_VERTICAL 0.0366            //Guadagni di Kalman per il moto verticale
    #define K21_VERTICAL 0.0282
    #define K31_VERTICAL 0.0001
    #define K12_VERTICAL 0.0007            //Guadagni di Kalman per il moto verticale
    #define K22_VERTICAL 0.0089
    #define K32_VERTICAL 0.1869
    #define K1_ROLL 0.2534               //Guadagni di Kalman per il moto di rollio
    #define K2_ROLL 0.6857               //Guadagni di Kalman per il moto di rollio
    
    //PARAMETRI DEL CONTROLLORE
    #define DIVISORE_FREQUENZA_CONTROLLO 1  //Che significa 2 perchè il conto parte da zero
    #define SETPOINT 0                      //Velocità angolare da mantenere (rad/s)
    #define UPPER_P_LIMIT 1                 //Limiti superiori e inferiori delle tre azioni e del valore di uscita
    #define UPPER_I_LIMIT 10000
    #define UPPER_D_LIMIT 0.1
    #define UPPER_TOTAL_LIMIT 0.209
    #define LOWER_P_LIMIT -1
    #define LOWER_I_LIMIT -10000
    #define LOWER_D_LIMIT -0.1
    #define LOWER_TOTAL_LIMIT -0.209
    //Per i guadagni delle azioni del PID guardare tra i valori EEPROM

#endif