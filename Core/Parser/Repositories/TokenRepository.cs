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
    public IToken GetTokenByRepresentation(string representation)
    {
        throw new NotImplementedException(nameof(GetTokenByRepresentation));
    }

    /// <inheritdoc/>
    public IToken GetTokenByType(TokenType tokenType)
    {
        throw new NotImplementedException(nameof(GetTokenByType));
    }

    /// <inheritdoc/>
    public void RemoveTokenByRepresentation(string representation)
    {
        throw new NotImplementedException(nameof(RemoveTokenByRepresentation));
    }

    /// <inheritdoc/>
    public void RemoveTokenByType(TokenType tokenType)
    {
        throw new NotImplementedException(nameof(RemoveTokenByType));
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