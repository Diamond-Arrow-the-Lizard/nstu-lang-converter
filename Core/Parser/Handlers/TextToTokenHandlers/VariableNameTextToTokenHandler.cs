using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers;

/// <summary>
/// Handles variable name tokenization
/// </summary>
public class VariableNameTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => true;
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.VariableName, word);
}