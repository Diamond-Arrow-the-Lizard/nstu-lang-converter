using Core.Parser.Interfaces.Models;
using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Services;

public interface ITokenService
{
    /// <summary>
    /// Method for creating a new token wihtout a constructor
    /// </summary>
    /// <param name="tokenType"></param>
    /// <param name="representation"></param>
    /// <returns> Created token </returns>
    IToken CreateToken(TokenType tokenType, string representation);
    void ChangeToken(IToken token, TokenType tokenType, string representation);
}