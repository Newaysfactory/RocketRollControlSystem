%Datomi dalla professoressa Grillo per portare in forma dimensionale le
%matrici del modello matematico. P rende dimensionale la matrice, M rende
%dimensionale le variabili di stato.
%Le matrici di questo esempio sono per il moto longitudinale
%(V,alpha,q,theta,z).
%Si ricorda che l'inversa di una matrice diagonale [2 0;0 3] è un'altra matrice
%diagonale fatta semplicemente così [1/2 0;0 1/3]
P=[c/(2*v^2) 0 0 0 0; 0 c/(2*v) 0 0 0; 0 0 c^2/(4*v^2) 0 0; 0 0 0 c/(2*v) 0; 0 0 0 0 c/(b*(2*v))];
        M=[1/v 0 0 0 0; 0 1 0 0 0; 0 0 c/(2*v) 0 0; 0 0 0 1 0; 0 0 0 0 1/b];
        Ad=inv(P)*A*M;
        Bd=inv(P)*B;