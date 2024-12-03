
#ifndef DELAY_FLAG
#define DELAY_FLAG


/***************************************************
*
* Description: This creates a ms delays
* 					
*
* Parameters:
*
* value : delay in ms (min. 1 max. 10000)
*
* Return:
*
* void
*
***************************************************/
void delay_ms (int value);



/***************************************************
*
* Description: This function set the Quartz
* 	 
*
* Parameters:
*
* frequency : Frequency exspressed in MHz (integer)
*
* Return:
*
* void
*
* Note:
* Fractional frequency value are not supported
*
***************************************************/
void setQuartz (unsigned char frequency);


#endif