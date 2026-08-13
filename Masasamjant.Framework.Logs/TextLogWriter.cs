namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents component to write log messages to specified <see cref="TextWriter"/>.
    /// </summary>
    public class TextLogWriter : ILogWriter
    {
        private readonly ILogMessageFormatter formatter;
        private readonly TextWriter writer;

        /// <summary>
        /// Initializes new instance of the <see cref="TextLogWriter"/> class that use <see cref="DefaultLogMessageFormatter"/> to format messages.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to write messages.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="writer"/> is <c>null</c>.</exception>
        public TextLogWriter(TextWriter writer)
            : this(writer, new DefaultLogMessageFormatter())
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="TextLogWriter"/> class with specified <see cref="ILogMessageFormatter"/> to format messages.
        /// </summary>
        /// <param name="writer">The <see cref="TextWriter"/> to write messages.</param>
        /// <param name="formatter">The <see cref="ILogMessageFormatter"/> to format messages.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="writer"/> or <paramref name="formatter"/> is <c>null</c>.</exception>
        public TextLogWriter(TextWriter writer, ILogMessageFormatter formatter)
        {
            ArgumentNullException.ThrowIfNull(writer);
            ArgumentNullException.ThrowIfNull(formatter);
            this.writer = writer;
            this.formatter = formatter;
        }

        /// <summary>
        /// Write error message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteErrorAsync(string message, Type type)
        {
            var time = GetLocalDateTime();
            var formattedMessage = formatter.FormatErrorMessage(message, time, type);
            return writer.WriteLineAsync(formattedMessage);
        }

        /// <summary>
        /// Write informative message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteInformationAsync(string message, Type type)
        {
            var time = GetLocalDateTime();
            var formattedMessage = formatter.FormatInformationMessage(message, time, type);
            return writer.WriteLineAsync(formattedMessage);
        }

        /// <summary>
        /// Write warning message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteWarningAsync(string message, Type type)
        {
            var time = GetLocalDateTime();
            var formattedMessage = formatter.FormatWarningMessage(message, time, type); 
            return writer.WriteLineAsync(formattedMessage);
        }

        /// <summary>
        /// Gets current local time.
        /// </summary>
        /// <returns>A current local time.</returns>
        protected virtual DateTime GetLocalDateTime()
            => DateTime.Now;
    }
}
