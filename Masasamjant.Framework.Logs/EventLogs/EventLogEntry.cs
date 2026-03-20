namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// Represents a single entry in an event log.
    /// </summary>
    public sealed class EventLogEntry
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EvntLogEntry"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="source">The source.</param>
        /// <param name="level">The level.</param>
        public EventLogEntry(string message, string source, EventLogEntryLevel level)
        {
            Message = message ?? string.Empty;
            Source = source ?? string.Empty;
            Level = Enum.IsDefined(level) ? level : EventLogEntryLevel.Information;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the source.
        /// </summary>
        public string Source { get; }

        /// <summary>
        /// Gets the entry level.
        /// </summary>
        public EventLogEntryLevel Level { get; }
    }
}
