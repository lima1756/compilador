using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace compiler
{
    class Program
    {
        private static LinkedList<tokens> myTokens;                // Almacena todos los tokens del codigo
        private static LinkedList<tokens> myTokens2;               // Auxiliar. Almacena todos los tokens del codigo
        private static int level;                                  // Almacena el nivel actual para identificarlo en la tabla de simbolos
        private static bool errorFlag;                             // En caso de ser verdadero en sintactico busca el siguiente EOL
        private static Dictionary<string, simbols> simbolsTable;   // Equivale a mi tabla de simbolos
        private static LinkedList<errors> errorsTable;             // Representa mi tabla de errores

        static void Main(string[] args)
        {
            LinkedList<lineas> allCode = new LinkedList<lineas>();
            myTokens = new LinkedList<tokens>();
            simbolsTable = new Dictionary<string, simbols>();
            errorsTable = new LinkedList<errors>();
            LinkedList<tokens> newCopy;                                     // Auxiliar para la lista de myTokens
            int conteo = 0;                                                 // Conteo auxiliar
            level = 0;
            errorFlag = false;
            string path = @"Pruebas\Lenguaje inventado-prueba1.txt";       //Path donde leera el archivo a compilar
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
            //Lo siguiente sirve para ignorar tabuladores y la tokenización
            foreach (lineas x in allCode)
            {
                // Expresión regular para identificar posibles tokens 
                Match check = Regex.Match(x.linea,
                    @"(\u00B4((\\\u00B4)|([^\u00B4]))*\u00B4)|(([A-Za-z]+[A-Za-z-_\d]*[A-Za-z\d])|[A-Za-z])|([\(\)\{\}\[\].,])|([\.=\+\-/*])|(&|>=|<=|>|<|==|!|&&|\|\|)|(\d*\.?\d+)|\'.*\'|.");
                while (check.Success)
                {
                    tokens myToken = new tokens();
                    //Identifica al token especificamente con la función identifier.
                    myToken.id=identifier(check.Value);
                    myToken.myValue= string.Copy(check.Value);
                    myToken.noLinea =x.noLinea;
                    myTokens.AddLast(myToken);
                    check = check.NextMatch();
                }
            }
            //Agrega todo aquello que fue error lexico a la tabla de errores.
            foreach(tokens x in myTokens)
            {
                if (x.id == "ERROR")
                {
                    errors newError = new errors();
                    newError.line = x.noLinea;
                    newError.type = "Lexical";
                    newError.aprox = x.id;
                    errorsTable.AddLast(newError);
                    //Se deben de agregar los errores a una tabla de errores para que se impriman al final todos los conjuntos de errores en orden de lineas
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
            myTokens2 = new LinkedList<tokens>(myTokens);
            program();
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

        /*
         * Aqui inicia el analizador sintactico, 
         * todas las funciones hasta donde se 
         * indiquen son parte del mismo
         */
        public static bool program()
        {
            bool check;
            check = varDecl();
            if (check)
            {
                eq2();
                check = check && program();
            }
            else
            {
                eq1(); 
                check = functionList();
            }
            return check;
        }

        public static bool functionList()
        {
            bool check;
            check = function();
            /*
             * Revisa si hay algo despues de la primera función leida
             * si lo hay revisa si es una funcion
             * en caso de no serlo se guarda como error
             */
            if (check && myTokens.First != null) 
            {
                eq2();
                check = check && functionList();
                if (check)
                {
                    eq1();
                }
                else
                {
                    eq2();
                    for (int x = 0; myTokens.First.Value.id != "EOL" || myTokens.First.Value.id != "RightPar" || myTokens.First.Value.id == null; x++)
                    {
                        myTokens.RemoveFirst();
                    }
                    errors newError = new errors();
                    if (myTokens.First.Value.id != null)
                    {
                        newError.line = myTokens.First.Value.noLinea;
                        newError.type = "Syntax";
                        newError.aprox = myTokens.First.Value.id;
                        errorsTable.AddLast(newError);
                    }
                    else
                    {
                        newError.line = -1;
                        newError.type = "Syntax";
                        newError.aprox = "EOF";
                        errorsTable.AddLast(newError);
                    }
                }
            }
            return check;
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
                tokensRemove(ref count);
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
            bool check;
            bool check2;
            string type="";
            if (myTokens.First.Value.id == "integer")
            {
                type = "integer";
            }
            else if(myTokens.First.Value.id == "char")
            {
                type = "char";
            }
            else if (myTokens.First.Value.id == "float")
            {
                type = "float";
            }
            if (type != "")
            {
                myTokens.RemoveFirst();
                check = listDecl(type);
                check2 = assignOp(type);
            }
            else
            {
                check = false;
            }
            return check;
        }

        public static bool listDecl(string type)
        {
            bool check=true;
            int count = 0;
            string last;
            last = myTokens.First.Value.id;
            foreach(tokens x in myTokens)
            {
                if (check)
                {
                    if (x.id != "EOL")
                    {
                        if (last != "UserDefined" && x.id == "COMMA")
                        {
                            last = x.id;
                            check = true;
                        }
                        if (last != "COMMA" && x.id == "UserDefined")
                        {
                            last = x.id;
                            check = true;
                        }
                    }
                    else if (last != "UserDefined")
                    {
                        check = false;
                    }
                    count += 1;
                }
            }
            tokensRemove(ref count);
            if (!check)
            {
                for(int x=0; myTokens.First.Value.id!="EOL" || myTokens.First.Value.id != "RightPar"; x++)
                {
                    myTokens.RemoveFirst();
                }
                errors newError = new errors();
                newError.line = myTokens.First.Value.noLinea;
                newError.type = "Syntax";
                newError.aprox = myTokens.First.Value.id;
                errorsTable.AddLast(newError);
            }
            return check;
        }

        public static bool assignOp(string type)
        {
            bool check;
            if (myTokens2.First.Value.id == "UserDefined" && myTokens2.First.Next.Value.id == "OpAssign")
            {
                check = opExpr(type);
            }
            else
            {
                check = false;
                tokensRemoveEOL2();
            }
            return check;
        }

        private static bool opExpr(string type)
        {
            bool check;
            if(type == "char" && myTokens2.First.Value.id == "char")
            {
                myTokens2.RemoveFirst();
                if (myTokens2.First.Value.id == "EOL")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            else if(type =="integer" || type == "float")
            {
                for(int x=0; myTokens.First.Value.id!="EOL"|| myTokens.First.Value.id != "RightPar")
                {
                    if()
                }
            }
            else
            {
                check = false;
            }
            return check;
        }
        private static void tokensRemove(ref int count)
        {
            while (count > 0)
            {
                myTokens.RemoveFirst();
                count -= 1;
            }
        }

        private static void tokensRemove2(ref int count)
        {
            while (count > 0)
            {
                myTokens2.RemoveFirst();
                count -= 1;
            }
        }

        private static void tokensRemoveEOL2()
        {
            for (int x = 0; myTokens.First.Value.id != "EOL" || myTokens.First.Value.id != "RightPar"; x++)
            {
                myTokens.RemoveFirst();
            }
        }

        private static void eq1()
        {
            myTokens = new LinkedList<tokens>(myTokens2);
        }

        private static void eq2()
        {
            myTokens2 = new LinkedList<tokens>(myTokens);
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

        /*
         * Estructura para almacenar datos
         * acerca de las declaraciones de usuario
         */
        private struct simbols
        {
            public string type;
            public string value;
            public int lastLine;
        }

        /*
         * Estructura para almacenar datos
         * acerca de las declaraciones de usuario
         */
        private struct errors
        {
            public string type;
            public string aprox;
            public int line;
        }
    }
}
