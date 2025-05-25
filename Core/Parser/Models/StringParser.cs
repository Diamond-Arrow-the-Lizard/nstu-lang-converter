
using Core.Parser.Interfaces.Models;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Interfaces.Services;
using Core.Parser.Tokens;
using Core.Parser.Interfaces.Handlers;
using System.Text.RegularExpressions;

namespace Core.Parser.Models;

/// <summary>
/// Turns text into tokens for an interpreter to understand
/// </summary>
public class StringParser(ITokenRepository tokenRepository, IEnumerable<ITextToTokenHandler> TextToTokenHandlers) : IStringParser
{
    private string _text = "";
    private readonly ITokenRepository _tokenRepository = tokenRepository;
    private readonly List<ITextToTokenHandler> _TextToTokenHandlers = TextToTokenHandlers.ToList();

    public void SetText(string text) => _text = text;

    public void MakeTokenizedExpression()
    {
        foreach (var token in SplitIntoTokens())
        {
            //Console.WriteLine($"Parsing {token}");

            bool handled = TryTokenize(token);

            if (!handled && token.Contains(';'))
            {
                //Console.WriteLine("EOF found");

                string noEofToken = token.Remove(token.IndexOf(';'));
                handled = TryTokenize(noEofToken);

                //Console.WriteLine("Pre EOF text tokenized");

                _tokenRepository.AddToken(TokenType.Eof, ";");
                handled = true;
            }

            if (!handled)
            {
                _tokenRepository.AddToken(TokenType.VariableName, token);
            }

            //Console.WriteLine($"{token} Parsed");
        }

    }

    /// <summary>
    /// Tokenizes an expression if possible
    /// </summary>
    /// <param name="token"> Text to tokenize </param>
    /// <returns> true if successfully hancled </returns>
    private bool TryTokenize(string token)
    {
        foreach (var handler in _TextToTokenHandlers)
        {
            if (handler.CanHandle(token))
            {
                handler.Handle(token, _tokenRepository);
                return true;
            }
        }
        return false;
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