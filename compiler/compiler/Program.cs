using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
namespace compiler
{
    class Program
    {
        private static LinkedList<tokens> myTokens;                // Almacena todos los tokens del codigo
        private static string zone;                                // Almacena ubicación actual para identificarlo en la tabla de simbolos
        private static LinkedList<simbols> simbolsTable;           // Equivale a mi tabla de simbolos
        private static LinkedList<errors> errorsTable;             // Representa mi tabla de errores

        static void Main(string[] args)
        {
            LinkedList<lineas> allCode = new LinkedList<lineas>();
            myTokens = new LinkedList<tokens>();
            simbolsTable = new LinkedList<simbols>();
            errorsTable = new LinkedList<errors>();
            LinkedList<tokens> newCopy;                                     // Auxiliar para la lista de myTokens
            int conteo = 0;                                                 // Conteo auxiliar
            string path = @"Pruebas\Lenguaje inventado-prueba1.txt";       //Path donde leera el archivo a compilar
            zone = "global";
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
                    myToken.id = identifier(check.Value);
                    myToken.myValue = string.Copy(check.Value);
                    myToken.noLinea = x.noLinea;
                    myTokens.AddLast(myToken);
                    check = check.NextMatch();
                }
            }
            //Agrega todo aquello que fue error lexico a la tabla de errores.
            foreach (tokens x in myTokens)
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
            if (word == "estampa")
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
                return "OpMul";
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
            LinkedList<tokens> aux = new LinkedList<tokens>(myTokens);
            check = varDecl();
            if (check)
            {
                myTokens.RemoveFirst();
                check = check && program();
            }
            else
            {
                myTokens = new LinkedList<tokens>(aux);
                check = functionList();
            }
            if(!check && myTokens.Count != 0)
            {
                syntaxError();
                check = program();
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
                check = check && functionList();
                /*if (check)
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
                }*/
            }
            return check;
        }

        public static bool function()
        {
            int count = 0;
            bool check = myTokens.First.Value.id == "integer";
            count += 1;
            check = check && (myTokens.First.Next.Value.id == "UserDefined");
            count += 1;
            check = check && (myTokens.First.Next.Next.Value.id == "LeftBrack");
            count += 1;
            if (check)
            {
                zone = myTokens.First.Next.Value.myValue;
                tokensRemove(ref count);
                check = check && listDeclVar();
                check = check && (myTokens.First.Value.id == "RightBrack");
                myTokens.RemoveFirst();
                check = check && (myTokens.First.Value.id == "LeftPar");
                count += 1;
                tokensRemove(ref count);
                check = check && statementList();
            }
            count = 0;
            zone = "global";
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
            bool check2=false;
            LinkedList<tokens> aux;
            string type = "";
            if (myTokens.First.Value.id == "integer")
            {
                type = "integer";
            }
            else if (myTokens.First.Value.id == "char")
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
                aux = new LinkedList<tokens>(myTokens);
                check = listDecl(type);
                if (!check)
                {
                    myTokens = new LinkedList<tokens>(aux);
                    check2 = assignOp(type);
                }
                check = check || check2;
            }
            else
            {
                check = false;
            }
            return check;
        }

        public static bool listDecl(string type)
        {
            bool check = true;
            int count = 0;
            string last;
            LinkedList<simbols> aux = new LinkedList<simbols>();
            if (myTokens.First.Value.id == "UserDefined") {
                last = myTokens.First.Value.id;
                simbols sim = new simbols();
                sim.id = myTokens.First.Value.myValue;
                sim.lastLine = myTokens.First.Value.noLinea;
                sim.type = type;
                sim.zone = zone;
                aux.AddLast(sim);
                myTokens.RemoveFirst();
                foreach (tokens x in myTokens)
                {
                    if (check)
                    {
                        if (x.id != "EOL" && x.id!= "OpAssign" && x.id != "RightBrack")
                        {
                            if (last == "UserDefined" && x.id == "COMMA")
                            {
                                last = x.id;
                                
                                check = true;
                            }
                            if (last == "COMMA" && x.id == "UserDefined")
                            {
                                simbols another = new simbols();
                                another.id = myTokens.First.Value.myValue;
                                another.lastLine = myTokens.First.Value.noLinea;
                                another.type = type;
                                another.zone = zone;
                                aux.AddLast(another);
                                myTokens.RemoveFirst();
                                last = x.id;
                                check = true;
                            }
                            else
                            {
                                check = false;
                                break;
                            }
                        }
                        else if (last != "UserDefined" && x.id == "EOL")
                        {
                            check = false;
                            break;
                        }
                        else if (last == "UserDefined" && x.id == "RightBrack")
                        {
                            check = true;
                            break;
                        }
                        else
                        {
                            check = false;
                            break;
                        }
                        count += 1;
                    }
                }
                tokensRemove(ref count);
            }
            if (check)
            {
                foreach (simbols x in aux)
                {
                    simbolsTable.AddLast(x);
                }
            }
            return check;
        }

        public static bool assignOp(string type)
        {
            bool check;
            LinkedList<simbols> aux = new LinkedList<simbols>();
            if (myTokens.First.Value.id == "UserDefined" && myTokens.First.Next.Value.id == "OpAssign")
            {
                simbols another = new simbols();
                another.id = myTokens.First.Value.myValue;
                another.lastLine = myTokens.First.Value.noLinea;
                another.type = type;
                another.zone = zone;
                another.value = "";
                myTokens.RemoveFirst();
                myTokens.RemoveFirst();
                check = opExpr(type, ref another);
                aux.AddLast(another);
            }
            else
            {
                check = false;
            }
            if (check)
            {
                simbolsTable.AddLast(aux.Last.Value);
            }
            return check;
        }

        private static bool opExpr(string type, ref simbols newSymbol)
        {
            bool check=true;
            string last;
            if((type == "char" && myTokens.First.Value.id == "string" && myTokens.First.Value.myValue.Length==3) || (type == "char" && myTokens.First.Value.id == "UserDefined"))
            {
                newSymbol.value = myTokens.First.Value.myValue;
                myTokens.RemoveFirst();
                if (myTokens.First.Value.id == "EOL")
                {
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            else if(type =="integer" || type == "float" || type == "UserDefined")
            {
                newSymbol.value = newSymbol.value + myTokens.First.Value.myValue;
                last = myTokens.First.Value.id;
                myTokens.RemoveFirst();
                for (int x = 0; (myTokens.First.Value.id != "EOL" && myTokens.First.Value.id != "RightPar") && last != "EOL"; x++)
                {
                    if (last == "Entero" || last == "Flotante" || last == "UserDefined")
                    {
                        if (myTokens.First.Value.id == "OpAdd" || myTokens.First.Value.id == "OpMinus" || myTokens.First.Value.id == "OpMul" || myTokens.First.Value.id == "OpDiv")
                        {
                            newSymbol.value = newSymbol.value + myTokens.First.Value.myValue;
                            last = myTokens.First.Value.id;
                            myTokens.RemoveFirst();
                            check = true;
                        }
                        else
                        {
                            check = false;
                            break;
                        }
                    }
                    else if (last == "OpAdd" || last == "OpMinus" || last == "OpMul" || last == "OpDiv")
                    {
                        if (myTokens.First.Value.id == "OpAdd" || myTokens.First.Value.id == "OpMinus" || myTokens.First.Value.id == "Entero" || myTokens.First.Value.id == "Flotante" || myTokens.First.Value.id == "UserDefined")
                        {
                            newSymbol.value = newSymbol.value + myTokens.First.Value.myValue;
                            last = myTokens.First.Value.id;
                            myTokens.RemoveFirst();
                            check = true;
                        }
                        else if(myTokens.First.Value.id== "LeftPar")
                        {
                            newSymbol.value = newSymbol.value + myTokens.First.Value.myValue;
                            myTokens.RemoveFirst();
                            check = opExpr(type, ref newSymbol);
                            newSymbol.value = newSymbol.value + myTokens.First.Value.myValue;
                            myTokens.RemoveFirst();
                            last = "Entero";
                            if (!check)
                            {
                                break;
                            }
                        }
                        else
                        {
                            check = false;
                            break;
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
            }
            else
            {
                check = false;
            }
            return check;
        }


        public static bool assignOp()
        {
            bool check = false;
            bool semantic = true;
            simbols another = new simbols();
            string type="";
            if (myTokens.First.Value.id == "UserDefined" && myTokens.First.Next.Value.id == "OpAssign")
            {
                foreach(simbols key in simbolsTable)
                {
                    if (key.id == myTokens.First.Value.myValue && (key.zone == "global" || key.zone == zone))
                    {
                        another = key;
                        another.value = "";
                        type = key.type;
                        semantic = true;
                        break;
                    }
                        
                }
                if (semantic)
                {
                    myTokens.RemoveFirst();
                    myTokens.RemoveFirst();
                    check = opExpr(type, ref another);
                }
                else
                {
                    check = true;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }
        /*
        private static bool lookSimbols(tokens anotherToken)
        {
            bool semantic = true;
            bool check = true;
            foreach (simbols x in simbolsTable)
            {
                if(x.id== anotherToken.myValue && (x.zone == zone || x.zone == "global"))
                {
                    semantic = true;
                    break;
                }
                else
                {
                    semantic = false;
                }
            }
            if (!semantic || simbolsTable.Count==0)
            {
                tokensRemoveEOL1();
                errors newError = new errors();
                newError.line = myTokens.First.Value.noLinea;
                newError.type = "Semantic";
                newError.aprox = myTokens.First.Value.id;
                errorsTable.AddLast(newError);
                check = false;
            }
            return check;
        }

            */
        private static bool statement()
        {
            bool check=false;
            switch (myTokens.First.Value.id)
            {
                case "integer":
                    check = varDecl();
                    break;
                case "char":
                    check = varDecl();
                    break;
                case "float":
                    check = varDecl();
                    break;
                case "UserDefined":
                    assignOp();
                    break;
                case "read":
                    inputOp();
                    break;
                case "write":
                    outputOp();
                    break;
                case "if":
                    ifCond();
                    break;
                case "while":
                    whileLoop();
                    break;
                case "do":
                    doWhileLoop();
                    break;
                case "for":
                    forLoop();
                    break;
                default:
                    check = false;
                    break;
            } 
            if (check)
            {
                myTokens.RemoveFirst();
            }
            else
            {
                syntaxError();
            }
            return check;
        }

        private static bool statementList()
        {
            bool check = true;
            if(myTokens.First.Value.id== "RightPar")
            {
                myTokens.RemoveFirst();
                return true;
            }
            else
            {
                check = statement() && statementList();
            }
            if (!check)
            {
                check = statement() && statementList();
            }
            return check;
        }

        private static bool inputOp()
        {
            myTokens.RemoveFirst();
        }

        private static void syntaxError()
        {
            tokensRemoveEOL1();
            errors newError = new errors();
            newError.line = myTokens.First.Value.noLinea;
            newError.type = "Syntax";
            newError.aprox = myTokens.First.Value.id;
            errorsTable.AddLast(newError);
            myTokens.RemoveFirst();
        }
        private static void tokensRemove(ref int count)
        {
            while (count > 0)
            {
                myTokens.RemoveFirst();
                count -= 1;
            }
        }

        private static void tokensRemoveEOL1()
        {
            for (int x = 0; myTokens.First.Value.id != "EOL" && myTokens.First.Value.id != "RightPar"; x++)
            {
                myTokens.RemoveFirst();
            }
        }

        private static void semanticError()
        {
            tokensRemoveEOL1();
            errors newError = new errors();
            newError.line = myTokens.First.Value.noLinea;
            newError.type = "Semantic";
            newError.aprox = myTokens.First.Value.id;
            errorsTable.AddLast(newError);
            myTokens.RemoveFirst();
        }
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
            public string id;
            public string zone;
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
