using Core.Parser.Interfaces.AST;
using System;

namespace Core.Parser.AST.Nodes.LiteralNodes;

/// <summary>
/// Represents a double literal value in the Abstract Syntax Tree (AST).
/// Example: The '3.14' in "плав Pi = 3.14;".
/// </summary>
public class DoubleLiteralNode(double value) : IAstNode
{
    /// <summary>
    /// Gets the double value of the literal.
    /// </summary>
    public double Value { get; } = value;

    /// <inheritdoc/>
    public string ToDebugString()
    {
        return $"DoubleLiteralNode: Value={Value}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}