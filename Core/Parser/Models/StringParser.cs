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

            if (!handled) 
            {
                // This 'else' branch handles cases where a token might be a VariableName
                // after special characters have been separated by the regex.
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _tokenRepository.AddToken(TokenType.VariableName, token);
                }
            }
        }
        if (_tokenRepository.GetAllTokens().LastOrDefault()?.TokenType != TokenType.Eof)
        {
            _tokenRepository.AddToken(TokenType.Eof, ""); 
        }
    }

    /// <summary>
    /// Tokenizes an expression if possible
    /// </summary>
    /// <param name="token"> Text to tokenize </param>
    /// <returns> true if successfully hancled </returns>
    private bool TryTokenize(string token)
    {
        if (token == ";")
        {
            _tokenRepository.AddToken(TokenType.Semicolon, token); 
            return true;
        }
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
        // Regex to split the input string into meaningful tokens.
        // It prioritizes:
        // 1. Quoted strings (e.g., "Hello world")
        // 2. Multi-character operators (e.g., "==")
        // 3. Numbers (integers and doubles) using \b\d+(\.\d+)?\b for precise matching
        // 4. Word characters (for keywords and variable names) using \b\w+\b
        // 5. Single-character operators and delimiters (e.g., +, -, *, /, =, ;, {, })
        // It specifically ignores whitespace.
        var regex = new Regex(@"""[^""]*""|==|\b\d+(\.\d+)?\b|\b\w+\b|[+\-*/=;{}]");
        
        foreach (Match match in regex.Matches(_text))
        {
            // Only yield non-empty, non-whitespace matches
            if (!string.IsNullOrWhiteSpace(match.Value) && !char.IsWhiteSpace(match.Value[0]))
            {
                yield return match.Value; 
            }
        }
    }
}