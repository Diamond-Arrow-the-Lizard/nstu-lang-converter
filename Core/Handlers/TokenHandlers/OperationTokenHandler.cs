using Core.Parser.Interfaces.Handlers;
using Core.Parser.Interfaces.Repositories;
using Core.Parser.Tokens;

namespace Core.Parser.Handlers.TokenHandlers;

/// <summary>
/// Handles Operation (multiplication, division, etc.) tokenization
/// </summary>
public class OperationTokenHandler : ITokenHandler
{
    /// <summary>
    /// List of valid operations
    /// </summary>
    private readonly List<string> _operations =
    [
        "+",
        "-",
        "/",
        "*",
        "=",
        "==",
    ];

    public bool CanHandle(string word) => _operations.Contains(word);

    public void Handle(string word, ITokenRepository repo) => repo.AddToken(TokenType.Operation, word);

}