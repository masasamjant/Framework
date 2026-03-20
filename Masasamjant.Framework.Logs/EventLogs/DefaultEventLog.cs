using System.Collections.Concurrent;

namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// Represents default implementation of <see cref="IEventLog"/> that stores log entries in memory using a thread-safe collection.
    /// </summary>
    public sealed class DefaultEventLog : IEventLog
    {
        private readonly ConcurrentQueue<EventLogEntry> entries = new ConcurrentQueue<EventLogEntry>();

        /// <summary>
        /// Write event log entry.
        /// </summary>
        /// <param name="entry">The event log entry.</param>
        public void WriteEntry(EventLogEntry entry)
        {
            entries.Enqueue(entry);
        }

        /// <summary>
        /// Write event log entry.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="level">The entry level.</param>
        public void WriteEntry(string source, string message, EventLogEntryLevel level)
        {
            WriteEntry(new EventLogEntry(message, source, level));
        }

        /// <summary>
        /// Gets entries written to the log and clears the log.
        /// </summary>
        /// <returns>A read-only collection of entries written to log.</returns>
        public IReadOnlyCollection<EventLogEntry> GetEntries()
        {
            var list = new List<EventLogEntry>();

            while (entries.TryDequeue(out var entry))
                list.Add(entry);

            return list.AsReadOnly();
        }
    }
}
