% © Adriano Arcadipane 2011
%Applica il filtro di Kalman per sensori barometrici. In ingresso richiede
%il vettore colonna dei dati (sensor_reading) e la frequenza di campionamento in Hz (sampling_rate)

%Istruzioni:
%chiamare la funzione dalla command window con ad esempio:
%kalman_script(data,200)

%FARE ATTENZIONE CHE IL GUADAGNO DEL FILTRO K SIA CORRETTO PER LA
%PARTICOLARE APPLICAZIONE



function [ output_args ] = kalman_script_solo_barometro( sensor_readings , sampling_rate,measure_noise,variance_ratio)

%inizialization                       

t = 1/sampling_rate;                 %compute time between samples
computed_data = [];                  %define vector of results
phi = [1 t t^2/2 ; 0 1 t ; 0 0 1];   %define state transition matrix
H = [1 0 0];                        %define matrix from state to measurement
x = [0 ; 0 ; 0];                %initial state (rocket on the pad = all zero)
P =[ 0.0102 0.0169 0.0139;0.0169 0.0418 0.0459;0.0139 0.0459 0.0759];         %Initial covariance matrix
data_dim = length (sensor_readings);  %look for the dimension of data (used late for the FOR cicle)
measure_variance = measure_noise^2;
model_variance = measure_variance / variance_ratio;
Q = [0 0 0;0 0 0;0 0 model_variance];

%program start

  for iii = 1:data_dim                                        %for all row of input matrix
    x = phi * x;                                   %state update equation
    P = phi*P*phi' + Q;
    K = P*H'*(H*P*H' + measure_variance);
    x = x + K*(sensor_readings(iii) - H * x); %state correction equation
    computed_data = [computed_data , x];                 %append to the matrix of computed data (it is a row vector)  
    P =(eye(3)-K*H)*P;
  end

%final result
output_args = computed_data';        %output is the transponse matrix (it is a column vector)
plot([sensor_readings,computed_data'])
K
end

