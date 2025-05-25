namespace Core.Parser.Tokens;

/// <summary>
/// Enum for all the token types pseudo language has
/// </summary>
public enum TokenType
{
    Eof, // End of file, a.k.a. ';'
    ProgramBegin,
    ProgramEnd,
    Integer, 
    String,
    Add, // +
    Decrement, // -
    Multiply, // *
    Divide, // /
    Assign, // =
    Equals, // ==
    ControlBegin, // a.k.a. '{'
    ControlEnd, // a.k.a. '}'
    If,
    Else,
    LoopBegin, 
    LoopTimes,
    LoopEnd,
    Return,
    Write,
    Read,
}