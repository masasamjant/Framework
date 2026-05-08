namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// <see cref="IEventLogFactory"/> to create instance of <see cref="WindowsEventLog"/>.
    /// </summary>
    public sealed class WindowsEventLogFactory : IEventLogFactory
    {
        private readonly WindowsEventLog instance;

        /// <summary>
        /// Initializes new instance of the <see cref="WindowsEventLogFactory"/> class.
        /// </summary>
        public WindowsEventLogFactory()
        {
            instance = new WindowsEventLog();
        }

        /// <summary>
        /// Gets shared instance of <see cref="WindowsEventLog"/> class.
        /// </summary>
        /// <returns>A instance of <see cref="WindowsEventLog"/>.</returns>
        public IEventLog CreateEventLog()
        {
            return instance;
        }
    }
}
