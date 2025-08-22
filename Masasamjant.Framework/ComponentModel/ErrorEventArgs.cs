namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents arguments of <see cref="IProvideError.Error"/> event.
    /// </summary>
    public sealed class ErrorEventArgs : EventArgs
    {
        private ErrorBehavior errorBehavior;

        /// <summary>
        /// Initializes new instance of the <see cref="ErrorEventArgs"/> class.
        /// </summary>
        /// <param name="errorType">The type where error occurred.</param>
        /// <param name="errorSource">The name of the member where error occurred.</param>
        /// <param name="error">The error exception.</param>
        /// <param name="errorContext">The context related to error or <c>null</c>.</param>
        public ErrorEventArgs(Type errorType, string? errorSource, Exception error, object? errorContext = null)
        {
            ErrorType = errorType;
            ErrorSource = errorSource;
            Error = error;
            ErrorBehavior = ErrorBehavior.Throw;
            ErrorContext = errorContext;
        }

        /// <summary>
        /// Gets the type where error occurred.
        /// </summary>
        public Type ErrorType { get; }

        /// <summary>
        /// Gets the name of the member where error occurred.
        /// </summary>
        public string? ErrorSource { get; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        public Exception Error { get; }

        /// <summary>
        /// Gets or sets the error behavior. Default value is <see cref="ErrorBehavior.Throw"/>.
        /// </summary>
        /// <exception cref="ArgumentException">If setted value is not defined.</exception>
        public ErrorBehavior ErrorBehavior
        {
            get { return errorBehavior; }
            set
            {
                if (!Enum.IsDefined(value))
                    throw new ArgumentException($"The value '{value}' is not defined.", nameof(ErrorBehavior));

                errorBehavior = value;
            }
        }

        /// <summary>
        /// Gets the any context related to error or <c>null</c>.
        /// </summary>
        public object? ErrorContext { get; set; }
    }
}
