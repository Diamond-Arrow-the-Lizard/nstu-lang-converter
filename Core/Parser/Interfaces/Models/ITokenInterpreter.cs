namespace Core.Parser.Interfaces.Models;

/// <summary>
/// Interface responsible for interpreting tokenized expressions
/// </summary>
public interface ITokenInterpreter
{
    void InterpretExpession(IToken[] expression);
}