using Core.Parser.Interfaces.AST; 

namespace Core.Parser.AST.Nodes.LiteralNodes;

/// <summary>
/// Represents an integer literal value in the Abstract Syntax Tree (AST).
/// Example: The '1' in "цел X = 1;".
/// </summary>
public class IntegerLiteralNode(int value) : IAstNode
{
    /// <summary>
    /// Gets the integer value of the literal.
    /// </summary>
    public int Value { get; } = value;

    /// <inheritdoc/>
    public string ToDebugString()
    {
        return $"IntegerLiteralNode: Value={Value}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}