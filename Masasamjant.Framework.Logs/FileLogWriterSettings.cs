namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents setting for <see cref="FileLogWriter"/>.
    /// </summary>
    public class FileLogWriterSettings
    {
        /// <summary>
        /// Initializes new instance of the <see cref="FileLogWriterSettings"/> class.
        /// </summary>
        /// <param name="filePathProvider">The function delegate to get file path to log file.</param>
        /// <param name="batchSize">The size of the batch to write log messages.</param>
        /// <param name="flushIntervalMilliseconds">The interval in milliseconds to flush log messages to file.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="batchSize"/> is less than 1 or greater than 1000.
        /// -or-
        /// If value of <paramref name="flushIntervalMilliseconds"/> is less than 100 or greater than 60000.
        /// </exception>
        public FileLogWriterSettings(Func<string> filePathProvider, int batchSize, int flushIntervalMilliseconds)
        {
            ArgumentNullException.ThrowIfNull(filePathProvider);

            if (batchSize < 1 || batchSize > 1000)
                throw new ArgumentOutOfRangeException(nameof(batchSize), batchSize, "Batch size must be between 1 and 1000.");

            if (flushIntervalMilliseconds < 100 || flushIntervalMilliseconds > 60000)
                throw new ArgumentOutOfRangeException(nameof(flushIntervalMilliseconds), flushIntervalMilliseconds, "Flush interval must be between 100 and 60000 milliseconds.");

            FilePathProvider = filePathProvider;
            BatchSize = batchSize;
            FlushIntervalMilliseconds = flushIntervalMilliseconds;
        }

        /// <summary>
        /// Gets the function delegate to get file path to log file.
        /// </summary>
        public Func<string> FilePathProvider { get; }

        /// <summary>
        /// Gets the size of the batch to write log messages.
        /// The write does not occur until the batch size is reached or the flush interval is elapsed.
        /// </summary>
        public int BatchSize { get; }

        /// <summary>
        /// Gets the interval in milliseconds to flush log messages to file.
        /// </summary>
        public int FlushIntervalMilliseconds { get; }
    }
}
