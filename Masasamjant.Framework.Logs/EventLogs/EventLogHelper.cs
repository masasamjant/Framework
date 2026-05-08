using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// Provides helper methods for working with event logs.
    /// </summary>
    public static class EventLogHelper
    {
        /// <summary>
        /// Tries to write specified event log entry.
        /// </summary>
        /// <param name="log">The event log.</param>
        /// <param name="entry">The event log entry.</param>
        public static void TryWriteEntry(this IEventLog log, EventLogEntry entry)
        {
            TryWriteEntry(log, entry, out var _);
        }

        /// <summary>
        /// Tries to write specified event log entry.
        /// </summary>
        /// <param name="log">The event log.</param>
        /// <param name="entry">The event log entry.</param>
        /// <param name="exception">The exception occurred when returns <c>false</c>.</param>
        /// <returns><c>true</c> if write entry without exception; <c>false</c> if exception occurred.</returns>
        public static bool TryWriteEntry(this IEventLog log, EventLogEntry entry, [NotNullWhen(false)] out Exception? exception)
        {
            try
            {
                log.WriteEntry(entry);
                exception = null;
                return true;
            }
            catch (Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        /// <summary>
        /// Tries to write specified event log entry.
        /// </summary>
        /// <param name="log">The event log.</param>
        /// <param name="source">The event log source.</param>
        /// <param name="message">The event log message.</param>
        /// <param name="level">The event entry level.</param>
        public static void TryWriteEntry(this IEventLog log, string source, string message, EventLogEntryLevel level)
        {
            TryWriteEntry(log, new EventLogEntry(source, message, level));
        }
    }
}
