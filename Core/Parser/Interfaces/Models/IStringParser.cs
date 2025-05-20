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
    /// Create tokenized expression
    /// </summary>
    void MakeTokenizedExpression();
}