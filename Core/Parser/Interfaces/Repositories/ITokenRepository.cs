using Core.Parser.Interfaces.Models;
using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Repositories;

public interface ITokenRepository
{
    List<IToken> GetAllTokens();
    IToken GetTokenByRepresentation(string representation);
    IToken GetTokenByType(TokenType tokenType);
    void RemoveTokenByRepresentation(string representation);
    void RemoveTokenByType(TokenType tokenType);
    void AddToken(TokenType tokenType, string representation);
    void AddToken(IToken newToken);

}