using Core.Parser.Interfaces.Repositories;

namespace Core.Parser.Interfaces.Handlers;

/// <summary>
/// Interface for handling text tokenization
/// </summary>
public interface ITokenHandler
{
    /// <summary>
    /// Check if the handler can handle the given word
    /// </summary>
    /// <param name="word"> Word to tokenize </param>
    /// <returns> true if handler is valid </returns>
    bool CanHandle(string word);

    /// <summary>
    /// Handle tokenization of the word
    /// </summary>
    /// <param name="word"> Word to tokenize </param>
    /// <param name="tokenRepository"> Repository of the tokenized words </param>
    void Handle(string word, ITokenRepository tokenRepository);
}