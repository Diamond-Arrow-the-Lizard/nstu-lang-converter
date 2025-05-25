
using System.Runtime.CompilerServices;
using Core.Parser.Handlers.TextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.IfElseHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.LoopHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.FunctionHandlers.ReadWriteHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.VariableTypeNameTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.OperationTextToTokenHandlers;
using Core.Parser.Handlers.TextToTokenHandlers.VariableTypeTextToTokenHandlers;
using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Models;
using Core.Parser.Models;
using Core.Parser.Repositories;
using Core.Parser.Services;

namespace CLI;

public static class CLI
{
    public static void Main()
    {
        List<ITextToTokenHandler> handlers =
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

            new ControlBeginKeywordTextToTokenHandler(),
            new ControlEndKeywordTextToTokenHandler(),
            new IfKeywordTextToTokenHandler(),
            new ElseKeywordTextToTokenHandler(),

            new LoopBeginKeywordTextToTokenHandler(),
            new LoopTimesKeywordTextToTokenHandler(),
            new LoopEndKeywordTextToTokenHandler(),

            new ReadKeywordTextToTokenHandler(),
            new WriteKeywordTextToTokenHandler(),

        ];
        TokenService tokenService = new();
        TokenRepository tokenRepository = new(tokenService);
        StringParser parser = new(tokenRepository, handlers);

        string textToParse = "начало цел X = 1 + 1 нц 5 раз кц если 1 + 1 == 2 то написать \"Афифметика верная\"; кесли иначе написать \"Hello, World\"; кесли вернуть 0; конец".TrimEnd();

        Console.WriteLine("Text to parse:");
        foreach(var i in textToParse)
        {
            Console.Write($"{i}");
        }
        Console.WriteLine();

        parser.SetText(textToParse);
        parser.MakeTokenizedExpression();

        List<IToken> result = tokenRepository.GetAllTokens();
        Console.WriteLine("Parse result:");
        foreach(var token in result)
        {
            Console.WriteLine($"Token type:{token.TokenType}, string representation: {token.Representation}");
        }

    }
}