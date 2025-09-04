namespace Masasamjant.Repositories
{
    /// <summary>
    /// Represents exception thrown by repository when concurrent save occurs.
    /// </summary>
    public class RepositoryConcurrencyException : RepositoryException
    {
        /// <summary>
        /// Initializes new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public RepositoryConcurrencyException(string message)
            : this(message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="RepositoryException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public RepositoryConcurrencyException(string message, Exception? innerException)
            : base(message, innerException)
        { }
    }
}
