namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Provides configuration settings for writing log entries to daily rolling files using batch processing and timed flushing.
    /// </summary>
    public class DailyFileLogWriterSettings : FileLogWriterSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DailyFileLogWriterSettings"/> class with daily file rolling, batch size, and
        /// flush interval settings.
        /// </summary>
        /// <param name="directoryPath">The directory path where log files will be written.</param>
        /// <param name="batchSize">The maximum number of log entries to batch before writing to the file.</param>
        /// <param name="flushIntervalMilliseconds">The interval, in milliseconds, between automatic flushes of batched log entries to the file.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="directoryPath"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="batchSize"/> is less than 1 or greater than 1000.
        /// -or-
        /// If value of <paramref name="flushIntervalMilliseconds"/> is less than 100 or greater than 60000.
        /// </exception>
        public DailyFileLogWriterSettings(string directoryPath, int batchSize, int flushIntervalMilliseconds)
            : base(() => GetDailyRollingFilePath(directoryPath), batchSize, flushIntervalMilliseconds)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath), "Directory path is null, empty or only whitespace.");
        }

        private static string GetDailyRollingFilePath(string directoryPath)
        {
            if (!Directory.Exists(directoryPath))
                throw new InvalidOperationException($"Directory '{directoryPath}' does not exist.");

            var datePart = DateTime.Now.ToString("yyyyMMdd");
            var fileName = $"{datePart}-LOG.log";
            return Path.Combine(directoryPath, fileName);
        }
    }
}
