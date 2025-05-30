using Core.Parser.Interfaces.AST;

namespace Core.Parser.AST.Nodes;

/// <summary>
/// Represents a return in the Abstract Syntax Tree (AST).
/// Example: "вернуть 0;" or "вернуть X + Y;".
/// </summary>
public class ReturnNode : IAstNode
{
    /// <summary>
    /// Gets the expression whose value is to be returned.
    /// This can be null if the return statement doesn't specify a value (e.g., "вернуть;").
    /// </summary>
    public IAstNode? Expression { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReturnNode"/> class.
    /// </summary>
    /// <param name="expression">The optional expression to return.</param>
    public ReturnNode(IAstNode? expression = null)
    {
        Expression = expression;
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        if (Expression != null)
        {
            return $"ReturnStatementNode: Expression={Expression.ToDebugString()}";
        }
        else
        {
            return "ReturnStatementNode (void)";
        }
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}