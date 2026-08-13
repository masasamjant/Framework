namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// An implementation of <see cref="IEventLog"/> that writes entries to the Windows Event Log.
    /// </summary>
    public sealed class WindowsEventLog : IEventLog
    {
        /// <summary>
        /// Writes an event log entry to the Windows Event Log using the specified entry details.
        /// </summary>
        /// <remarks>This method is only supported on Windows operating systems.</remarks>
        /// <param name="entry">The event log entry to write.</param>
        /// <exception cref="PlatformNotSupportedException">If the current operating system is not Windows.</exception>
        public void WriteEntry(EventLogEntry entry)
        {
            if (!OperatingSystem.IsWindows())
                throw new PlatformNotSupportedException("WindowsEventLog is only supported on Windows operating systems.");

            if (!System.Diagnostics.EventLog.SourceExists(entry.Source))
                return;

            System.Diagnostics.EventLog.WriteEntry(entry.Source, entry.Message, ConvertToEventLogEntryType(entry.Level));
        }

        /// <summary>
        /// Writes an event log entry to the Windows Event Log using the specified entry details.
        /// </summary>
        /// <remarks>This method is only supported on Windows operating systems.</remarks>
        /// <param name="source">The source.</param>
        /// <param name="message">The message.</param>
        /// <param name="level">The entry level.</param>
        /// <exception cref="PlatformNotSupportedException">If the current operating system is not Windows.</exception>
        public void WriteEntry(string source, string message, EventLogEntryLevel level)
        {
            WriteEntry(new EventLogEntry(message, source, level));
        }

        private static System.Diagnostics.EventLogEntryType ConvertToEventLogEntryType(EventLogEntryLevel level)
        {
            if (!OperatingSystem.IsWindows())
                throw new PlatformNotSupportedException("WindowsEventLog is only supported on Windows operating systems.");

            switch (level)
            {
                case EventLogEntryLevel.Error:
                    return System.Diagnostics.EventLogEntryType.Error;
                case EventLogEntryLevel.Warning:
                    return System.Diagnostics.EventLogEntryType.Warning;
                default:
                    return System.Diagnostics.EventLogEntryType.Information;
            }
        }
    }
}
