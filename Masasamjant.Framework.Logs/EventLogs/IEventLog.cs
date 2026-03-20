namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// Represents event log.
    /// </summary>
    public interface IEventLog
    {
        /// <summary>
        /// Write event log entry.
        /// </summary>
        /// <param name="entry">The event log entry.</param>
        void WriteEntry(EventLogEntry entry);

        /// <summary>
        /// Write event log entry.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="level">The entry level.</param>
        void WriteEntry(string source, string message, EventLogEntryLevel level);
    }
}
