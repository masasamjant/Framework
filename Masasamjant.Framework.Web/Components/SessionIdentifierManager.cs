using Masasamjant.ComponentModel;
using Masasamjant.Security;
using Masasamjant.Web.Sessions;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Web.Components
{
    /// <summary>
    /// Represents <see cref="IIdentifierManager"/> where scope of identifiers is session. The identifiers are stored using <see cref="IdentifierManager"/> class.
    /// </summary>
    /// <remarks>Instance of this class is thread-safe and can be used as singleton instance.</remarks>
    public sealed class SessionIdentifierManager : ISessionIdentifierManager, IIdentifierManager
    {
        private readonly IdentifierManager identifierManager;
        private readonly List<ISessionIdentifierManagerEvents> eventsList;
        private readonly Lock eventsListLock;

        /// <summary>
        /// Initializes new default instance of the <see cref="SessionIdentifierManager"/> class that creates Base-64 SHA1 temporary identifiers.
        /// </summary>
        public SessionIdentifierManager()
            : this(new Base64SHA1Provider())
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="SessionIdentifierManager"/> class with specified <see cref="IStringHashProvider"/> to compute temporary identifiers.
        /// </summary>
        /// <param name="hashProvider">The <see cref="IStringHashProvider"/> to compute temporary identifiers.</param>
        public SessionIdentifierManager(IStringHashProvider hashProvider)
        {
            this.identifierManager = new IdentifierManager(hashProvider);
            this.eventsList = new List<ISessionIdentifierManagerEvents>();
            this.eventsListLock = new Lock();
        }

        /// <summary>
        /// Register specified <see cref="ISessionIdentifierManagerEvents"/> with manager. After this manager 
        /// will react to events of <paramref name="events"/>.
        /// </summary>
        /// <param name="events">The <see cref="ISessionIdentifierManagerEvents"/> to add.</param>
        public void AddEventsProvider(ISessionIdentifierManagerEvents events)
        {
            lock (eventsListLock) 
            {
                if (!eventsList.Contains(events))
                {
                    eventsList.Add(events);
                    events.RemoveIdentifiers += OnEventsRemoveIdentifiers;
                }
            }
        }

        /// <summary>
        /// Remove all registered <see cref="ISessionIdentifierManagerEvents"/> from manager.
        /// </summary>
        public void ClearEventsProviders()
        {
            List<ISessionIdentifierManagerEvents> list;
            
            lock (eventsListLock)
            {
                list = eventsList.ToList();
                eventsList.Clear();
            }

            foreach (var events in list)
                events.RemoveIdentifiers -= OnEventsRemoveIdentifiers;
        }

        /// <summary>
        /// Get temporary identifier for specified <typeparamref name="T"/> identifier.
        /// </summary>
        /// <typeparam name="T">The type of the identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="identifier">The actual identifier.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier<T>(string sessionIdentifier, T identifier) where T : notnull
        {
            return identifierManager.GetTemporaryIdentifier(sessionIdentifier, identifier);
        }

        /// <summary>
        /// Get temporary identifier for specified actual identifiers.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="identifiers">The actual identifier values.</param>
        /// <returns>A temporary identifier.</returns>
        public string GetTemporaryIdentifier(string sessionIdentifier, params object[] identifiers)
        {
            return identifierManager.GetTemporaryIdentifier(sessionIdentifier, identifiers);
        }

        /// <summary>
        /// Remove specified <see cref="ISessionIdentifierManagerEvents"/> from manager. After this manager 
        /// will not react to events of <paramref name="events"/>.
        /// </summary>
        /// <param name="events">The <see cref="ISessionIdentifierManagerEvents"/> to remove.</param>
        public void RemoveEventsProvider(ISessionIdentifierManagerEvents events)
        {
            lock (eventsListLock)
            {
                if (eventsList.Remove(events))
                    events.RemoveIdentifiers -= OnEventsRemoveIdentifiers;
            }
        }

        /// <summary>
        /// Removes identifiers from specified key.
        /// </summary>
        /// <param name="key">The scope key.</param>
        public void RemoveIdentifiers(string sessionIdentifier)
        {
            identifierManager.RemoveIdentifiers(sessionIdentifier);
        }

        /// <summary>
        /// Tries to get the actual identifier for specified temporary identifier.
        /// </summary>
        /// <typeparam name="T">The type of the actual identifier.</typeparam>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifier">The actual identifier when returns <c>true</c>; otherwise <c>null</c> or <c>default</c>.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier<T>(string sessionIdentifier, string temporaryIdentifier, [MaybeNullWhen(false)] out T identifier)
        {
            return identifierManager.TryGetIdentifier(sessionIdentifier, temporaryIdentifier, out identifier);
        }

        /// <summary>
        /// Tries to get the actual identifiers for specified temporary identifier.
        /// </summary>
        /// <param name="key">The scope key.</param>
        /// <param name="temporaryIdentifier">The temporary identifier.</param>
        /// <param name="identifiers">The actual identifiers or empty array.</param>
        /// <returns><c>true</c> if there was actual identifier for <paramref name="temporaryIdentifier"/>; <c>false</c> otherwise.</returns>
        public bool TryGetIdentifier(string sessionIdentifier, string temporaryIdentifier, out object[] identifiers)
        {
            return identifierManager.TryGetIdentifier(sessionIdentifier, temporaryIdentifier, out identifiers);
        }

        private void OnEventsRemoveIdentifiers(object? sender, SessionIdentifierEventArgs e)
        {
            RemoveIdentifiers(e.SessionIdentifier);
        }
    }
}
