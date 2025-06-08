namespace Core.Parser.Interfaces.AST;

/// <summary>
/// Basic interface for AST nodes.
/// </summary>
public interface IAstNode
{

    /// <summary>
    /// Perform an operation via the Visitor pattern
    /// </summary>
    /// <param name="visitor"></param>
    void Accept(IAstVisitor visitor);

    /// <summary>
    /// Debug method for string representation of the node
    /// </summary>
    /// <returns> String representation of the node </returns>
    string ToDebugString(); 

}