using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace compiler
{
    class Program
    {
        private static LinkedList<tokens> myTokens;
        static void Main(string[] args)
        {
            LinkedList<lineas> allCode = new LinkedList<lineas>();
            myTokens = new LinkedList<tokens>();
            int conteo = 0;
            string path = 
                @"D:\OneDrive\CETI\7 - Septimo Semestre\Compiladores e Interpretes\Tercer Parcial\compilador\analyzer\Lex\myLex.lex";
            StreamReader sr = new StreamReader(path);
            string code = sr.ReadToEnd();
            string[] codelines = Regex.Split(code, @"\n |\r |\n\r |\r\n"); //Separa el codigo en lineas
            foreach (string x in codelines)
            {
                lineas line = new lineas();
                line.linea = x;
                line.noLinea = ++conteo;
                allCode.AddLast(line);
            }
            
            foreach (lineas x in allCode)
            {
                Match check = Regex.Match(x.linea,
                    @"(\u00B4((\\\u00B4)|([^\u00B4]))*\u00B4)|(([A-Za-z]+[A-Za-z-_\d]*[A-Za-z\d])|[A-Za-z])|([\(\)\{\}\[\].,])|([\.=\+\-/*])|(>=|<=|>|<|==|!|&&|\|\|)|(\d*\.?\d+)|.");
                while (check.Success)
                {
                    tokens myToken = new tokens();
                    myToken.id=identifier(check.Value);
                    myToken.myValue= string.Copy(check.Value);
                    myToken.noLinea =x.noLinea;
                    myTokens.AddLast(myToken);
                    check = check.NextMatch();
                }
            }
            foreach(tokens x in myTokens)
            {
                if (x.id == "ERROR")
                {
                    Console.WriteLine("Error en la linea: " + x.noLinea.ToString() + "\tError cerca de: " + x.myValue);
                }
            }
            Console.ReadKey();
        }

        /*
         * Función para identifica que tipo de token es
         * dependiendo del valor
         */
        public static string identifier(string word)
        {
            if (Regex.IsMatch(word, @"(\u00B4((\\\u00B4)|([^\u00B4]))*\u00B4)"))
            {
                return "string";
            }
            if (Regex.IsMatch(word, @"escanear"))
            {
                return "read";
            }
            if (Regex.IsMatch(word, @"estampa"))
            {
                return "write";
            }
            if (Regex.IsMatch(word, @"zy"))
            {
                return "if";
            }
            if (Regex.IsMatch(word, @"tons"))
            {
                return "else";
            }
            if (Regex.IsMatch(word, @"mentre"))
            {
                return "while";
            }
            if (Regex.IsMatch(word, @"to"))
            {
                return "do";
            }
            if (Regex.IsMatch(word, @"por"))
            {
                return "for";
            }
            if (Regex.IsMatch(word, @"intel"))
            {
                return "integer";
            }
            if (Regex.IsMatch(word, @"flot"))
            {
                return "float";
            }
            if (Regex.IsMatch(word, @"car"))
            {
                return "char";
            }
            if (Regex.IsMatch(word, @"([A-Za-z]+[A-Za-z-_\d]*[A-Za-z\d])|[A-Za-z]"))
            {
                return "UserDefined";
            }
            if (Regex.IsMatch(word, @"\."))
            {
                return "EOL";
            }
            if (Regex.IsMatch(word, @"\d+"))
            {
                return "Entero";
            }
            if (Regex.IsMatch(word, @"\d*\.?\d+"))
            {
                return "Flotante";
            }
            if (Regex.IsMatch(word, @"\+"))
            {
                return "OpAdd";
            }
            if (Regex.IsMatch(word, @"-"))
            {
                return "OpMinus";
            }
            if (Regex.IsMatch(word, @"\*"))
            {
                return "OpMyl";
            }
            if (Regex.IsMatch(word, @"\/"))
            {
                return "OpDiv";
            }
            if (Regex.IsMatch(word, @"\("))
            {
                return "LeftPar";
            }
            if (Regex.IsMatch(word, @"\)"))
            {
                return "RightPar";
            }
            if (Regex.IsMatch(word, @"\{"))
            {
                return "LeftBrack";
            }
            if (Regex.IsMatch(word, @"\}"))
            {
                return "RightBrack";
            }
            if (Regex.IsMatch(word, @"&&"))
            {
                return "OpAnd";
            }
            if (Regex.IsMatch(word, @"\|\|"))
            {
                return "OpOr";
            }
            if (Regex.IsMatch(word, @"!="))
            {
                return "OpNotEqu";
            }
            
            if (Regex.IsMatch(word, @"=="))
            {
                return "OpEqu";
            }
            if (Regex.IsMatch(word, @">="))
            {
                return "OpGtEq";
            }
            if (Regex.IsMatch(word, @"<="))
            {
                return "OpLtEq";
            }
            if (Regex.IsMatch(word, @"="))
            {
                return "OpAssign";
            }
            if (Regex.IsMatch(word, @"!"))
            {
                return "OpNot";
            }
            if (Regex.IsMatch(word, @"<"))
            {
                return "OpLt";
            }
            if (Regex.IsMatch(word, @">"))
            {
                return "OpGt";
            }
            if (Regex.IsMatch(word, @"\s"))
            {
                return "WhiteSpace";
            }
            return "ERROR";
        }

        /*
         * Estructura para separar cada linea
         * Y para obtener su numero
         */
        struct lineas
        {
            public int noLinea;
            public string linea;
        }

        /*
         * Estructura para almacenar la información
         * de los distintos tokens del codigo
         */
        struct tokens
        {
            public string id;
            public string myValue;
            public int noLinea;
        }


    }
}
