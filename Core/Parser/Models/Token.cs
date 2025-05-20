using Core.Parser.Interfaces.Models;
using Core.Parser.Tokens;

namespace Core.Parser.Models;

/// <summary>
/// Model of pseudo language token and its human-readable representation
/// </summary>
public class Token(TokenType tokenType, string representation) : IToken
{
    public TokenType TokenType { get; private set; } = tokenType;
    public string Representation { get; private set; } = representation;
}