using System.Text;
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
    /// Gets a list of (condition, block) pairs for 'else if' clauses.
    /// </summary>
    public List<(IAstNode ElseIfCondition, List<IAstNode> ElseIfBlock)> ElseIfBlocks { get; } // NEW: List of else if clauses

    /// <summary>
    /// Gets the optional list of statements to execute if all preceding conditions are false (the final 'else' block).
    /// This can be null if there is no final 'else' part.
    /// </summary>
    public List<IAstNode>? ElseBlock { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IfElseControlFlowNode"/> class.
    /// </summary>
    /// <param name="condition">The initial condition expression.</param>
    /// <param name="thenBlock">The statements for the 'then' block.</param>
    /// <param name="elseIfBlocks">The list of 'else if' condition-block pairs.</param>
    /// <param name="elseBlock">The optional statements for the final 'else' block.</param>
    public IfElseControlFlowNode(IAstNode condition, List<IAstNode> thenBlock, List<(IAstNode ElseIfCondition, List<IAstNode> ElseIfBlock)> elseIfBlocks, List<IAstNode>? elseBlock = null)
    {
        Condition = condition ?? throw new ArgumentNullException(nameof(condition));
        ThenBlock = thenBlock ?? new List<IAstNode>();
        ElseIfBlocks = elseIfBlocks ?? new List<(IAstNode ElseIfCondition, List<IAstNode> ElseIfBlock)>(); // Initialize the new list
        ElseBlock = elseBlock; 
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        var thenStrings = ThenBlock.Select(s => s.ToDebugString()).ToList();
        var indentedThen = string.Join("\n", thenStrings.Select(s => $"    {s}")); // 4 spaces indent

        StringBuilder elseIfPart = new();
        foreach (var (elseIfCond, elseIfBlock) in ElseIfBlocks)
        {
            var elseIfBlockStrings = elseIfBlock.Select(s => s.ToDebugString()).ToList();
            var indentedElseIf = string.Join("\n", elseIfBlockStrings.Select(s => $"    {s}"));
            elseIfPart.AppendLine($"  ElseIf (Condition: {elseIfCond.ToDebugString()}):");
            elseIfPart.AppendLine($"{indentedElseIf}");
        }

        string elsePart = "";
        if (ElseBlock != null && ElseBlock.Count != 0)
        {
            var elseStrings = ElseBlock.Select(s => s.ToDebugString()).ToList();
            var indentedElse = string.Join("\n", elseStrings.Select(s => $"    {s}")); // 4 spaces indent
            elsePart = $"\n  Else:\n{indentedElse}";
        }
        else if (ElseBlock != null && ElseBlock.Count == 0)
            elsePart = $"\n  Else (empty):";


        return $"IfElseControlFlowNode (Condition: {Condition.ToDebugString()}):" +
               $"\n  Then:\n{indentedThen}" +
               (ElseIfBlocks.Count != 0 ? $"\n{elseIfPart}" : "") + // Append else if parts if they exist
               $"{elsePart}";
    }

    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}