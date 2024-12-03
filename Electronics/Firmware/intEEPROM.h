/*******************************************************
********************************************************
 
Autore : Mauro Laurenti
Versione : 1.0
Data : 14/08/2007
 
CopyRight 2007 all rights are reserved


********************************************************
SOFTWARE LICENSE AGREEMENT
********************************************************

The usage of the supplied software imply the acceptance of the following license.

The software supplied herewith by Mauro Laurenti (the Author) 
is intended for use solely and exclusively on Microchip PIC Microcontroller (registered mark).  
The software is owned by the Author, and is protected under applicable copyright laws. 
All rights are reserved. 
Any use in violation of the foregoing restrictions may subject the 
user to criminal sanctions under applicable laws (Italian or International ones), as well as to 
civil liability for the breach of the terms and conditions of this license. 
Commercial use is forbidden without a written acknowledgment with the Author.
Personal or educational use is allowed if the application containing the following 
software doesn't aim to commercial use or monetary earning of any kind.    

THIS SOFTWARE IS PROVIDED IN AN "AS IS" CONDITION. NO WARRANTIES, 
WHETHER EXPRESS, IMPLIED OR STATUTORY, INCLUDING, BUT NOT LIMITED 
TO, IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
PARTICULAR PURPOSE APPLY TO THIS SOFTWARE. THE AUTHOR SHALL NOT, 
IN ANY CIRCUMSTANCES, BE LIABLE FOR SPECIAL, INCIDENTAL OR 
CONSEQUENTIAL DAMAGES, FOR ANY REASON WHATSOEVER.

********************************************************
PURPOSES
********************************************************

This library is supposed for being used for read and 
write the PIC microcontroller internal EEPROM 
 
*******************************************************
*******************************************************/

#include <p18cxxx.h>

#ifndef FLAG_intEEPROM
#define FLAG_intEEPROM


/***************************************************
*
* Description: This function writes a byte inside 
*              the EEPROM.   
*
* Parameters:
*
* data: byte to write
* address : Address where the byte must be written
* 
* Return:
*
* char: 1: The byte has been properly written
*       0: The byte has not been properly written
*
* Note: 
*
* During the writing process the Interrupts are 
* disabled. But the old status is restored.
*
***************************************************/
char writeIntEEPROM (unsigned char data, unsigned char address);



/***************************************************
*
* Description: This function read a byte inside 
*              the EEPROM.  
*
* Parameters:
*
* address : Address where the byte must be read
* 
* Return:
*
* usingned char: It returns the data byte 
*
*
***************************************************/
unsigned char readIntEEPROM (unsigned char address);


#endif
