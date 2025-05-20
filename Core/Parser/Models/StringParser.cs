
using Core.Parser.Interfaces.Models;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Interfaces.Services;
using Core.Parser.Tokens;
using Core.Parser.Keywords;

namespace Core.Parser.Models;

/// <summary>
/// Turns text into tokens for an interpreter to understand
/// </summary>
public class StringParser : IStringParser
{
    private string _text = "";
    private readonly List<string> _operations = ["+", "-", "/", "*", "=", "=="];
    private readonly ITokenRepository _tokenRepository;
    private readonly ReservedKeywords _keywords = new();
    private string[] SplitText => _text.Split(' ');
    public StringParser(ITokenRepository tokenRepository) 
    {
        _tokenRepository = tokenRepository;
    }

    /// <inheritdoc/>
    public void SetText(string text) => _text = text;

    /// <inheritdoc/>
    public void EatToken(TokenType tokenType)
    {
        throw new NotImplementedException(nameof(EatToken));
    }

    /// <inheritdoc/>
    public void MakeTokenizedExpression()
    {
        foreach(var word in SplitText)
        {
            Console.WriteLine($"Parsing {word}");

            if (int.TryParse(word, out int i)) _tokenRepository.AddToken(TokenType.Integer, word);
            else if(_operations.Contains(word)) _tokenRepository.AddToken(TokenType.Operation, word);
            else if (word.Contains('"')) _tokenRepository.AddToken(TokenType.String, word);
            else if(_keywords.List.Contains(word)) _tokenRepository.AddToken(TokenType.Keyword, word);
            else throw new InvalidDataException($"Cannot parse {word}");

            if(word.EndsWith(';')) 
            {
                Console.WriteLine("EOF found");
                _tokenRepository.AddToken(TokenType.Eof, Convert.ToString(word.Last()));
            }

            Console.WriteLine($"{word} Parsed");
        }
        Console.WriteLine("Cleaning EOF from the last token");
        CleanEofToken();
    }

    /// <summary>
    /// Cleans up EOF sign from the last token's representation 
    /// </summary>
    private void CleanEofToken()
    {
        var tokens = _tokenRepository.GetAllTokens();
        foreach(var token in tokens)
        {
            if(token.TokenType != TokenType.Eof && token.Representation.Contains(';'))
            {
                _ = token.Representation.Replace(";", "");
            }
        }
    }
}