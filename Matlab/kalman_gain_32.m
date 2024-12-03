% © Adriano Arcadipane 2012
%Kalman gain computation for pressure and acceleration sensor
%Put in the correct values for Sampling rate in Hz, barometer noise in
%meters,accelerometer noise in m/s^2 and model noise (va inserito un poco a
%muzzo. In genere è parecchio inferiore al rumore degli strumenti)

function [output_args] = kalman_gain_32 ( sampling_rate , barometer_noise , accelerometer_noise , model_noise)

  clear t phi barometer_variance accelerometer_variance model_variance iter iii R Q Pk_piu Pk_meno K H  %clear variables

  %inizialization
  dt = 1/sampling_rate;                   %time between samples
  barometer_variance = barometer_noise^2; 
  accelerometer_variance = accelerometer_noise^2;
  model_variance = model_noise^2;
  iter = 5000;                           %set iteration cycles
  H = [[1 0 0];[0 0 1]];                           %Matrix that transform system state into measurements 
Q = [[model_variance 0 0];[0 model_variance 0];[0 0 model_variance]];  %model noise covariace matrix
  P = [[100 0 0];[0 100 0];[0 0 100]];               %first value to start the iteration
  R = [[barometer_variance 0];[0 accelerometer_variance]];
  phi = [[1,dt,dt^2/2];[0,1,dt];[0,0,1]]; %set the model matrix
  K_progress_1 = [];                      %matrix with first 3 K of each iteration (to do a convergence graph)
  K_progress_2 = [];                      %matrix with last 3 K of each iteration (to do a convergence graph)
  
  %start
  disp(['Sampling rate: ',num2str(sampling_rate),' Hz'])
  disp(['last two values of ',num2str(iter),' iterations'])
  disp(['barometer noise [m]: ',num2str(barometer_noise), '    accelerometer noise[m/s^2]: ',num2str(accelerometer_noise),'   model noise: ',num2str(sqrt(model_noise))])
  for iii=1:iter                                %Make a lot of iterations
     
    P = phi * P * phi' + Q;                            %Error covariance update
    K = P*H'*((H * P * H')+ R)^-1;     %Kalman gain computation
    P = (eye(3) - K * H) * P ;                         %Error covariance correction
    K_progress_1 = [K_progress_1,K(:,1)];              %Add first 3 K of this iteration
    K_progress_2 = [K_progress_2,K(:,2)];              %Add last 3 K of this iteration
    
    if iii > (iter-2)                          %display only the last two..
       disp(K)                                %..K values. If values are..
    end                                        %..the same the iteration converged
   
  end
  disp(['END **********************************************',10,13])
  semilogy([K_progress_1',K_progress_2'])
  output_args = K;
  clear t sampling_rate phi model_noise barometer_noise iter iii R Q Pk_piu Pk_meno H K  %Clear variables prior to exit
end