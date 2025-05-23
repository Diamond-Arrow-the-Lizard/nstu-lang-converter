using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Keywords;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers;

public class IfKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "если";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.If, word);
}