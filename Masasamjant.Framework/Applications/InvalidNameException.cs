namespace Masasamjant.Applications
{
    /// <summary>
    /// Exception thrown when name provided to application item is not valid.
    /// </summary>
    public class InvalidNameException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="InvalidNameException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public InvalidNameException(string name)
            : this(name, "Specified name is not valid.")
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="InvalidNameException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The exception message.</param>
        public InvalidNameException(string name, string message)
            : this(name, message, null) 
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="InvalidNameException"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public InvalidNameException(string name, string message, Exception? innerException)
            : base(message, innerException)
        {
            Name = name;
        }

        /// <summary>
        /// Gets the invalid name.
        /// </summary>
        public string Name { get; }
    }
}
