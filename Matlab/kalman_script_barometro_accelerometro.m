% © Adriano Arcadipane 2011
%Calcola i guadagni del filtro di Kalman statico (con barometro e accelerometro) 
%e lo applica ai dati forniti.
%In ingresso richiede il vettore colonna dei dati (sensor_reading), la frequenza
%di campionamento in Hz (sampling_rate), il rumore del barometro in metri,
%il rumore dell'accelerometro in m/s^2 e il rumore del modello matematico
%che viene supposto concentrato nell'accelerazione.
%La matrice sensor_readings contiene nella prima colonna le accelerazioni in
%metri al secondo quadro e nella seconda la quota misurata in metri.

%Istruzioni:
%chiamare la funzione dalla command window con ad esempio:
%kalman_script_barometro_accelerometro(data,200,)

function [ output_args ] = kalman_script_barometro_accelerometro( sensor_readings ,sampling_rate, barometer_noise , accelerometer_noise , model_noise);

clear t computed_data phi H x P data_dim Q barometer_variance accelerometer_variance...      %clear variables
    model_variance iter iii R K Pk_piu Pk_meno  

%Kalman gains 
%Inizialization
dt = 1/sampling_rate;                   %time between samples
  barometer_variance = barometer_noise^2; 
  accelerometer_variance = accelerometer_noise^2;
  model_variance = model_noise^2;
  iter = 5000;                           %set iteration cycles
  H = [[1 0 0];[0 0 1]];                           %Matrix that transform system state into measurements 
  Q = [[model_variance 0 0];[0 model_variance 0];[0 0 model_variance]];  %model noise covariace matrix
  P = [[1 0 0];[0 1 0];[0 0 1]];               %first value to start the iteration
  R = [[barometer_variance 0];[0 accelerometer_variance]];
  phi = [[1,dt,dt^2/2];[0,1,dt];[0,0,1]]; %set the model matrix
  K_progress_1 = [];                      %matrix with first 3 K of each iteration (to do a convergence graph)
  K_progress_2 = [];                      %matrix with last 3 K of each iteration (to do a convergence graph)

  disp(['Sampling rate: ',num2str(sampling_rate),' Hz'])
  disp(['last two values of ',num2str(iter),' iterations'])
  disp(['barometer noise [m]: ',num2str(barometer_noise), '    accelerometer noise[m/s^2]: ',num2str(accelerometer_noise),'   model noise: ',num2str(sqrt(model_noise))])
  
  %Computation
  for iii=1:iter                                %Make a lot of iterations
    P = phi * P * phi' + Q;                            %Error covariance update
    K = P*H'*((H * P * H')+ R)^-1;     %Kalman gain computation
    P = (eye(3) - K * H) * P ;                         %Error covariance correction
    K_progress_1 = [K_progress_1,K(:,1)];              %Add first 3 K of this iteration
    K_progress_2 = [K_progress_2,K(:,2)];              %Add last 3 K of this iteration 
  end
  disp(['Gains computed ******************************************',10,13])
  semilogy([K_progress_1',K_progress_2'])

  
%Applying Kalman filter to data 
%Inizialization
computed_data = [];                   %define vector of results
x = [0 ; 0 ; 0];                      %initial state (rocket on the pad = all zero)
sensor_readings = sensor_readings( : ,[2 1]);  %Scambio le colonne per metterle in ordine giusto (nell'algoritmo serve che la prima sia la quota)
data_dim = length (sensor_readings);  %look for the dimension of data (used later for the FOR cicle)


%Computation
  for iii = 1:data_dim                             %for all row of input matrix
    x = phi * x;                                   %state update equation
    x = x + K*(sensor_readings(iii,:)' - H * x);   %state correction equation. I take the iiiesim row of sensor_reading and I make the transpose
    computed_data = [computed_data , x];           %append to the matrix of computed data
  end

%final result
output_args = computed_data';        %output is the transponse matrix (it is a column vector)
K
plot([sensor_readings,computed_data'])

end

