using Core.Parser.AST.Nodes;
using Core.Parser.AST.Nodes.StatementNodes;
using Core.Parser.AST.Nodes.LiteralNodes;
using Core.Parser.AST.Nodes.ControlFlowNodes.LoopControlFlowNodes;
using Core.Parser.AST.Nodes.ControlFlowNodes.IfElseControlFlowNodes;
using Core.Parser.AST.Nodes.ExpressionNodes;

namespace Core.Parser.Interfaces.AST;

/// <summary>
/// Interface for the code generator via the Visitor pattern
/// </summary>
public interface IAstVisitor
{
    void Visit(IAstNode node)
    {
        _ = new NotImplementedException(nameof(node));
    }

    void Visit(ProgramNode node);
    void Visit(VariableDeclarationNode node);
    void Visit(DoubleLiteralNode node);
    void Visit(IntegerLiteralNode node);
    void Visit(StringLiteralNode node);
    void Visit(BinaryExpressionNode node);
    void Visit(VariableReferenceNode node);
    void Visit(WriteStatementNode node);
    void Visit(ReadStatementNode node);
    void Visit(ReturnNode node);
    void Visit(LoopControlFlowNode node);
    void Visit(IfElseControlFlowNode node);

    /// <summary>
    /// Gets the generated code.
    /// </summary>
    /// <returns>A string containing the code.</returns>
    public string GetGeneratedCode();
}