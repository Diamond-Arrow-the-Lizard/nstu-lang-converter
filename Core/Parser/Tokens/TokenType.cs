namespace Core.Parser.Tokens;

/// <summary>
/// Enum for all the token types pseudo language has
/// </summary>
public enum TokenType
{
    Eof, // End of file, a.k.a. ';'

    ProgramBegin,
    ProgramEnd,

    IntegerType,
    DoubleType,
    StringType,
    VariableName,

    Integer, 
    Double,
    Bool,
    String,

    Add, // +
    Decrement, // -
    Multiply, // *
    Divide, // /
    Assign, // =
    Equals, // ==

    ControlBegin, // a.k.a. '{'
    If,
    Else,
    ControlEnd, // a.k.a. '}'

    LoopBegin, 
    LoopTimes,
    LoopEnd,

    Return,

    Write,
    Read,
}