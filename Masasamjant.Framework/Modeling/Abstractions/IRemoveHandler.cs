namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that wants to be notified when removed from non-volatile memory like database or file. 
    /// </summary>
    public interface IRemoveHandler
    {
        /// <summary>
        /// Invoked when model instance is removed from non-volatile memory like database or file. This usually means physical delete
        /// where same model does not exist in non-volatile memory after remove.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing remove.</param>
        void OnRemove(string? identity);
    }
}
