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

            new SemicolonTextToTokenHandler(),

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

            new ControlBeginKeywordTextToTokenHandler(),
            new ControlEndKeywordTextToTokenHandler(),
            new IfKeywordTextToTokenHandler(),
            new ElseKeywordTextToTokenHandler(),

            new LoopBeginKeywordTextToTokenHandler(),
            new LoopTimesKeywordTextToTokenHandler(),
            new LoopEndKeywordTextToTokenHandler(),

            new ReadKeywordTextToTokenHandler(),
            new WriteKeywordTextToTokenHandler(),
        ]);

        serviceCollection.AddSingleton<ITokenService, TokenService>();
        serviceCollection.AddSingleton<ITokenRepository, TokenRepository>();
        serviceCollection.AddSingleton<IStringParser, StringParser>();
        serviceCollection.AddSingleton<IParser, Parser>();

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();

        // 2. Comprehensive Pseudo-Language Code String
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

    прочитать Message;       
    написать ""Вы ввели: "";
    написать Message;

    если Num1 == 11 то      
        написать ""Num1 стало 11!"";
    иначе                   
        написать ""Num1 не 11."";
    кесли;                  

    нц 2 раз                
        написать ""Повтор внутри цикла!"";
        цел Counter = 1;
        Counter = Counter + 1; 
    кц;                     

    вернуть 0;              
конец
".Trim(); // Trim to remove leading/trailing whitespace from the multiline string

        Console.WriteLine("--- Text to parse ---");
        Console.WriteLine(textToParse);
        Console.WriteLine("---------------------\n");

        // 3. Get services and execute parsing
        var stringParser = serviceProvider.GetRequiredService<IStringParser>();
        var parser = serviceProvider.GetRequiredService<IParser>();
        var tokenRepository = serviceProvider.GetRequiredService<ITokenRepository>();


        stringParser.SetText(textToParse);
        stringParser.MakeTokenizedExpression();

        List<IToken> tokens = tokenRepository.GetAllTokens();
        Console.WriteLine("--- Tokenization Result ---");
        foreach(var token in tokens)
        {
            Console.WriteLine($"Token type:{token.TokenType}, representation: '{token.Representation}'");
        }
        Console.WriteLine("---------------------------\n");

        Console.WriteLine("--- AST Parsing Result ---");
        try
        {
            ProgramNode ast = parser.Parse();
            Console.WriteLine("AST built successfully!");
            // You can print the debug string of the root node to see the AST structure
            Console.WriteLine(ast.ToDebugString());
        }
        catch (SyntaxException ex)
        {
            Console.WriteLine($"Syntax Error: {ex.Message}");
        }
        /*
        catch (Exception ex)
        {
            Console.WriteLine($"An unexpected error occurred: {ex.Message}");
        }
        */
        Console.WriteLine("--------------------------\n");
        
    }
}