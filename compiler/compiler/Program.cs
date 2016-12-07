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
        private static LinkedList<simbols> simbolsTable;           // Equivale a la tabla de simbolos
        private static LinkedList<errors> errorsTable;             // Representa la tabla de errores
        private static LinkedList<string> functions;               //Lista de funciones ya declaradas, solo se puede llamar una función si fue declarada previamente
        private static string path;                                //Almacena el directorio del archivo
        private static bool retornar;

        static void Main(string[] args)
        {
            LinkedList<lineas> allCode = new LinkedList<lineas>();
            functions = new LinkedList<string>();
            myTokens = new LinkedList<tokens>();
            simbolsTable = new LinkedList<simbols>();
            errorsTable = new LinkedList<errors>();
            LinkedList<tokens> newCopy;                                     // Auxiliar para la lista de myTokens
            int conteo = 0;                                                 // Conteo auxiliar
            path = @"Pruebas\Lenguaje inventado-prueba1.txt";                                  //Path donde leera el archivo a compilar
            zone = "global";
            StreamReader sr = new StreamReader(path);
            string code = sr.ReadToEnd();
            string[] codelines = Regex.Split(code, @"\n |\r |\n\r |\r\n"); //Separa el codigo en lineas
            int last = 0;                                                  //Variable que almacenara el valor de la ultima linea
            foreach (string x in codelines)
            {
                lineas line = new lineas();
                line.linea = x;
                line.noLinea = ++conteo;
                allCode.AddLast(line);
            }
            sr.Close();
            //Lo siguiente sirve para ignorar tabuladores y la tokenización
            foreach (lineas x in allCode)
            {
                // Expresión regular para identificar posibles tokens 
                Match check = Regex.Match(x.linea,
                    @"(\u00B4((\\\u00B4)|([^\u00B4]))*\u00B4)|(([A-Za-z]+[A-Za-z-_\d]*[A-Za-z\d])|[A-Za-z])|([\(\)\{\}\[\].,])|(&|>=|<=|>|<|!=|==|!|&&|\|\|)|(\d*\.?\d+)|([\.=\+\-/*])|\'[^\']*\'|.");
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
                    newError.type = "Lexico, simbolo no detectado";
                    newError.aprox = x.myValue;
                    errorsTable.AddLast(newError);
                    //Se deben de agregar los errores a una tabla de errores para que se impriman al final todos los conjuntos de errores en orden de lineas
                }
            }
            last = myTokens.Last.Value.noLinea;
            newCopy = new LinkedList<tokens>(myTokens);
            myTokens.Clear();
            foreach (tokens x in newCopy)
            {
                if (x.id != "ERROR" && x.id != "Comment" && x.id != "WhiteSpace")
                {
                    myTokens.AddLast(x); ; ;
                }
            }
            newCopy.Clear();
            newCopy = new LinkedList<tokens>(myTokens);
            program();
            if (errorsTable.Count > 0)
            {
                Console.WriteLine("ERRORES: ");
                for (int z = 1; z <= last; z++)
                {
                    foreach (errors x in errorsTable)
                    {
                        if (x.line == z)
                        {
                            Console.WriteLine("Linea: " + x.line + "\t\tDescripcion: " + x.type + "\t\tCerca de: " + x.aprox);
                        }
                    }
                }
            }
            else
            {
                myTokens = new LinkedList<tokens>(newCopy);
                translator();
                Console.WriteLine("Todo correcto :D. Presione una tecla para continuar.");
                //Aqui iria la función para el traductor
            }
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
            if (word == "retornar")
            {
                return "return";
            }
            if (Regex.IsMatch(word, @"([A-Za-z]+[A-Za-z-_\d]*[A-Za-z\d])|[A-Za-z]"))
            {
                return "UserDefined";
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
            if (Regex.IsMatch(word, @"\."))
            {
                return "EOL";
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
            if (myTokens.Count > 0)
            {
                check = varDecl();
                if (check)
                {
                    if (myTokens.Count > 0)
                    {
                        if (myTokens.First.Value.id == "EOL")
                        {
                            myTokens.RemoveFirst();
                        }
                    }
                    check = check && program();
                }
                else
                {
                    myTokens = new LinkedList<tokens>(aux);
                    check = functionList();
                }
                if (!check && myTokens.Count != 0)
                {
                    syntaxError();
                    tokensRemove(1);
                    check = program();
                }
            }
            else
            {
                check = true;
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
            }
            return check;
        }

        public static bool function()
        {
            int count = 0;
            bool check = myTokens.First.Value.id == "integer";
            count += 1;
            if (check)
            {
                check = check && (myTokens.First.Next.Value.id == "UserDefined");
                functions.AddLast(myTokens.First.Next.Value.myValue);
                count += 1;
                if (check)
                {
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
                        retornar = false;
                        check = check && statementList();
                        if (!retornar)
                        {
                            errors newError = new errors();
                            newError.line = myTokens.First.Value.noLinea;
                            newError.type = "Falta retornar algo";
                            newError.aprox = myTokens.First.Value.myValue;
                            errorsTable.AddLast(newError);
                        }
                        if (check && retornar)
                        {
                            tokensRemove(1);
                        }
                    }
                }
                else
                {
                    tokensRemove(count);
                }
            }
            else
            {
                tokensRemove(count);
            }
            count = 0;
            zone = "global";
            return check;
        }

        public static bool listDeclVar()
        {
            bool check;
            if (myTokens.First.Value.id != "RightBrack")
            {
                check = varDecl();
                if (myTokens.First.Value.id == "COMMA")
                {
                    myTokens.RemoveFirst();
                    check = check && listDeclVar();
                }
            }
            else
            {
                check = true;
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

        public static bool varDeclFor()
        {
            bool check;
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
                check = assignOp(type);
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
                        else if (last == "UserDefined" && x.id == "EOL")
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
                another.value = new LinkedList<tokens>(); ;
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
                checkSimbol(aux.Last.Value);
                simbolsTable.AddLast(aux.Last.Value);
            }
            return check;
        }

        private static bool opExpr(string type, ref simbols newSymbol)
        {
            bool check=true;
            bool rev = false;
            bool ch = true;
            string last="";
            if((type == "char" && myTokens.First.Value.id == "string" && myTokens.First.Value.myValue.Length==3) || (type == "char" && myTokens.First.Value.id == "UserDefined"))
            {                
                if(myTokens.First.Value.id == "UserDefined")
                {
                    foreach(simbols x in simbolsTable)
                    {
                        if (x.id == myTokens.First.Value.myValue && x.type == "char" && x.zone == zone)
                        {
                            rev = true;
                        }
                    }
                    if (!rev)
                    {
                        newSymbol.value.AddLast(myTokens.First.Value);
                        semanticError("Error en la variable " + myTokens.First.Value.myValue+ " en la igualación", myTokens.First.Value);
                        tokensRemoveEOL1();
                        check = true;
                        ch = false;
                    }
                }
                if (ch)
                {
                    newSymbol.value.AddLast(myTokens.First.Value);
                }
                tokensRemoveEOL1();
                if (myTokens.First.Value.id == "EOL")
                {
                    check = true;
                }
                else
                {
                    semanticError("Error en tipo y su valor", myTokens.First.Value);
                    tokensRemoveEOL1();
                    check = true;
                }
            }
            else if(type =="integer" || type == "float" || type == "UserDefined")
            {
                newSymbol.value.AddLast(myTokens.First.Value);
                last = myTokens.First.Value.id;
                myTokens.RemoveFirst();
                for (int x = 0; (myTokens.First.Value.id != "EOL" && myTokens.First.Value.id != "RightPar" && myTokens.First.Value.id != "RightBrack") && last != "EOL"; x++)
                {
                    if (myTokens.First.Value.id == "UserDefined")
                    {
                        
                        foreach (simbols y in simbolsTable)
                        {
                            if (y.id == myTokens.First.Value.myValue && (y.type == "integer" || y.type == "float") && y.zone==zone)
                            {
                                rev = true;
                            }
                        }
                        if (!rev)
                        {
                            semanticError("Error en la variable " + myTokens.First.Value.myValue + " en la igualación", myTokens.First.Value);
                            tokensRemoveEOL1();
                            check = true;
                        }
                    }
                    if (last == "Entero" || last == "Flotante" || last == "UserDefined")
                    {
                        if (myTokens.First.Value.id == "OpAdd" || myTokens.First.Value.id == "OpMinus" || myTokens.First.Value.id == "OpMul" || myTokens.First.Value.id == "OpDiv")
                        {
                            newSymbol.value.AddLast(myTokens.First.Value);
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
                            newSymbol.value.AddLast(myTokens.First.Value);
                            last = myTokens.First.Value.id;
                            myTokens.RemoveFirst();
                            check = true;
                        }
                        else if(myTokens.First.Value.id== "LeftPar")
                        {
                            newSymbol.value.AddLast(myTokens.First.Value);
                            myTokens.RemoveFirst();
                            check = opExpr(type, ref newSymbol);
                            newSymbol.value.AddLast(myTokens.First.Value);
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
            if (last == "OpAdd" || last == "OpMinus" || last == "OpMul" || last == "OpDiv")
            {
                check = false;
            }
            check = !rev || check;
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
                        another.value = new LinkedList<tokens>();
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
                    checkSimbol(another);
                    if (another.id == null) {
                        check = false;
                    }
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;            
        }

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
                    if (myTokens.First.Next.Value.id == "LeftBrack")
                    {
                        foreach(string fun in functions)
                        {
                            if(fun == myTokens.First.Value.myValue)
                            {
                                check = true;
                                break;
                            }
                        }
                        if (check)
                        {
                            tokensRemove(2);
                            check = printVars();
                            if (myTokens.First.Value.id == "EOL")
                            {
                                tokensRemove(1);
                                check = true;
                            }
                            else
                            {
                                check = false;
                            }
                        }
                    }
                    else if(myTokens.First.Next.Value.id == "OpAssign")
                    {
                        check = assignOp();
                    }
                    else
                    {
                        check = false;
                    }
                    break;
                case "read":
                    check = inputOp();
                    break;
                case "write":
                    check = outputOp();
                    break;
                case "if":
                    check = ifCond();
                    break;
                case "while":
                    check = whileLoop();
                    break;
                case "do":
                    check = doWhileLoop();
                    break;
                case "for":
                    check = forLoop();
                    break;
                case "return":
                    tokensRemove(1);
                    if ((myTokens.First.Value.id == "Entero" || myTokens.First.Value.id == "UserDefined") && myTokens.First.Next.Value.id == "EOL")
                    {
                        bool rev = true;
                        if (myTokens.First.Value.id == "UserDefined")
                        {
                            rev = false;
                            foreach(simbols x in simbolsTable)
                            {
                                if (x.id == myTokens.First.Value.myValue && (x.zone == "global" || x.zone == "zone") && x.type == "integer")
                                {
                                    rev = true;
                                }
                            }
                        }
                        if (rev)
                        {
                            tokensRemove(2);
                        }
                        else
                        {
                            semanticError("Tipo de dato retornado incorrecto o desconocido");
                        }
                        retornar = true;
                        check = true;
                    }
                    else if ((myTokens.First.Value.id == "Entero" || myTokens.First.Value.id == "UserDefined") && myTokens.First.Next.Value.id == "RightPar")
                    {
                        TokensRemoveRightPar();
                        errors newError = new errors();
                        newError.line = myTokens.First.Value.noLinea;
                        newError.type = "Syntax";
                        newError.aprox = myTokens.First.Value.myValue;
                        errorsTable.AddLast(newError);
                        check = true;
                    }
                    else
                    {
                        check = false;
                    }
                    break;
                case "EOL":
                    tokensRemove(1);
                    check = true;
                    break;
                default:
                    check = false;
                    break;
            } 
            if (check && myTokens.First.Value.id == "EOL")
            {
                myTokens.RemoveFirst();
            }
            else if(!check)
            {
                syntaxError();
            }
            return check;
        }

        private static bool whileLoop()
        {
            bool check = false;
            tokensRemove(1);
            if (myTokens.First.Value.id == "LeftBrack")
            {
                tokensRemove(1);
                check = logicOp();
                if (!check)
                {
                    for (int x = 0; myTokens.First.Value.id != "RightBrack"; x++) 
                    {
                        myTokens.RemoveFirst();
                    }
                    errors newError = new errors();
                    newError.line = myTokens.First.Value.noLinea;
                    newError.type = "Syntax";
                    newError.aprox = myTokens.First.Value.myValue;
                    errorsTable.AddLast(newError);
                    check = true;
                }
                if (check && myTokens.First.Value.id == "RightBrack")
                {
                    tokensRemove(1);
                    if (myTokens.First.Value.id == "LeftPar")
                    {
                        tokensRemove(1);
                        check = statementList();
                        if (check && myTokens.First.Value.id == "RightPar")
                        {
                            tokensRemove(1);
                        }
                        else
                        {
                            check = false;
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }

        private static bool doWhileLoop()
        {
            bool check = false;
            tokensRemove(1);

            if (myTokens.First.Value.id == "LeftPar")
            {
                tokensRemove(1);
                check = statementList();
                if (check && myTokens.First.Value.id == "RightPar" && myTokens.First.Next.Value.id == "while")
                {
                    tokensRemove(2);
                    if (myTokens.First.Value.id == "LeftBrack")
                    {
                        tokensRemove(1);
                        check = logicOp();
                        if (!check)
                        {
                            for (int x = 0; myTokens.First.Value.id != "RightBrack"; x++)
                            {
                                myTokens.RemoveFirst();
                            }
                            errors newError = new errors();
                            newError.line = myTokens.First.Value.noLinea;
                            newError.type = "Syntax";
                            newError.aprox = myTokens.First.Value.myValue;
                            errorsTable.AddLast(newError);
                            check = true;
                        }
                        if (check && myTokens.First.Value.id == "RightBrack" && myTokens.First.Next.Value.id == "EOL")
                        {
                            tokensRemove(1);
                        }
                        else
                        {
                            check = false;
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }

            return check;
        }

        private static bool forLoop()
        {
            bool check=false;
            tokensRemove(1);
            if (myTokens.First.Value.id == "LeftBrack")
            {
                tokensRemove(1);
                if (myTokens.First.Value.id == "integer" || myTokens.First.Value.id == "float" || myTokens.First.Value.id == "char")
                {
                    check = varDeclFor();
                }
                else if(myTokens.First.Value.id == "UserDefined")
                {
                    check = assignOp();
                }
                else
                {
                    check = false;
                }
                if (!check)
                {
                    for (int x = 0; myTokens.First.Value.id != "EOL" && myTokens.First.Value.id != "RightBrack"; x++)
                    {
                        myTokens.RemoveFirst();
                    }
                    errors newError = new errors();
                    newError.line = myTokens.First.Value.noLinea;
                    newError.type = "Syntax";
                    newError.aprox = myTokens.First.Value.myValue;
                    errorsTable.AddLast(newError);
                    check = true;
                }
                if (check && myTokens.First.Value.id == "EOL")
                {
                    tokensRemove(1);
                    check = logicOp();
                    if (!check)
                    {
                        for (int x = 0; myTokens.First.Value.id != "EOL" && myTokens.First.Value.id != "RightBrack"; x++)
                        {
                            myTokens.RemoveFirst();
                        }
                        errors newError = new errors();
                        newError.line = myTokens.First.Value.noLinea;
                        newError.type = "Syntax";
                        newError.aprox = myTokens.First.Value.myValue;
                        errorsTable.AddLast(newError);
                        check = true;
                    }
                    if (check && myTokens.First.Value.id == "EOL")
                    {
                        tokensRemove(1);
                        check = assignOp();
                        if (!check)
                        {
                            for (int x = 0; myTokens.First.Value.id != "EOL" && myTokens.First.Value.id != "RightBrack"; x++)
                            {
                                myTokens.RemoveFirst();
                            }
                            errors newError = new errors();
                            newError.line = myTokens.First.Value.noLinea;
                            newError.type = "Syntax";
                            newError.aprox = myTokens.First.Value.myValue;
                            errorsTable.AddLast(newError);
                            check = true;
                        }
                        if (check && myTokens.First.Value.id == "RightBrack" && myTokens.First.Next.Value.id == "LeftPar")
                        {
                            tokensRemove(2);
                            check = statementList();
                            if(check && myTokens.First.Value.id == "RightPar")
                            {
                                tokensRemove(1);
                            }
                            else
                            {
                                check = false;
                            }
                        }
                        else
                        {
                            check = false;
                        }
                    }
                    else if (check && myTokens.First.Value.id == "RightBrack" && myTokens.First.Next.Value.id == "LeftPar")
                    {
                        tokensRemove(2);
                        check = statementList();
                        if (check && myTokens.First.Value.id == "RightPar")
                        {
                            tokensRemove(1);
                        }
                        else
                        {
                            check = false;
                        }
                    }
                    else
                    {
                        check = false;
                    }
                }
                else if (check && myTokens.First.Value.id == "RightBrack" && myTokens.First.Next.Value.id == "LeftPar")
                {
                    tokensRemove(2);
                    check = statementList();
                    if (check && myTokens.First.Value.id == "RightPar")
                    {
                        tokensRemove(1);
                    }
                    else
                    {
                        check = false;
                    }
                }
                else
                {
                    check = false;
                }
            }

            return check;
        }

        private static bool statementList()
        {
            bool check = true;
            if(myTokens.First.Value.id== "RightPar")
            {
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
            bool check = false;
            tokensRemove(1);
            if (myTokens.First.Value.id== "LeftBrack" && myTokens.First.Next.Value.id == "string" 
                && myTokens.First.Next.Next.Value.id == "COMMA")
            {
                tokensRemove(3);
                check = inVars();
                check = check && myTokens.First.Value.id == "EOL";
            }
            return check;
        }

        private static bool inVars()
        {
            bool check = false;
            if(myTokens.First.Value.id == "Direction" && myTokens.First.Next.Value.id == "UserDefined")
            {
                tokensRemove(2);
                check = true;
                if(myTokens.First.Value.id == "COMMA")
                {
                    tokensRemove(1);
                    check = check && inVars();
                }
                else if(myTokens.First.Value.id == "RightBrack")
                {
                    tokensRemove(1);
                    check = true;
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }

        private static bool outputOp()
        {
            bool check = false;
            tokensRemove(1);
            if (myTokens.First.Value.id == "LeftBrack")
            {
                tokensRemove(1);
                check = printExpr();
                check = check && myTokens.First.Value.id == "EOL";
            }
            else
            {
                check = false;
            }
            return check;

        }

        private static bool printExpr()
        {
            bool check = false;
            if(myTokens.First.Value.id == "string")
            {
                tokensRemove(1);
                if(myTokens.First.Value.id == "RightBrack")
                {
                    tokensRemove(1);
                    check = true;
                }
                else if (myTokens.First.Value.id == "COMMA")
                {
                    tokensRemove(1);
                    check = printVars();
                }
                else
                {
                    check = false;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }

        private static bool printVars()
        {
            bool check = false;
            if (myTokens.First.Value.id == "UserDefined")
            {
                tokensRemove(1);
                check = true;
                if (myTokens.First.Value.id == "COMMA")
                {
                    tokensRemove(1);
                    check = check && printVars();
                }
                else if(myTokens.First.Value.id == "RightBrack")
                {
                    tokensRemove(1);
                    check = true;
                }
            }
            else
            {
                check = false;
            }
            return check;
        }

        private static bool ifCond()
        { 
            bool check = false;
            tokensRemove(1);
            if(myTokens.First.Value.id == "LeftBrack")
            {
                tokensRemove(1);
                check = logicOp();
                if (!check)
                {
                    for (int x = 0; myTokens.First.Value.id != "RightBrack"; x++)
                    {
                        myTokens.RemoveFirst();
                    }
                    errors newError = new errors();
                    newError.line = myTokens.First.Value.noLinea;
                    newError.type = "Syntax";
                    newError.aprox = myTokens.First.Value.myValue;
                    errorsTable.AddLast(newError);
                    check = true;
                }
                if (check && myTokens.First.Value.id == "RightBrack" && myTokens.First.Next.Value.id == "LeftPar")
                {
                    tokensRemove(2);
                    statementList();
                    if(myTokens.First.Value.id == "RightPar")
                    {
                        tokensRemove(1);
                        check = true;
                    }
                    else
                    {
                        check = false;
                    }
                    if (myTokens.First.Value.id == "else" && myTokens.First.Next.Value.id == "LeftPar")
                    {
                        tokensRemove(2);
                        statementList();
                        if (myTokens.First.Value.id == "RightPar")
                        {
                            tokensRemove(1);
                            check = true;
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
            }
            else
            {
                check = false;
            }
                return check;
        }
        
        private static bool logicOp()
        {
            string last;
            bool check = false;
            last = myTokens.First.Value.id;
            myTokens.RemoveFirst();
            for (int x = 0; (myTokens.First.Value.id != "RightBrack" && myTokens.First.Value.id != "RightPar" && myTokens.First.Value.id != "EOL") && last != "EOL" && last != "RightBrack"; x++)
            {
                if (last == "Entero" || last == "Flotante" || last == "UserDefined" || last == "string")
                {
                    if (myTokens.First.Value.id == "OpAnd" || myTokens.First.Value.id == "OpOr" || myTokens.First.Value.id == "OpNotEqu" 
                        || myTokens.First.Value.id == "OpEqu" || myTokens.First.Value.id == "OpGtEq" || myTokens.First.Value.id == "OpLtEq" 
                        || myTokens.First.Value.id == "OpNot" || myTokens.First.Value.id == "OpLt" || myTokens.First.Value.id == "OpGt")
                    {
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
                else if (last == "OpAnd" || last == "OpOr" || last == "OpNotEqu" || last == "OpEqu" || last == "OpGtEq" || last == "OpLtEq"
                        || last == "OpLt" || last == "OpGt")
                {
                    if (myTokens.First.Value.id == "Entero" || myTokens.First.Value.id == "Flotante" || myTokens.First.Value.id == "UserDefined" || myTokens.First.Value.id == "string" || myTokens.First.Value.id == "OpNot")
                    {
                        last = myTokens.First.Value.id;
                        myTokens.RemoveFirst();
                        check = true;
                    }
                    else if (myTokens.First.Value.id == "OpAdd" || myTokens.First.Value.id == "OpMinus")
                    {
                        last = myTokens.First.Value.id;
                        myTokens.RemoveFirst();
                        check = true;
                    }
                    else if (myTokens.First.Value.id == "LeftPar")
                    {
                        myTokens.RemoveFirst();
                        check = logicOp();
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
                else if (last == "OpNot")
                {
                    if (myTokens.First.Value.id == "Entero" || myTokens.First.Value.id == "Flotante" || myTokens.First.Value.id == "UserDefined" || myTokens.First.Value.id == "string" || myTokens.First.Value.id == "OpNot")
                    {
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
                else if (last == "OpAdd" || last == "OpMinus")
                {
                    if (myTokens.First.Value.id == "Entero" || myTokens.First.Value.id == "Flotante" || myTokens.First.Value.id == "UserDefined")
                    {
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
                    if (myTokens.First.Value.id == "OpAdd" || myTokens.First.Value.id == "OpMinus" || myTokens.First.Value.id == "Entero" 
                        || myTokens.First.Value.id == "Flotante" || myTokens.First.Value.id == "UserDefined")
                    {
                        last = myTokens.First.Value.id;
                        myTokens.RemoveFirst();
                        check = true;
                    }
                    else if (myTokens.First.Value.id == "LeftPar")
                    {
                        myTokens.RemoveFirst();
                        check = logicOp();
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
            if(last == "OpAnd" || last == "OpOr" || last == "OpNotEqu" || last == "OpEqu" || last == "OpGtEq" || last == "OpLtEq"
                        || last == "OpLt" || last == "OpGt" || last == "OpAdd" || last == "OpMinus" || last == "OpMul" || last == "OpDiv")
            {
                check = false;
            }
            return check;
        }

        private static void syntaxError()
        {
            tokensRemoveEOL1();
            errors newError = new errors();
            newError.line = myTokens.First.Value.noLinea;
            newError.type = "Syntax";
            newError.aprox = myTokens.First.Value.myValue;
            errorsTable.AddLast(newError);
        }

        private static void tokensRemove(ref int count)
        {
            while (count > 0)
            {
                myTokens.RemoveFirst();
                count -= 1;
            }
        }

        private static void tokensRemove(int count)
        {
            while (count > 0)
            {
                myTokens.RemoveFirst();
                count -= 1;
            }
        }

        private static void tokensRemoveEOL1()
        {
            for (int x = 0; myTokens.First.Value.id != "EOL" && myTokens.First.Value.id != "read"
                && myTokens.First.Value.id != "write" && myTokens.First.Value.id != "if" && myTokens.First.Value.id != "else"
                && myTokens.First.Value.id != "while" && myTokens.First.Value.id != "do" && myTokens.First.Value.id != "for"
                && myTokens.First.Value.id != "integer" && myTokens.First.Value.id != "float" && myTokens.First.Value.id != "char"
                && myTokens.First.Value.id != "return"; x++)
            {
                myTokens.RemoveFirst();
            }
        }

        private static void TokensRemoveRightPar()
        {
            for (int x = 0; myTokens.First.Value.id != "RightPar"; x++)
            {
                myTokens.RemoveFirst();
            }
        }
        /*
         * Inicio del analizadir semantico
         * Lo que hace falta que no se analizo junto al sintactico
         * o partes que se llaman en el sintactico
         */

        private static bool checkSimbol(simbols x)
        {
            bool check = false;
            if (x.type == "char")
            {
                foreach(tokens iT in x.value)
                {
                    if(iT.id != "string" && iT.id.Length != 1 && iT.id != "UserDefined")
                    {
                        semanticError("Expresión para el tipo de variable declarada incorrecta");
                        check = false;
                        break;
                    }
                    else if(iT.id == "UserDefined")
                    {
                        foreach(simbols z in simbolsTable)
                        {
                            if (z.id == iT.myValue)
                            {
                                if (z.zone != zone && z.zone != "global")
                                {
                                    semanticError("La variable " + iT.myValue + " no ha sido declarada", iT);
                                    check = false;
                                    break;
                                }
                                else if (z.type != "char")
                                {
                                    semanticError("La variable " + iT.myValue + " no es del tipo correcto", iT);
                                    check = false;
                                    break;
                                }
                                else
                                {
                                    check = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else if(x.type == "float" || x.type=="integer")
            {
                foreach (tokens iT in x.value)
                {
                    if (iT.id != "Entero" && iT.id != "Flotante" && iT.id != "UserDefined" && iT.id != "OpAdd" && iT.id != "OpMinus" && iT.id != "OpMul" 
                        && iT.id != "OpDiv" && iT.id != "LeftPar" && iT.id != "RightPar")
                    {
                        semanticError("Expresión para el tipo de variable declarada incorrecta");
                        check = false;
                        break;
                    }
                    else if (iT.id == "UserDefined")
                    {
                        foreach (simbols abc in simbolsTable)
                        {
                            if (abc.id == iT.myValue)
                            {
                                if (abc.zone != zone && abc.zone != "global")
                                {
                                    semanticError("La variable " + iT.myValue + " no ha sido declarada", iT);
                                    check = false;
                                    break;
                                }
                                else if (abc.type != "integer" && abc.type != "float")
                                {
                                    semanticError("La variable " + iT.myValue + " no es del tipo correcto", iT);
                                    check = false;
                                    break;
                                }
                                else
                                {
                                    check = true;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                check = false;
            }
            return check;
        }

        private static void semanticError()
        {
            tokensRemoveEOL1();
            errors newError = new errors();
            newError.line = myTokens.First.Value.noLinea;
            newError.type = "Semantic";
            newError.aprox = myTokens.First.Value.myValue;
            errorsTable.AddLast(newError);
            myTokens.RemoveFirst();
        }

        private static void semanticError(string error)
        {
            tokensRemoveEOL1();
            errors newError = new errors();
            newError.line = myTokens.First.Value.noLinea;
            newError.type = error;
            newError.aprox = myTokens.First.Value.myValue;
            errorsTable.AddLast(newError);
            myTokens.RemoveFirst();
        }

        private static void semanticError(string error, tokens near)
        {
            errors newError = new errors();
            newError.line = near.noLinea;
            newError.type = error;
            newError.aprox = myTokens.First.Value.myValue;
            errorsTable.AddLast(newError);
            myTokens.RemoveFirst();
            tokensRemoveEOL1();
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
            public LinkedList<tokens> value;
            public string type;
            public int lastLine;
            public string id;
            public string zone;
        }

        /*
         * Estructura para almacenar información
         * de los errores en el codigo
         */
        private struct errors
        {
            public string type;
            public string aprox;
            public int line;
        }
    }
}
