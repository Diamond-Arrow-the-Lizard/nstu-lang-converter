using Core.Parser.Interfaces.AST;

namespace Core.Parser.AST.Nodes.StatementNodes;

/// <summary>
/// Represents a 'write' (output) statement in the Abstract Syntax Tree (AST).
/// Example: "написать "Hello, World";" or "написать X + 1;".
/// </summary>
public class WriteStatementNode : IAstNode
{
    /// <summary>
    /// Gets the expression whose value is to be written to the output.
    /// </summary>
    public IAstNode Expression { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WriteStatementNode"/> class.
    /// </summary>
    /// <param name="expression">The expression to write.</param>
    public WriteStatementNode(IAstNode expression)
    {
        Expression = expression ?? throw new ArgumentNullException(nameof(expression));
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        return $"WriteStatementNode: Expression={Expression.ToDebugString()}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}