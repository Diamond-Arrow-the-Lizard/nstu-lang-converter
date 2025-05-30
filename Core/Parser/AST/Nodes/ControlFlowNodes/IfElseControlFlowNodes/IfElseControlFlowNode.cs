using Core.Parser.Interfaces.AST;

namespace Core.Parser.AST.Nodes.ControlFlowNodes.IfElseControlFlowNodes;

/// <summary>
/// Represents an if-else control flow statement in the Abstract Syntax Tree (AST).
/// Example: "если (условие) то <statements> иначе <statements> кесли".
/// </summary>
public class IfElseControlFlowNode : IAstNode
{
    /// <summary>
    /// Gets the condition expression that determines whether the 'then' or 'else' block is executed.
    /// This expression typically evaluates to a boolean value.
    /// </summary>
    public IAstNode Condition { get; }

    /// <summary>
    /// Gets the list of statements to execute if the condition is true (the 'then' block).
    /// </summary>
    public List<IAstNode> ThenBlock { get; }

    /// <summary>
    /// Gets the optional list of statements to execute if the condition is false (the 'else' block).
    /// This can be null if there is no 'else' part.
    /// </summary>
    public List<IAstNode>? ElseBlock { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IfElseControlFlowNode"/> class.
    /// </summary>
    /// <param name="condition">The condition expression.</param>
    /// <param name="thenBlock">The statements for the 'then' block.</param>
    /// <param name="elseBlock">The optional statements for the 'else' block.</param>
    public IfElseControlFlowNode(IAstNode condition, List<IAstNode> thenBlock, List<IAstNode>? elseBlock = null)
    {
        Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        ThenBlock = thenBlock ?? new List<IAstNode>();
        ElseBlock = elseBlock; 
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        var thenStrings = ThenBlock.Select(s => s.ToDebugString()).ToList();
        var indentedThen = string.Join("\n", thenStrings.Select(s => $"    {s}")); // 4 spaces indent

        string elsePart = "";
        if (ElseBlock != null && ElseBlock.Any())
        {
            var elseStrings = ElseBlock.Select(s => s.ToDebugString()).ToList();
            var indentedElse = string.Join("\n", elseStrings.Select(s => $"    {s}")); // 4 spaces indent
            elsePart = $"\n  Else:\n{indentedElse}";
        }
        else if (ElseBlock != null && !ElseBlock.Any())
            elsePart = $"\n  Else (empty):";


        return $"IfElseControlFlowNode (Condition: {Condition.ToDebugString()}):\n  Then:\n{indentedThen}{elsePart}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}