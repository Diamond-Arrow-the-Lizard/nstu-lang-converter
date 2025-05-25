using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers;

/// <summary>
/// Handles program beginning tokenization
/// </summary>
public class ProgramEndTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "конец";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.ProgramEnd, word);
}