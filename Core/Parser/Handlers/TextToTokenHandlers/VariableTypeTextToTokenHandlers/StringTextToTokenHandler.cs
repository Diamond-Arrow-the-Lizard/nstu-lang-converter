using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.VariableTypeTextToTokenHandlers;

/// <summary>
/// Handles string tokenization
/// </summary>
public class StringTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word.StartsWith('"') && word.EndsWith('"');
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.String, word);
}