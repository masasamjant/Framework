namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents base exception for duplicate collection item exceptions.
    /// </summary>
    public abstract class DuplicateException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DuplicateException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        protected DuplicateException(string message, Exception? innerException)
            : base(message, innerException)
        { }
    }
}
