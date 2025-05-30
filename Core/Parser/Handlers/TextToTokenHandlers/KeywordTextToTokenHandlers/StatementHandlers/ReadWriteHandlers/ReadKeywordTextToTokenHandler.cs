using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.StatementHandlers.ReadWriteHandlers;

public class ReadKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "прочитать";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Read, word);
}