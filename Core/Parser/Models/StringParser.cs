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
            // First, try to handle the token as a whole (e.g., keywords, literals, operators)
            bool handled = TryTokenize(token);

            if (!handled) // If the token was not handled by specific handlers
            {
                if (token.Contains(';'))
                {
                    // If it contains a semicolon, separate the part before the semicolon
                    string noEofToken = token.Remove(token.IndexOf(';'));
                    bool preEofHandled = TryTokenize(noEofToken); // Try to tokenize the part before ';'

                    if (!preEofHandled && !string.IsNullOrWhiteSpace(noEofToken))
                    {
                        // If the part before ';' is not handled by specific handlers,
                        // and it's not empty, assume it's a VariableName.
                        _tokenRepository.AddToken(TokenType.VariableName, noEofToken);
                    }
                    // Always add Eof for the semicolon
                    _tokenRepository.AddToken(TokenType.Eof, ";");
                }
                else
                {
                    // If no semicolon and not handled by specific handlers,
                    // and it's not empty, it must be a VariableName.
                    if (!string.IsNullOrWhiteSpace(token))
                    {
                        _tokenRepository.AddToken(TokenType.VariableName, token);
                    }
                }
            }
            // If `handled` is true from the first `TryTokenize(token)` call,
            // or if the semicolon logic took care of it, we do nothing further.
        }
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
        // This regex tries to capture:
        // 1. Quoted strings (e.g., "Hello world")
        // 2. Any non-whitespace, non-semicolon characters (e.g., "Num1")
        // 3. Semicolons themselves
        var regex = new Regex(@"(\"".*?\""|[^;\s]+|;)");
        foreach (Match match in regex.Matches(_text))
        {
            yield return match.Value.Trim(); 
        }
    }
}