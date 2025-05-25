using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.ControlFlowHandlers.IfElseHandlers;

public class ElseKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "иначе";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Else, word);
}