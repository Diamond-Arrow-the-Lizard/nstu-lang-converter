using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.IfElseHandlers;

public class ControlBeginKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "то";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.ControlBegin, word);
}