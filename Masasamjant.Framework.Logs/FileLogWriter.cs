using Masasamjant.Diagnostics.EventLogs;
using System.Collections.Concurrent;

namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents a log writer that writes log messages to a file. 
    /// The class is thread-safe and implements <see cref="IDisposable"/> to ensure proper resource cleanup.
    /// </summary>
    public class FileLogWriter : ILogWriter, IDisposable
    {
        private readonly FileLogWriterSettings settings;
        private readonly ConcurrentQueue<LogEntry> queue;
        private readonly System.Timers.Timer timer;
        private readonly ILogMessageFormatter formatter;
        private readonly SemaphoreSlim semaphore;
        private readonly IEventLogFactory eventLogFactory;
        private long disposed;
        private const long disposedFlag = 1L;

        /// <summary>
        /// Initialiazes a new instance of the <see cref="FileLogWriter"/> class that use a default log message formatter.
        /// </summary>
        /// <param name="settings">The writer settings.</param>
        /// <param name="eventLogFactory">The event log factory.</param>
        /// <exception cref="ArgumentNullException">If any of parameters is <c>null</c>.</exception>
        public FileLogWriter(FileLogWriterSettings settings, IEventLogFactory eventLogFactory)
            : this(settings, new DefaultLogMessageFormatter(), eventLogFactory)
        { }

        /// <summary>
        /// Initialiazes a new instance of the <see cref="FileLogWriter"/> class that use specified log message formatter.
        /// </summary>
        /// <param name="settings">The writer settings.</param>
        /// <param name="eventLogFactory">The event log factory.</param>
        /// <param name="formatter">The log message formatter.</param>
        /// <exception cref="ArgumentNullException">If any of parameters is <c>null</c>.</exception>
        public FileLogWriter(FileLogWriterSettings settings, ILogMessageFormatter formatter, IEventLogFactory eventLogFactory)
        {
            this.settings = settings ?? throw new ArgumentNullException(nameof(settings));
            this.formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
            this.eventLogFactory = eventLogFactory ?? throw new ArgumentNullException(nameof(eventLogFactory));
            this.disposed = 0;
            this.queue = new ConcurrentQueue<LogEntry>();
            this.semaphore = new SemaphoreSlim(1, 1);
            this.timer = new System.Timers.Timer(this.settings.FlushIntervalMilliseconds)
            {
                AutoReset = true,
                Enabled = true
            };
            this.timer.Elapsed += OnTimerElapsed;
            this.timer.Start();
        }

        /// <summary>
        /// Write error message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public async Task WriteErrorAsync(string message, Type type)
        {
            CheckDisposed();

            queue.Enqueue(new LogEntry(LogCategory.ErrorCategory, message, type, GetLocalDateTime()));
            
            if (IsBatchSizeReached())
                await FlushQueueToFile();
        }

        /// <summary>
        /// Write informative message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public async Task WriteInformationAsync(string message, Type type)
        {
            CheckDisposed();
            
            queue.Enqueue(new LogEntry(LogCategory.InformationCategory, message, type, GetLocalDateTime()));

            if (IsBatchSizeReached())
                await FlushQueueToFile();
        }

        /// <summary>
        /// Write warning message to log.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="type">The type that write log.</param>
        /// <returns>A task representing writing.</returns>
        public async Task WriteWarningAsync(string message, Type type)
        {
            CheckDisposed();

            queue.Enqueue(new LogEntry(LogCategory.WarningCategory, message, type, GetLocalDateTime()));

            if (IsBatchSizeReached())
                await FlushQueueToFile();
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Gets local date and time.
        /// </summary>
        /// <returns>A local date and time.</returns>
        internal virtual DateTime GetLocalDateTime()
            => DateTime.Now;

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected void Dispose(bool disposing)
        {
            if (Interlocked.Exchange(ref disposed, disposedFlag) == disposedFlag)
                return;

            try
            {
                timer.Stop();
                timer.Elapsed -= OnTimerElapsed;
                timer.Dispose();

                semaphore.Wait();

                DoFlushQueueToFile().GetAwaiter().GetResult();
            }
            catch (Exception exception)
            {
                TryWriteErrorEventLogEntry(exception);
            }
            finally
            {
                semaphore.Release();
                semaphore.Dispose();
            }
        }

        private bool IsBatchSizeReached()
            => queue.Count >= settings.BatchSize;

        private async Task FlushQueueToFile()
        {
            if (queue.IsEmpty)
                return;

            if (!await semaphore.WaitAsync(0))
                return;

            try
            {

                await DoFlushQueueToFile();
            }
            catch (Exception exception)
            {
                TryWriteErrorEventLogEntry(exception);   
            }
            finally
            {
                semaphore.Release();
            }
        }

        private void TryWriteErrorEventLogEntry(Exception exception)
        {
            var eventLog = eventLogFactory.CreateEventLog();
            eventLog.TryWriteEntry("Application", "Failed to write queued messages to log: " + exception.Message, EventLogEntryLevel.Error);
        }

        private async Task DoFlushQueueToFile()
        {
            if (queue.IsEmpty)
                return;

            var batch = new List<LogEntry>(settings.BatchSize);

            while (batch.Count < settings.BatchSize && queue.TryDequeue(out var entry))
                batch.Add(entry);

            if (batch.Count == 0)
                return;

            var lines = GetBatchLines(batch);

            string filePath = settings.FilePathProvider();

            await File.AppendAllLinesAsync(filePath, lines);
        }

        private IEnumerable<string> GetBatchLines(IEnumerable<LogEntry> batch)
        {
            return batch.Select(entry =>
            {
                return entry.Level switch
                {
                    LogCategory.ErrorCategory => formatter.FormatErrorMessage(entry.Message, entry.Time, entry.Type),
                    LogCategory.WarningCategory => formatter.FormatWarningMessage(entry.Message, entry.Time, entry.Type),
                    _ => formatter.FormatInformationMessage(entry.Message, entry.Time, entry.Type),
                };
            });

        }

        private async void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            await FlushQueueToFile();
        }

        private void CheckDisposed()
        {
            ObjectDisposedException.ThrowIf(Interlocked.Read(ref disposed) == disposedFlag, this);
        }

        private record LogEntry(string Level, string Message, Type Type, DateTime Time);
    }
}
