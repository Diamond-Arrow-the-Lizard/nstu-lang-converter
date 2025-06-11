using Core.Parser.Interfaces.AST;
using System;
using System.Collections.Generic;
using System.Linq; 

namespace Core.Parser.AST.Nodes.ControlFlowNodes.LoopControlFlowNodes;

/// <summary>
/// Represents a while loop control flow statement in the Abstract Syntax Tree (AST).
/// Example: "пока <expression> нц <statements> кц".
/// </summary>
public class WhileLoopControlFlowNode : IAstNode
{
    /// <summary>
    /// Gets the expression that determines how many times the loop should run.
    /// </summary>
    public IAstNode LoopExpression { get; }

    /// <summary>
    /// Gets the list of statements within the loop body.
    /// </summary>
    public List<IAstNode> Body { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoopControlFlowNode"/> class.
    /// </summary>
    /// <param name="loopCountExpression">The expression defining the number of loop iterations.</param>
    /// <param name="body">The list of statements within the loop body.</param>
    public WhileLoopControlFlowNode(IAstNode loopExpression, List<IAstNode> body)
    {
        LoopExpression = loopExpression ?? throw new ArgumentNullException(nameof(loopExpression));
        Body = body ?? new List<IAstNode>();
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        var bodyStrings = Body.Select(s => s.ToDebugString()).ToList();
        var indentedBody = string.Join("\n", bodyStrings.Select(s => $"    {s}")); // 4 spaces indent for body
        return $"LoopControlFlowNode (Times: {LoopExpression.ToDebugString()}):\n{indentedBody}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}