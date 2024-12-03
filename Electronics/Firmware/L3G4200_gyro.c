#include "L3G4200_gyro.h"

#define READ_COMMAND 0b10000000            //All'inizio di ogni invio (1°bit) bisogna dire al gyro se vogliamo
#define WRITE_COMMAND 0b00000000           //leggere o scrivere i registri
#define ADDRESS_FIXED 0b00000000           //All'inizio di ogni invio (2°bit) bisogna dire al gyro se vogliamo
#define ADDRESS_AUTO_INCREMENT 0b01000000  //che l'indirizzo si autoincrementi a ogni lettura oppure no

//Ritorna il registro indicato
unsigned char gyro_read_register (unsigned char indirizzo_registro){
    unsigned char registro_letto = 0;
    CS_GYRO = 0;
    //registro_letto = READ_COMMAND | ADDRESS_FIXED | indirizzo_registro;    //Per il debug
    WriteSPI (READ_COMMAND | ADDRESS_FIXED | indirizzo_registro);
    //while (!DataRdySPI());
    registro_letto = ReadSPI ();
    CS_GYRO = 1;
    return registro_letto;
}

//Scrive il registro indicato
void gyro_write_register (unsigned char indirizzo_registro, unsigned char dato){
    CS_GYRO = 0;
    WriteSPI (WRITE_COMMAND | ADDRESS_FIXED | indirizzo_registro);
    WriteSPI (dato);
    CS_GYRO = 1;
}


//Legge la velocità angolare di un asse. In ingresso riceve l'LSB del
//registro relativo all'asse interessato e restituisce la velocità angolare
//in gradi al sec con tanto di segno.
int gyro_get_axis (unsigned char  LSB_asse){
    unsigned char LSB = 0;
    unsigned char MSB = 0;

   LSB = gyro_read_register(LSB_asse);   //Legge l'LSB dell'asse scelto
   MSB = gyro_read_register(LSB_asse + 1);   //Legge l'MSB dell'asse scelto
      //Combina i due byte in un integer. Per far questo promuove
      //MSB a 16 bit e schifta a sinistra di 8 posti, poi lo combina
      //tramite un bitwise OR con l'LSB
   return(  ( ((int) MSB) << 8 ) | LSB   );           
}