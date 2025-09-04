namespace Masasamjant.Repositories
{
    /// <summary>
    /// Represents exception thrown by repository operations.
    /// </summary>
    public class RepositoryException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RepositoryException(string message)
            : this(message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public RepositoryException(string message, Exception? innerException)
            : base(message, innerException) 
        { } 
    }
}
