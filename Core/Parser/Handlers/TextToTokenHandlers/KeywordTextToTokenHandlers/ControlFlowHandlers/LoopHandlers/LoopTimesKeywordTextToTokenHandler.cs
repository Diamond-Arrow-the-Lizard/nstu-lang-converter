using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.LoopHandlers;

public class LoopTimesKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "раз";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.LoopTimes, word);
}