%Adriano Arcadipane 07/03/2010
%Tesi magistrale
%Compone il modello matematico del razzo per il solo rollio

clear                     %elimina tutte le variabili dal workspace
clc                       %cancella tutta la schermata precedente




%SEZIONE INPUT ************************************************

%INPUT GENERICI
%Tutte le derivate hanno: 
%             superficie di riferimento -> sezione di fusoliera
%lunghezza di riferimento longitudinale -> diametro di fusoliera 
% lunghezza di riferimento latero-direz -> diametro di fusoliera 

Ve = 55;                  %V di equilibrio (m/s). ATTENZIONE PER FARE SIMULAZIONI A V...
                          %...DIVERSE BISOGNA CAMBIARE ANCHE IL Cde
ro = 1.18;                %Densità dell'aria (kg/m^3)
diam = 0.079;             %Lunghezza di riferimento [m] (Diametro del razzo)
S = 4.902E-3;             %Superficie di riferimento [m^2] (sezione della fusoliera)
Ix = 3.606E-3;            %Momento d'inerzia longitudinale [kg*m^2]

%INPUT DERIVATE DI STABILITA' LATERO-DIREZIONALI
%Derivate in p
%Clp = -53.33;      %Clp trovato con il DATCOM e sicuramente errato
%Clp = -240;        %Clp trovato per tentativi i base ai dati sperimentali
Clp = -53;

%INPUT DERIVATE DI CONTROLLO
Cldelta = -Clp/80*2.2;   %ricavato dagli esperimenti. Il 2.2 tiene conto del canard maggiorato

%Numeratore e denominatore della funzione di trasferimento
num = [0 ro*Ve^2*S*diam*Cldelta/Ix];
den = [1 -ro*Ve*S*diam^2*Clp/(2*Ix)];
fdt = tf(num,den);

%SEZIONE DISPLAY DELL'OUTPUT ************************************************************************

disp('PROGRAM START **********************************')
disp('Funzione di trasferimento del rollio')
disp(fdt)
disp(['END OF THE JOB *********************************',10,10,10])
 