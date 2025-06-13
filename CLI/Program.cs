using Microsoft.Extensions.DependencyInjection;
using Core.Parser.Handlers.TextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.IfElseHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.LoopHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.StatementHandlers.ReadWriteHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.VariableTypeNameTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.OperationTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.VariableTypeTextToTokenHandlers;
using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Models;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Interfaces.Services;
using Core.Parser.Models;
using Core.Parser.Repositories;
using Core.Parser.Services;
using Core.Parser.AST.ASTParser;
using Core.Parser.AST.Nodes;
using Core.Parser.Interfaces.AST;
using Core.Parser.CodeGenerator;
using Core.CodeSaver;
using System.Diagnostics;

namespace CLI;

public static class CLI
{
    public static void Main()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IEnumerable<ITextToTokenHandler>>(sp =>
        [
            new ProgramBeginTextToTokenHandler(),
            new ProgramEndTextToTokenHandler(),

            new IntegerTextToTokenHandler(),
            new StringTextToTokenHandler(),
            new DoubleTextToTokenHandler(),

            new DoubleTypeKeywordTextToTokenHandler(),
            new StringTypeKeywordTextToTokenHandler(),
            new IntegerTypeKeywordTextToTokenHandler(),

            new AddOperationTextToTokenHandler(),
            new AssignOperationTextToTokenHandler(),
            new DecrementOperationTextToTokenHandler(),
            new DivideOperationTextToTokenHandler(),
            new EqualsOperationTextToTokenHandler(),
            new ReturnKeywordTextToTokenHandler(),
            new MultiplyOperationTextToTokenHandler(),
            new MoreEqualsOperationTextToTokenHandler(),
            new LessEqualsOperationTextToTokenHandler(),
            new MoreOperationTextToTokenHandler(),
            new LessOperationTextToTokenHandler(),

            new ControlBeginKeywordTextToTokenHandler(),
            new ControlEndKeywordTextToTokenHandler(),
            new WhileTextToTokenHandler(),
            new IfKeywordTextToTokenHandler(),
            new ElseKeywordTextToTokenHandler(),

            new LoopBeginKeywordTextToTokenHandler(),
            new LoopTimesKeywordTextToTokenHandler(),
            new LoopEndKeywordTextToTokenHandler(),

            new ReadKeywordTextToTokenHandler(),
            new WriteKeywordTextToTokenHandler(),
            new SemicolonTextToTokenHandler(),
        ]);

        serviceCollection.AddSingleton<ITokenService, TokenService>();
        serviceCollection.AddSingleton<ITokenRepository, TokenRepository>();
        serviceCollection.AddSingleton<IStringParser, StringParser>();
        serviceCollection.AddSingleton<IParser, Parser>();
        serviceCollection.AddSingleton<IAstVisitor, CSharpCodeGeneratorVisitor>();

        var serviceProvider = serviceCollection.BuildServiceProvider();

        // The input program text
        string textToParse = @"
начало
    цел Num1 = 5;
    плав DecVal = 10.5;
    строка Message = ""Привет, мир!"";

    написать ""Начальные значения:"";
    написать Num1;
    написать DecVal;
    написать Message;

    Num1 = Num1 + 2 * 3;
    DecVal = DecVal / 2.0 - 1.5;
    написать ""Новые значения:"";
    написать Num1;
    написать DecVal;

    написать ""Введите строку Message:"";
    прочитать Message;
    написать ""Вы ввели: "";
    написать Message;

    цел Var;
    написать ""Введите целое число Var:"";
    прочитать Var;
    написать ""Var: "";
    написать Var;

    если Num1 == 11 то
        написать ""Num1 стало 11!"";
    иначе
        написать ""Num1 не 11."";
    кесли

    если Num1 <= Var то
        написать ""Num1 меньше или равно Var"";
    иначе
        написать ""Num1 больше Var"";
    кесли

    нц 2 раз
        написать ""Повтор внутри цикла!"";
        цел Counter = 1;
        Counter = Counter + 1;
    кц

    цел x = 10;
    пока x == 10 нц
        написать ""X равно 10"";
        x = 5;
    кц

    цел y = 0;
    нц
        написать ""Y равно"";
        написать y;
        y = y + 1;
    пока y < 3 кц

конец".Trim(); 

        Console.WriteLine("--- Text to parse ---");
        Console.WriteLine(textToParse);
        Console.WriteLine("---------------------\n");

        // 3. Get services and execute parsing
        var stringParser = serviceProvider.GetRequiredService<IStringParser>();
        var tokenRepository = serviceProvider.GetRequiredService<ITokenRepository>();

        stringParser.SetText(textToParse);
        stringParser.MakeTokenizedExpression();

        List<IToken> tokens = tokenRepository.GetAllTokens();
        Console.WriteLine("--- Tokenization Result ---");
        int i = 0;
        foreach (var token in tokens)
        {
            Console.WriteLine($"Position:{i}, Token type:{token.TokenType}, representation: '{token.Representation}'");
            ++i;
        }
        Console.WriteLine("---------------------------\n");

        var parser = serviceProvider.GetRequiredService<IParser>();

        Console.WriteLine("--- AST Parsing Result ---");
        try
        {
            ProgramNode ast = parser.Parse();
            Console.WriteLine("AST built successfully!");
            Console.WriteLine(ast.ToDebugString());
            Console.WriteLine("--------------------------\n");
            Console.WriteLine("--- C# Code Generation Result ---");
            var cSharpGenerator = serviceProvider.GetRequiredService<IAstVisitor>();
            ast.Accept(cSharpGenerator); 
            string generatedCSharpCode = cSharpGenerator.GetGeneratedCode();
            Console.WriteLine(generatedCSharpCode);
            Console.WriteLine("---------------------------------\n");

            bool saveSuccess = CodeSaver.SaveGeneratedCode(generatedCSharpCode, "Program.cs");
            if (saveSuccess)
            {
                Console.WriteLine("Generated C# code saved to file successfully.");
            }
            else
            {
                Console.WriteLine("Failed to save generated C# code to file.");
            }
        }
        
        catch (SyntaxException ex)
        {
            Console.WriteLine($"Syntax Error: {ex.Message}");
        }
        
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
    }
}