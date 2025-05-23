using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers;

public class ControlEndKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "кесли";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.ControlEnd, word);
}