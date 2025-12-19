using Microsoft.Extensions.Logging;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents component to write log messages using <see cref="ILogger"/> obtained 
    /// from specified <see cref="ILoggerFactory"/>.
    /// </summary>
    public sealed class LogWriter : ILogWriter
    {
        private readonly ILoggerFactory loggerFactory;

        /// <summary>
        /// Initializes new instance of the <see cref="LogWriter"/> class.
        /// </summary>
        /// <param name="loggerFactory">The <see cref="ILoggerFactory"/> to obtain <see cref="ILogger"/>.</param>
        public LogWriter(ILoggerFactory loggerFactory)
        {
            this.loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Write error message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteErrorAsync(string message, Type type)
        {
            return WriteLogMessageAsync(LogLevel.Error, message, type);
        }

        /// <summary>
        /// Write informative message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteInformationAsync(string message, Type type)
        {
            return WriteLogMessageAsync(LogLevel.Information, message, type);
        }

        /// <summary>
        /// Write warning message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public Task WriteWarningAsync(string message, Type type)
        {
            return WriteLogMessageAsync(LogLevel.Warning, message, type);
        }

        private Task WriteLogMessageAsync(LogLevel level, string message, Type type)
        {
            var logger = GetLogger(type);
#pragma warning disable CA2254 // Template should be a static expression
            logger.Log(level, message);
#pragma warning restore CA2254 // Template should be a static expression
            return Task.CompletedTask;
        }

        private ILogger GetLogger(Type type)
            => loggerFactory.CreateLogger(type);
    }
}
