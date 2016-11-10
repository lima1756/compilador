// CompiladoresEinterpretes1.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "rlutil.h"
#include "conio.h"
#include <dos.h> 
#include <string>
#include <time.h> 
#include <stdio.h>
#define ESC 27
#define Enter 13
#define BACK 8
#include <iostream>

#include <fcntl.h>
#include<fstream>
#include<cstdlib>
#include<sstream>

int origRow;
int origCol;

using namespace std;

#ifdef __linux__ 

#include <unistd.h>
#define GetCurrentDir getcwd
#elif _WIN32 
#include <direct.h>
#include "rlutil.h"
#define GetCurrentDir _getcwd
#elif _WIN64
#include <direct.h>
#define GetCurrentDir _getcwd
#else

#endif



#ifdef __linux__ 

#include <unistd.h>
#define GetCurrentDir getcwd
#elif _WIN32 
#include <direct.h>
#define GetCurrentDir _getcwd
#elif _WIN64
#include <direct.h>
#define GetCurrentDir _getcwd
#else

#endif


void ClearConsoleToColors(int ForgC, int BackC);

void cambia(string parametros[]) {
	string ruta = "";

		char path[100];
		for (int c = 0; c<parametros[1].size(); c++)
			ruta += parametros[c] + " ";
		strcpy_s(path, ruta.c_str());
		_chdir(path);	
		
}

void tiempo() {
	time_t rawtime;
	struct tm timeinfo;
	localtime_s(&timeinfo, &rawtime);
	
}




int main()
{
	string comandoSDado, comandoSDado1;
	int parcolor;
	int posx, posy;
	boolean exit = false;
	string holi;
	char car = 'A';
	char caraux,numc,nump;
	char cadena[255], cadena2[255], cadena3[255], comandoDado[255], comandoDado1[255],  remover[100];;
	char aux[255];
	int x = 0;
	int cont = 0, l = 0, contadorpal = 1, numpal = 0,contCom,echo=1;
	string cadenaS, PALABRA[100];
	int checador = 1;
	char * anterior = _getcwd(NULL, 0);
	char * CurrentPath = _getcwd(NULL, 0);
	char cCurrentPath[FILENAME_MAX];

	if (!GetCurrentDir(cCurrentPath, sizeof(cCurrentPath)))
	{
		return errno;
	}

	
	do {
		if (echo == 1)
			char * CurrentPath = _getcwd(NULL, 0);

			 cout << CurrentPath <<": "<< endl; 
			
		while (car != Enter)
		{
			fflush(stdin);
			car = '\0';
			car = _getch();


			caraux = car;




			if (car == 0 || car == -32 )
			{
				
				if (car = GetAsyncKeyState(0x4F))
				{

					car = -32;
				}
				else {
					//x = 1; POR SI ACASO 
					
					
						car = _getch();

						car = '\0';
					
				}
			}
			

			if (car != 0)
			{
				if (car != Enter) {
					if (car == BACK)
					{
					
						if (cont > 0)
						{
							if (cadena[cont] != '\n')
							{
								
								cont--;
								printf("\b");
								cadena[cont] = '\0';
								printf(" ");
								printf("\b");
							}
							else {
						

							}
							
						}
					}
					else if (car == ESC)
					{
						

						while (cont > 0)
						{

							cont--;
							printf("\b");
							cadena[cont] = '\0';
							printf(" ");
							printf("\b");
						}
					}



					else
					{
						cadena[cont] = caraux;
						printf("%c", cadena[cont]);
						cont++;
					}
				}
				else
				{
					//printf("\nfin de captura\n");
					

					// copia de la cadena 
					while (cadena[cont - 1] == ' ')
					{
						cont--;
						cadena[cont] = '\0';
					}

					for (int i = 0;i < cont;i++) {
						cadena2[i] = cadena[i];
						cadena3[i] = cadena[i];


					}

					cadena[cont] = '\0';
					cadena2[cont] = '\0';
					cadena3[cont] = '\0';

				/*	for (int i = 0;i < cont;i++) {
						printf("-%c-", cadena[i]);
						cout << "%i" << i << endl;


					}  util para los errores*/


					//printf("total %i", strlen(cadena)); contar las letras




					while (checador != 2) {

						if (cadena[x] != ' ')
						{
							cadena[x] = toupper(cadena[x]);
							cadena[x + 1] = toupper(cadena[x + 1]);
							cadena[x + 2] = toupper(cadena[x + 2]);
							cadena[x + 3] = toupper(cadena[x + 3]);

							if ((cadena[x] == 'E' && cadena[x + 1] == 'X'&& cadena[x + 2] == 'I' && cadena[x + 3] == 'T') && (cadena[x + 4] == ' ' || (cadena[x + 4] = GetAsyncKeyState(0x0D))))
							{
								exit = true;
								printf("Adios n.n \n");
								return 0;
								system("pause");
								return 0;
							}

							checador = 2;

						}
						else
						{
							x++;
							checador = 1;
						}

					} //fin while verifica exit

					checador = 1;

				



					if (cont > 0) {
						while (checador != 2) {


							if (cadena3[x] != ' ' && cadena3[x] != '/0')
							{

								aux[l] = cadena3[x];


								if (cadena3[x + 1] == ' ')
								{

									aux[l] = cadena3[x];

									aux[l + 1] = '\0';

									cadenaS = aux;




									//cout << " " << cadenaS << "numero :  " << contadorpal << '\n';
									PALABRA[numpal] = cadenaS;

									numpal++;
									contadorpal++;
									l = -1;
								}
								if (cadena3[x + 1] == '\0')
								{
									aux[l] = cadena3[x];

									aux[l + 1] = '\0';
									cadenaS = aux;
								//	cout << " " << cadenaS << " numero :" << contadorpal << '\n';
									PALABRA[numpal] = cadenaS;

									numpal++;
									contadorpal++;
									l = -1;
									checador = 2;
								}
								l++;
								x++;

							}
							else
							{

								
								if (cadena3[x + 1] == '\0')
								{
									
								}


								x++;

								checador = 1;
							}

						}
					}// separa las palabras



				}//enter 

			}// no especiales			





		}// fin captura y while principal 


		
		aux[l] = cadena3[x];

		aux[l + 1] = '\0';

		cadenaS = aux;

		
		for (contCom = 0; contCom <= PALABRA[0].length(); contCom++) {
			comandoDado[contCom] = tolower(PALABRA[0][contCom]);
			 
		}
	comandoDado[contCom +1]= '\0';
	comandoSDado=comandoDado;

	for (contCom = 0; contCom <= PALABRA[1].length(); contCom++) {
		comandoDado1[contCom] = tolower(PALABRA[1][contCom]);

	}
	
	comandoDado1[contCom + 1] = '\0';
	comandoSDado1 = comandoDado1;


	if (comandoSDado == "help" && numpal==1)
	{	
		cout << "\n" << endl;
		cout << "COLOR          Establece los colores de primer plano y fondo predeterminados de la consola." << endl;
		cout << "PAUSE          Suspende el proceso de un archivo por lotes y muestra un mensaje." << endl;
		cout << "EXIT           Sale del programa (intérprete de comandos)." << endl;
		cout << "HELP           Proporciona información de Ayuda para los comandos de Windows." << endl;
		cout << "CLS            Borra la pantalla." << endl;
		cout << "ECHO           Muestra mensajes, o activa y desactiva el eco." << endl;
		cout << "MD				Crea un directorio."<< endl;
		cout << "RENAME         Cambia el nombre de uno o más archivos." << endl;
		cout << "REN            Cambia el nombre de uno o más archivos." << endl;
		cout << "TIME           Muestra o establece la hora del sistema."<< endl;
		cout << "TYPE           Muestra el contenido de un archivo de texto." << endl;
		cout << "POPD           Restaura el valor anterior del directorio actual guardado" << endl;
		cout << "PUSHD          Guarda el directorio actual y después lo cambia." << endl;
		cout << "ERASE          Elimina uno o más archivos." << endl;
		cout << "DEL            Elimina uno o más archivos." << endl;
		cout << "CD             Muestra la ruta en la que se encuentra" << endl;
		cout << "VER 			Muestra la version del interprete actual" << endl;
	}

	else if (comandoSDado == "help")
	{
		if (comandoSDado1 == "color")
		{
			cout << "\n" << endl;
			cout << "Configura los colores predeterminados de primer y segundo plano de la consola.\nCOLOR [attr]\nattr \nEspecifica el atributo de color de la salida de consola.\nLos atributos de color están especificados con DOS dígitos hexadecimales(el primero corresponde al segundo plano; el segundo al primer plano).\n" << endl;
		}

		if (comandoSDado1 == "popd")
		{
			cout << "\n" << endl;
			cout << "Cambia al directorio guardado por el comando PUSHD.\n" << endl;
		}

		if (comandoSDado1 == "pushd")
		{
			cout << "\n" << endl;
			cout << "Guarda el directorio actual para que lo use el comando POPD y después\ncambia al directorio especificado.\nPUSHD[ruta | ..]\nruta\nEspecifica el directorio al que hay que cambiar el actual.\n" << endl;
	
		
		}


		if (comandoSDado1 == "cd")
		{
			cout << "\n" << endl;
			cout << "En esta version del interprete cd solo muestra la ruta actual.\n" << endl;


		}
		if (comandoSDado1 == "ver")
		{
			cout << "\n" << endl;
			cout << "Muestra la version del interprete actual /p muestra el nombre del autor\n" << endl;


		}

		else if (comandoSDado1 == "pause")

		{
			cout << "\n" << endl;
			cout << "Suspende el proceso de un programa por lotes y muestra el mensaje\nPresione una tecla para continuar. . ." << endl;
		}

		else if (comandoSDado1 == "del")

		{
			cout << "\n" << endl;
			cout << "Elimina uno o más archivos.\nDEL[/ P][/ Q]atributos]] nombres\n" << endl;
		}

		else if (comandoSDado1 == "erase")

		{
			cout << "\n" << endl;
			cout << "Elimina uno o más archivos.\nERASEL[/ P][/ Q]atributos]] nombres\n" << endl;
		}
		

		else if (comandoSDado1 == "exit")

		{
			cout << "\n" << endl;
			cout << "EXIT\nespecifica que se debe abandonar el archivo por\nlotes actual ." << endl;
		}
		else if (comandoSDado1 == "md")

		{
			cout << "\n" << endl;

			cout << "Crea un directorio en la ruta actual o en la especificada" << endl;
		}


		else if (comandoSDado1 == "help")

		{
			cout << "\n" << endl;
			cout << "Proporciona información de ayuda para los comandos de Windows.\nHELP [comando]\ncomando - muestra información de ayuda del comando especificado." << endl;
		}

		else if (comandoSDado1 == "cls")

		{
			cout << "\n" << endl;

			cout << "Borra la pantalla.\nCLS" << endl;
		}

		else if (comandoSDado1 == "echo")

		{
			cout << "\n" << endl;

			cout << "Muestra mensajes o activa y desactiva el eco del comando.\nECHO[ON | OFF]\nECHO[message]" << endl;
		}

		else if (comandoSDado1 == "rename")

		{
			cout << "\n" << endl;

			cout << "Cambia el nombre de uno o más archivos.\nRENAME [unidad:][ruta]archivo1 archivo2." << endl;
		}

		else if (comandoSDado1 == "ren")

		{
			cout << "\n" << endl;

			cout << "Cambia el nombre de uno o más archivos.\nREN [unidad:][ruta]archivo1 archivo2." << endl;

			
		}

		else if (comandoSDado1 == "type")

		{
			cout << "\n" << endl;

			cout << "Muestra el contenido de uno o más archivos de texto.\nTYPE[unidad:][ruta]archivo" << endl;


		}

		

	}

	else if (comandoSDado == "color")
	{
		
		if (PALABRA[1].length() <= 2 && PALABRA[1].length() > 0)
		{
			numc = PALABRA[1][0];
			nump= PALABRA[1][1];
			parcolor = int(numc);
		
		parcolor = int(nump);
		ClearConsoleToColors(numc, nump);
		}
		else
			cout << "ha sobrepasado " << endl;
	}
	else if (comandoSDado == "pause")
	{
		cout <<"\nPresione una tecla para continuar . . ." << endl;
		_getch();
	}

	else if (comandoSDado == "remove")
	{
		if (PALABRA[1] == "/p")
		{
			cout << "¿deseas eliminar archivo?\presiona y da enter S para confirmar" << endl;
			char confirmacion;
			cin >> confirmacion;
			if (confirmacion == 's' || confirmacion == 'S')
			{
				for (contCom = 0; contCom <= PALABRA[2].length(); contCom++) {
					remover[contCom] = PALABRA[2][contCom];

				}

				remover[contCom + 1] = '\0';
				remove(remover);
			}
		}

		else if (PALABRA[1] == "/q") {
			for (contCom = 0; contCom <= PALABRA[2].length(); contCom++) {
				remover[contCom] = PALABRA[2][contCom];

			}

			remover[contCom + 1] = '\0';
			remove(remover);
		}

		else
		{
			cout << "asegurese de escribir los parametros" << endl;
		}
	
	}

	else if (PALABRA[0] == "PUSHD") {
		*anterior = *CurrentPath;
		string dirName = PALABRA[1];
		_chdir(dirName.c_str());
	}

	else if (PALABRA[0] == "POPD") {
		_chdir(anterior);
	}

	else if (comandoSDado == "cls") {
		rlutil::cls();
				
	}
	else if (comandoSDado == "type") {
		string dirName = PALABRA[1];
		char cadena[1000];
		ifstream fe(dirName.c_str());
		fe.getline(cadena, 1000);
		cout << cadena << endl << endl;

	}

	
	else if (comandoSDado == "rename") {
		int result;
		string name1 = PALABRA[1];
		string name2 = PALABRA[2];
		result = rename(name1.c_str(), name2.c_str());
		if (result == 0)
			puts("Archivo renombrado");

	}
	else if (comandoSDado == "ren") {
		int result;
		string name1 = PALABRA[1];
		string name2 = PALABRA[2];
		result = rename(name1.c_str(), name2.c_str());
		if (result == 0)
			puts("Archivo renombrado");

	}
	else if (comandoSDado == "cd") {
		cout <<"\n"<< PALABRA[1] << endl;
		string dirName = PALABRA[1];
		_chdir(dirName.c_str());
		cout << CurrentPath << endl;
		}
	else if (comandoSDado == "cd/" ) {
		_chdir("C:\\");
	}
	else if (comandoSDado == "cd.." ) {
		_chdir("..");
	}
	else if (comandoSDado == "cd/" ) {
		_chdir("C:\\");
	}
	else if (comandoSDado == "ver" && PALABRA[1]=="/p") {
		cout << "\nversion 1: por Brenda Avila" << endl;;
	}
	else if (comandoSDado == "ver" ) {
		cout << "\nversion 1" << endl;
	}
	
	else if (comandoSDado == "md") {

		string dirName = PALABRA[1];
		_mkdir(dirName.c_str());
		
	}

	else if (comandoSDado == "time") {
		tiempo();
		
	}


	else if (comandoSDado == "echo") {
		if (PALABRA[1] == "on")
		{
			echo = 1;
		}
		else if (PALABRA[1] == "off")
		{
			echo = 0;
		}
		else
		{
			cout << "\n" << endl;

			for (int conpal = 1;conpal < numpal;conpal++)
			{
				cout  << PALABRA[conpal]<<" ";


			}
		}

	}
	else if (comandoSDado == "rename")
	{

		int result;
		string name1 = PALABRA[1];
		string name2 = PALABRA[2];
		result = rename(name1.c_str(), name2.c_str());
		if (result == 0)
			puts("File successfully renamed");
		


	}






	else
	{
		cout << "'"<< PALABRA[0] <<"' no se reconoce como comando" << endl;
	}


		for (int conpal = 0;conpal < numpal;conpal++)
		{
			//cout << "numero de palabra " << conpal + 1 << " :" << PALABRA[conpal] << endl;


		}

		for (int conpal = 0;conpal < l;conpal++)
		{
			cout << "auxiliar " << conpal + 1 << " :" << aux[l] << endl;


		}

	//	printf("..............................\n");


		for (int i = 0;i < cont;i++) {
			cadena[i] = '\0';


		}
		for (int conpal = 0;conpal < l;conpal++)
		{
			cadena[conpal] = '\0';


		}
		for (int conpal = 0;conpal < numpal;conpal++)
		{
			PALABRA[conpal] = "";


		}
		cont = 0;
		numpal = 0;
		l = 0;
		x = 0;
		car = '\0';
		contadorpal = 0;

		//printf("seg parte");
		for (int conpal = 0;conpal < l;conpal++)
		{
		//	cout << "auxiliar " << conpal + 1 << " :" << aux[l] << endl;


		}

		for (int i = 0; i < cont; i++)
		{


			printf("---%c", cadena2[i]);
		}
		printf("\n");

		for (int conpal = 0;conpal < numpal;conpal++)
		{
			//cout << "numero de palabra " << conpal + 1 << " :" << PALABRA[conpal] << endl;


		}

		printf("");

	} while (exit == false);






	return 0;
}

void ClearConsoleToColors(int ForgC, int BackC)
{
	WORD wColor = ((BackC & 0x0F) << 4) + (ForgC & 0x0F);
	HANDLE hStdOut = GetStdHandle(STD_OUTPUT_HANDLE);
	
	COORD coord = { 0, 0 };

	DWORD count;

	CONSOLE_SCREEN_BUFFER_INFO csbi;
	SetConsoleTextAttribute(hStdOut, wColor);
	if (GetConsoleScreenBufferInfo(hStdOut, &csbi))
	{
		FillConsoleOutputCharacter(hStdOut, (TCHAR)32, csbi.dwSize.X * csbi.dwSize.Y, coord, &count);

		FillConsoleOutputAttribute(hStdOut, csbi.wAttributes, csbi.dwSize.X * csbi.dwSize.Y, coord, &count);
		SetConsoleCursorPosition(hStdOut, coord);
	}
	return;
}