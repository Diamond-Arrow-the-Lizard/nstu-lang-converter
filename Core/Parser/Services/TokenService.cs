using Core.Parser.Interfaces.Models;
using Core.Parser.Interfaces.Services;
using Core.Parser.Models;
using Core.Parser.Tokens;

namespace Core.Parser.Services;

public class TokenService : ITokenService
{
    /// <inheritdoc/>
    public IToken CreateToken(TokenType tokenType, string representation) => new Token(tokenType, representation);

    /// <inheritdoc/>
    public IToken ChangeToken(TokenType tokenType, string representation) => CreateToken(tokenType, representation);
    
}