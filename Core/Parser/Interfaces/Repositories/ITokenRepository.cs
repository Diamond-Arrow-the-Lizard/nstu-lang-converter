using Core.Parser.Interfaces.Models;
using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Repositories;

/// <summary>
/// Stores the tokens and gives methods to interact with them
/// </summary>
public interface ITokenRepository
{
    /// <summary>
    /// Get the full list of processed tokens
    /// </summary>
    /// <returns> The list of saved tokens </returns>
    List<IToken> GetAllTokens();

    /// <summary>
    /// Get the token by its human-readable representation
    /// </summary>
    /// <param name="representation"> Human-readable representation </param>
    /// <returns> The found token </returns>
    IToken GetTokenByRepresentation(string representation);

    /// <summary>
    /// Get the token by its type
    /// </summary>
    /// <param name="tokenType"> The type of token </param>
    /// <returns> The found token </returns>
    IToken GetTokenByType(TokenType tokenType);

    /// <summary>
    /// Remove the token by its human-readable representation
    /// </summary>
    /// <param name="representation"> Human-readable representation </param>
    void RemoveTokenByRepresentation(string representation);

    /// <summary>
    /// Remove the token by its type
    /// </summary>
    /// <param name="tokenType"> The type of token </param>
    void RemoveTokenByType(TokenType tokenType);

    /// <summary>
    /// Add new token to the repository
    /// </summary>
    /// <param name="tokenType"> The type of token </param>
    /// <param name="representation"> Human-readable representation </param>
    void AddToken(TokenType tokenType, string representation);

    /// <summary>
    /// Add new token to the repository
    /// </summary>
    /// <param name="newToken"> Token to add </param>
    void AddToken(IToken newToken);

    /// <summary>
    /// Clears the repository
    /// </summary>
    void ClearRepository();

}