using Core.Parser.Interfaces.AST;

namespace Core.Parser.AST.Nodes;

/// <summary>
/// The root node of the program in AST which contains the list of everything in the program
/// </summary>
public class ProgramNode(List<IAstNode> statements) : IAstNode
{
    /// <summary>
    /// Operators list in the program
    /// </summary>
    public List<IAstNode> Statements { get; } = statements;

    /// <inheritdoc/>
    public string ToDebugString()
    {
        var statementStrings = Statements.Select(s => s.ToDebugString()).ToList();
        return $"ProgramNode:\n{string.Join("\n", statementStrings.Select(s => $"  {s}"))}";
    }
}