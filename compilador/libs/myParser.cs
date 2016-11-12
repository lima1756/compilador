// This code was generated by the Gardens Point Parser Generator
// Copyright (c) Wayne Kelly, John Gough, QUT 2005-2014
// (see accompanying GPPGcopyright.rtf)

// GPPG version 1.5.2
// Machine:  LIMA-PC
// DateTime: 12/11/2016 04:29:15 p. m.
// UserName: Luis Iv n Morett
// Input file <myParser.y - 12/11/2016 04:20:15 p. m.>

// options: lines

using System;
using System.Collections.Generic;
using System.CodeDom.Compiler;
using System.Globalization;
using System.Text;
using QUT.Gppg;
using SimpleScript.RunTime;

namespace SimpleScript.Analyzing
{
public enum Tokens {error=2,EOF=3,COMMENT=4,IDENTIFIER=5,INTEGER_LITERAL=6,
    DOUBLE_LITERAL=7,STRING_LITERAL=8,CHAR_LITERAL=9,EOL=10,INT=11,STRING=12,
    DOUBLE=13,CHAR=14,PRINT=15,INPUT=16,FOR=17,WHILE=18,
    DO=19,IF=20,ELSE=21,OP_RIGHT_PAR=22,OP_LEFT_PAR=23,OP_RIGHT_BRACK=24,
    OP_LEFT_BRACK=25,COMMA=26,OP_ASSIGN=27,OP_ADD=28,OP_MINUS=29,OP_MUL=30,
    OP_DIV=31,OP_AND=32,OP_OR=33,OP_NOT=34,OP_EQU=35,OP_NOT_EQU=36,
    OP_LT=37,OP_GT=38,OP_GT_EQ=39,OP_LT_EQ=40};

public struct ValueType
#line 12 "myParser.y"
       {
    public long Integer;
    public string String;
    public double Double;
	public char Char;
	public Expression expr;
	public StatementList statementList;
	public IStatement  statement;
}
#line default
[GeneratedCodeAttribute( "Gardens Point Parser Generator", "1.5.2")]
public class Parser: ShiftReduceParser<ValueType, LexLocation>
{
  // Verbatim content from myParser.y - 12/11/2016 04:20:15 p. m.
#line 6 "myParser.y"
	SymbolTable symTable = SymbolTable.GetInstance;
	public StatementList program = new StatementList();
#line default
  // End verbatim content from myParser.y - 12/11/2016 04:20:15 p. m.

#pragma warning disable 649
  private static Dictionary<int, string> aliases;
#pragma warning restore 649
  private static Rule[] rules = new Rule[59];
  private static State[] states = new State[110];
  private static string[] nonTerms = new string[] {
      "program", "$accept", "statementList", "statement", "varDecl", "assignOp", 
      "printOp", "inputOp", "forLoop", "ifCond", "whileLoop", "Expr", "Literal", 
      "forBody", "ifBody", "else", "elseBody", "whileBody", };

  static Parser() {
    states[0] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,10,-3,3,-3},new int[]{-1,1,-3,3,-4,107,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[1] = new State(new int[]{3,2});
    states[2] = new State(-1);
    states[3] = new State(new int[]{10,4,3,-2});
    states[4] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98},new int[]{-4,5,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[5] = new State(-5);
    states[6] = new State(-6);
    states[7] = new State(new int[]{5,8},new int[]{-6,50});
    states[8] = new State(new int[]{27,9,10,-13,3,-13});
    states[9] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,10,-13,42});
    states[10] = new State(new int[]{28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-19,3,-19});
    states[11] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,12,-13,42});
    states[12] = new State(new int[]{28,-23,29,-23,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-23,3,-23,22,-23,24,-23});
    states[13] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,14,-13,42});
    states[14] = new State(new int[]{28,-24,29,-24,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-24,3,-24,22,-24,24,-24});
    states[15] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,16,-13,42});
    states[16] = new State(new int[]{28,-26,29,-26,30,-26,31,-26,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-26,3,-26,22,-26,24,-26});
    states[17] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,18,-13,42});
    states[18] = new State(new int[]{28,-27,29,-27,30,-27,31,-27,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-27,3,-27,22,-27,24,-27});
    states[19] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,20,-13,42});
    states[20] = new State(new int[]{28,-28,29,-28,30,-28,31,-28,32,-28,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-28,3,-28,22,-28,24,-28});
    states[21] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,22,-13,42});
    states[22] = new State(new int[]{28,-29,29,-29,30,-29,31,-29,32,-29,33,-29,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-29,3,-29,22,-29,24,-29});
    states[23] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,24,-13,42});
    states[24] = new State(new int[]{28,-30,29,-30,30,-30,31,-30,32,-30,33,-30,34,-30,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-30,3,-30,22,-30,24,-30});
    states[25] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,26,-13,42});
    states[26] = new State(new int[]{28,-31,29,-31,30,-31,31,-31,32,-31,33,-31,34,-31,35,-31,36,27,37,29,38,31,39,33,40,35,26,37,10,-31,3,-31,22,-31,24,-31});
    states[27] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,28,-13,42});
    states[28] = new State(new int[]{28,-32,29,-32,30,-32,31,-32,32,-32,33,-32,34,-32,35,-32,36,-32,37,29,38,31,39,33,40,35,26,37,10,-32,3,-32,22,-32,24,-32});
    states[29] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,30,-13,42});
    states[30] = new State(new int[]{28,-33,29,-33,30,-33,31,-33,32,-33,33,-33,34,-33,35,-33,36,-33,37,-33,38,31,39,33,40,35,26,37,10,-33,3,-33,22,-33,24,-33});
    states[31] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,32,-13,42});
    states[32] = new State(new int[]{28,-34,29,-34,30,-34,31,-34,32,-34,33,-34,34,-34,35,-34,36,-34,37,-34,38,-34,39,33,40,35,26,37,10,-34,3,-34,22,-34,24,-34});
    states[33] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,34,-13,42});
    states[34] = new State(new int[]{28,-35,29,-35,30,-35,31,-35,32,-35,33,-35,34,-35,35,-35,36,-35,37,-35,38,-35,39,-35,40,35,26,37,10,-35,3,-35,22,-35,24,-35});
    states[35] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,36,-13,42});
    states[36] = new State(new int[]{28,-36,29,-36,30,-36,31,-36,32,-36,33,-36,34,-36,35,-36,36,-36,37,-36,38,-36,39,-36,40,-36,26,37,10,-36,3,-36,22,-36,24,-36});
    states[37] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,38,-13,42});
    states[38] = new State(new int[]{28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-37,3,-37,22,-37,24,-37});
    states[39] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,40,-13,42});
    states[40] = new State(new int[]{22,41,28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37});
    states[41] = new State(-20);
    states[42] = new State(-21);
    states[43] = new State(-38);
    states[44] = new State(-39);
    states[45] = new State(-40);
    states[46] = new State(-41);
    states[47] = new State(-22);
    states[48] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,49,-13,42});
    states[49] = new State(new int[]{28,-25,29,-25,30,-25,31,-25,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37,10,-25,3,-25,22,-25,24,-25});
    states[50] = new State(-16);
    states[51] = new State(new int[]{5,52},new int[]{-6,53});
    states[52] = new State(new int[]{27,9,10,-14,3,-14});
    states[53] = new State(-17);
    states[54] = new State(new int[]{5,55},new int[]{-6,56});
    states[55] = new State(new int[]{27,9,10,-15,3,-15});
    states[56] = new State(-18);
    states[57] = new State(-7);
    states[58] = new State(new int[]{27,9});
    states[59] = new State(-8);
    states[60] = new State(new int[]{25,61});
    states[61] = new State(new int[]{8,62});
    states[62] = new State(new int[]{24,63,26,64});
    states[63] = new State(-42);
    states[64] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,65,-13,42});
    states[65] = new State(new int[]{24,66,28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37});
    states[66] = new State(-43);
    states[67] = new State(-9);
    states[68] = new State(-10);
    states[69] = new State(new int[]{25,70});
    states[70] = new State(new int[]{27,71});
    states[71] = new State(new int[]{10,72});
    states[72] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,73,-13,42});
    states[73] = new State(new int[]{10,74,28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37});
    states[74] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,75,-13,42});
    states[75] = new State(new int[]{24,76,28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37});
    states[76] = new State(new int[]{23,77});
    states[77] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-46,10,-3},new int[]{-14,78,-3,80,-4,107,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[78] = new State(new int[]{22,79});
    states[79] = new State(-45);
    states[80] = new State(new int[]{10,81});
    states[81] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-47},new int[]{-4,5,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[82] = new State(-11);
    states[83] = new State(new int[]{25,84});
    states[84] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,85,-13,42});
    states[85] = new State(new int[]{24,86,28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37});
    states[86] = new State(new int[]{23,87});
    states[87] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-50,10,-3},new int[]{-15,88,-3,108,-4,107,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[88] = new State(new int[]{22,89});
    states[89] = new State(new int[]{21,91,10,-49,3,-49},new int[]{-16,90});
    states[90] = new State(-48);
    states[91] = new State(new int[]{23,92});
    states[92] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-54,10,-3},new int[]{-17,93,-3,95,-4,107,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[93] = new State(new int[]{22,94});
    states[94] = new State(-53);
    states[95] = new State(new int[]{10,96});
    states[96] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-55},new int[]{-4,5,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[97] = new State(-12);
    states[98] = new State(new int[]{25,99});
    states[99] = new State(new int[]{23,39,8,43,9,44,6,45,7,46,5,47,29,48},new int[]{-12,100,-13,42});
    states[100] = new State(new int[]{24,101,28,11,29,13,30,15,31,17,32,19,33,21,34,23,35,25,36,27,37,29,38,31,39,33,40,35,26,37});
    states[101] = new State(new int[]{23,102});
    states[102] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-57,10,-3},new int[]{-18,103,-3,105,-4,107,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[103] = new State(new int[]{22,104});
    states[104] = new State(-56);
    states[105] = new State(new int[]{10,106});
    states[106] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-58},new int[]{-4,5,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});
    states[107] = new State(-4);
    states[108] = new State(new int[]{10,109});
    states[109] = new State(new int[]{11,7,13,51,14,54,5,58,15,60,17,69,20,83,18,98,22,-51},new int[]{-4,5,-5,6,-6,57,-7,59,-8,67,-9,68,-10,82,-11,97});

    for (int sNo = 0; sNo < states.Length; sNo++) states[sNo].number = sNo;

    rules[1] = new Rule(-2, new int[]{-1,3});
    rules[2] = new Rule(-1, new int[]{-3});
    rules[3] = new Rule(-3, new int[]{});
    rules[4] = new Rule(-3, new int[]{-4});
    rules[5] = new Rule(-3, new int[]{-3,10,-4});
    rules[6] = new Rule(-4, new int[]{-5});
    rules[7] = new Rule(-4, new int[]{-6});
    rules[8] = new Rule(-4, new int[]{-7});
    rules[9] = new Rule(-4, new int[]{-8});
    rules[10] = new Rule(-4, new int[]{-9});
    rules[11] = new Rule(-4, new int[]{-10});
    rules[12] = new Rule(-4, new int[]{-11});
    rules[13] = new Rule(-5, new int[]{11,5});
    rules[14] = new Rule(-5, new int[]{13,5});
    rules[15] = new Rule(-5, new int[]{14,5});
    rules[16] = new Rule(-5, new int[]{11,-6});
    rules[17] = new Rule(-5, new int[]{13,-6});
    rules[18] = new Rule(-5, new int[]{14,-6});
    rules[19] = new Rule(-6, new int[]{5,27,-12});
    rules[20] = new Rule(-12, new int[]{23,-12,22});
    rules[21] = new Rule(-12, new int[]{-13});
    rules[22] = new Rule(-12, new int[]{5});
    rules[23] = new Rule(-12, new int[]{-12,28,-12});
    rules[24] = new Rule(-12, new int[]{-12,29,-12});
    rules[25] = new Rule(-12, new int[]{29,-12});
    rules[26] = new Rule(-12, new int[]{-12,30,-12});
    rules[27] = new Rule(-12, new int[]{-12,31,-12});
    rules[28] = new Rule(-12, new int[]{-12,32,-12});
    rules[29] = new Rule(-12, new int[]{-12,33,-12});
    rules[30] = new Rule(-12, new int[]{-12,34,-12});
    rules[31] = new Rule(-12, new int[]{-12,35,-12});
    rules[32] = new Rule(-12, new int[]{-12,36,-12});
    rules[33] = new Rule(-12, new int[]{-12,37,-12});
    rules[34] = new Rule(-12, new int[]{-12,38,-12});
    rules[35] = new Rule(-12, new int[]{-12,39,-12});
    rules[36] = new Rule(-12, new int[]{-12,40,-12});
    rules[37] = new Rule(-12, new int[]{-12,26,-12});
    rules[38] = new Rule(-13, new int[]{8});
    rules[39] = new Rule(-13, new int[]{9});
    rules[40] = new Rule(-13, new int[]{6});
    rules[41] = new Rule(-13, new int[]{7});
    rules[42] = new Rule(-7, new int[]{15,25,8,24});
    rules[43] = new Rule(-7, new int[]{15,25,8,26,-12,24});
    rules[44] = new Rule(-8, new int[]{-7});
    rules[45] = new Rule(-9, new int[]{17,25,27,10,-12,10,-12,24,23,-14,22});
    rules[46] = new Rule(-14, new int[]{});
    rules[47] = new Rule(-14, new int[]{-3,10});
    rules[48] = new Rule(-10, new int[]{20,25,-12,24,23,-15,22,-16});
    rules[49] = new Rule(-10, new int[]{20,25,-12,24,23,-15,22});
    rules[50] = new Rule(-15, new int[]{});
    rules[51] = new Rule(-15, new int[]{-3,10});
    rules[52] = new Rule(-16, new int[]{});
    rules[53] = new Rule(-16, new int[]{21,23,-17,22});
    rules[54] = new Rule(-17, new int[]{});
    rules[55] = new Rule(-17, new int[]{-3,10});
    rules[56] = new Rule(-11, new int[]{18,25,-12,24,23,-18,22});
    rules[57] = new Rule(-18, new int[]{});
    rules[58] = new Rule(-18, new int[]{-3,10});
  }

  protected override void Initialize() {
    this.InitSpecialTokens((int)Tokens.error, (int)Tokens.EOF);
    this.InitStates(states);
    this.InitRules(rules);
    this.InitNonTerminals(nonTerms);
  }

  protected override void DoAction(int action)
  {
#pragma warning disable 162, 1522
    switch (action)
    {
      case 2: // program -> statementList
#line 70 "myParser.y"
                          {program = ValueStack[ValueStack.Depth-1].statementList;}
#line default
        break;
      case 3: // statementList -> /* empty */
#line 73 "myParser.y"
                          {if(CurrentSemanticValue.statementList == null)	{CurrentSemanticValue.statementList = new StatementList();}}
#line default
        break;
      case 4: // statementList -> statement
#line 75 "myParser.y"
                {	if(CurrentSemanticValue.statementList == null)	{CurrentSemanticValue.statementList = new StatementList();}
									CurrentSemanticValue.statementList.InsertFront(ValueStack[ValueStack.Depth-1].statement);
									
								}
#line default
        break;
      case 5: // statementList -> statementList, EOL, statement
#line 79 "myParser.y"
                                  { ValueStack[ValueStack.Depth-3].statementList.Add(ValueStack[ValueStack.Depth-1].statement); CurrentSemanticValue.statementList = ValueStack[ValueStack.Depth-3].statementList; }
#line default
        break;
      case 6: // statement -> varDecl
#line 83 "myParser.y"
                     { CurrentSemanticValue.statement = ValueStack[ValueStack.Depth-1].statement; }
#line default
        break;
      case 7: // statement -> assignOp
#line 84 "myParser.y"
              { CurrentSemanticValue.statement = ValueStack[ValueStack.Depth-1].statement; }
#line default
        break;
      case 8: // statement -> printOp
#line 85 "myParser.y"
              { CurrentSemanticValue.statement = ValueStack[ValueStack.Depth-1].statement; }
#line default
        break;
      case 9: // statement -> inputOp
#line 86 "myParser.y"
              { CurrentSemanticValue.statement = ValueStack[ValueStack.Depth-1].statement; }
#line default
        break;
      case 10: // statement -> forLoop
#line 87 "myParser.y"
              { CurrentSemanticValue.statement = ValueStack[ValueStack.Depth-1].statement; }
#line default
        break;
      case 11: // statement -> ifCond
#line 88 "myParser.y"
             { CurrentSemanticValue.statement = ValueStack[ValueStack.Depth-1].statement; }
#line default
        break;
      case 12: // statement -> whileLoop
#line 89 "myParser.y"
               { CurrentSemanticValue.statement = ValueStack[ValueStack.Depth-1].statement; }
#line default
        break;
      case 13: // varDecl -> INT, IDENTIFIER
#line 92 "myParser.y"
                           {int yId = symTable.Add(ValueStack[ValueStack.Depth-1].String); symTable.SetType(yId, SimpleScriptTypes.Integer); CurrentSemanticValue.statement = new VriableDeclStatement(yId);}
#line default
        break;
      case 14: // varDecl -> DOUBLE, IDENTIFIER
#line 93 "myParser.y"
                       {int yId = symTable.Add(ValueStack[ValueStack.Depth-1].String); symTable.SetType(yId, SimpleScriptTypes.Double);  CurrentSemanticValue.statement = new VriableDeclStatement(yId);}
#line default
        break;
      case 15: // varDecl -> CHAR, IDENTIFIER
#line 94 "myParser.y"
                      {int yId = symTable.Add(ValueStack[ValueStack.Depth-1].String); symTable.SetType(yId, SimpleScriptTypes.Char); CurrentSemanticValue.statement = new VriableDeclStatement(yId);}
#line default
        break;
      case 16: // varDecl -> INT, assignOp
#line 95 "myParser.y"
                   {int yId = symTable.Add(ValueStack[ValueStack.Depth-1]); symTable.SetType(yId, SimpleScriptTypes.Integer); CurrentSemanticValue.statement = new VriableDeclStatement(yId);}
#line default
        break;
      case 17: // varDecl -> DOUBLE, assignOp
#line 96 "myParser.y"
                      {int yId = symTable.Add(ValueStack[ValueStack.Depth-1]); symTable.SetType(yId, SimpleScriptTypes.Double);  CurrentSemanticValue.statement = new VriableDeclStatement(yId);}
#line default
        break;
      case 18: // varDecl -> CHAR, assignOp
#line 97 "myParser.y"
                    {int yId = symTable.Add(ValueStack[ValueStack.Depth-1]); symTable.SetType(yId, SimpleScriptTypes.Char); CurrentSemanticValue.statement = new VriableDeclStatement(yId);}
#line default
        break;
      case 19: // assignOp -> IDENTIFIER, OP_ASSIGN, Expr
#line 101 "myParser.y"
                                      {CurrentSemanticValue.statement = new AssignmentStatement(symTable.GetID(ValueStack[ValueStack.Depth-3].String), ValueStack[ValueStack.Depth-1].expr);}
#line default
        break;
      case 20: // Expr -> OP_LEFT_PAR, Expr, OP_RIGHT_PAR
#line 135 "myParser.y"
                                       { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-2].expr; }
#line default
        break;
      case 21: // Expr -> Literal
#line 136 "myParser.y"
                  { CurrentSemanticValue.expr = ValueStack[ValueStack.Depth-1].expr; }
#line default
        break;
      case 22: // Expr -> IDENTIFIER
#line 137 "myParser.y"
                    { CurrentSemanticValue.expr = new Expression(symTable.Get(ValueStack[ValueStack.Depth-1].String));}
#line default
        break;
      case 23: // Expr -> Expr, OP_ADD, Expr
#line 138 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Add,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 24: // Expr -> Expr, OP_MINUS, Expr
#line 139 "myParser.y"
                          { CurrentSemanticValue.expr = new Expression(Operation.Sub,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 25: // Expr -> OP_MINUS, Expr
#line 140 "myParser.y"
                                { CurrentSemanticValue.expr = new Expression(Operation.UnaryMinus,null,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 26: // Expr -> Expr, OP_MUL, Expr
#line 141 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Mul,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 27: // Expr -> Expr, OP_DIV, Expr
#line 142 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Div,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 28: // Expr -> Expr, OP_AND, Expr
#line 143 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.And,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 29: // Expr -> Expr, OP_OR, Expr
#line 144 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Or,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 30: // Expr -> Expr, OP_NOT, Expr
#line 145 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Not,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 31: // Expr -> Expr, OP_EQU, Expr
#line 146 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Equ,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 32: // Expr -> Expr, OP_NOT_EQU, Expr
#line 147 "myParser.y"
                           { CurrentSemanticValue.expr = new Expression(Operation.NotEqu,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 33: // Expr -> Expr, OP_LT, Expr
#line 148 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Lt,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 34: // Expr -> Expr, OP_GT, Expr
#line 149 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.Gt,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 35: // Expr -> Expr, OP_GT_EQ, Expr
#line 150 "myParser.y"
                          { CurrentSemanticValue.expr = new Expression(Operation.GtEq,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 36: // Expr -> Expr, OP_LT_EQ, Expr
#line 151 "myParser.y"
                          { CurrentSemanticValue.expr = new Expression(Operation.LtEq,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 37: // Expr -> Expr, COMMA, Expr
#line 152 "myParser.y"
                        { CurrentSemanticValue.expr = new Expression(Operation.comma,ValueStack[ValueStack.Depth-3].expr,ValueStack[ValueStack.Depth-1].expr); }
#line default
        break;
      case 38: // Literal -> STRING_LITERAL
#line 155 "myParser.y"
                          {CurrentSemanticValue.expr = new Expression(ValueStack[ValueStack.Depth-1].String);}
#line default
        break;
      case 39: // Literal -> CHAR_LITERAL
#line 156 "myParser.y"
                  {CurrentSemanticValue.expr = new Expression(ValueStack[ValueStack.Depth-1].Char);}
#line default
        break;
      case 40: // Literal -> INTEGER_LITERAL
#line 157 "myParser.y"
                     {CurrentSemanticValue.expr = new Expression(ValueStack[ValueStack.Depth-1].Integer);}
#line default
        break;
      case 41: // Literal -> DOUBLE_LITERAL
#line 158 "myParser.y"
                    {CurrentSemanticValue.expr = new Expression(ValueStack[ValueStack.Depth-1].Double);}
#line default
        break;
      case 42: // printOp -> PRINT, OP_LEFT_BRACK, STRING_LITERAL, OP_RIGHT_BRACK
#line 161 "myParser.y"
                                                             {CurrentSemanticValue.statement = new PrintStatement(ValueStack[ValueStack.Depth-2].String.expr);}
#line default
        break;
      case 43: // printOp -> PRINT, OP_LEFT_BRACK, STRING_LITERAL, COMMA, Expr, OP_RIGHT_BRACK
#line 162 "myParser.y"
                                                                  {CurrentSemanticValue.statement = new PrintStatement(ValueStack[ValueStack.Depth-4].String.expr);}
#line default
        break;
      case 44: // inputOp -> printOp
#line 165 "myParser.y"
                   {CurrentSemanticValue.statement = new InputStatement(symTable.GetID(ValueStack[ValueStack.Depth-1]));}
#line default
        break;
      case 45: // forLoop -> FOR, OP_LEFT_BRACK, OP_ASSIGN, EOL, Expr, EOL, Expr, OP_RIGHT_BRACK, 
               //            OP_LEFT_PAR, forBody, OP_RIGHT_PAR
#line 169 "myParser.y"
    {CurrentSemanticValue.statement = new ForStatement(symTable.Get(ValueStack[ValueStack.Depth-10]) as SymbolTableIntegerElement, ValueStack[ValueStack.Depth-8].expr, ValueStack[ValueStack.Depth-6].expr, ValueStack[ValueStack.Depth-4].statementList);}
#line default
        break;
      case 46: // forBody -> /* empty */
#line 172 "myParser.y"
                       {CurrentSemanticValue.statementList = new StatementList();}
#line default
        break;
      case 47: // forBody -> statementList, EOL
#line 173 "myParser.y"
                       {CurrentSemanticValue.statementList = ValueStack[ValueStack.Depth-2].statementList;}
#line default
        break;
      case 48: // ifCond -> IF, OP_LEFT_BRACK, Expr, OP_RIGHT_BRACK, OP_LEFT_PAR, ifBody, 
               //           OP_RIGHT_PAR, else
#line 177 "myParser.y"
    {CurrentSemanticValue.statement = new IfCondStatement(ValueStack[ValueStack.Depth-6].expr,ValueStack[ValueStack.Depth-3].statementList,ValueStack[ValueStack.Depth-1].statementList);}
#line default
        break;
      case 49: // ifCond -> IF, OP_LEFT_BRACK, Expr, OP_RIGHT_BRACK, OP_LEFT_PAR, ifBody, 
               //           OP_RIGHT_PAR
#line 179 "myParser.y"
    {CurrentSemanticValue.statement = new IfCondStatement(ValueStack[ValueStack.Depth-5].expr,ValueStack[ValueStack.Depth-2].statementList);}
#line default
        break;
      case 50: // ifBody -> /* empty */
#line 182 "myParser.y"
                       {CurrentSemanticValue.statementList = new StatementList();}
#line default
        break;
      case 51: // ifBody -> statementList, EOL
#line 183 "myParser.y"
                        {CurrentSemanticValue.statementList = ValueStack[ValueStack.Depth-2].statementList;}
#line default
        break;
      case 52: // else -> /* empty */
#line 186 "myParser.y"
                      {CurrentSemanticValue.statementList = new StatementList();}
#line default
        break;
      case 53: // else -> ELSE, OP_LEFT_PAR, elseBody, OP_RIGHT_PAR
#line 187 "myParser.y"
                                            {CurrentSemanticValue.statementList = ValueStack[ValueStack.Depth-2].statementList;}
#line default
        break;
      case 54: // elseBody -> /* empty */
#line 190 "myParser.y"
                       {CurrentSemanticValue.statementList = new StatementList();}
#line default
        break;
      case 55: // elseBody -> statementList, EOL
#line 191 "myParser.y"
                       {CurrentSemanticValue.statementList = ValueStack[ValueStack.Depth-2].statementList;}
#line default
        break;
      case 56: // whileLoop -> WHILE, OP_LEFT_BRACK, Expr, OP_RIGHT_BRACK, OP_LEFT_PAR, whileBody, 
               //              OP_RIGHT_PAR
#line 195 "myParser.y"
    {CurrentSemanticValue.statement = new WhileLoopStatement(ValueStack[ValueStack.Depth-5].expr,ValueStack[ValueStack.Depth-1].statementList);}
#line default
        break;
      case 57: // whileBody -> /* empty */
#line 198 "myParser.y"
                        {CurrentSemanticValue.statementList = new StatementList();}
#line default
        break;
      case 58: // whileBody -> statementList, EOL
#line 199 "myParser.y"
                       {CurrentSemanticValue.statementList = ValueStack[ValueStack.Depth-2].statementList;}
#line default
        break;
    }
#pragma warning restore 162, 1522
  }

  protected override string TerminalToString(int terminal)
  {
    if (aliases != null && aliases.ContainsKey(terminal))
        return aliases[terminal];
    else if (((Tokens)terminal).ToString() != terminal.ToString(CultureInfo.InvariantCulture))
        return ((Tokens)terminal).ToString();
    else
        return CharToString((char)terminal);
  }

#line 202 "myParser.y"

// No argument CTOR. By deafult Parser's ctor requires scanner as param.
public Parser(Scanner scn) : base(scn) { }
#line default
}
}
