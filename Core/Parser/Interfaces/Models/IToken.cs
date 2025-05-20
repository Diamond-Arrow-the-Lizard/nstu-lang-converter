using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Models;

/// <summary>
/// Interface for pseudo language tokens and their human-readable representation
/// </summary>
public interface IToken
{
    /// <summary>
    /// Type of the token
    /// </summary>
    /// <value> TokenType </value>
    TokenType TokenType { get; }
    
    /// <summary>
    /// Human-readable representation of the token (what the user sees)
    /// </summary>
    /// <value> string </value>
    string Representation { get; }

}