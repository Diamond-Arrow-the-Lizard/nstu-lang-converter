namespace Core.Parser.Tokens;

/// <summary>
/// Enum for all the token types pseudo language has
/// </summary>
public enum TokenType
{
    Eof, // End of file, a.k.a. ';'
    Integer, 
    String,
    Operation, // a.k.a. +, -, /, *, =, ==
    ControlBegin, // a.k.a. '{'
    ControlEnd, // a.k.a. '}'
    If,
    Else,
    LoopTimes, // int i = how many times
    Return,
    Write,
    Read,
    Keyword, // TODO remove this
}