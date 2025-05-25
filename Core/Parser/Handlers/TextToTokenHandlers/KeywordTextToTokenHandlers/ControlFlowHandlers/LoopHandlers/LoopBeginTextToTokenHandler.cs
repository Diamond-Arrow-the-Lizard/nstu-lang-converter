using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.LoopHandlers;

public class LoopBeginKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "нц";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.LoopBegin, word);
}