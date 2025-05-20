using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Models;

/// <summary>
/// Interface for pseudo language tokens and their human-readable representation
/// </summary>
public interface IToken
{
    TokenType TokenType { get; }
    string Representation { get; }

}