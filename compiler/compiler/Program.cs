using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace compiler
{
    class Program
    {
        private static LinkedList<tokens> myTokens;
        private static int level;
        private static bool errorFlag;

        static void Main(string[] args)
        {
            LinkedList<lineas> allCode = new LinkedList<lineas>();
            myTokens = new LinkedList<tokens>();
            LinkedList<tokens> newCopy;
            int conteo = 0;
            level = 0;
            errorFlag = false;
            string path =@"Pruebas\Lenguaje inventado-prueba1.txt";
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
                    @"(\u00B4((\\\u00B4)|([^\u00B4]))*\u00B4)|(([A-Za-z]+[A-Za-z-_\d]*[A-Za-z\d])|[A-Za-z])|([\(\)\{\}\[\].,])|([\.=\+\-/*])|(&|>=|<=|>|<|==|!|&&|\|\|)|(\d*\.?\d+)|\'.*\'|.");
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
                else
                {
                    Console.WriteLine(x.id + " - " + x.myValue + " - " + x.noLinea.ToString());
                }
            }
            newCopy = new LinkedList<tokens>(myTokens);
            myTokens.Clear();
            foreach (tokens x in newCopy)
            {
                if (x.id != "ERROR" && x.id != "Comment" && x.id != "WhiteSpace")
                {
                    myTokens.AddLast(x);
                }
            }
            newCopy.Clear();
            newCopy = new LinkedList<tokens>(myTokens);
            Console.ReadKey();
        }

        /*
         * Función para identifica que tipo de token es
         * dependiendo del valor
         */
        public static string identifier(string word)
        {
            if (Regex.IsMatch(word, @"\'.*\'"))
            {
                return "Comment";
            }
            if (Regex.IsMatch(word, @"(\u00B4((\\\u00B4)|([^\u00B4]))*\u00B4)"))
            {
                return "string";
            }
            if (word == "escanear")
            {
                return "read";
            }
            if (word == @"estampa")
            {
                return "write";
            }
            if (word == "zy")
            {
                return "if";
            }
            if (word == "tons")
            {
                return "else";
            }
            if (word == "mentre")
            {
                return "while";
            }
            if (word == "to")
            {
                return "do";
            }
            if (word == "por")
            {
                return "for";
            }
            if (word == "intel")
            {
                return "integer";
            }
            if (word == "flot")
            {
                return "float";
            }
            if (word == "car")
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
            if (Regex.IsMatch(word, @","))
            {
                return "COMMA";
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
            if (Regex.IsMatch(word, @"&"))
            {
                return "Direction";
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

        public static bool program()
        {
            return functionList() || (varDecl() && program());
        }

        public static bool functionList()
        {
            return function() || (function() && functionList());
        }

        public static bool function()
        {
            int count = 0;
            bool check = myTokens.First.Value.id == "integer";
            count += 1;
            check = check && (myTokens.First.Value.id == "UserDefined");
            count += 1;
            check = check && (myTokens.First.Value.id == "LeftBrack");
            count += 1;
            if (check)
            {
                while (count > 0)
                {
                    myTokens.RemoveFirst();
                    count -= 1;
                }
                check = check && listDeclVar();
                check = check && (myTokens.First.Value.id == "RightBrack");
                count += 1;
                check = check && (myTokens.First.Value.id == "LeftPar");
                count += 1;
                while (count > 0)
                {
                    myTokens.RemoveFirst();
                    count -= 1;
                }
            }
            //myTokens.First.Value.id;
            return check;
        }

        public static bool listDeclVar()
        {
            bool check;
            check = varDecl();
            if (myTokens.First.Value.id == "COMMA")
            {
                myTokens.RemoveFirst();
                check = check && listDeclVar();
            }
            return check;
        }
        public static bool varDecl()
        {
            return true;
        }
/*
        public static void syntax()
        {
            
            while (myTokens.Count>0)
            {
                tokens actual = myTokens.First.Value;
                myTokens.RemoveFirst();
                FirstTokenIdentifier(actual);
            }
        }

        private static void FirstTokenIdentifier(tokens myToken)
        {
            if (level == 0){
                if (myToken.id == "integer") {

                }
                else if (myToken.id == "float" || myToken.id == "car") { }
                else {
                    Console.WriteLine("Error near line: " + myToken.noLinea);
                    errorFlag = true;
                }
            }
        }

        private static bool varDecl()
        {
            int toErase;
            bool toReturn = true;
            foreach(tokens actual in myTokens)
            {
                if(actual.id== "UserDefined")
                {
                    actual.
                }
            }
            return toReturn;
        }
        */
        /*
         * Estructura para almacenar cada linea
         * Y el numero correspondiente de linea
         */
        private struct lineas
        {
            public int noLinea;
            public string linea;
        }

        /*
         * Estructura para almacenar la información
         * de los distintos tokens del codigo
         */
        private struct tokens
        {
            public string id;
            public string myValue;
            public int noLinea;
        }
    }
}
