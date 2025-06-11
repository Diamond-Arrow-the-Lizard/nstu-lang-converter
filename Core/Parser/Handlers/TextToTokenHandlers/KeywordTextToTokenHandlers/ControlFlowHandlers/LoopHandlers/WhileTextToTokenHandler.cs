using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.LoopHandlers;

public class WhileTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "пока";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.While, word);
}
