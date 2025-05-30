namespace Core.Parser.Interfaces.AST;

/// <summary>
/// Basic interface for AST nodes.
/// </summary>
public interface IAstNode
{
    /// <summary>
    /// Debug method for string representation of the node
    /// </summary>
    /// <returns> String representation of the node </returns>
    string ToDebugString(); 
}