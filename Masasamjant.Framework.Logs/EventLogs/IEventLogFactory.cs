namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// Factory to create instance of <see cref="IEvenLog"/> implementation.
    /// </summary>
    public interface IEventLogFactory
    {
        /// <summary>
        /// Creates a instance of <see cref="IEventLog"/> implementation.
        /// The instance might be a new instance or a shared instance.
        /// </summary>
        /// <returns>A instance of <see cref="IEventLog"/>.</returns>
        IEventLog CreateEventLog();
    }
}
