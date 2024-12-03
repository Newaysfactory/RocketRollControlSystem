#include "FRAM.h"                

//Abilita la scrittura
void fram_write_enable(void){
   CS_MEMORIA = 0;
   WriteSPI(WREN);
   CS_MEMORIA = 1;
}

//Disabilita la scrittura
void fram_write_disable(void){
   CS_MEMORIA = 0;
   WriteSPI(WRDI);
   CS_MEMORIA = 1;
}

//Scrive un byte all'indirizzo indicato
void fram_write (unsigned short long indirizzo_scrittura_fram,unsigned char dato_da_scrivere_su_fram){                        
    fram_write_enable();                      //Attiva la scrittura
    CS_MEMORIA = 0; 
    WriteSPI(WRITE);                          //Invia il comando di scrittura
    WriteSPI((char) (indirizzo_scrittura_fram >> 16));  //Invia l'indirizzo in tre pezzi
    WriteSPI((char) (indirizzo_scrittura_fram >> 8));            
    WriteSPI((char)  indirizzo_scrittura_fram);  
    WriteSPI(dato_da_scrivere_su_fram);                           //Invia il byte da scrivere
    CS_MEMORIA = 1;                           
}

//Legge un byte all'indirizzo indicato
unsigned char fram_read (unsigned short long indirizzo){
	unsigned char dato_letto = 0;
    CS_MEMORIA = 0; 
    WriteSPI(READ); 
    WriteSPI((char) (indirizzo >> 16));  //Invia l'indirizzo in tre pezzi
    WriteSPI((char) (indirizzo >> 8));            
    WriteSPI((char)  indirizzo);  
    while (!DataRdySPI());
    dato_letto = ReadSPI();
    CS_MEMORIA = 1;
    return (dato_letto);                       //Ritorna il byte ricevuto
}

//Scrive lo status register della FRAM
void fram_write_register(unsigned char nuovo_registro){
    fram_write_enable();                      //Attiva la scrittura
    CS_MEMORIA = 0; 
    WriteSPI(WRSR);                           //Invia il nuovo registro
    WriteSPI(nuovo_registro);
    CS_MEMORIA = 1;
}


//Legge lo status register della FRAM
unsigned char fram_read_register(void){
    unsigned char registro = 0;
    CS_MEMORIA = 0;
    WriteSPI(RDSR);                           //Comando di lettura dello status register
    registro = ReadSPI();                     //Legge il byte ricevuto
    CS_MEMORIA = 1;
    return (registro);                          
}//FINE fram_read_register


//Cancella l'intera FRAM
void fram_erase(void){
	unsigned short long iii;
	fram_write_enable();                   //Attiva la scrittura
    CS_MEMORIA = 0; 
    WriteSPI(WRITE);                       //Invia il comando di scrittura
    WriteSPI(0);                           //Invia l'indirizzo in tre pezzi (0 in questo caso)
    WriteSPI(0);
    WriteSPI(0);    
	for(iii = 0; iii < 131072; iii++){
		WriteSPI(0xFF);                    //Cancella tutta la memoria
	}
	CS_MEMORIA = 1;  
}//FINE fram_erase

