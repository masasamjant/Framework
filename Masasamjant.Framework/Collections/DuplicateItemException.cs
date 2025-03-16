namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents exception thrown when duplicate item is found.
    /// </summary>
    public class DuplicateItemException : DuplicateException
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateItemException"/> class.
        /// </summary>
        /// <param name="duplicateItem">The duplicate item.</param>
        public DuplicateItemException(object duplicateItem)
            : this(duplicateItem, "The duplicate item found.")
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateItemException"/> class.
        /// </summary>
        /// <param name="duplicateItem">The duplicate item.</param>
        /// <param name="message">The exception message.</param>
        public DuplicateItemException(object duplicateItem, string message)
            : this(duplicateItem, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateItemException"/> class.
        /// </summary>
        /// <param name="duplicateItem">The duplicate item.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public DuplicateItemException(object duplicateItem, string message, Exception? innerException)
            : base(message, innerException)
        {
            DuplicateItem = duplicateItem;
        }

        /// <summary>
        /// Gets the duplicate item.
        /// </summary>
        public object DuplicateItem { get; }
    }
}
