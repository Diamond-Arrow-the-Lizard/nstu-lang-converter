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
    /// Compare the current token type with the passed token type. 
    /// If they match then "eat" the current token and assign the next token to the current one.
    /// </summary>
    /// <param name="tokenType"> Current token type </param>
    void EatToken(TokenType tokenType);

    /// <summary>
    /// Create tokenized expression
    /// </summary>
    void MakeTokenizedExpression();
}