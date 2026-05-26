namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Provides configuration settings for writing log entries to a single file using batch processing and timed flushing.
    /// </summary>
    public class SingleFileLogWriterSettings : FileLogWriterSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SingleFileLogWriterSettings"/> class with the specified file path, batch
        /// size, and flush interval.
        /// </summary>
        /// <param name="filePath">The path to the log file where entries will be written.</param>
        /// <param name="batchSize">The maximum number of log entries to batch before writing to the file.</param>
        /// <param name="flushIntervalMilliseconds">The interval, in milliseconds, between automatic flushes of batched log entries to the file.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="filePath"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="batchSize"/> is less than 1 or greater than 1000.
        /// -or-
        /// If value of <paramref name="flushIntervalMilliseconds"/> is less than 100 or greater than 60000.
        /// </exception>
        public SingleFileLogWriterSettings(string filePath, int batchSize, int flushIntervalMilliseconds)
            : base(() => filePath, batchSize, flushIntervalMilliseconds)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "The file path is null, empty or only whitespace.");
        }
    }
}
