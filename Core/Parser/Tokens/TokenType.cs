namespace Core.Parser.Tokens;

/// <summary>
/// Enum for all the token types pseudo language has
/// </summary>
public enum TokenType
{
    Eof, // End of file
    Integer,
    String,
    Operation,
}