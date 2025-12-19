using System.Globalization;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents component to write log messages to specified <see cref="TextWriter"/>.
    /// </summary>
    public class TextLogWriter : ILogWriter
    {
        private const string AppendTimeMessageFormat = "[{0}] [{1}] [{2}] {3}";
        private const string DefaultMessageFormat = "[{0}] [{1}] {2}";

        private readonly TextWriter writer;

        /// <summary>
        /// Initializes new instance of the <see cref="TextLogWriter"/> class.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to write messages.</param>
        public TextLogWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        /// <summary>
        /// Name of error category.
        /// </summary>
        public const string ErrorCategory = "ERROR";

        /// <summary>
        /// Name of information category.
        /// </summary>
        public const string InformationCategory = "INFORMATION";
        
        /// <summary>
        /// Name of warning category.
        /// </summary>
        public const string WarningCategory = "WARNING";

        /// <summary>
        /// Gets or sets if time is appended to message.
        /// </summary>
        public bool AppendTime { get; set; }

        /// <summary>
        /// Write error message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteErrorAsync(string message, Type type)
        {
            return WriteFinalMessageAsync(message, type, ErrorCategory);
        }

        /// <summary>
        /// Write informative message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteInformationAsync(string message, Type type)
        {
            return WriteFinalMessageAsync(message, type, InformationCategory);
        }

        /// <summary>
        /// Write warning message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteWarningAsync(string message, Type type)
        {
            return WriteFinalMessageAsync(message, type, WarningCategory);
        }

        /// <summary>
        /// Gets current local time.
        /// </summary>
        /// <returns>A current local time.</returns>
        protected virtual DateTime GetLocalDateTime()
            => DateTime.Now;

        private string GetFinalMessage(string message, bool appendTime, Type type, string category)
        {
            if (appendTime)
                return string.Format(AppendTimeMessageFormat, category, GetLocalDateTime().ToString(CultureInfo.InvariantCulture), type.FullName ?? type.Name, message);
            else
                return string.Format(DefaultMessageFormat, category, type.FullName ?? type.Name, message);
        }

        private Task WriteFinalMessageAsync(string message, Type type, string category)
        {
            var finalMessage = GetFinalMessage(message, AppendTime, type, category);
            return writer.WriteLineAsync(finalMessage);
        }
    }
}
