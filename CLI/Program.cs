
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
        ITokenService tokenService = new TokenService();
        ITokenRepository tokenRepository = new TokenRepository(tokenService);
        IStringParser parser = new StringParser(tokenRepository);

        string textToParse = "1 + 1 \"Hello, World\";".TrimEnd();

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