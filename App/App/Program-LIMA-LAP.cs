using System;
using System.Collections.Generic;

namespace App {
    class Program {
        static void Main(string[] args) {
            char[] text=new char[255];
            int x, z; //Para contadores
            LinkedList<string> words = new LinkedList<string>(); //Genera una lista enlazada doble con valor de tipo string
            string word; // String para ir agregando a la lista enlazada
            var car = new ConsoleKeyInfo(); //Esta variables es de tipo tecla, lo cual le permite obtener los valores de las teclas presionadas 
            bool exit;
            do
            {
                Console.Clear();
                x = 0;
                do
                {
                    words.Clear(); //Limpia la lista enlazada
                    car = Console.ReadKey(true); //ReadKey(atributo booleano), readkey lee la tecla presionada, falso (o sin atributo) muestra en consola lo presionado
                    if (car.KeyChar != '\0' && car.Key != ConsoleKey.Enter && car.Key != ConsoleKey.Backspace && car.Key != ConsoleKey.Escape)
                    {
                        Console.Write(car.KeyChar.ToString());
                        text[x++] = car.KeyChar;
                    }
                    else if (car.Key == ConsoleKey.Backspace)
                    {
                        text[--x] = '\0';
                        Console.Clear();
                        Console.Write(text, 0, x); //Escribe el arreglo de caracteres desde la posición cero hasta la x
                    }
                } while (car.Key != ConsoleKey.Enter);
                text[x] = '\0';
                for (int y = 0; y < x; y++)
                {
                    z = 0;
                    word = null;
                    if (text[y] != ' ')
                        while (text[y + z] != ' ' && text[y + z] != '\0')
                            word = word + text[y + z++].ToString();
                    y += z;
                    if (word != null)
                        words.AddLast(word);
                }
                Console.Write("\n");
                
                exit = words.Contains("exit"); ;//words.First.Value.ToLower().Equals("exit");
                x = 1; //reinicia el conteo para el siguiente foreach
                if (!exit) {
                    foreach (string w in words)
                        Console.Write("\nPalabra " + x++.ToString() + ": " + w);
                  Console.Write("\nPresione cualquier tecla para reiniciar");
                    car = Console.ReadKey();
            }
            } while(!exit);
        }
        
    }

}