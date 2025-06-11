using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers.OperationTextToTokenHandlers;

public class MoreOperationTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => word == ">";

    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.More, word);

}