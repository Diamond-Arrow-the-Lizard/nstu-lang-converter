using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.KeywordTextToTokenHandlers.VariableTypeNameTextToTokenHandlers;

public class IntegerTypeKeywordTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == "цел";
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.IntegerType, word);
}