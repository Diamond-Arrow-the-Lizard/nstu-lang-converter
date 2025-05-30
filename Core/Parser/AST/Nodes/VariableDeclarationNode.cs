using Core.Parser.Interfaces.AST;
using Core.Parser.Tokens; 

namespace Core.Parser.AST.Nodes;

/// <summary>
/// Represents a variable declaration or definition in the Abstract Syntax Tree (AST).
/// Examples: "цел X;" or "цел X = 1 + 1;".
/// </summary>
public class VariableDeclarationNode : IAstNode
{
    /// <summary>
    /// Gets the type of the variable (e.g., IntegerType, StringType).
    /// </summary>
    public TokenType VariableType { get; }

    /// <summary>
    /// Gets the name of the variable.
    /// </summary>
    public string VariableName { get; }

    /// <summary>
    /// Gets the initial value expression for the variable, if present.
    /// Null if the variable is only declared without an initial assignment.
    /// </summary>
    public IAstNode? InitialValueExpression { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="VariableDeclarationNode"/> class.
    /// </summary>
    /// <param name="variableType">The type of the variable.</param>
    /// <param name="variableName">The name of the variable.</param>
    /// <param name="initialValueExpression">The optional initial value expression.</param>
    public VariableDeclarationNode(TokenType variableType, string variableName, IAstNode? initialValueExpression = null)
    {
        if (variableType != TokenType.IntegerType &&
            variableType != TokenType.DoubleType &&
            variableType != TokenType.StringType)
        {
            throw new ArgumentException($"Invalid token type for variable declaration: {variableType}. Expected a variable type token.", nameof(variableType));
        }

        VariableType = variableType;
        VariableName = variableName ?? throw new ArgumentNullException(nameof(variableName));
        InitialValueExpression = initialValueExpression;
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this); 
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        if (InitialValueExpression != null)
        {
            return $"VariableDeclarationNode: Type={VariableType}, Name='{VariableName}', InitialValue={InitialValueExpression.ToDebugString()}";
        }
        else
        {
            return $"VariableDeclarationNode: Type={VariableType}, Name='{VariableName}'";
        }
    }
}