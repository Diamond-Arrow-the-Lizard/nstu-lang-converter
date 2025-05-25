using Core.Parser.Interfaces.Models;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Interfaces.Services;
using Core.Parser.Tokens;

namespace Core.Parser.Repositories;

/// <inheritdoc/>
public class TokenRepository(ITokenService tokenService) : ITokenRepository
{
    private readonly ITokenService _tokenService = tokenService;
    private readonly List<IToken> _tokens = [];

    /// <inheritdoc/>
    public List<IToken> GetAllTokens()
    {
        return _tokens;
    }

    /// <inheritdoc/>
    public IToken GetTokenByRepresentation(string representation) => _tokens.First(item => item.Representation == representation);

    /// <inheritdoc/>
    public IToken GetTokenByType(TokenType tokenType) => _tokens.First(item => item.TokenType == tokenType);

    /// <inheritdoc/>
    public void RemoveTokenByRepresentation(string representation)
    {
        var removeIndex = _tokens.FindIndex(item => item.Representation == representation);
        _tokens.RemoveAt(removeIndex);
    }

    /// <inheritdoc/>
    public void RemoveTokenByType(TokenType tokenType)
    {
        var removeIndex = _tokens.FindIndex(item => item.TokenType == tokenType);
        _tokens.RemoveAt(removeIndex);
    }

    /// <inheritdoc/>
    public void AddToken(IToken newToken)
    {
        _tokens.Add(newToken);
    }

    /// <inheritdoc/>
    public void AddToken(TokenType tokenType, string representation)
    {
        _tokens.Add(_tokenService.CreateToken(tokenType, representation));
    }

}