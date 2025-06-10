using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Models;

/// <summary>
/// Interface responsibe for turning text into tokens for an interpreter to understand.
/// </summary>
public interface IStringParser
{
    /// <summary>
    /// Set the string of text for the parser 
    /// </summary>
    /// <param name="text"> Text to interpret </param>
    void SetText(string text);

    /// <summary>
    /// Creates tokenized expression
    /// </summary>
    void MakeTokenizedExpression();

    /// <summary>
    /// Get the tokens repository from the parser explicitly
    /// </summary>
    /// <returns>Token Repository</returns>
    ITokenRepository GetTokenRepository();

    /// <summary>
    /// Clears the repository from the parser
    /// </summary>
    void ClearRepository();
}