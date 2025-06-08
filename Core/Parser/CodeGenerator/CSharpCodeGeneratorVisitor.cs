using Core.Parser.AST.Nodes;
using Core.Parser.AST.Nodes.StatementNodes;
using Core.Parser.AST.Nodes.LiteralNodes;
using Core.Parser.AST.Nodes.ControlFlowNodes.LoopControlFlowNodes;
using Core.Parser.AST.Nodes.ControlFlowNodes.IfElseControlFlowNodes;
using Core.Parser.AST.Nodes.ExpressionNodes;
using Core.Parser.Interfaces.AST;
using Core.Parser.Tokens;
using System.Text;

namespace Core.Parser.CodeGenerator;

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
            _stringBuilder.AppendLine(line);
        }
        else
        {
            _stringBuilder.AppendLine();
        }
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
    /// Visits the root ProgramNode and generates the main structure of the C# program.
    /// </summary>
    /// <param name="node">The ProgramNode to visit.</param>
    public void Visit(ProgramNode node)
    {
        AppendLine("using System;");
        AppendLine("using System.Linq;"); 
        AppendLine();
        AppendLine("namespace GeneratedProgram;");
        AppendLine();
        AppendLine("public class Program");
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
    }

    /// <summary>
    /// Visits a VariableDeclarationNode and generates a C# variable declaration.
    /// </summary>
    /// <param name="node">The VariableDeclarationNode to visit.</param>
    public void Visit(VariableDeclarationNode node)
    {
        string csharpType = TokenToCSharpType(node.VariableType);
        _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
        _stringBuilder.Append($"{csharpType} {node.VariableName}");

        if (node.InitialValueExpression != null)
        {
            _stringBuilder.Append(" = ");
            node.InitialValueExpression.Accept(this); // Visit the expression to get its value
        }
        _stringBuilder.AppendLine(";");
    }

    /// <summary>
    /// Visits a DoubleLiteralNode and appends its value to the generated code.
    /// </summary>
    /// <param name="node">The DoubleLiteralNode to visit.</param>
    public void Visit(DoubleLiteralNode node)
    {
        _stringBuilder.Append(node.Value.ToString(System.Globalization.CultureInfo.InvariantCulture));
    }

    /// <summary>
    /// Visits an IntegerLiteralNode and appends its value to the generated code.
    /// </summary>
    /// <param name="node">The IntegerLiteralNode to visit.</param>
    public void Visit(IntegerLiteralNode node)
    {
        _stringBuilder.Append(node.Value);
    }

    /// <summary>
    /// Visits a StringLiteralNode and appends its value to the generated code, properly quoted.
    /// </summary>
    /// <param name="node">The StringLiteralNode to visit.</param>
    public void Visit(StringLiteralNode node)
    {
        _stringBuilder.Append($"\"{node.Value}\"");
    }

    /// <summary>
    /// Visits a BinaryExpressionNode and generates a C# binary expression.
    /// </summary>
    /// <param name="node">The BinaryExpressionNode to visit.</param>
    public void Visit(BinaryExpressionNode node)
    {
        // For assignment, handle it as a statement rather than an expression if it's top-level
        if (node.Operator == TokenType.Assign)
        {
            _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
            node.Left.Accept(this);
            _stringBuilder.Append($" {TokenToCSharpOperator(node.Operator)} ");
            node.Right.Accept(this);
            _stringBuilder.AppendLine(";");
        }
        else // For other binary operations (e.g., arithmetic, comparison)
        {
            _stringBuilder.Append("(");
            node.Left.Accept(this);
            _stringBuilder.Append($" {TokenToCSharpOperator(node.Operator)} ");
            node.Right.Accept(this);
            _stringBuilder.Append(")");
        }
    }

    /// <summary>
    /// Visits a VariableReferenceNode and appends its name to the generated code.
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
        _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
        _stringBuilder.Append("Console.WriteLine(");
        node.Expression.Accept(this);
        _stringBuilder.AppendLine(");");
    }

    /// <summary>
    /// Visits a ReadStatementNode and generates a C# Console.ReadLine statement for variable assignment with type parsing.
    /// </summary>
    /// <param name="node">The ReadStatementNode to visit.</param>
    public void Visit(ReadStatementNode node)
    {
        _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
        string csharpType = TokenToCSharpType(node.TargetVariable.VariableType);

        switch (node.TargetVariable.VariableType)
        {
            case TokenType.IntegerType:
                _stringBuilder.AppendLine($"{node.TargetVariable.VariableName} = int.Parse(Console.ReadLine()!);");
                break;
            case TokenType.DoubleType:
                _stringBuilder.AppendLine($"{node.TargetVariable.VariableName} = double.Parse(Console.ReadLine()!, System.Globalization.CultureInfo.InvariantCulture);");
                break;
            case TokenType.StringType:
                _stringBuilder.AppendLine($"{node.TargetVariable.VariableName} = Console.ReadLine();");
                break;
            default:
                throw new ArgumentException($"Unsupported variable type for ReadStatement: {node.TargetVariable.VariableType}");
        }
    }

    /// <summary>
    /// Visits a ReturnNode and generates a C# return statement.
    /// </summary>
    /// <param name="node">The ReturnNode to visit.</param>
    public void Visit(ReturnNode node)
    {
        if (node.Expression!= null)
        {
            _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
            _stringBuilder.Append("return ");
            node.Expression.Accept(this);
            _stringBuilder.AppendLine(";");
        }
        else
        {
            AppendLine("return;");
        }
    }

    /// <summary>
    /// Visits a LoopControlFlowNode and generates a C# for loop.
    /// </summary>
    /// <param name="node">The LoopControlFlowNode to visit.</param>
    public void Visit(LoopControlFlowNode node)
    {
        _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
        _stringBuilder.Append("for (int i = 0; i < ");
        node.LoopCountExpression.Accept(this);
        _stringBuilder.AppendLine("; i++)");
        AppendLine("{");
        Indent();

        foreach (var statement in node.Body)
        {
            statement.Accept(this);
        }

        Dedent();
        AppendLine("}");
    }

    /// <summary>
    /// Visits an IfElseControlFlowNode and generates a C# if-else statement.
    /// </summary>
    /// <param name="node">The IfElseControlFlowNode to visit.</param>
    public void Visit(IfElseControlFlowNode node)
    {
        _stringBuilder.Append(string.Concat(Enumerable.Repeat(IndentUnit, _indentationLevel)));
        _stringBuilder.Append("if (");
        node.Condition.Accept(this);
        _stringBuilder.AppendLine(")");
        AppendLine("{");
        Indent();

        foreach (var statement in node.ThenBlock)
        {
            statement.Accept(this);
        }

        Dedent();
        AppendLine("}");

        if (node.ElseBlock != null && node.ElseBlock.Count != 0)
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
    /// Converts a TokenType (variable type) to its corresponding C# string representation.
    /// </summary>
    /// <param name="tokenType">The TokenType representing the variable type.</param>
    /// <returns>The C# string representation of the type.</returns>
    /// <exception cref="ArgumentException">Thrown for unsupported variable types.</exception>
    private static string TokenToCSharpType(TokenType tokenType)
    {
        return tokenType switch
        {
            TokenType.IntegerType => "int",
            TokenType.DoubleType => "double",
            TokenType.StringType => "string",
            _ => throw new ArgumentException($"Unsupported variable type: {tokenType}"),
        };
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

    /// <summary>
    /// Default visit method for IAstNode. Should ideally not be called for specific node types.
    /// </summary>
    /// <param name="node">The IAstNode to visit.</param>
    /// <exception cref="NotImplementedException">Always thrown as a fallback.</exception>
    public void Visit(IAstNode node)
    {
        throw new NotImplementedException($"Visit method not implemented for node type: {node.GetType().Name}");
    }
}