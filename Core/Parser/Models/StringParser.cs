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
            // First, try to handle the token as a whole (e.g., keywords, literals, operators, SEMICOLON)
            bool handled = TryTokenize(token);

            if (!handled) // If the token was not handled by specific handlers
            {
                // This 'else' branch handles cases where a token might be a VariableName
                // after special characters have been separated by the regex.
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _tokenRepository.AddToken(TokenType.VariableName, token);
                }
            }
        }
        // IMPORTANT: Add the true Eof token at the very end of the entire token stream
        _tokenRepository.AddToken(TokenType.Eof, ""); // Representation can be empty or "EOF"
    }

    /// <summary>
    /// Tokenizes an expression if possible
    /// </summary>
    /// <param name="token"> Text to tokenize </param>
    /// <returns> true if successfully handled </returns>
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
        // Split on spaces, but preserve quoted strings as single tokens.
        // Also ensure semicolons directly attached to words are split, but don't split within quoted strings.
        // The regex specifically captures:
        // 1. Quoted strings (e.g., "Hello world")
        // 2. Any non-whitespace, non-semicolon characters (e.g., "Num1", "написать")
        // 3. Semicolons themselves (;)
        var regex = new Regex(@"(""[^""]*""|\w+|;|\S)"); // \w+ matches word characters (letters, numbers, underscore).
                                                      // ; matches semicolon.
                                                      // \S matches any non-whitespace for other symbols like operators if they are not covered by \w+ or quotes.
        foreach (Match match in regex.Matches(_text))
        {
            // Only yield non-empty, non-whitespace matches
            if (!string.IsNullOrWhiteSpace(match.Value))
            {
                yield return match.Value; 
            }
        }
    }
}