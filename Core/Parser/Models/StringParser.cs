
using Core.Parser.Interfaces.Models;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Interfaces.Services;
using Core.Parser.Tokens;
using Core.Parser.Keywords;
using Core.Parser.Interfaces.Handlers;
using System.Text.RegularExpressions;

namespace Core.Parser.Models;

/// <summary>
/// Turns text into tokens for an interpreter to understand
/// </summary>
public class StringParser(ITokenRepository tokenRepository, IEnumerable<ITokenHandler> tokenHandlers) : IStringParser
{
    private string _text = "";
    private readonly ITokenRepository _tokenRepository = tokenRepository;
    private readonly List<ITokenHandler> _tokenHandlers = tokenHandlers.ToList();

    public void SetText(string text) => _text = text;

    public void MakeTokenizedExpression()
    {
        foreach (var token in SplitIntoTokens())
        {
            Console.WriteLine($"Parsing {token}");

            bool handled = false;
            foreach (var handler in _tokenHandlers)
            {
                if (handler.CanHandle(token))
                {
                    handler.Handle(token, _tokenRepository);
                    handled = true;
                    break;
                }
            }

            if (!handled && token == ";")
            {
                _tokenRepository.AddToken(TokenType.Eof, ";");
                Console.WriteLine("EOF found");
                handled = true;
            }

            if (!handled)
                throw new InvalidDataException($"Cannot parse '{token}'");

            Console.WriteLine($"{token} Parsed");
        }

    }

    /// <summary>
    /// Properly format text into tokens (i.e. to not fail at parsing "Hello, world" since it's space-separated)
    /// </summary>
    /// <returns> String valid for parsing </returns>
    private IEnumerable<string> SplitIntoTokens()
    {
        // Split on spaces, but preserve quoted strings as single tokens
        var regex = new Regex(@"("".*?""|\S+)"); // Regex magic
        foreach (Match match in regex.Matches(_text))
        {
            yield return match.Value.Trim(); // Trim to handle trailing spaces
        }
    }
}