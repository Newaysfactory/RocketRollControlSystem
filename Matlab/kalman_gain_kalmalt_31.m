% © Adriano Arcadipane 2011
%Kalman gain computation for pressure measurement only
%Put in the correct values for Sampling rate, model noise and measurement
%noise

function [output_args] = kalman_gain_kalmalt_31 ( sampling_rate , measurement_noise , variance_ratio);

  clear t phi model_noise iter iii R Q Pk_piu Pk_meno K H  %clear variables

  %INPUTS
  %sampling_rate = 200;                    %In sample per second
  %measurement_noise = 1;                 %measurement noise (standard deviation)
  %variance_ratio = 50000;                 %Measurement noise / model noise


  %inizialization
  dt = 1/sampling_rate;                   %time between samples
  measurement_variance = measurement_noise^2;  
  model_variance = measurement_variance / variance_ratio;
  iter = 10000;                           %set iteration cycles
  H = [1 0 0];                           %Matrix that transform system state into measurements 
  Q = [0 0 0;0 0 0;0 0 model_variance];  %model noise covariace matrix
  P = [2  0  0;0 9 0;0 0 9];               %first value to start the iteration
  phi = [[1,dt,dt^2/2];[0,1,dt];[0,0,1]]; %set the model matrix
  K_progress = [];                        %matrix with the K of each iteration (to do a convergence graph)
  
  %start
  disp(['Sampling rate: ',num2str(sampling_rate),' Hz'])
  disp(['last two values of ',num2str(iter),' iterations'])
  disp(['measurement noise: ',num2str(measurement_noise),'   model noise: ',num2str(sqrt(model_variance))])
  for iii=1:iter                                %Make a lot of iterations
     
    P = phi * P * phi' + Q;                            %Error covariance update
    K = P*H'/((H * P * H')+ measurement_variance);     %Kalman gain computation
    P = (eye(3) - K * H) * P ;                         %Error covariance correction
    K_progress = [K_progress,K];                       %Add K of this iteration
    
    if iii > (iter-2)                          %display only the last two..
       disp(K)                                %..K values. If values are..
    end                                        %..the same the iteration converged
   
  end
  disp(['END **********************************************',10,13])
  plot(K_progress')
  output_args = K;
  clear t sampling_rate phi model_noise measurement_noise iter iii R Q Pk_piu Pk_meno H K  %Clear variables prior to exit
end