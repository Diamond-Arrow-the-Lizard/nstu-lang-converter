using Core.Parser.Interfaces.AST;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Core.Parser.AST.Nodes.ControlFlowNodes.LoopControlFlowNodes;

/// <summary>
/// Represents a loop control flow statement in the Abstract Syntax Tree (AST).
/// Example: "нц 5 раз <statements> кц".
/// </summary>
public class LoopControlFlowNode : IAstNode
{
    /// <summary>
    /// Gets the expression that determines how many times the loop should run.
    /// This is typically an integer literal or a variable reference.
    /// </summary>
    public IAstNode LoopCountExpression { get; }

    /// <summary>
    /// Gets the list of statements within the loop body.
    /// </summary>
    public List<IAstNode> Body { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoopControlFlowNode"/> class.
    /// </summary>
    /// <param name="loopCountExpression">The expression defining the number of loop iterations.</param>
    /// <param name="body">The list of statements within the loop body.</param>
    public LoopControlFlowNode(IAstNode loopCountExpression, List<IAstNode> body)
    {
        LoopCountExpression = loopCountExpression ?? throw new ArgumentNullException(nameof(loopCountExpression));
        Body = body ?? new List<IAstNode>();
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        var bodyStrings = Body.Select(s => s.ToDebugString()).ToList();
        var indentedBody = string.Join("\n", bodyStrings.Select(s => $"    {s}")); // 4 spaces indent for body
        return $"LoopControlFlowNode (Times: {LoopCountExpression.ToDebugString()}):\n{indentedBody}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}