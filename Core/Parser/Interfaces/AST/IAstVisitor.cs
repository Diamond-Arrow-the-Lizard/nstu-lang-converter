using Core.Parser.AST.Nodes;

namespace Core.Parser.Interfaces.AST;

/// <summary>
/// Interface for the code generator via the Visitor pattern
/// </summary>
public interface IAstVisitor
{
    void Visit(IAstNode node);
    void Visit(VariableDeclarationNode node);
}