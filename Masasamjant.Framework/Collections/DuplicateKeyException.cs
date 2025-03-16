namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents exception thrown when duplicate item is found with specified index.
    /// </summary>
    public class DuplicateKeyException<TKey> : DuplicateException where TKey : notnull
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateKeyException{TKey}"/> class.
        /// </summary>
        /// <param name="duplicateKey">The key of duplicate item.</param>
        /// <param name="message">The exception message.</param>
        public DuplicateKeyException(TKey duplicateKey)
            : this(duplicateKey, "The duplicate item found with specified key.")
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateKeyException{TKey}"/> class.
        /// </summary>
        /// <param name="duplicateKey">The key of duplicate item.</param>
        /// <param name="message">The exception message.</param>
        public DuplicateKeyException(TKey duplicateKey, string message)
            : this(duplicateKey, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateKeyException{TKey}"/> class.
        /// </summary>
        /// <param name="duplicateKey">The key of duplicate item.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public DuplicateKeyException(TKey duplicateKey, string message, Exception? innerException)
            : base(message, innerException)
        {
            DuplicateKey = duplicateKey;
        }

        /// <summary>
        /// Gets the <typeparamref name="TKey"/> of duplicate item.
        /// </summary>
        public TKey DuplicateKey { get; }
    }
}
