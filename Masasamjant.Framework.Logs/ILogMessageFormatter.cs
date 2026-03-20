namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents component to format log messages.
    /// </summary>
    public interface ILogMessageFormatter
    {
        /// <summary>
        /// Format information level message.
        /// </summary>
        /// <param name="message">The original message.</param>
        /// <param name="time">The date and time.</param>
        /// <param name="type">The logging type.</param>
        /// <returns>A formatted information message.</returns>
        string FormatInformationMessage(string message, DateTime time, Type type);

        /// <summary>
        /// Format warning level message.
        /// </summary>
        /// <param name="message">The original message.</param>
        /// <param name="time">The date and time.</param>
        /// <param name="type">The logging type.</param>
        /// <returns>A formatted warning message.</returns>
        string FormatWarningMessage(string message, DateTime time, Type type);

        /// <summary>
        /// Format error level message.
        /// </summary>
        /// <param name="message">The original message.</param>
        /// <param name="time">The date and time.</param>
        /// <param name="type">The logging type.</param>
        /// <returns>A formatted error message.</returns>
        string FormatErrorMessage(string message, DateTime time, Type type);
    }
}
