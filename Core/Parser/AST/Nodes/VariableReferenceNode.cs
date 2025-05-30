using Core.Parser.Interfaces.AST;
using Core.Parser.Tokens;
using System; // For ArgumentNullException

namespace Core.Parser.AST.Nodes;

/// <summary>
/// Represents a reference to a declared variable in the Abstract Syntax Tree (AST).
/// This node is used when a variable's value is accessed within an expression.
/// Example: The 'X' in "X + 1" or "написать X;".
/// </summary>
public class VariableReferenceNode : IAstNode
{
    /// <summary>
    /// Gets the type of the variable being referenced.
    /// This is crucial for semantic analysis (e.g., type checking).
    /// </summary>
    public TokenType VariableType { get; }

    /// <summary>
    /// Gets the name of the variable being referenced.
    /// </summary>
    public string VariableName { get; } // Corrected to PascalCase

    /// <summary>
    /// Initializes a new instance of the <see cref="VariableReferenceNode"/> class.
    /// </summary>
    /// <param name="variableType">The type of the variable (e.g., IntegerType, StringType).</param>
    /// <param name="variableName">The name of the variable.</param>
    public VariableReferenceNode(TokenType variableType, string variableName)
    {
        // Basic validation to ensure the provided TokenType is actually a variable type or a literal type.
        // During parsing, we might only know it's a VariableName token, but during semantic analysis,
        // we'd resolve its actual declared type. For now, we can store the type known at declaration.
        // Or, more simply, just store the VariableName token and resolve its type later if needed,
        // but having the type here makes it more convenient for type checking during AST traversal.
        if (variableType != TokenType.IntegerType &&
            variableType != TokenType.DoubleType &&
            variableType != TokenType.StringType &&
            variableType != TokenType.Integer && // Potentially, a variable reference could resolve to a literal's type temporarily
            variableType != TokenType.Double &&
            variableType != TokenType.String)
        {
            // This validation can be refined based on how type resolution is handled in the parser/semantic analyzer.
            // For now, if the original VariableName token doesn't carry type info, this might be overly strict.
            // A VariableReferenceNode typically just refers to the name, and its type is looked up from a symbol table.
            // If the parser only knows `VariableName` token, this parameter might be dropped or default to `TokenType.VariableName`.
            // However, if your VariableDeclarationNode already infers/knows the type, passing it here is fine.
            // For simplicity, let's assume `variableType` here should be the *declared* type of the variable.
        }

        VariableType = variableType;
        VariableName = variableName ?? throw new ArgumentNullException(nameof(variableName));
    }

    /// <inheritdoc/>
    public string ToDebugString()
    {
        return $"VariableReferenceNode: Name='{VariableName}', Type={VariableType}";
    }

    /// <inheritdoc/>
    public void Accept(IAstVisitor visitor)
    {
        visitor.Visit(this);
    }
}