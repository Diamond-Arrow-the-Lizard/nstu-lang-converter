using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Models;

/// <summary>
/// Interface responsibe for turning text into tokens for a parser to understand.
/// </summary>
public interface IStringParser
{
    /// <summary>
    /// Get the string of text for the interpreter 
    /// </summary>
    /// <param name="text"> Text to interpret </param>
    /// <returns> Text to interpret </returns>
    string GetText(string text);

    /// <summary>
    /// This method is responsible for breaking a sentence apart into tokens. One token at a time.
    /// </summary>
    /// <returns> Next token </returns>
    IToken GetNextToken();

    /// <summary>
    /// Compare the current token type with the passed token type 
    /// If they match then "eat" the current token and assign the next token to the current one.
    /// </summary>
    /// <param name="tokenType"> Current token type </param>
    void EatToken(TokenType tokenType);

    /// <summary>
    /// Get the parsed token expression
    /// </summary>
    /// <returns> Tokenized expression </returns>
    IToken[] GetTokenizedExpression();
}