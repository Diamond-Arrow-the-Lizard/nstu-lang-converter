using Core.Parser.AST.Nodes;
using Core.Parser.AST.Nodes.StatementNodes;
using Core.Parser.AST.Nodes.LiteralNodes;
using Core.Parser.AST.Nodes.ControlFlowNodes.LoopControlFlowNodes;
using Core.Parser.AST.Nodes.ControlFlowNodes.IfElseControlFlowNodes;
using Core.Parser.AST.Nodes.ExpressionNodes;
using Core.Parser.Interfaces.AST;
using Core.Parser.Tokens;
using System.Text;

namespace GeneratedProgram; 

/// <summary>
/// A visitor that generates C# code from an Abstract Syntax Tree (AST).
/// This class implements the IAstVisitor interface to traverse the AST
/// and convert each node into its corresponding C# syntax.
/// </summary>
public class CSharpCodeGeneratorVisitor : IAstVisitor
{
    private readonly StringBuilder _stringBuilder = new();
    private int _indentationLevel = 0;
    private const string IndentUnit = "    "; // 4 spaces for indentation

    /// <summary>
    /// Gets the generated C# code.
    /// </summary>
    /// <returns>A string containing the complete C# code.</returns>
    public string GetGeneratedCode()
    {
        return _stringBuilder.ToString();
    }

    /// <summary>
    /// Appends a line to the generated code with the current indentation.
    /// </summary>
    /// <param name="line">The line of code to append.</param>
    private void AppendLine(string line = "")
    {
        if (line != "")
        {
            _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
        }
        _stringBuilder.AppendLine(line);
    }

    /// <summary>
    /// Increases the current indentation level.
    /// </summary>
    private void Indent()
    {
        _indentationLevel++;
    }

    /// <summary>
    /// Decreases the current indentation level.
    /// </summary>
    private void Dedent()
    {
        if (_indentationLevel > 0)
        {
            _indentationLevel--;
        }
    }

    /// <summary>
    /// Default visit method for any IAstNode. Throws NotImplementedException as
    /// specific visit methods should be implemented for each node type.
    /// </summary>
    /// <param name="node">The AST node to visit.</param>
    /// <exception cref="NotImplementedException">Always thrown for unhandled node types.</exception>
    public void Visit(IAstNode node)
    {
        // This default implementation should ideally not be hit if all specific Visit methods are handled.
        // However, it's part of the interface contract.
        throw new NotImplementedException($"Visit method not implemented for node type: {node.GetType().Name}");
    }

    /// <summary>
    /// Visits the ProgramNode, which represents the root of the AST.
    /// This method sets up the basic C# program structure (namespace, class, Main method).
    /// </summary>
    /// <param name="node">The ProgramNode to visit.</param>
    public void Visit(ProgramNode node)
    {
        AppendLine("using System;");
        AppendLine();
        AppendLine("namespace GeneratedProgram"); 
        AppendLine("{");
        Indent();
        AppendLine("public static class Program"); 
        AppendLine("{");
        Indent();
        AppendLine("public static void Main()"); 
        AppendLine("{");
        Indent();

        foreach (var statement in node.Statements)
        {
            statement.Accept(this); 
        }

        Dedent();
        AppendLine("}"); // End of Main method
        Dedent();
        AppendLine("}"); // End of Program class
        Dedent();
        AppendLine("}"); // End of namespace
    }

    /// <summary>
    /// Visits a VariableDeclarationNode and generates the corresponding C# variable declaration.
    /// Examples: int Num1 = 5;, double DecVal = 10.5;, string Message = "Привет, мир!";
    /// </summary>
    /// <param name="node">The VariableDeclarationNode to visit.</param>
    public void Visit(VariableDeclarationNode node)
    {
        string cSharpType = "";
        switch (node.VariableType)
        {
            case TokenType.IntegerType:
                cSharpType = "int";
                break;
            case TokenType.DoubleType:
                cSharpType = "double";
                break;
            case TokenType.StringType:
                cSharpType = "string";
                break;
        }

        if (node.InitialValueExpression != null)
        {
            AppendLine($"{cSharpType} {node.VariableName} = ");
            Indent();
            // Visit the initial value expression. The expression itself will append its representation.
            // Note: We temporarily reduce indentation for the expression itself to align correctly with the assignment.
            // This is a simple approach, more complex scenarios might need a dedicated expression visitor.
            _stringBuilder.Length -= IndentUnit.Length * _indentationLevel; 
            node.InitialValueExpression.Accept(this); 
            _stringBuilder.AppendLine(";"); 
            Indent(); 
        }
        else
        {
            AppendLine($"{cSharpType} {node.VariableName};");
        }
    }

    /// <summary>
    /// Visits a DoubleLiteralNode and appends its double value to the generated code.
    /// </summary>
    /// <param name="node">The DoubleLiteralNode to visit.</param>
    public void Visit(DoubleLiteralNode node)
    {
        _stringBuilder.Append($"{node.Value}");
    }

    /// <summary>
    /// Visits an IntegerLiteralNode and appends its integer value to the generated code.
    /// </summary>
    /// <param name="node">The IntegerLiteralNode to visit.</param>
    public void Visit(IntegerLiteralNode node)
    {
        _stringBuilder.Append($"{node.Value}");
    }

    /// <summary>
    /// Visits a StringLiteralNode and appends its string value (quoted) to the generated code.
    /// </summary>
    /// <param name="node">The StringLiteralNode to visit.</param>
    public void Visit(StringLiteralNode node)
    {
        _stringBuilder.Append($"\"{node.Value}\"");
    }

    /// <summary>
    /// Visits a BinaryExpressionNode and generates the corresponding C# binary expression.
    /// Examples: Num1 + 2 * 3, DecVal / 2.0 - 1.5, Num1 == 11.
    /// </summary>
    /// <param name="node">The BinaryExpressionNode to visit.</param>
    public void Visit(BinaryExpressionNode node)
    {
        // For assignment expressions, we handle the left side (variable reference) without appending new line yet.
        // For other binary expressions, we directly append the full expression.
        if (node.Operator == TokenType.Assign)
        {
            // For assignment, the left side (variable) doesn't need parentheses.
            // Append the left operand, then the assignment operator.
            // Example: Num1 = Num1 + 2 * 3;
            // The left side (VariableReferenceNode) generates 'Num1'.
            // Then we append ' = '.
            // Then the right side (expression) generates '...
            node.Left.Accept(this); // This should be a VariableReferenceNode
            _stringBuilder.Append($" {TokenToCSharpOperator(node.Operator)} ");
            node.Right.Accept(this);
        }
        else
        {
            // For other binary expressions, wrap operands in parentheses for correct operator precedence
            _stringBuilder.Append("(");
            node.Left.Accept(this);
            _stringBuilder.Append($" {TokenToCSharpOperator(node.Operator)} ");
            node.Right.Accept(this);
            _stringBuilder.Append(")");
        }
    }

    /// <summary>
    /// Visits a VariableReferenceNode and appends the variable name to the generated code.
    /// </summary>
    /// <param name="node">The VariableReferenceNode to visit.</param>
    public void Visit(VariableReferenceNode node)
    {
        _stringBuilder.Append(node.VariableName);
    }

    /// <summary>
    /// Visits a WriteStatementNode and generates a C# Console.WriteLine statement.
    /// </summary>
    /// <param name="node">The WriteStatementNode to visit.</param>
    public void Visit(WriteStatementNode node)
    {
        AppendLine("Console.WriteLine(");
        Indent();
        _stringBuilder.Length -= IndentUnit.Length * _indentationLevel; 
        node.Expression.Accept(this); 
        _stringBuilder.AppendLine(");");
        Indent(); 
    }

    /// <summary>
    /// Visits a ReadStatementNode and generates a C# Console.ReadLine statement for variable assignment.
    /// </summary>
    /// <param name="node">The ReadStatementNode to visit.</param>
    public void Visit(ReadStatementNode node)
    {
        switch(node.TargetVariable.VariableType)
        {
            case TokenType.Integer:
                AppendLine($"{((VariableReferenceNode)node.TargetVariable).VariableName} = int.Parse(Console.ReadLine());");
                break;
            case TokenType.Double:
                AppendLine($"{((VariableReferenceNode)node.TargetVariable).VariableName} = double.Parse(Console.ReadLine());");
                break;
            case TokenType.String:
                AppendLine($"{((VariableReferenceNode)node.TargetVariable).VariableName} = Console.ReadLine();");
                break;
            default:
                throw new InvalidCastException(nameof(node.TargetVariable.VariableType));
        }
    }

    /// <summary>
    /// Visits a ReturnNode and generates a C# return statement.
    /// </summary>
    /// <param name="node">The ReturnNode to visit.</param>
    public void Visit(ReturnNode node)
    {
        ArgumentNullException.ThrowIfNull(node.Expression);
        AppendLine("return ");
        Indent();
        _stringBuilder.Length -= IndentUnit.Length * _indentationLevel; 
        node.Expression.Accept(this);
        _stringBuilder.AppendLine(";");
        Indent(); 
    }

    /// <summary>
    /// Visits a LoopControlFlowNode and generates a C# for loop.
    /// Assumes LoopCountExpression evaluates to an integer.
    /// </summary>
    /// <param name="node">The LoopControlFlowNode to visit.</param>
    public void Visit(LoopControlFlowNode node)
    {
        AppendLine("for (int i = 0; i < ");
        Indent();
        _stringBuilder.Length -= IndentUnit.Length * _indentationLevel; 
        node.LoopCountExpression.Accept(this); 
        _stringBuilder.AppendLine("; i++)");
        Indent();
        AppendLine("{");
        Indent();

        foreach (var statement in node.Body)
        {
            statement.Accept(this); 
        }

        Dedent();
        AppendLine("}");
        Dedent();
    }

    /// <summary>
    /// Visits an IfElseControlFlowNode and generates a C# if-else statement.
    /// </summary>
    /// <param name="node">The IfElseControlFlowNode to visit.</param>
    public void Visit(IfElseControlFlowNode node)
    {
        AppendLine("if (");
        Indent();
        _stringBuilder.Length -= IndentUnit.Length * _indentationLevel; 
        node.Condition.Accept(this); 
        _stringBuilder.AppendLine(")");
        Indent();
        AppendLine("{");
        Indent();

        foreach (var statement in node.ThenBlock) 
        {
            statement.Accept(this); 
        }

        Dedent();
        AppendLine("}");

        if (node.ElseBlock != null && node.ElseBlock.Any()) 
        {
            AppendLine("else");
            AppendLine("{");
            Indent();
            foreach (var statement in node.ElseBlock) 
            {
                statement.Accept(this); 
            }
            Dedent();
            AppendLine("}");
        }
    }

    /// <summary>
    /// Converts a TokenType (operator) to its corresponding C# string representation.
    /// </summary>
    /// <param name="tokenType">The TokenType representing the operator.</param>
    /// <returns>The C# string representation of the operator.</returns>
    /// <exception cref="ArgumentException">Thrown for unsupported operator types.</exception>
    private static string TokenToCSharpOperator(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.Add => "+",
            TokenType.Decrement => "-",
            TokenType.Multiply => "*",
            TokenType.Divide => "/",
            TokenType.Assign => "=",
            TokenType.Equals => "==",
            _ => throw new ArgumentException($"Unsupported operator type: {tokenType}"),
        };
    }
}