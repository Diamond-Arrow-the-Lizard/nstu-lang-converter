using Converter.Core.Parser.Tokens;

namespace Converter.Core.Parser.Interfaces;

public interface IToken
{
    TokenType TokenType { get; }
    string Representation { get; }
}