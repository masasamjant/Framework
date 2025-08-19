namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that wants to be notified when updated in non-volatile memory like database or file. 
    /// </summary>
    public interface IUpdateHandler
    {
        /// <summary>
        /// Invoked when model instance is updated in non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing update.</param>
        void OnUpdate(string? identity);
    }
}
