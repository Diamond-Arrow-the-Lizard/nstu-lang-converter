using Core.Parser.AST.Nodes; 

namespace Core.Parser.Interfaces.AST;

/// <summary>
/// Defines the contract for a parser that transforms a stream of tokens into an Abstract Syntax Tree (AST).
/// </summary>
public interface IParser
{
    /// <summary>
    /// Parses the token stream and constructs the Abstract Syntax Tree.
    /// </summary>
    /// <returns>The root node of the constructed AST, which is a ProgramNode.</returns>
    ProgramNode Parse();
}