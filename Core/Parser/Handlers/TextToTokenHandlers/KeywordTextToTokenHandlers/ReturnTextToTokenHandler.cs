using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers;

public class ReturnKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "вернуть";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Return, word);
}