%Adriano Arcadipane 07/03/2010
%Tesi magistrale
%Compone il modello matematico del razzo sia per il moto longitudinale
%(matrici Al Bl Cl Dl), sia quello latero-direzionale (Ald Bld Cld Dld)

clear                     %elimina tutte le variabili dal workspace
clc                       %cancella tutta la schermata precedente




%SEZIONE INPUT ************************************************

%INPUT GENERICI
%Tutte le derivate hanno: 
%             superficie di riferimento -> sezione di fusoliera
%lunghezza di riferimento longitudinale -> diametro di fusoliera 
% lunghezza di riferimento latero-direz -> diametro di fusoliera 

Ve = 100;                   %V di equilibrio (m/s). ATTENZIONE PER FARE SIMULAZIONI A V
                          %DIVERSE BISOGNA CAMBIARE ANCHE IL Cde
gammae = 89.9/57.3;         %Angolo di rampa (rad). Impongo un angolo di 89° per evitare l'ambiguità dei 90°
%gammae = 0;
CLe = 0;                  %Cl di equilibrio. E' zero perchè la condizione di equilibrio è il volo verticale
Cde = 0.54;               %Cd di equilibrio trovato con rocksim alla velocità V di equilibrio (non dal DATCOM perchè per la configurazione scelta non considera l'interferenza
%gammae = 0;
AlfaThrust = 0;           %angolo spinta motore (rad)
ro = 1.18;                %Densità dell'aria (kg/m^3)
m = 3.00;                 %massa del razzo (kg)
g = 9.81;                 %Acc di gravità [m/s^2]
S = 4.902E-3;             %Superficie di riferimento [m^2] (sezione della fusoliera)
diam = 0.079;             %Lunghezza di riferimento [m] (Diametro del razzo)
Ix = 3.606E-3;            %Momento d'inerzia longitudinale [kg*m^2]
Iy = 0.515;               %Momento d'inerzia longitudinale [kg*m^2]
Iz = 0.515;               %Momento d'inerzia longitudinale [kg*m^2]
Xcg = 1.04;               %Posizione del baricentro misurato a partire dalla sommità dell'ogiva [m]
Ycpcanard = 0.0374;       %Posizione del centro di pressione di un'aletta canard [m]. Serve a calcolare il momento di rollio generato



%INPUT DERIVATE DI STABILITA' LONGITUDINALI
%derivate in V
Ctv = 0;                  %E' un razzo, quindi la spinta non dipende dalla velocità.
Cdv = 0;
CLv = 0;
Cmv = 0;
%derivate in alfa
CLa = 28.58;              %Cl alfa dell'intero velivolo. Trovato con Digital DATCOM+.
Cma = -120.7;             %CM alfa dell'intero velivolo
Cda = 2.85;
%derivate in alfa punto
CLaDot = 0;               %Sono nulle perchè il contributo maggiore è quello della downwash che non c'è.
CmaDot = 0;
%derivate in q
CLq = 281.25;
Cmq = -1321.25;

%INPUT DERIVATE DI STABILITA' LATERO-DIREZIONALI
%Derivate in beta
Cyb = -19.43;
Cnb = 120;
Clb = -1.97;
%Derivate in p
Cyp = -2.85;
Cnp = 18.35;
%Clp = -53.33;     %Clp trovato con il DATCOM e sicuramente errato
Clp = -53;        %Clp trovato per tentativi i base ai dati sperimentali
%derivate in r
Cyr = 0;                 %la considero trascurabile
Cnr = -1696;
Clr = 36;

%INPUT DERIVATE DI CONTROLLO
%Cldelta = 4.29;         %Direttamente dal DATCOM
Cldelta = -Clp/80*2.2;   %ricavato dagli esperimenti. Il 2.2 tiene conto del canard maggiorato
%Cddelta = 0.01226;      %Che è linearizzato anche se non si potrebbe
Cddelta = 0;





%SEZIONE CALCOLO E MANIPOLAZIONE DATI ***************************************************************

%calcolo alcune quantità
%lt = abs(Xcg - NPt);      %Distanza tra baricentro e fuoco tail in valore assoluto [m]. Il segno corretto è poi contemplao nelle formule
%lc = abs(Xcg - NPc);      %Distanza tra baricentro e fuoco canard in valore assoluto [m]. Il segno corretto è poi contemplao nelle formule
mu = m/(ro*S*diam/2);          %Massa adimensionale
Cwe = m*g/(1/2*ro*Ve^2*S);     %Forza peso adimensionale
Ixa = Ix/(ro*S*(diam/2)^3);    %Momento d'inerzia adimensionale
Iya = Iy/(ro*S*(diam/2)^3);    %Momento d'inerzia longitudinale adimensionale
Iza = Iz/(ro*S*(diam/2)^3);    %Momento d'inerzia adimensionale
AR = diam^2/S;
Ixap = Ixa * AR;         %Ixa' (' sta per primo. Vedi Etkin 5.13.20)
Izap = Iza * AR;         %Iza' (' sta per primo. Vedi Etkin 5.13.20)


%Costruisce la matrice di stabilità longitudinale (Etkin 5.13.19)
A_longitudinale = [1/(2*mu)*(Ctv*cos(AlfaThrust) - Cdv + 2*Cwe*sin(gammae))  ,  1/(2*mu)*(CLe - Cda)  ,  0  ,  -Cwe*cos(gammae)/(2*mu)  ;
    -((Ctv*sin(AlfaThrust) + CLv + 2*Cwe*cos(gammae))/(2*mu + CLaDot))  ,  -((CLa + Cde)/(2*mu + CLaDot))  ,  (2*mu - CLq)/(2*mu + CLaDot)  ,  -Cwe*sin(gammae)/(2*mu + CLaDot)  ;
    1/Iya*(Cmv - CmaDot*(Ctv*sin(AlfaThrust) + CLv + 2*Cwe*cos(gammae))/(2*mu + CLaDot))  ,  1/Iya*(Cma - CmaDot*(CLa+Cde)/(2*mu + CLaDot))  ,  1/Iya*(Cmq+CmaDot*(2*mu - CLq)/(2*mu + CLaDot))  ,  -CmaDot*Cwe*sin(gammae)/(Iya*(2*mu+CLaDot))  ;
0  ,  0  ,  1  ,  0];

%Costruisce la matrice di stabilità latero-direzionale (Etkin 5.13.20)
A_laterale = [Cyb/(2*mu)  ,  Cyp/(2*mu)  ,  (Cyr-(2*mu/(diam^2/S)))/(2*mu)  ,  Cwe*cos(gammae)/(2*mu)  ;
    Clb/Ixap  ,  Clp/Ixap  , Clr/Ixap  , 0  ;
    Cnb/Izap  ,  Cnp/Izap  ,  Cnr/Izap  ,  0;
    0  ,  -1/AR  ,  tan(gammae)/AR  ,  0];

%Costruisce la matrice di controllo longitudinale (delta alettoni è il
%solo input)
B_longitudinale = [ -1/(2*mu)*Cddelta  ;  0  ;  0  ;  0 ];

%Costruisce la matrice di controllo latero-direzionale (delta alettoni è il
%solo input)
B_laterale = [ 0  ;  Cldelta/Ixap  ;  0  ;  0 ];

%calcola gli autovalori e gli autovettori adimensionali(per quanto riguarda le variabili di stato)
[autovett_long_adim autoval_long_adim] = eig(A_longitudinale);
[autovett_lat_adim autoval_lat_adim] = eig(A_laterale);

%trasforma gli autovalori e gli autovettori in forma dimensionale (per
%quanto riguarda il tempo)
autoval_long = diag(autoval_long_adim / (diam/(2*Ve)));     %Devo prendere la diagonale perchè il comando eig fatto prima restituisce
autoval_lat = diag(autoval_lat_adim / (diam/(2*Ve)));       %gli autovalori come diagonale di una matrice
autovett_long = autovett_long_adim / (diam/(2*Ve));
autovett_lat = autovett_lat_adim / (diam/(2*Ve));

%Caratteristiche dei modi
T_sp = 2*pi/imag(autoval_long(1,1));                              %Periodo corto periodo
xi_sp = -real(autoval_long(1,1))/((real(autoval_long(1,1)))^2+(imag(autoval_long(1,1)))^2)^(1/2);   %Smorzamento di corto periodo
T_dr = 2*pi/imag(autoval_lat(1,1));                               %Periodo datch roll
xi_dr = -real(autoval_lat(1,1))/((real(autoval_lat(1,1)))^2+(imag(autoval_lat(1,1)))^2)^(1/2);     %Smorzamento di dutch roll

%Prepara le matrici adimensionali per il blocco "State space" di Simulink
A = [A_longitudinale, zeros(4,4) ; zeros(4,4) , A_laterale];       %Assembla le matrici A 
B = [B_longitudinale ; B_laterale];                                %Assembla le matrici B
C = eye(8);                              %Legame stato uscita. Stato e uscita coincidono, quindi è una matrice identità
D = zeros(8,1);                          %Vettore nullo con 8 righe perchè il legame diretto ingresso uscita non c'è                 

%Matrici per la dimensionalizzazione delle equazioni
%Matrice per trasformare gli elementi della matrice A
P = [diam/(2*Ve^2) 0 0 0 0 0 0 0;
     0 diam/(2*Ve) 0 0 0 0 0 0;
     0 0 (diam/(2*Ve))^2 0 0 0 0 0;
     0 0 0 diam/(2*Ve) 0 0 0 0;
     0 0 0 0 diam/(2*Ve) 0 0 0;
     0 0 0 0 0 (diam/(2*Ve))^2 0 0;
     0 0 0 0 0 0 (diam/(2*Ve))^2 0;
     0 0 0 0 0 0 0 diam/(2*Ve)];
     
% P = [(2*Ve^2)/diam 0 0 0 0 0 0 0;
%       0 (2*Ve)/diam 0 0 0 0 0 0;
%       0 0 (2*Ve^2)/diam 0 0 0 0 0;
%       0 0 0 (2*Ve)/diam 0 0 0 0;
%       0 0 0 0 (2*Ve)/diam 0 0 0;
%       0 0 0 0 0 (2*Ve^2)/diam 0 0;
%       0 0 0 0 0 0 (2*Ve^2)/diam 0;
%       0 0 0 0 0 0 0 (2*Ve)/diam];

%Matrice per trasformare le variabili di stato
% M = [Ve 0 0 0 0 0 0 0;
%      0 1 0 0 0 0 0 0;
%      0 0 2*Ve/diam 0 0 0 0 0;
%      0 0 0 1 0 0 0 0;
%      0 0 0 0 1 0 0 0;
%      0 0 0 0 0 2*Ve/diam 0 0;
%      0 0 0 0 0 0 2*Ve/diam 0;
%      0 0 0 0 0 0 0 1];
 
M = [1/Ve 0 0 0 0 0 0 0;
     0 1 0 0 0 0 0 0;
     0 0 diam/(2*Ve) 0 0 0 0 0;
     0 0 0 1 0 0 0 0;
     0 0 0 0 1 0 0 0;
     0 0 0 0 0 diam/(2*Ve) 0 0;
     0 0 0 0 0 0 diam/(2*Ve) 0;
     0 0 0 0 0 0 0 1]; 
 
%Dimensionalizza le equazioni matrici
Ad = inv(P)*A*M;
Bd = inv(P)*B;     

%Trasforma le matrici per lo spazio di stato in una matrice di
%trasferimento con il solo ingresso del delta (peraltro unico ingresso)
[num den]=ss2tf(A,B,C,D,1);

%NON USATO
%Matrice diagonale per passare da variabili di stato adimensionali a dimensionali
%dimensionalizzazione = diag([Ve , 1 , 2*Ve/diam , 1 , 1 , 2*Ve/diam , 2*Ve/diam , 1]);


%SEZIONE DISPLAY DELL'OUTPUT ************************************************************************

disp('PROGRAM START **********************************')
disp('Matrice di stabilità longitudinale')
disp(A_longitudinale)
disp('Matrice di stabilità latero-direzionale')
disp(A_laterale)
disp('Autovalori, autovettori longitudinali (per colonne), periodo e smorzamento di corto periodo')
disp(autoval_long)
disp(autovett_long)
disp(['Corto periodo -> Periodo:' , num2str(T_sp) , '   Smorz:' , num2str(xi_sp),10])
disp('Autovalori, autovettori latero-direzionali (per colonne), periodo e smorzamento di dutch roll')
disp(autoval_lat)
disp(autovett_lat)
disp(['Datch Roll    -> Periodo:' , num2str(T_dr) , '   Smorz:' , num2str(xi_dr),10])
disp('Matrice A')
disp(A)
disp('Matrice B')
disp(B)
disp('Matrice C')
disp(C)
disp('Matrice D')
disp(D)
disp('Numeratore matrice di trasferimento')
disp(num)
disp('denominatore matrice di trasferimento')
disp(den)
disp(['END OF THE JOB *********************************',10,10,10])
 