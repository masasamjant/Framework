namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// Defines levels of event log entries.
    /// </summary>
    public enum EventLogEntryLevel : int
    {
        /// <summary>
        /// Information.
        /// </summary>
        Information = 0,

        /// <summary>
        /// Warning.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// Error.
        /// </summary>
        Error = 2
    }
}
