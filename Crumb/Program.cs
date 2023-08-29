﻿using Crumb.Core.Lexing;
using Crumb.Core.Parsing;

var debug = false;

const string MISSING_FILE = "Error: Supply file path to read from.";

var fileArg = 0;

if (args.Length > 0 && args[0] == "-d")
{
    debug = true;
    fileArg = 1;
}

if (args.Length < fileArg + 1)
{
    Console.Error.WriteLine(MISSING_FILE);
    Environment.Exit(1);
}

var code = File.ReadAllText(args[fileArg]);

if (debug)
{
    Console.WriteLine();
    Console.WriteLine($"CODE");
    Console.WriteLine(code);
}

var tokens = Lexer.Lex(code);

if (debug)
{
    Console.WriteLine();
    Console.WriteLine($"TOKENS");
    Token.Print(tokens);
    Console.WriteLine($"Token Count: {tokens.Count}");
}

var program = new List<AstNode>();

try
{
    program.AddRange(Parser.Parse(tokens));
}
catch (ParsingException e)
{
    Console.Error.WriteLine(e.Message);
    Environment.Exit(1);
}

if (debug)
{
    Console.WriteLine();
    Console.WriteLine($"AST");
    Console.WriteLine(AstNode.Print(program));
}