using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers;

/// <summary>
/// Handles program ending tokenization
/// </summary>
public class ProgramBeginTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "начало";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.ProgramBegin, word);
}