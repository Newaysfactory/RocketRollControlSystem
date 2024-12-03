%Applica il filtro di Kalman per sensori barometrici. In ingresso richiede
%il vettore colonna dei dati (sensor_reading) e la frequenza di campionamento in Hz (sampling_rate)

%Istruzioni:
%chiamare la funzione dalla command window con ad esempio:
%kalman_script(data,200)

%FARE ATTENZIONE CHE IL GUADAGNO DEL FILTRO K SIA CORRETTO PER LA
%PARTICOLARE APPLICAZIONE



function [ output_args ] = kalman_script_BOZZA( sensor_readings ,K, sampling_rate)

%inizialization                       

t = 1/sampling_rate;                 %compute time between samples
computed_data = [];                  %define vector of results
phi = [1 t t^2/2 ; 0 1 t ; 0 0 1];   %define state transition matrix
H = [1 0 0];                        %define matrix from state to measurement
%K = [0.003976293742239 ; 0.009238581723976 ; 0.004450643419407];      %define kalman gain vector
x_plus = [0 ; 0 ; 0];                %initial state (rocket on the pad = all zero)
data_dim = length (sensor_readings);  %look for the dimension of data (used late for the FOR cicle)

%program start

  for iii = 1:data_dim                                        %for all row of input matrix
    x_minus = phi * x_plus;                                   %state update equation
    x_plus = x_minus + K*(sensor_readings(iii) - H * x_minus); %state correction equation
    computed_data = [computed_data , x_plus];                 %append to the matrix of computed data (it is a row vector)  
  end

%final result
output_args = computed_data';        %output is the transponse matrix (it is a column vector)
plot([sensor_readings,computed_data'])
end

