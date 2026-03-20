namespace Masasamjant.Diagnostics.EventLogs
{
    public sealed class WindowsEventLogFactory : IEventLogFactory
    {
        private readonly WindowsEventLog instance;

        public WindowsEventLogFactory()
        {
            instance = new WindowsEventLog();
        }

        public IEventLog CreateEventLog()
        {
            return instance;
        }
    }
}
