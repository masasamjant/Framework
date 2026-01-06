namespace Masasamjant
{
    /// <summary>
    /// Exception thrown when string argument is not valid.
    /// </summary>
    public class StringArgumentException : ArgumentException
    {
        /// <summary>
        /// Initializes new instance of the <see cref="StringArgumentException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="paramValue">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        public StringArgumentException(string message, string? paramValue, string paramName)
            : this(message, paramValue, paramName, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="StringArgumentException"/> class.
        /// </summary>
        /// <param name="message">The exception message.</param>
        /// <param name="paramValue">The value of the parameter.</param>
        /// <param name="paramName">The name of the parameter.</param>
        /// <param name="innerException">The inner exception or <c>null</c>.</param>
        public StringArgumentException(string message, string? paramValue, string paramName, Exception? innerException)
            : base(message, paramName, innerException)
        {
            ParamValue = paramValue;
        }

        /// <summary>
        /// Gets the value of the parameter that caused the exception.
        /// </summary>
        public string? ParamValue { get; }
    }
}
