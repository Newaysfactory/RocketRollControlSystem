# RocketRollControlSystem
Repository containing code, documentation and other about my master's degree thesis about an aerodynamic roll control system for model rockets 

## Things to know before you start
* Main source of documentation is my degree thesis placed on the repository root folder.
* This source code and documentation is from 2012. It is provided essentially for educational purposes, since many of the hardware components used are obsolete today.
* Documentation and the master's degree thesis are mainly in italian.

## What you find here
* **Thesis_2.3.pdf** Start here to understand this project
* **License** This project is under GNU General Public License v3.0
* **FlightTrialsLogbook** Logbook of the six experimental fligts I did with the roll control system
* **Electronics -> Firmware** The source code of the roll control system in C language. Open it with Microchip MPLAB IDE. Under "File per MPLAB Simulator" it also contains some stimuli file I used to test the system on bench
* **Electronics -> HardwareDesign** The PCB images (top and bottom) and the schematic and PCB source. To open in Labcenter Proteus ISIS and ARES. Further info in the local readme.txt
* **ExcelSheets** Some spreadsheets I used to dimension the control fins. Further info in the local readme.txt
* **Matlab** A couple of Simulink models I used to design the control system. Those closed loop models contains the mathematical model of the rocket, the actuator dynamics, the fins aerodynamics and the controller. Further info in the local readme.txt
* **Openrocket** The .ENG rocket motor thrust curves to be used with RockSim
* **PC Software** The source code in Visual Basic and the executable of the software I developed to setup the roll control system and to download flight data after the flight. Further info in the local readme.txt
* **Pictures** Pictures of all the building process
* **Rocket drawings** 3D CAD mechanical drawings of the rocket and of the control system in McNeel Rhinoceros format
* **RocksimModel** Rocksim model to simulate the rocket stability and simulate the flight

## Known issues and limitations:
* Roll angle, rate and acceleration in output from the roll Kalman fileter are not fully coherent. I never investigate further.
