
using Core.Parser.Handlers.TextToTokenHandlers;
using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Models;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Interfaces.Services;
using Core.Parser.Models;
using Core.Parser.Repositories;
using Core.Parser.Services;
using Core.Parser.Tokens;
using System;

namespace CLI;

public static class CLI
{
    public static void Main()
    {
        List<ITextToTokenHandler> handlers =
        [
            new IntegerTextToTokenHandler(),
            new KeywordTextToTokenHandler(),
            new OperationTextToTokenHandler(),
            new StringTextToTokenHandler(),
        ];
        TokenService tokenService = new();
        TokenRepository tokenRepository = new(tokenService);
        StringParser parser = new(tokenRepository, handlers);

        string textToParse = "1 + 1 нц кц если 1 + 1 == 2 то кесли \"Hello, World\";".TrimEnd();

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