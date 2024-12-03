#ifndef FRAM_H
#define FRAM_H

#include "HardwareProfile.h"
#include <spi.h>                 //Libreria SPI del C18

/***** PROTOTIPI *********************************/
void fram_write_enable(void);
void fram_write_disable(void);
void fram_write (unsigned short long ,unsigned char);
unsigned char fram_read (unsigned short long);
void fram_write_register(unsigned char);
unsigned char fram_read_register(void);
void fram_erase(void);

//Comandi per la FRAM RAMTRON RM25V10
#define WREN 0b00000110   //Set Write Enable Latch
#define WRDI 0b00000100   //Write Disable
#define RDSR 0b00000101   //Read Status Register
#define WRSR 0b00000001   //Write Status Register
#define READ 0b00000011   //Read Memory Data
#define FSTRD 0b00001011  //Fast Read Memory Data
#define WRITE 0b00000010  //Write Memory Data
#define SLEEP 0b10111001  //Enter Sleep Mode
#define RDID 0b10011111   //Read Device ID
#define SNR 0b11000011    //Read S/N

#endif