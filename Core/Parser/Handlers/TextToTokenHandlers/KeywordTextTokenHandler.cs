using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Keywords;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TextToTokenHandlers;

/// <summary>
/// Handles reserved keyword tokenization
/// </summary>
public class KeywordTextToTokenHandler : ITextToTokenHandler
{
    private readonly ReservedKeywords _keywords = new();
    public bool CanHandle(string word) => _keywords.List.Contains(word);
    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Keyword, word);
}