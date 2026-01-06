namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents component to write log messages.
    /// </summary>
    public interface ILogWriter
    {
        /// <summary>
        /// Write informative message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        Task WriteInformationAsync(string message, Type type);

        /// <summary>
        /// Write warning message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        Task WriteWarningAsync(string message, Type type);

        /// <summary>
        /// Write error message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        Task WriteErrorAsync(string message, Type type);
    }
}
