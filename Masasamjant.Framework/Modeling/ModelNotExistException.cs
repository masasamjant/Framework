namespace Masasamjant.Modeling
{
    /// <summary>
    /// Represents exception thrown when expected model does not exist.
    /// </summary>
    public class ModelNotExistException : Exception
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="ModelNotExistException"/> class.
        /// </summary>
        public ModelNotExistException()
            : this("The specified model instance does not exist.")
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ModelNotExistException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        public ModelNotExistException(string message)
            : this(message, null) 
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ModelNotExistException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public ModelNotExistException(string message, Exception? innerException)
            : base(message, innerException)
        { }
    }
}
