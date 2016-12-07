using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Diagnostics;

namespace Interprete{
    public class Shell{
        private static LinkedList<string> words = new LinkedList<string>(); //Genera una lista enlazada doble con valor de tipo string
        private static bool echoBool;//Activar o desactivar ECHO
        private static string myPushD;
        public static void Main(string[] args) {
            char[] text=new char[255];
            int x, z; //Para contadores
            string word; // String para ir agregando a la lista enlazada
            var car = new ConsoleKeyInfo(); //Esta variables es de tipo tecla, lo cual le permite obtener los valores de las teclas presionadas 
            string path = Directory.GetCurrentDirectory();//AppDomain.CurrentDomain.BaseDirectory; Otra forma de obtener el path
            path = path + "\\";
            echoBool = true; //Echo esta activo por defecto
            bool exit;
            do
            {
                x = 0;
                Console.Write(path+">");
                do
                {
                    words.Clear(); //Limpia la lista enlazada
                    car = Console.ReadKey(true); //ReadKey(atributo booleano), readkey lee la tecla presionada, falso (o sin atributo) muestra en consola lo presionado
                    if (car.KeyChar != '\0' && car.Key != ConsoleKey.Enter && car.Key != ConsoleKey.Backspace && car.Key != ConsoleKey.Escape)
                    {
                        Console.Write(car.KeyChar.ToString());
                        text[x++] = car.KeyChar;
                    }
                    else if (car.Key == ConsoleKey.Backspace && x != 0)
                    {
                        if (Console.CursorLeft == 0) //Regresar una linea en caso de que el cursor cambie de linea
                        {
                            Console.SetCursorPosition(Console.BufferWidth-1, --Console.CursorTop);
                            text[--x] = '\0';
                            Console.Write(" \b");
                            Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop);
                        }
                        else
                        {
                            text[--x] = '\0';
                            Console.Write("\b \b");//Con el /b retrocedo un espacio en el texto, el espacio lo sustituye para eliminarlo de pantalla
                                                   //y de nuevo el /b para que el usuario siga insertando su texto
                        }
                    }
                    else if (car.Key == ConsoleKey.Escape)
                    {
                        while (x > 0) {
                            if (Console.CursorLeft == 0)
                            {
                                Console.SetCursorPosition(Console.BufferWidth - 1, --Console.CursorTop);
                                text[--x] = '\0';
                                Console.Write(" \b");
                                Console.SetCursorPosition(Console.BufferWidth - 1, Console.CursorTop);
                            }
                            else
                            {
                                text[--x] = '\0';
                                Console.Write("\b \b");//Con el /b retrocedo un espacio en el texto, el espacio lo sustituye para eliminarlo de pantalla
                                                       //y de nuevo el /b para que el usuario siga insertando su texto
                            }
                        }
                    }
                } while (car.Key != ConsoleKey.Enter);
                bool comillas = false;
                text[x] = '\0';
                for (int y = 0; y < x; y++)
                {
                    z = 0;
                    word = null;
                    if (text[y] == '"')
                        comillas = true;
                    if (text[y] != ' ' && !comillas)
                        while (text[y + z] != ' ' && text[y + z] != '\0')
                            word = word + text[y + z++].ToString();
                    if (comillas) {
                        z++;
                        while (text[y + z] != '"' && text[y + z] != '\0')
                        {
                            word = word + text[y + z++].ToString();
                            comillas = false;
                        }
                    }
                    y += z;
                    if (word != null)
                        words.AddLast(word);
                }
                Console.Write("\n");
                if (words.Count > 0)
                {
                    exit = words.First.Value.ToLower().Equals("exit");  //No es sensible a mayusculas pero solo detecta el "exit" al inicio
                    //exit=words.Contains("exit"); //Este es sensible a mayusculas pero detecta el exit en cualquier punto 
                    x = 1; //reinicia el conteo para el siguiente foreach
                    if (!exit)
                    {
                        switch (words.First.Value.ToLower())
                        {
                            case "dir": //Obligatorio
                                dir(path);
                                break;
                            case "help": //Obligatorio
                                help();
                                Console.WriteLine();
                                break;
                            //Escogidos:
                            case "cls"://1
                                Console.Clear();
                                break;
                            case "ver"://2
                                Console.WriteLine("Version de interprete: LIMA-13300226-1.0.1.2\n");
                                break;
                            case "echo"://3
                                echo(text);
                                break;
                            case "cd"://4
                                cd(ref path, text);
                                break;
                            case "cd.."://4, es una adaptación de cd
                                if (words.Count == 1)
                                {
                                    words.Clear();
                                    words.AddLast("cd");
                                    words.AddLast("..");
                                    cd(ref path);
                                }
                                else
                                    Console.WriteLine("Inserte un comando valido, para mas informacion escriba help\n");
                                break;
                            case "erase":
                                erase(path);//5
                                break;
                            case "del":
                                erase(path);//5 Adaptacion de erase
                                break;
                            case "color"://6
                                color();
                                break;
                            case "copy"://7
                                copy(path);
                                break;
                            case "hostname": //8
                                hostname();
                                break;
                            case "md"://9
                                md(path);
                                break;
                            case "mkdir"://9 Adaptación de md
                                md(path);
                                break;
                            case "pause"://10
                                Console.WriteLine("Presione una tecla para continuar...");
                                Console.ReadKey();
                                Console.Write("\b ");
                                Console.WriteLine();
                                break;
                            case "rd"://11
                                rd(path);
                                break;
                            case "rmdir"://11 alterado
                                rd(path);
                                break;
                            case "ren": //12
                                ren(path);
                                break;
                            case "rename": //12 alterado
                                ren(path);
                                break;
                            case "title": //13
                                title(text);
                                Console.WriteLine();
                                break;
                            case "move": //14
                                move(path);
                                Console.WriteLine();
                                break;
                            case "cmd": //15
                                Process.Start("App.exe");
                                Console.WriteLine();
                                break;
                            case "pushd"://16
                                pushd(ref path);
                                Console.WriteLine();
                                break;
                            case "popd"://17
                                popd(ref path);
                                Console.WriteLine();
                                break;
                            default:
                                Console.WriteLine("Inserte un comando valido, para mas informacion escriba help\n");
                                break;
                        }
                        text = new char[255];
                    }
                }
                else exit = false;
            } while(!exit);
        }
        public static void dir(string path)
        {
            tolow();
            DirectoryInfo directory = new DirectoryInfo(path);
            FileInfo[] files = directory.GetFiles();
            DirectoryInfo[] directories = directory.GetDirectories();
            switch (words.Count)
            {
                case 1:
                    Console.WriteLine("Fecha y hora creacion     -- Dir-- bytes -- nombre");
                    foreach (FileInfo file in files)
                        Console.WriteLine("{0} -- {1} -- {2} -- {3}", file.CreationTime, "no", file.Length, file.Name);
                    foreach (DirectoryInfo d in directories)
                        Console.WriteLine("{0} -- {1} -- {2} -- {3}", d.CreationTime, "si", "     ", d.Name);
                    break;
                case 2:
                    switch (words.First.Next.Value.ToLower())
                    {
                        case "/l":
                            Console.WriteLine("Fecha y hora creacion     -- Dir-- bytes -- nombre");
                            foreach (FileInfo file in files)
                                Console.WriteLine("{0} -- {1} -- {2} -- {3}", file.CreationTime, "no", file.Length, file.Name.ToLower());
                            foreach (DirectoryInfo d in directories)
                                Console.WriteLine("{0} -- {1} -- {2} -- {3}", d.CreationTime, "si", "     ", d.Name.ToLower());
                            break;
                        case "/b":
                            Console.WriteLine("Nombre de archivos y carpetas");
                            foreach (FileInfo file in files)
                                Console.WriteLine("{0}", file.Name);
                            foreach (DirectoryInfo d in directories)
                                Console.WriteLine("{0}", d.Name);
                            break;
                        case "/p":
                            Console.WriteLine("Fecha y hora creacion     -- Dir-- bytes -- nombre");
                            int anothercount = 1;
                            foreach (FileInfo file in files)
                            {
                                Console.WriteLine("{0} -- {1} -- {2} -- {3}", file.CreationTime, "no", file.Length, file.Name);
                                anothercount++;
                                if (anothercount == 20)
                                {
                                    Console.WriteLine("Presione una tecla para continuar");
                                    Console.ReadKey();
                                    anothercount = 1;
                                }
                            }
                            foreach (DirectoryInfo d in directories)
                            {
                                Console.WriteLine("{0} -- {1} -- {2} -- {3}", d.CreationTime, "si", "     ", d.Name);
                                anothercount++;
                                if (anothercount == 20)
                                {
                                    Console.WriteLine("Presione una tecla para continuar");
                                    Console.ReadKey();
                                    anothercount = 1;
                                }
                            }
                            break;
                        default:
                            Console.Write("Revise el parametro introducido\n");
                            break;
                    }
                    break;
                case 3:
                    string comb = words.First.Next.Value.ToLower() + words.First.Next.Next.Value.ToLower();
                    if (comb == "/l/b" || comb == "/b/l")
                    {
                        Console.WriteLine("Nombre de archivos y carpetas");
                        foreach (FileInfo file in files)
                            Console.WriteLine("{0}", file.Name.ToLower());
                        foreach (DirectoryInfo d in directories)
                            Console.WriteLine("{0}", d.Name.ToLower());
                    }
                    else if (comb == "/l/p" || comb == "/p/l")
                    {
                        int anothercount = 1;
                        foreach (FileInfo file in files)
                        {
                            Console.WriteLine("{0} -- {1} -- {2} -- {3}", file.CreationTime, "no", file.Length, file.Name.ToLower());
                            anothercount++;
                            if (anothercount == 20)
                            {
                                Console.WriteLine("Presione una tecla para continuar");
                                Console.ReadKey();
                                anothercount = 1;
                            }
                        }
                        foreach (DirectoryInfo d in directories)
                        {
                            Console.WriteLine("{0} -- {1} -- {2} -- {3}", d.CreationTime, "si", "     ", d.Name.ToLower());
                            anothercount++;
                            if (anothercount == 20)
                            {
                                Console.WriteLine("Presione una tecla para continuar");
                                Console.ReadKey();
                                anothercount = 1;
                            }
                        }
                    }
                    else if (comb == "/b/p" || comb == "/p/b")
                    {
                        Console.WriteLine("Nombre de archivos y carpetas");
                        int anothercount = 1;
                        foreach (FileInfo file in files)
                        {
                            Console.WriteLine("{0}", file.Name);
                            anothercount++;
                            if (anothercount == 20)
                            {
                                Console.WriteLine("Presione una tecla para continuar");
                                Console.ReadKey();
                                anothercount = 1;
                            }
                        }
                        foreach (DirectoryInfo d in directories)
                        {
                            Console.WriteLine("{0}", d.Name);
                            anothercount++;
                            if (anothercount == 20)
                            {
                                Console.WriteLine("Presione una tecla para continuar");
                                Console.ReadKey();
                                anothercount = 1;
                            }
                        }
                    }
                    else
                        Console.Write("Revise los parametros introducidos\n");
                    break;
                case 4:
                    string comb2 = words.First.Next.Value.ToLower() + words.First.Next.Next.Value.ToLower() + words.First.Next.Next.Next.Value.ToLower();
                    if (comb2 == "/l/b/p" || comb2 == "/l/p/b" || comb2 == "/b/l/p" || comb2 == "/b/p/l" || comb2 == "/p/b/l" || comb2 == "/p/l/b")
                    {
                        Console.WriteLine("Nombre de archivos y carpetas");
                        int anothercount = 1;
                        foreach (FileInfo file in files)
                        {
                            Console.WriteLine("{0}", file.Name.ToLower());
                            anothercount++;
                            if (anothercount == 20)
                            {
                                Console.WriteLine("Presione una tecla para continuar");
                                Console.ReadKey();
                                anothercount = 1;
                            }
                        }
                        foreach (DirectoryInfo d in directories)
                        {
                            Console.WriteLine("{0}", d.Name.ToLower());
                            anothercount++;
                            if (anothercount == 20)
                            {
                                Console.WriteLine("Presione una tecla para continuar");
                                Console.ReadKey();
                                anothercount = 1;
                            }
                        }
                    }
                    else
                        Console.Write("Revise los parametros introducidos, para mas informacion vea help dir\n");
                    break;
                default:
                    Console.WriteLine("Ingrese parametros validos, para mas informacion vea help dir\n");
                    break;
            }
            Console.WriteLine();
        } //Funcion para obtener contenido de directorio
        public static void cd(ref string path, char[] text = null)
        {
            tolow();
            if (words.Count == 2 && words.First.Next.Value == "..") {
                string[] data = path.Split('\\');
                path = "";
                int length = data.Length - 2;
                for (int count = 0; count < length; count++)
                    path = path + data[count] + "\\";
            }
            else if(words.Count>1)
            {
                string newPath = complement(text, "cd");
                if (Directory.Exists(path + newPath))
                    path = path + newPath + "\\";
                else
                    Console.WriteLine("Ingrese un directorio valido\n");
            }
            else
                Console.WriteLine("Formato incorrecto, revise la funcion del comando con help cd\n");
        } //Desplazarme entre directorios
        public static void echo(char[] text)
        {
            if (words.Count > 1 && words.First.Next.Value.ToLower() != "on" && words.First.Next.Value.ToLower() != "off" && echoBool)
            {
                string line = complement(text, "echo");
                Console.WriteLine(line);
            }
            else if (words.Count == 2 && words.First.Next.Value.ToLower() == "on")
            {
                echoBool = true;
                Console.WriteLine("ECHO ON");
            }
            else if (words.Count == 2 && words.First.Next.Value.ToLower() == "off")
            {
                echoBool = false;
                Console.WriteLine("ECHO OFF");
            }
            else
            {
                if (echoBool)
                    Console.WriteLine("ECHO ON");
                else
                    Console.WriteLine("ECHO OFF");
            }
            Console.WriteLine();
        } //Escribir en pantalla lo mismo que el usuario
        public static void erase(string path)
        {
            tolow();
            if (words.Contains("/p"))
            {
                if (words.First.Value == "del")
                    words.Remove("del");
                else
                    words.Remove("erase");
                words.Remove("/p");
                foreach (string word in words)
                {
                    if (File.Exists(path + word))
                        while (true)
                        {
                            Console.WriteLine("Seguro que desea eliminar " + word +" [Y/N]");
                            ConsoleKey aceptar = Console.ReadKey().Key;
                            Console.WriteLine();
                            if (aceptar == ConsoleKey.Y)
                            {
                                File.Delete(path + word);
                                break;
                            }
                            else if (aceptar == ConsoleKey.N)
                                break;
                        }
                    else
                        Console.WriteLine("No existe el archivo " + word);
                }
            }
            else
            {
                if (words.First.Value == "del")
                    words.Remove("del");
                else
                    words.Remove("erase");
                foreach (string word in words)
                {
                    if (File.Exists(path + word))
                        File.Delete(path + word);
                    else
                        Console.WriteLine("No existe el archivo " + word);
                }
            }
            Console.WriteLine();
        } //eliminar archivos
        public static void color()
        {
            tolow();
            if (words.Count == 2)
            {
                char[] code = words.First.Next.Value.ToCharArray();
                ConsoleColor bckgnd = ConsoleColor.Black;
                ConsoleColor fntgnd = ConsoleColor.White;
                if (code.Length == 1)
                {
                    switch (code[0].ToString().ToLower())
                    {
                        case "0":
                            fntgnd = ConsoleColor.Black;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "1":
                            fntgnd = ConsoleColor.DarkBlue;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "2":
                            fntgnd = ConsoleColor.DarkGreen;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "3":
                            fntgnd = ConsoleColor.DarkCyan;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "4":
                            fntgnd = ConsoleColor.DarkRed;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "5":
                            fntgnd = ConsoleColor.DarkMagenta;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "6":
                            fntgnd = ConsoleColor.DarkYellow;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "7":
                            fntgnd = ConsoleColor.Gray;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "8":
                            fntgnd = ConsoleColor.DarkGray;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "9":
                            fntgnd = ConsoleColor.Blue;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "a":
                            fntgnd = ConsoleColor.Green;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "b":
                            fntgnd = ConsoleColor.Cyan;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "c":
                            fntgnd = ConsoleColor.Red;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "d":
                            fntgnd = ConsoleColor.Magenta;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "e":
                            fntgnd = ConsoleColor.Yellow;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        case "f":
                            fntgnd = ConsoleColor.Black;
                            if (fntgnd != bckgnd)
                                Console.ForegroundColor = fntgnd;
                            break;
                        default:
                            break;
                    }
                }
                else if (code.Length == 2)
                {
                    ConsoleColor temp = Console.ForegroundColor;
                    bool check = true;
                    if (code[0] != code[1])
                        switch (code[0].ToString().ToLower())
                        {
                            case "0":
                                fntgnd = ConsoleColor.Black;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "1":
                                fntgnd = ConsoleColor.DarkBlue;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "2":
                                fntgnd = ConsoleColor.DarkGreen;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "3":
                                fntgnd = ConsoleColor.DarkCyan;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "4":
                                fntgnd = ConsoleColor.DarkRed;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "5":
                                fntgnd = ConsoleColor.DarkMagenta;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "6":
                                fntgnd = ConsoleColor.DarkYellow;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "7":
                                fntgnd = ConsoleColor.Gray;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "8":
                                fntgnd = ConsoleColor.DarkGray;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "9":
                                fntgnd = ConsoleColor.Blue;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "a":
                                fntgnd = ConsoleColor.Green;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "b":
                                fntgnd = ConsoleColor.Cyan;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "c":
                                fntgnd = ConsoleColor.Red;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "d":
                                fntgnd = ConsoleColor.Magenta;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "e":
                                fntgnd = ConsoleColor.Yellow;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            case "f":
                                fntgnd = ConsoleColor.Black;
                                if (fntgnd != bckgnd)
                                    Console.ForegroundColor = fntgnd;
                                break;
                            default:
                                Console.WriteLine("Vea el comando \"help color\" para mas informacion sobre los colores dispoinbles");
                                check = false;
                                break;
                        }
                    if (code[0] != code[1] && check)
                        switch (code[1].ToString().ToLower())
                        {
                            case "0":
                                bckgnd = ConsoleColor.Black;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "1":
                                bckgnd = ConsoleColor.DarkBlue;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "2":
                                bckgnd = ConsoleColor.DarkGreen;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "3":
                                bckgnd = ConsoleColor.DarkCyan;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "4":
                                bckgnd = ConsoleColor.DarkRed;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "5":
                                bckgnd = ConsoleColor.DarkMagenta;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "6":
                                bckgnd = ConsoleColor.DarkYellow;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "7":
                                bckgnd = ConsoleColor.Gray;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "8":
                                bckgnd = ConsoleColor.DarkGray;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "9":
                                bckgnd = ConsoleColor.Blue;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "a":
                                bckgnd = ConsoleColor.Green;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "b":
                                bckgnd = ConsoleColor.Cyan;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "c":
                                bckgnd = ConsoleColor.Red;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "d":
                                bckgnd = ConsoleColor.Magenta;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "e":
                                bckgnd = ConsoleColor.Yellow;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            case "f":
                                bckgnd = ConsoleColor.Black;
                                if (fntgnd != bckgnd)
                                    Console.BackgroundColor = bckgnd;
                                break;
                            default:
                                Console.ForegroundColor = temp;
                                Console.WriteLine("Vea el comando \"help color\" para mas informacion sobre los colores dispoinbles");
                                break;
                        }
                    else Console.WriteLine("Revise el comando introducido, para mas información veas help color");
                }
                else Console.WriteLine("Revise el comando introducido, para mas información veas help color");
            }
            else if (words.Count == 1)
                Console.ResetColor();
            else
                Console.WriteLine("Revise el comando introducido, para mas información veas help color");
            Console.WriteLine();
        } //cambiar el color de la consola
        public static void copy(string path)
        {
            tolow();
            if (words.Contains("/y"))
            {
                words.Remove("copy");
                words.Remove("/y");
                char[] orig = words.First.Value.ToCharArray();
                char[] dest = words.First.Next.Value.ToCharArray();
                if (File.Exists(path + words.First.Value))
                    if (char.IsLetter(dest[0]) && dest[1] == ':' && dest[2] == '\\' && char.IsLetter(orig[0]) && orig[1] == ':' && orig[2] == '\\')
                        try
                        {
                            File.Copy(words.First.Value, words.First.Next.Value,true);
                        }
                        catch
                        {
                            File.Copy(words.First.Value, words.First.Next.Value + words.First.Value, true);
                        }
                    else if (char.IsLetter(dest[0]) && dest[1] == ':' && dest[2] == '\\')
                        try
                        {
                            File.Copy(path + words.First.Value, words.First.Next.Value, true);
                        }
                        catch
                        {
                            File.Copy(path + words.First.Value, words.First.Next.Value + words.First.Value, true);
                        }
                    else if (char.IsLetter(orig[0]) && orig[1] == ':' && orig[2] == '\\')
                        try
                        {
                            File.Copy(words.First.Value, path + words.First.Next.Value, true);
                        }
                        catch
                        {
                            File.Copy(words.First.Value, path + words.First.Next.Value + words.First.Value, true);
                        }
                    else
                        try
                        {
                            File.Copy(path + words.First.Value, path + words.First.Next.Value, true);
                        }
                        catch
                        {
                            File.Copy(path + words.First.Value, path + words.First.Next.Value + words.First.Value, true);
                        }
                else
                    Console.WriteLine("El archivo de origen no existe");
            }
            else if (words.Contains("/-y"))
            {
                words.Remove("copy");
                words.Remove("/-y");
                char[] dest = words.First.Next.Value.ToCharArray();
                char[] orig = words.First.Value.ToCharArray();
                if (char.IsLetter(orig[0]) && orig[1] == ':' && orig[2] == '\\')
                {
                    if (File.Exists(words.First.Value))
                    {
                        if (File.Exists(path + words.First.Next.Value))
                        {
                            while (true)
                            {
                                Console.WriteLine("El archivo de destino ya existe, desea sobreescribirlo [Y/N]");
                                ConsoleKey aceptar = Console.ReadKey().Key;
                                Console.WriteLine();
                                if (aceptar == ConsoleKey.Y)
                                {
                                    if (char.IsLetter(dest[0]) && dest[1] == ':' && dest[2] == '\\')
                                        try
                                        {
                                            File.Copy(words.First.Value, words.First.Next.Value, true);
                                        }
                                        catch
                                        {
                                            File.Copy(words.First.Value, words.First.Next.Value + words.First.Value, true);
                                        }
                                    else
                                        try
                                        {
                                            File.Copy(words.First.Value, path + words.First.Next.Value, true);
                                        }
                                        catch
                                        {
                                            File.Copy(words.First.Value, path + words.First.Next.Value + words.First.Value, true);
                                        }
                                }
                                else if (aceptar == ConsoleKey.N)
                                    break;
                            }

                        }
                    }
                    else
                        Console.WriteLine("El archivo de origen no existe");
                }
                else
                {
                    if (File.Exists(path + words.First.Value))
                    {
                        if (File.Exists(path + words.First.Next.Value))
                        {
                            while (true)
                            {
                                Console.WriteLine("El archivo de destino ya existe, desea sobreescribirlo [Y/N]");
                                ConsoleKey aceptar = Console.ReadKey().Key;
                                Console.WriteLine();
                                if (aceptar == ConsoleKey.Y)
                                {
                                    if (char.IsLetter(dest[0]) && dest[1] == ':' && dest[2] == '\\')
                                        try
                                        {
                                            File.Copy(path + words.First.Value, words.First.Next.Value, true);
                                        }
                                        catch
                                        {
                                            File.Copy(path + words.First.Value, words.First.Next.Value + words.First.Value, true);
                                        }
                                    else
                                        try
                                        {
                                            File.Copy(path + words.First.Value, path + words.First.Next.Value, true);
                                        }
                                        catch
                                        {
                                            File.Copy(path + words.First.Value, path + words.First.Next.Value + words.First.Value, true);
                                        }
                                    break;
                                }
                                else if (aceptar == ConsoleKey.N)
                                    break;
                            }

                        }
                    }
                    else
                        Console.WriteLine("El archivo de origen no existe");
                }
            }
            else
            {
                words.Remove("copy");
                char[] orig = words.First.Value.ToCharArray();
                char[] dest = words.First.Next.Value.ToCharArray();
                if (File.Exists(path + words.First.Value))
                    if (char.IsLetter(dest[0]) && dest[1] == ':' && dest[2] == '\\' && char.IsLetter(orig[0]) && orig[1] == ':' && orig[2] == '\\')
                        try {
                            File.Copy(words.First.Value, words.First.Next.Value);
                        }
                        catch
                        {
                            File.Copy(words.First.Value, words.First.Next.Value+ words.First.Value);
                        }
                    else if (char.IsLetter(dest[0]) && dest[1] == ':' && dest[2] == '\\')
                        try
                        {
                            File.Copy(path + words.First.Value, words.First.Next.Value);
                        }
                        catch
                        {
                            File.Copy(path + words.First.Value, words.First.Next.Value + words.First.Value);
                        }
                    else if (char.IsLetter(orig[0]) && orig[1] == ':' && orig[2] == '\\')
                        try {
                            File.Copy(words.First.Value, path + words.First.Next.Value);
                        }
                        catch
                        {
                            File.Copy(words.First.Value, path + words.First.Next.Value + words.First.Value);
                        }
                    else
                        try {
                            File.Copy(path + words.First.Value, path + words.First.Next.Value);
                        }
                        catch
                        {
                            File.Copy(path + words.First.Value, path + words.First.Next.Value + words.First.Value);
                        }
                else
                    Console.WriteLine("El archivo de origen no existe");
            }
        } //copiar archivos
        public static void hostname()
        {
            if (words.Count == 1)
            {
                string host = Dns.GetHostName();
                Console.WriteLine(host);
            }
            else
                Console.WriteLine("El comando no tiene parametros, revise help hostname");
            Console.WriteLine();
        } //Obtener el nombre del host
        public static void md(string path)
        {
            tolow();
            if(words.First.Value=="md")
                words.Remove("md");
            else
                words.Remove("mkdir");
            char[] orig = words.First.Value.ToCharArray();
            if(words.Count==1)
                if (char.IsLetter(orig[0]) && orig[1] == ':' && orig[2] == '\\')
                    Directory.CreateDirectory(words.First.Value);
                else
                    Directory.CreateDirectory(path + words.First.Value);
            else
                Console.WriteLine("El comando no tiene parametros, revise help md");
            Console.WriteLine();
        } //Crear directorios
        public static void rd(string path)
        {
            tolow();
            if (words.First.Value == "rd")
                words.Remove("rd");
            else
                words.Remove("rmdir");
            char[] orig = words.First.Value.ToCharArray();
            if (words.Count == 1)
                if (char.IsLetter(orig[0]) && orig[1] == ':' && orig[2] == '\\' && Directory.Exists(words.First.Value))
                    Directory.Delete(words.First.Value,true);
                else if(Directory.Exists(path + words.First.Value))
                    Directory.Delete(path + words.First.Value, true);
                else
                    Console.WriteLine("Ingrese un directorio vaido");
            else
                Console.WriteLine("El comando no tiene parametros, revise help rd");
            Console.WriteLine();
        } //Eliminar directorios
        public static void title(char[] text)
        {
            string myTitle = complement(text, "title");
            Console.Title= myTitle;
            Console.WriteLine();
        } //Cambia el titulo de la consola
        public static void move(string path)
        {
            words.Remove("move");
            words.AddFirst("copy");
            copy(path);
            words.AddFirst("erase");
            words.RemoveLast();
            erase(path);
        } //Mover archivos
        public static void ren(string path)
        {
            if (words.Count == 3)
            {
                words.RemoveFirst();
                words.AddFirst("move");
                move(path);
            }
            else
                Console.WriteLine("Revise el comando introducido, para mas información help ren");
        } //Renombrar archivos
        public static void pushd(ref string path)
        {
            myPushD = path;
            words.RemoveFirst();
            if (words.Count == 0)
                Console.WriteLine("Directorio actual guardado");
            else if (words.Count == 1) {
                char[] dest = words.First.Value.ToCharArray();
                if (char.IsLetter(dest[0]) && dest[1] == ':' && dest[2] == '\\')
                {
                    if (Directory.Exists(words.First.Value))
                        path = words.First.Value;
                    else
                        Console.WriteLine("Directorio de destino no valido, pero actual guardado para mas información vea help pushd");
                }
                else
                {
                    if (Directory.Exists(path + words.First.Value))
                        path = path + words.First.Value;
                    else
                        Console.WriteLine("Directorio de destino no valido, pero actual guardado para mas información vea help pushd");
                }
            }
            else
                Console.WriteLine("Error, para mas información vea help pushd");
        } //Cambiarme de path y guardar el actual
        public static void popd(ref string path)
        {
            words.RemoveFirst();
            if (words.Count == 0) {
                path = myPushD;
            }
            else Console.WriteLine("Error, para mas información vea help popd");
        } //Regresar al path guardado con pushd
        public static void help()
        {
            string myString;
            if (words.Count > 1) {
                words.RemoveFirst();
                switch (words.First.Value.ToLower())
                {
                    case "dir":
                        myString = Properties.Resources.dir;
                        Console.WriteLine(myString);
                        break;
                    case "help":
                        myString = Properties.Resources.help;
                        Console.WriteLine(myString);
                        break;
                    case "cls":
                        myString = Properties.Resources.cls;
                        Console.WriteLine(myString);
                        break;
                    case "ver":
                        myString = Properties.Resources.ver;
                        Console.WriteLine(myString);
                        break;
                    case "echo":
                        myString = Properties.Resources.echo;
                        Console.WriteLine(myString);
                        break;
                    case "cd":
                        myString = Properties.Resources.cd;
                        Console.WriteLine(myString);
                        break;
                    case "erase":
                        myString = Properties.Resources.erase;
                        Console.WriteLine(myString);
                        break;
                    case "del":
                        myString = Properties.Resources.erase;
                        Console.WriteLine(myString);
                        break;
                    case "color":
                        myString = Properties.Resources.color;
                        Console.WriteLine(myString);
                        break;
                    case "copy":
                        myString = Properties.Resources.copy;
                        Console.WriteLine(myString);
                        break;
                    case "hostname":
                        myString = Properties.Resources.hostname;
                        Console.WriteLine(myString);
                        break;
                    case "md":
                        myString = Properties.Resources.md;
                        Console.WriteLine(myString);
                        break;
                    case "mkdir":
                        myString = Properties.Resources.md;
                        Console.WriteLine(myString);
                        break;
                    case "pause":
                        myString = Properties.Resources.pause;
                        Console.WriteLine(myString);
                        break;
                    case "rd":
                        myString = Properties.Resources.rd;
                        Console.WriteLine(myString);
                        break;
                    case "rmdir":
                        myString = Properties.Resources.rd;
                        Console.WriteLine(myString);
                        break;
                    case "ren":
                        myString = Properties.Resources.ren;
                        Console.WriteLine(myString);
                        break;
                    case "rename":
                        myString = Properties.Resources.ren;
                        Console.WriteLine(myString);
                        break;
                    case "title":
                        myString = Properties.Resources.title;
                        Console.WriteLine(myString);
                        break;
                    case "move":
                        myString = Properties.Resources.move;
                        Console.WriteLine(myString);
                        break;
                    case "cmd":
                        myString = Properties.Resources.cmd;
                        Console.WriteLine(myString);
                        break;
                    case "pushd":
                        myString = Properties.Resources.pushd;
                        Console.WriteLine(myString);
                        break;
                    case "popd":
                        myString = Properties.Resources.popd;
                        Console.WriteLine(myString);
                        break;
                    default:
                        Console.WriteLine("Help no admite este comando");
                        break;
                }
            }
            else
            {
                myString = Properties.Resources.generalHelp;
                Console.WriteLine(myString);
            }
        } //Obtener ayuda, depende de archivos txt externos
        public static string complement(char[] array, string command)
        {
            char[] not = { '\0' };
            string exit = new string(array);
            exit = exit.Remove(0, exit.IndexOf(command + " ") + command.Length + 1);
            exit = exit.Trim(not);
            return exit;
        } //Me regresa el string despues del codigo introducido
        public static void tolow()
        {
            LinkedList<string> temp = new LinkedList<string>();
            foreach(string x in words)
                temp.AddLast(x.ToLower());
            words.Clear();
            words = temp;
        } //Convierte todo a minusculas para su analisis posterior

    }
}
//Copyright (c) Luis Ivan Morett Arévalo - 2016. All Rights Reserved.