using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;
using System.Globalization;

namespace Core.Parser.Handlers.TextToTokenHandlers.VariableTypeTextToTokenHandlers;

/// <summary>
/// Tokenizes double values
/// </summary>
public class DoubleTextToTokenHandler : ITextToTokenHandler
{
    public bool CanHandle(string word) => double.TryParse(word, NumberStyles.Any, CultureInfo.InvariantCulture, out _);

    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Double, word);
}