# MPLAB IDE generated this makefile for use with GNU make.
# Project: Tesi Magistrale.mcp
# Date: Mon Aug 08 13:25:49 2011

AS = MPASMWIN.exe
CC = mcc18.exe
LD = mplink.exe
AR = mplib.exe
RM = rm

Tesi\ Magistrale.cof : main.o usb_function_hid.o usb_descriptors.o usb_device.o CorpoProgramma.o FRAM.o
	$(LD) /p18F2553 /l"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\intEEPROM" /l"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\delay" /l"C:\Programmi\Microchip\MCC18\lib" "C:\Programmi\Microchip\MCC18\lkr\18f2553.lkr" "main.o" "usb_function_hid.o" "usb_descriptors.o" "usb_device.o" "CorpoProgramma.o" "FRAM.o" "D:\Documenti\Università\Tesi Magistrale\Elettronica\Firmware\v0.3\delay_2553.lib" /u_CRUNTIME /u_DEBUG /z__MPLAB_BUILD=1 /z__MPLAB_DEBUG=1 /z__MPLAB_DEBUGGER_PICKIT2=1 /z__ICD2RAM=1 /o"Tesi Magistrale.cof" /M"Tesi Magistrale.map" /W

main.o : main.c C:/Programmi/Microchip/MCC18/h/stdio.h C:/Programmi/Microchip/MCC18/h/stdlib.h C:/Programmi/Microchip/MCC18/h/string.h usb_common.h usb_device.h usb_hal.h C:/Programmi/Microchip/MCC18/h/adc.h ../../../../../Archivio/Elettronica/Software/LaurTec_c18_libraries/delay/delay.h C:/Programmi/Microchip/MCC18/h/spi.h main.c VectorRemapping.h main.c usb.h GenericTypeDefs.h C:/Programmi/Microchip/MCC18/h/stddef.h Compiler.h C:/Programmi/Microchip/MCC18/h/p18cxxx.h C:/Programmi/Microchip/MCC18/h/p18f2553.h C:/Programmi/Microchip/MCC18/h/stdarg.h usb_config.h C:/Programmi/Microchip/MCC18/h/limits.h usb_ch9.h usb_hal_pic18.h HardwareProfile.h usb_function_hid.h C:/Programmi/Microchip/MCC18/h/pconfig.h
	$(CC) -p=18F2553 /i"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\intEEPROM" -I"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\delay" -I"C:\Programmi\Microchip\mplabc18\v3.39a\h" "main.c" -fo="main.o" -D__DEBUG -D__MPLAB_DEBUGGER_PICKIT2=1 -O-

usb_function_hid.o : usb_function_hid.c C:/Programmi/Microchip/MCC18/h/stdio.h C:/Programmi/Microchip/MCC18/h/stdlib.h C:/Programmi/Microchip/MCC18/h/string.h usb_common.h usb_device.h usb_hal.h usb_function_hid.c GenericTypeDefs.h C:/Programmi/Microchip/MCC18/h/stddef.h Compiler.h C:/Programmi/Microchip/MCC18/h/p18cxxx.h C:/Programmi/Microchip/MCC18/h/p18f2553.h C:/Programmi/Microchip/MCC18/h/stdarg.h usb.h usb_config.h C:/Programmi/Microchip/MCC18/h/limits.h usb_ch9.h usb_hal_pic18.h usb_function_hid.h
	$(CC) -p=18F2553 /i"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\intEEPROM" -I"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\delay" -I"C:\Programmi\Microchip\mplabc18\v3.39a\h" "usb_function_hid.c" -fo="usb_function_hid.o" -D__DEBUG -D__MPLAB_DEBUGGER_PICKIT2=1 -O-

usb_descriptors.o : usb_descriptors.c C:/Programmi/Microchip/MCC18/h/stdio.h C:/Programmi/Microchip/MCC18/h/stdlib.h C:/Programmi/Microchip/MCC18/h/string.h usb_common.h usb_device.h usb_hal.h usb_descriptors.c usb.h GenericTypeDefs.h C:/Programmi/Microchip/MCC18/h/stddef.h Compiler.h C:/Programmi/Microchip/MCC18/h/p18cxxx.h C:/Programmi/Microchip/MCC18/h/p18f2553.h C:/Programmi/Microchip/MCC18/h/stdarg.h usb_config.h C:/Programmi/Microchip/MCC18/h/limits.h usb_ch9.h usb_hal_pic18.h usb_function_hid.h
	$(CC) -p=18F2553 /i"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\intEEPROM" -I"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\delay" -I"C:\Programmi\Microchip\mplabc18\v3.39a\h" "usb_descriptors.c" -fo="usb_descriptors.o" -D__DEBUG -D__MPLAB_DEBUGGER_PICKIT2=1 -O-

usb_device.o : usb_device.c C:/Programmi/Microchip/MCC18/h/stdio.h C:/Programmi/Microchip/MCC18/h/stdlib.h C:/Programmi/Microchip/MCC18/h/string.h usb_common.h usb_device.h usb_hal.h usb_device.c usb.h GenericTypeDefs.h C:/Programmi/Microchip/MCC18/h/stddef.h Compiler.h C:/Programmi/Microchip/MCC18/h/p18cxxx.h C:/Programmi/Microchip/MCC18/h/p18f2553.h C:/Programmi/Microchip/MCC18/h/stdarg.h usb_config.h C:/Programmi/Microchip/MCC18/h/limits.h usb_ch9.h usb_hal_pic18.h HardwareProfile.h usb_device_local.h
	$(CC) -p=18F2553 /i"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\intEEPROM" -I"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\delay" -I"C:\Programmi\Microchip\mplabc18\v3.39a\h" "usb_device.c" -fo="usb_device.o" -D__DEBUG -D__MPLAB_DEBUGGER_PICKIT2=1 -O-

CorpoProgramma.o : CorpoProgramma.c ../../../../../Archivio/Elettronica/Software/LaurTec_c18_libraries/delay/delay.h C:/Programmi/Microchip/MCC18/h/spi.h CorpoProgramma.c C:/Programmi/Microchip/MCC18/h/p18F2553.h HardwareProfile.h C:/Programmi/Microchip/MCC18/h/pconfig.h C:/Programmi/Microchip/MCC18/h/p18cxxx.h C:/Programmi/Microchip/MCC18/h/p18f2553.h
	$(CC) -p=18F2553 /i"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\intEEPROM" -I"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\delay" -I"C:\Programmi\Microchip\mplabc18\v3.39a\h" "CorpoProgramma.c" -fo="CorpoProgramma.o" -D__DEBUG -D__MPLAB_DEBUGGER_PICKIT2=1 -O-

FRAM.o : FRAM.c C:/Programmi/Microchip/MCC18/h/spi.h FRAM.c C:/Programmi/Microchip/MCC18/h/p18F2553.h HardwareProfile.h C:/Programmi/Microchip/MCC18/h/pconfig.h C:/Programmi/Microchip/MCC18/h/p18cxxx.h C:/Programmi/Microchip/MCC18/h/p18f2553.h
	$(CC) -p=18F2553 /i"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\intEEPROM" -I"D:\Documenti\Archivio\Elettronica\Software\LaurTec_c18_libraries\delay" -I"C:\Programmi\Microchip\mplabc18\v3.39a\h" "FRAM.c" -fo="FRAM.o" -D__DEBUG -D__MPLAB_DEBUGGER_PICKIT2=1 -O-

clean : 
	$(RM) "main.o" "usb_function_hid.o" "usb_descriptors.o" "usb_device.o" "CorpoProgramma.o" "FRAM.o" "Tesi Magistrale.cof" "Tesi Magistrale.hex"

