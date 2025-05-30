using Core.Parser.Interfaces.AST;
using System;

namespace Core.Parser.AST.Nodes.LiteralNodes;

/// <summary>
/// Represents a string literal value in the Abstract Syntax Tree (AST).
/// Example: The '"Hello, World"' in "написать "Hello, World";".
/// </summary>
public class StringLiteralNode(string value) : IAstNode
{
    /// <summary>
    /// Gets the string value of the literal.
    /// </summary>
    public string Value { get; } = value;

    /// <inheritdoc/>
    public string ToDebugString()
    {
        return $"StringLiteralNode: Value=\"{Value}\"";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}