namespace Masasamjant.Diagnostics.EventLogs
{
    /// <summary>
    /// Represents <see cref="IEventLogFactory"/> for <see cref="DefaultEventLog"/> implementation.
    /// </summary>
    public sealed class DefaultEventLogFactory : IEventLogFactory
    {
        private readonly DefaultEventLog? instance;

        /// <summary>
        /// Initializes new instance of the <see cref="DefaultEventLogFactory"/> class that creates a new <see cref="DefaultEventLog"/> instance 
        /// every time the <see cref="CreateEventLog"/> method is called.
        /// </summary>
        public DefaultEventLogFactory()
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DefaultEventLogFactory"/> class that returns the specified <see cref="DefaultEventLog"/> instance 
        /// every time the <see cref="CreateEventLog"/> method is called.
        /// </summary>
        /// <param name="instance">The instance to provide.</param>
        public DefaultEventLogFactory(DefaultEventLog instance)
        {
            this.instance = instance;
        }

        /// <summary>
        /// Creates a instance of <see cref="DefaultEventLog"/> or returns the specified <see cref="DefaultEventLog"/> instance depending on 
        /// how factory instance was constructed.
        /// </summary>
        /// <returns>A instance of <see cref="IEventLog"/>.</returns>
        public IEventLog CreateEventLog()
        {
            if (instance != null)
                return instance;

            return new DefaultEventLog();
        }
    }
}
