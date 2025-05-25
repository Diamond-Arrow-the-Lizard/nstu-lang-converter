using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.VariableTypeTextToTokenHandlers;

/// <summary>
/// Tokenizes integer values
/// </summary>
public class IntegerTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => int.TryParse(word, out _);

    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Integer, word);
}