using Core.Parser.Interfaces.Models;
using Core.Parser.Tokens;

namespace Core.Parser.Models;

/// <summary>
/// Model of pseudo language token and its human-readable representation
/// </summary>
public class Token: IToken
{
    public TokenType TokenType { get; private set; } = TokenType.Eof;
    public string Representation { get; private set; } = "";


    public Token(TokenType tokenType, string representation)
    {
        TokenType = tokenType;
        Representation = representation;
    }

}