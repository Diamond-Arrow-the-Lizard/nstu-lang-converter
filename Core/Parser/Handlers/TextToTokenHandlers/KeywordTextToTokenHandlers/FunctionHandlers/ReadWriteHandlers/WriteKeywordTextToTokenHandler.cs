using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.FunctionHandlers.ReadWriteHandlers;

public class WriteKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "написать";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Write, word);
}