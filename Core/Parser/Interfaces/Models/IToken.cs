using Core.Parser.Tokens;

namespace Core.Parser.Interfaces.Models;

public interface IToken
{
    TokenType TokenType { get; }
    string Representation { get; }
}