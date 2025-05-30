using Core.Parser.Interfaces.AST;

namespace Core.Parser.AST.Nodes.StatementNodes;

/// <summary>
/// Represents a 'read' (input) statement in the Abstract Syntax Tree (AST).
/// This statement is used to read input and store it into a variable.
/// Example: "прочитать X;".
/// </summary>
public class ReadStatementNode : IAstNode
{
    /// <summary>
    /// Gets the variable reference node where the input value will be stored.
    /// </summary>
    public VariableReferenceNode TargetVariable { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="ReadStatementNode"/> class.
    /// </summary>
    /// <param name="targetVariable">The variable reference where the input will be stored.</param>
    public ReadStatementNode(VariableReferenceNode targetVariable)
    {
        TargetVariable = targetVariable ?? throw new ArgumentNullException(nameof(targetVariable));
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        return $"ReadStatementNode: TargetVariable={TargetVariable.ToDebugString()}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}