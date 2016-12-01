#include "stdafx.h"
int var=1;
char persona='b';
int nombre_variable=5+var/8*-(5+2/3);
char persona2=persona;
int miFuncion(char unCaracter){
char micar='b';
if(micar=='a'){
for(int x=0;
x<5;
x=x+1)
{
printf("%i ",x);
}
}
return x;
}
int mifuncion2(){
char mi_caracter='a';
miFuncion(mi_caracter);
while(nombre_variable<5){
printf("%i ",nombre_variable);
nombre_variable=nombre_variable*2;
}
printf("\n\n\n\n ");
do{
printf("%i ",nombre_variable);
nombre_variable=nombre_variable/2;
}
while(mi_caracter!='a')return 0;
}
int main(){
printf("%i @ ",nombre_variable);
float por_escanear;
scanf("%f ",&por_escanear);
if(por_escanear!=0){
if(por_escanear>0){
printf("Mayor a cero ");
}
else{
printf("Menor a cero ");
}
}
return 0;
}
