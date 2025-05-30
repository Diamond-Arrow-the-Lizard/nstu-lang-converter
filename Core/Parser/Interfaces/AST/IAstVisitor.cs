using Core.Parser.AST.Nodes;
using Core.Parser.AST.Nodes.LiteralNodes;

namespace Core.Parser.Interfaces.AST;

/// <summary>
/// Interface for the code generator via the Visitor pattern
/// </summary>
public interface IAstVisitor
{
    void Visit(ProgramNode node);
    void Visit(VariableDeclarationNode node);
    void Visit(DoubleLiteralNode node);
    void Visit(IntegerLiteralNode node);
    void Visit(StringLiteralNode node);
    void Visit(BinaryExpressionNode node);
}