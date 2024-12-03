% © Adriano Arcadipane 2012
%Calcola i guadagni del filtro di Kalman statico (solo giroscopio) 
%e lo applica ai dati forniti.
%In ingresso richiede il vettore colonna dei dati (sensor_reading), la frequenza
%di campionamento in Hz (sampling_rate), il rumore del giroscopio in rad/s
%e il rumore del modello matematico.
%La matrice sensor_readings contiene nella prima colonna le accelerazioni in
%metri al secondo quadro e nella seconda la quota misurata in metri.

function [output_args] = kalman_script_roll_gyro(input_array , sampling_rate , gyro_noise , model_noise);

  clear t phi model_variance iter iii R Q Pk_piu Pk_meno K H  %clear variables

  %inizialization
  dt = 1/sampling_rate;                   %time between samples
  gyro_variance = gyro_noise^2;           %Trova le varianze dalle deviazioni standard
  model_variance = model_noise^2;
  iter = 10000;                           %set iteration cycles
  H = [1 0];                           %Matrix that transform system state into measurements 
  Q = [model_variance/9 0;0 model_variance];  %model noise covariace matrix
  P = [1 0 ; 0 1];               %first value to start the iteration
  phi = [[1,dt];[0,1]];          %set the model matrix
  K_progress = [];                        %matrix with the K of each iteration (to do a convergence graph)
  
  disp(['Sampling rate: ',num2str(sampling_rate),' Hz'])
  disp(['last two values of ',num2str(iter),' iterations'])
  disp(['Gyro noise: ',num2str(gyro_noise),'   model noise: ',num2str(model_noise)])
  
  %Kalman gains computation
  for iii=1:iter                                %Make a lot of iterations
     
    P = phi * P * phi' + Q;                     %Error covariance update
    K = P*H'/((H * P * H')+ gyro_variance);     %Kalman gain computation
    P = (eye(2) - K * H) * P ;                  %Error covariance correction
    K_progress = [K_progress,K];                %Add K of this iteration
    
    if iii > (iter-2)                           %display only the last two..
       disp(K)                                  %..K values. If values are..
    end                                         %..the same the iteration converged
   
  end
  disp(['Gains computed ******************************************',10,13])
  semilogy(K_progress')
  
%Applying Kalman filter to data 
%Inizialization
computed_data = [];               %define vector of results
x = [0 ; 0];                      %initial state (rocket on the pad = all zero)
data_dim = length (input_array);  %look for the dimension of data (used later for the FOR cicle)

%Computation
  for iii = 1:data_dim                             %for all row of input matrix
    x = phi * x;                                   %state update equation
    x = x + K*(input_array(iii,:)' - H * x);   %state correction equation. I take the iiiesim row of sensor_reading and I make the transpose
    computed_data = [computed_data , x];           %append to the matrix of computed data
  end

%final result
output_args = computed_data';        %output is the transponse matrix (it is a column vector)
K
plot([input_array,computed_data'])
end