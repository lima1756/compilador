%using SimpleScript.RunTime;

%namespace SimpleScript.Analyzing

%{
	SymbolTable symTable = SymbolTable.GetInstance;
	public StatementList program = new StatementList();
%}

%start program

%union {
    public long Integer;
    public string String;
    public double Double;
	public char Char;
	public Expression expr;
	public StatementList statementList;
	public IStatement  statement;
}
// Defining Tokens
%token COMMENT
%token <String>	 IDENTIFIER
%token <Integer> INTEGER_LITERAL
%token <Double>	 DOUBLE_LITERAL
%token <String>	 STRING_LITERAL
%token <Char>	 CHAR_LITERAL
%token EOL
%token INT
%token STRING
%token DOUBLE
%token CHAR

// I/O Statement
%token PRINT
%token INPUT

// For statement
%token FOR

// While Statement
%token WHILE
%token DO

// If condition. 
%token IF	
%token ELSE

%token OP_RIGHT_PAR
%token OP_LEFT_PAR
%token OP_RIGHT_BRACK
%token OP_LEFT_BRACK
%token COMMA
%left OP_ASSIGN
%left OP_ADD OP_MINUS
%left OP_MUL OP_DIV 
%left OP_AND
%left OP_OR
%left OP_NOT
%left OP_EQU
%left OP_NOT_EQU
%left OP_LT
%left OP_GT
%left OP_GT_EQ
%left OP_LT_EQ


// YACC Rules
%%
program			:	statementList {program = $1.statementList;}
				;

statementList	:	/*Empty*/	{if($$.statementList == null)	{$$.statementList = new StatementList();}}

				|	statement	{	if($$.statementList == null)	{$$.statementList = new StatementList();}
									$$.statementList.InsertFront($1.statement);
									
								}
				|	statementList EOL statement	EOL { $1.statementList.Add($3.statement); $$.statementList = $1.statementList; }
				;
			

statement	:	varDecl		{ $$.statement = $1.statement; }
			|	assignOp	{ $$.statement = $1.statement; }
			|	printOp		{ $$.statement = $1.statement; }
			|	inputOp		{ $$.statement = $1.statement; }
			|	forLoop		{ $$.statement = $1.statement; }
			|	ifCond		{ $$.statement = $1.statement; }
			|	whileLoop	{ $$.statement = $1.statement; }
			;
// Variable Declaration
varDecl		:	INT IDENTIFIER		{int yId = symTable.Add($2); symTable.SetType(yId, SimpleScriptTypes.Integer); $$.statement = new VriableDeclStatement(yId);}
			|	DOUBLE IDENTIFIER	{int yId = symTable.Add($2); symTable.SetType(yId, SimpleScriptTypes.Double);  $$.statement = new VriableDeclStatement(yId);}
			|	CHAR IDENTIFIER		{int yId = symTable.Add($2); symTable.SetType(yId, SimpleScriptTypes.Char); $$.statement = new VriableDeclStatement(yId);}
			|	INT assignOp		{int yId = symTable.Add($2); symTable.SetType(yId, SimpleScriptTypes.Integer); $$.statement = new VriableDeclStatement(yId);}
			|	DOUBLE assignOp		{int yId = symTable.Add($2); symTable.SetType(yId, SimpleScriptTypes.Double);  $$.statement = new VriableDeclStatement(yId);}
			|	CHAR assignOp		{int yId = symTable.Add($2); symTable.SetType(yId, SimpleScriptTypes.Char); $$.statement = new VriableDeclStatement(yId);}
			;

			
assignOp	:	IDENTIFIER OP_ASSIGN Expr		{$$.statement = new AssignmentStatement(symTable.GetID($1), $3.expr);}
			;

//Grammmar for expressions.
//E->E+T | E-T | T 
//T->T*F | T/F | F 
//F->N | (E) | V 

/*
Expr		:	MathExpr	{ $$.expr = $1.expr; }
			//|	IDENTIFIER	{ $$.expr = new Expression(symTable.Get($1));}
			|	Literal		{ $$.expr = $1.expr; }
			;
*/
/*
Expr		:	Expr OP_ADD		Term	{ $$.expr = new Expression(Operation.Add,$1.expr,$3.expr); }
			|	Expr OP_MINUS	Term	{ $$.expr = new Expression(Operation.Sub,$1.expr,$3.expr); }	
			|	Term					{ $$.expr = $1.expr; }
			;

Term		:	Term OP_MUL Factor		{ $$.expr = new Expression(Operation.Mul,$1.expr,$3.expr); }
			|	Term OP_DIV Factor		{ $$.expr = new Expression(Operation.Div,$1.expr,$3.expr); }
			|	Factor					{ $$.expr = $1.expr; }
			;

Factor		:	Literal							{ $$.expr = $1.expr; }
			//|	OP_LEFT_PAR Expr OP_RIGHT_PAR	{ $$.expr = $2.expr; }
			|	IDENTIFIER						{ $$.expr = new Expression(symTable.Get($1));}
			;
*/		
/*NumLiteral	:	INTEGER_LITERAL	{$$.expr = new Expression($1);}
			|	DOUBLE_LITERAL	{$$.expr = new Expression($1);}		
			;
*/
Expr		:	OP_LEFT_PAR Expr OP_RIGHT_PAR		{ $$.expr = $2.expr; }
			|	Literal						{ $$.expr = $1.expr; }
			|	IDENTIFIER					{ $$.expr = new Expression(symTable.Get($1));}
			|	Expr OP_ADD Expr			{ $$.expr = new Expression(Operation.Add,$1.expr,$3.expr); }
			|	Expr OP_MINUS Expr			{ $$.expr = new Expression(Operation.Sub,$1.expr,$3.expr); }
			|	OP_MINUS Expr %prec OP_MUL	{ $$.expr = new Expression(Operation.UnaryMinus,null,$2.expr); }
			|	Expr OP_MUL Expr			{ $$.expr = new Expression(Operation.Mul,$1.expr,$3.expr); }
			|	Expr OP_DIV Expr			{ $$.expr = new Expression(Operation.Div,$1.expr,$3.expr); }
			|	Expr OP_AND Expr			{ $$.expr = new Expression(Operation.And,$1.expr,$3.expr); }		
			|	Expr OP_OR  Expr			{ $$.expr = new Expression(Operation.Or,$1.expr,$3.expr); }		
			|	Expr OP_NOT Expr			{ $$.expr = new Expression(Operation.Not,$1.expr,$3.expr); }		
			|	Expr OP_EQU Expr			{ $$.expr = new Expression(Operation.Equ,$1.expr,$3.expr); }
			|	Expr OP_NOT_EQU Expr		{ $$.expr = new Expression(Operation.NotEqu,$1.expr,$3.expr); }
			|	Expr OP_LT  Expr			{ $$.expr = new Expression(Operation.Lt,$1.expr,$3.expr); }		
			|	Expr OP_GT  Expr			{ $$.expr = new Expression(Operation.Gt,$1.expr,$3.expr); }		
			|	Expr OP_GT_EQ Expr			{ $$.expr = new Expression(Operation.GtEq,$1.expr,$3.expr); }	
			|	Expr OP_LT_EQ Expr			{ $$.expr = new Expression(Operation.LtEq,$1.expr,$3.expr); }	
			|	Expr COMMA Expr				{ $$.expr = new Expression(Operation.comma,$1.expr,$3.expr); }
			;

Literal		:	STRING_LITERAL	{$$.expr = new Expression($1);}
			|	CHAR_LITERAL	{$$.expr = new Expression($1);}
			|	INTEGER_LITERAL	{$$.expr = new Expression($1);}
			|	DOUBLE_LITERAL	{$$.expr = new Expression($1);}		
			;

printOp		:	PRINT OP_LEFT_BRACK STRING_LITERAL OP_RIGHT_BRACK	{$$.statement = new PrintStatement($3.expr);}
			|	PRINT OP_LEFT_BRACK STRING_LITERAL COMMA Expr OP_RIGHT_BRACK	{$$.statement = new PrintStatement($3.expr);}
			;

inputOp		:	printOp {$$.statement = new InputStatement(symTable.GetID($1));}
			;
			
forLoop		:	FOR OP_LEFT_BRACK OP_ASSIGN EOL Expr EOL Expr OP_RIGHT_BRACK OP_LEFT_PAR forBody OP_RIGHT_PAR  
				{$$.statement = new ForStatement(symTable.Get($2) as SymbolTableIntegerElement, $4.expr, $6.expr, $8.statementList);}
			;

forBody		:	/*Empty*/			{$$.statementList = new StatementList();}
			|	statementList EOL	{$$.statementList = $1.statementList;}
			;

ifCond		:	IF OP_LEFT_BRACK Expr OP_RIGHT_BRACK OP_LEFT_PAR ifBody OP_RIGHT_PAR else
				{$$.statement = new IfCondStatement($3.expr,$6.statementList,$8.statementList);}
			|	IF OP_LEFT_BRACK Expr OP_RIGHT_BRACK OP_LEFT_PAR ifBody OP_RIGHT_PAR
				{$$.statement = new IfCondStatement($3.expr,$6.statementList);}
			;

ifBody		:	/*Empty*/				{$$.statementList = new StatementList();}
			|	statementList EOL		{$$.statementList = $1.statementList;}
			;

else		:	/* Empty */			{$$.statementList = new StatementList();}
			|	ELSE OP_LEFT_PAR elseBody OP_RIGHT_PAR	{$$.statementList = $3.statementList;}
			;

elseBody	:	/*Empty*/			{$$.statementList = new StatementList();}
			|	statementList EOL	{$$.statementList = $1.statementList;}
			;

whileLoop	:	WHILE OP_LEFT_BRACK Expr OP_RIGHT_BRACK OP_LEFT_PAR whileBody OP_RIGHT_PAR
				{$$.statement = new WhileLoopStatement($3.expr,$7.statementList);}
			;

whileBody	:	/*Empty*/			{$$.statementList = new StatementList();}
			|	statementList EOL	{$$.statementList = $1.statementList;}
			;								
%%

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) { }