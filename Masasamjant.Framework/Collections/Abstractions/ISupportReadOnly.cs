namespace Masasamjant.Collections.Abstractions
{
    /// <summary>
    /// Represents collection that support read-only state.
    /// </summary>
    public interface ISupportReadOnly
    {
        /// <summary>
        /// Gets whether or not collection is in read-only state.
        /// </summary>
        bool IsReadOnly { get; }

        /// <summary>
        /// Sets collection to read-only state.
        /// </summary>
        void SetReadOnly();
    }
}
