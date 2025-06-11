namespace Core.Parser.Tokens;

/// <summary>
/// Enum for all the token types pseudo language has
/// </summary>
public enum TokenType
{
    Eof, // End of file
    Semicolon,

    ProgramBegin, // начало
    ProgramEnd, // конец

    IntegerType, // цел
    DoubleType, // плав
    StringType, // строка
    VariableName,

    // Types of the variable itself 
    Integer, 
    Double,
    String,

    Add, // +
    Decrement, // -
    Multiply, // *
    Divide, // /
    Assign, // =
    Equals, // ==

    If, // если
    ControlBegin, // то
    Else, // иначе
    ControlEnd, // кесли

    LoopBegin, // нц
    LoopTimes, // i раз
    LoopEnd, // кц
    While,

    Return, // вернуть

    Write, // написать
    Read, // прочитать
}