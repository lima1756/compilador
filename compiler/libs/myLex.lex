%using SimpleScript.RunTime;

%namespace SimpleScript.Analyzing

%option stack, minimize, parser, verbose, persistbuffer, unicode, compressNext, embedbuffers

%{
public void yyerror(string format, params object[] args) // remember to add override back
{
	System.Console.Error.WriteLine("Error: linea {0} - " + format, yyline);
}
%}
// Comentarios
CommentStart	'
CommentEnd	'


//Definiciones
D		[0-9]
AZ		[a-zA-Z]
AZex 		[a-zA-Z0-9_]+
//Literals
Identifier		\$([a-zA-Z]([a-zA-Z0-9_])*)
IntegerLiteral	{D}+
DoubleLiteral	{D}+(\.{D}+)?
StringLiteral	\u00B4.*\u00B4
CharLiteral		\u00B4.\u00B4
Comma			,

//Espacios y fin de linea
WhiteSpace		[ \t]
Eol				(\.)


Int			intel
Double		flot
Char		car 
String		string
OpAssign	=
OpAdd		+
OpMinus		"-"
OpMul		"*"
OpDiv		"/"
LeftPar		"("
RigthPar	")"
LeftBrack	"{"
RightBrack	"}"
OpAnd		"&&"
OpOr		"||"
OpNot		"!"
OpEqu		"=="
OpNotEqu	"!="
OpLt		"<"
OpGt		">"
OpGtEq		">="
OpLtEq		"<="
Input		escanear
Print		estampa
For			por
If			zy
Else		tons
While		mentre
Do			to

// Estados donde el AEF puede pasar por alto
%x CMMT		// Dentro de un comentario.
%x CMMT2	// Dentro de un comentario.
%%

//
// Inicio de reglas (regex)
//


// Eliminar espacios en blanco.
{WhiteSpace}+	{ ; }

// Fin de linea
{Eol}+		{ return (int) Tokens.EOL; }

// Ignorar las siguientes lineas 
<CMMT2>{
{Eol} { yy_pop_state ();}
}

/* Cambiar al estado de 'Comentario' cuando encuentre uno. */
{CommentStart}					{  yy_push_state (CMMT); }//return (int) Tokens.COMMENT; }

// Dentro de un bloque de comentarios:.
<CMMT>{
	[^*\n]+				{return (int) Tokens.COMMENT; }
	"*"					{return (int) Tokens.COMMENT; }
	{CommentEnd}		{ yy_pop_state(); return (int) Tokens.COMMENT; }
	<<EOF>>					{ ; /* Generar error. */ }
}

{Identifier}				{ yylval.String = yytext.Substring(1);
						   return (int) Tokens.IDENTIFIER; }

{IntegerLiteral}					{ Int64.TryParse (yytext, NumberStyles.Integer, CultureInfo.CurrentCulture, out yylval.Integer);
						   return (int) Tokens.INTEGER_LITERAL; }

{DoubleLiteral}					{ double.TryParse (yytext, NumberStyles.Float, CultureInfo.CurrentCulture, out yylval.Double); 
						   return (int) Tokens.DOUBLE_LITERAL; }

{StringLiteral}					{ if (yytext.Length > 2) { yylval.String = yytext.Substring(1, yytext.Length - 2); }
									else { yylval.String = ""; }
								return (int) Tokens.STRING_LITERAL; }

{CharLiteral}					{ if (yytext.Length == 1) { yylval.String = yytext.Substring(1, yytext.Length - 2); }
									else { yylval.String = ""; }
								return (int) Tokens.CHAR_LITERAL; }


{OpAssign}	{ return (int) Tokens.OP_ASSIGN; }
{OpAdd}		{ return (int) Tokens.OP_ADD; }
{OpMinus}	{ return (int) Tokens.OP_MINUS; }
{OpMul}		{ return (int) Tokens.OP_MUL; }
{OpDiv}		{ return (int) Tokens.OP_DIV; }
{LeftPar}	{ return (int) Tokens.OP_LEFT_PAR; }
{RigthPar}	{ return (int) Tokens.OP_RIGHT_PAR; }
{LeftBrack}	{ return (int) Tokens.OP_LEFT_BRACK; }
{RightBrack}	{ return (int) Tokens.OP_RIGHT_BRACK; }
{OpAnd}		{ return (int) Tokens.OP_AND; }
{OpOr}		{ return (int) Tokens.OP_OR; }
{OpNot}		{ return (int) Tokens.OP_NOT; }
{OpEqu}		{ return (int) Tokens.OP_EQU; }
{OpNotEqu}	{ return (int) Tokens.OP_NOT_EQU; }
{OpLt}		{ return (int) Tokens.OP_LT; }
{OpGt}		{ return (int) Tokens.OP_GT; }
{OpGtEq}	{ return (int) Tokens.OP_GT_EQ; }
{OpLtEq}	{ return (int) Tokens.OP_LT_EQ; }

{Int}		{ return (int) Tokens.INT; }
{String}	{ return (int) Tokens.STRING; }
{Char}		{ return (int) Tokens.CHAR; }
{Double}	{ return (int) Tokens.DOUBLE	; }

{Input}		{ return (int) Tokens.INPUT; }
{Print}		{ return (int) Tokens.PRINT; }
{For}		{ return (int) Tokens.FOR; }
{If}		{ return (int) Tokens.IF; }
{Else}		{ return (int) Tokens.ELSE; }
{While}		{ return (int) Tokens.WHILE; }
{Do}		{ return (int) Tokens.DO; }
{Comma}		{ return (int) Tokens.COMMA; }