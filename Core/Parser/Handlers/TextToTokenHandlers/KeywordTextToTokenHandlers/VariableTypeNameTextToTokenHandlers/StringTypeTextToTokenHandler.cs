using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.VariableTypeNameTextToTokenHandlers;

public class StringTypeKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "строка";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.StringType, word);
}