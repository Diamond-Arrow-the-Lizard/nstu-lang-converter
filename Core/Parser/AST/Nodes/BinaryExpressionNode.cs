using Core.Parser.Interfaces.AST; 
using Core.Parser.Tokens;          

namespace Core.Parser.AST.Nodes;

/// <summary>
/// Represents a binary expression in the Abstract Syntax Tree (AST).
/// Examples: "1 + 1", "X == Y", "A * B".
/// </summary>
public class BinaryExpressionNode : IAstNode
{
    /// <summary>
    /// Gets the left-hand side operand of the binary expression.
    /// </summary>
    public IAstNode Left { get; }

    /// <summary>
    /// Gets the operator of the binary expression (e.g., Add, Multiply, Equals).
    /// </summary>
    public TokenType Operator { get; }

    /// <summary>
    /// Gets the right-hand side operand of the binary expression.
    /// </summary>
    public IAstNode Right { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="BinaryExpressionNode"/> class.
    /// </summary>
    /// <param name="left">The left-hand side operand.</param>
    /// <param name="operatorType">The token type representing the operator.</param>
    /// <param name="right">The right-hand side operand.</param>
    public BinaryExpressionNode(IAstNode left, TokenType operatorType, IAstNode right)
    {
        Left = left ?? throw new ArgumentNullException(nameof(left));
        Right = right ?? throw new ArgumentNullException(nameof(right));

        // Basic validation for the operator type
        if (operatorType != TokenType.Add &&
            operatorType != TokenType.Decrement && // assuming decrement can be binary, e.g., A - B
            operatorType != TokenType.Multiply &&
            operatorType != TokenType.Divide &&
            operatorType != TokenType.Assign && // Assignment can also be treated as a binary operation for AST purposes
            operatorType != TokenType.Equals)
        {
            throw new ArgumentException($"Invalid token type for binary operator: {operatorType}.");
        }
        Operator = operatorType;
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        return $"BinaryExpressionNode: ({Left.ToDebugString()} {Operator} {Right.ToDebugString()})";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}