namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that wants to be notified when added to non-volatile memory like database or file. 
    /// </summary>
    public interface IAddHandler
    {
        /// <summary>
        /// Invoked when model instance is added to non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing addition.</param>
        void OnAdd(string? identity);
    }
}
