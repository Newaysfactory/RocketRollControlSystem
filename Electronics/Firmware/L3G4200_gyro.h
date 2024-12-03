/********************************************************************
 FileName:      L3G4200_gyro.h
 Dependencies:  See INCLUDES section
 Processor:		-
 Hardware:		Mio
 Complier:  	Microchip C18
 Owner:         Adriano Arcadipane

File header per la libreria del giroscopio STmicro L3G4200
*********************************************************************/

#ifndef L3G4200_GYRO_H
#define L3G4200_GYRO_H

#include "HardwareProfile.h"       //File con indicati i pin
#include <spi.h>                   //Libreria SPI del C18

/***** PROTOTIPI ****************************************************/
unsigned char gyro_read_register(unsigned char);
void gyro_write_register (unsigned char, unsigned char);
int gyro_get_axis (unsigned char);

/***** INIDIRIZZI REGISTRI ******************************************/
#define WHO_AM_I 0x0F
#define CTRL_REG1 0x20 
#define CTRL_REG2 0x21 
#define CTRL_REG3 0x22 
#define CTRL_REG4 0x23 
#define CTRL_REG5 0x24 
#define REFERENCE 0x25 
#define OUT_TEMP 0x26 
#define STATUS_REG 0x27
#define OUT_X_L 0x28 
#define OUT_X_H 0x29 
#define OUT_Y_L 0x2A 
#define OUT_Y_H 0x2B 
#define OUT_Z_L 0x2C 
#define OUT_Z_H 0x2D 
#define FIFO_CTRL_REG 0x2E 
#define FIFO_SRC_REG 0x2F
#define INT1_CFG 0x30
#define INT1_SRC 0x31
#define INT1_TSH_XH 0x32
#define INT1_TSH_XL 0x33
#define INT1_TSH_YH 0x34
#define INT1_TSH_YL 0x35
#define INT1_TSH_ZH 0x36
#define INT1_TSH_Z 0x37 
#define INT1_DURATION 0x38 

/***** ALIAS VARI *******************************************/
#define ASSE_X     OUT_X_L    
#define ASSE_Y     OUT_Y_L
#define ASSE_Z     OUT_Z_L



#endif