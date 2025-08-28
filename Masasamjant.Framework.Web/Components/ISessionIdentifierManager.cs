using Masasamjant.ComponentModel;

namespace Masasamjant.Web.Components
{
    /// <summary>
    /// Represents <see cref="IIdentifierManager"/> where scope of identifiers is session.
    /// </summary>
    public interface ISessionIdentifierManager : IIdentifierManager
    {
        /// <summary>
        /// Register specified <see cref="ISessionIdentifierManagerEvents"/> with manager. After this manager 
        /// will react to events of <paramref name="events"/>.
        /// </summary>
        /// <param name="events">The <see cref="ISessionIdentifierManagerEvents"/> to add.</param>
        void AddEventsProvider(ISessionIdentifierManagerEvents events);

        /// <summary>
        /// Remove specified <see cref="ISessionIdentifierManagerEvents"/> from manager. After this manager 
        /// will not react to events of <paramref name="events"/>.
        /// </summary>
        /// <param name="events">The <see cref="ISessionIdentifierManagerEvents"/> to remove.</param>
        void RemoveEventsProvider(ISessionIdentifierManagerEvents events);

        /// <summary>
        /// Remove all registered <see cref="ISessionIdentifierManagerEvents"/> from manager.
        /// </summary>
        void ClearEventsProviders();
    }
}
