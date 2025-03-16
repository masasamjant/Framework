namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents exception thrown when duplicate item is found at specified index.
    /// </summary>
    public class DuplicateIndexException : DuplicateException
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateIndexException"/> class.
        /// </summary>
        /// <param name="duplicateIndex">The index of duplicate item.</param>
        public DuplicateIndexException(int duplicateIndex)
            : this(duplicateIndex, "The duplicate item found from specified index.")
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateIndexException"/> class.
        /// </summary>
        /// <param name="duplicateIndex">The index of duplicate item.</param>
        /// <param name="message">The exception message.</param>
        public DuplicateIndexException(int duplicateIndex, string message)
            : this(duplicateIndex, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateIndexException"/> class.
        /// </summary>
        /// <param name="duplicateIndex">The index of duplicate item.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public DuplicateIndexException(int duplicateIndex, string message, Exception? innerException)
            : base(message, innerException)
        {
            DuplicateIndex = duplicateIndex;
        }

        /// <summary>
        /// Gets the index of duplicate item.
        /// </summary>
        public int DuplicateIndex { get; }
    }
}
