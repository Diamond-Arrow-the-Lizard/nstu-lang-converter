using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.LoopHandlers;

public class LoopEndKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "кц";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.LoopEnd, word);
}