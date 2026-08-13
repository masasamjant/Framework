using System.Globalization;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Default implementation of <see cref="ILogMessageFormatter"/> that is used if not other implementation is specified. 
    /// The default message format is "[{Category}] [{Time}] [{Type}] {Message}".
    /// </summary>
    public sealed class DefaultLogMessageFormatter : ILogMessageFormatter
    {
        private const string DefaultMessageFormat = "[{0}] [{1}] [{2}] {3}";

        /// <summary>
        /// Format error level message.
        /// </summary>
        /// <param name="message">The original message.</param>
        /// <param name="time">The date and time.</param>
        /// <param name="type">The logging type.</param>
        /// <returns>A formatted error message.</returns>
        public string FormatErrorMessage(string message, DateTime time, Type type)
        {
            return GetFinalMessage(message, type, LogCategory.ErrorCategory, time);
        }

        /// <summary>
        /// Format information level message.
        /// </summary>
        /// <param name="message">The original message.</param>
        /// <param name="time">The date and time.</param>
        /// <param name="type">The logging type.</param>
        /// <returns>A formatted information message.</returns>
        public string FormatInformationMessage(string message, DateTime time, Type type)
        {
            return GetFinalMessage(message, type, LogCategory.InformationCategory, time);
        }

        /// <summary>
        /// Format warning level message.
        /// </summary>
        /// <param name="message">The original message.</param>
        /// <param name="time">The date and time.</param>
        /// <param name="type">The logging type.</param>
        /// <returns>A formatted warning message.</returns>
        public string FormatWarningMessage(string message, DateTime time, Type type)
        {
            return GetFinalMessage(message, type, LogCategory.WarningCategory, time);
        }

        private static string GetFinalMessage(string message, Type type, string category, DateTime time)
        {
            return string.Format(DefaultMessageFormat, category, time.ToString(CultureInfo.InvariantCulture), type.FullName ?? type.Name, message);
        }
    }
}
