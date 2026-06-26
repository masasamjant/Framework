using Masasamjant.Security;

namespace Masasamjant.Web
{
    /// <summary>
    /// Represents abstract <see cref="ISessionStorage"/>.
    /// </summary>
    public abstract class SessionStorage : ISessionStorage
    {
        /// <summary>
        /// Default session identifier key.
        /// </summary>
        protected const string DefaultSessionIdentifierKey = "SESSION-IDENTIFIER-E4D397D25EC34DD3A819B0334388DF7A";

        /// <summary>
        /// Notifies when session is cleared.
        /// </summary>
        public event EventHandler<SessionIdentifierEventArgs>? Cleared;

        /// <summary>
        /// Gets the key to store session identifier.
        /// </summary>
        protected virtual string SessionIdentifierKey
        {
            get { return DefaultSessionIdentifierKey; }
        }

        /// <summary>
        /// Clears all values from session storage.
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// Gets the unique session identifier.
        /// </summary>
        /// <returns>A unique string to identify session.</returns>
        public virtual string GetSessionIdentifier()
        {
            var sessionIdentifier = GetString(SessionIdentifierKey);

            if (sessionIdentifier == null)
            {
                sessionIdentifier = CreateSessionIdentifier();
                SetString(SessionIdentifierKey, sessionIdentifier);
            }

            return sessionIdentifier;
        }

        /// <summary>
        /// Gets the value associated with the specified key from session storage.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key, or <c>null</c>, if the key does not exist.</returns>
        public abstract string? GetString(string key);

        /// <summary>
        /// Removes the value associated with the specified key from session storage.
        /// </summary>
        /// <param name="key">The key of the value to remove.</param>
        public abstract void Remove(string key);

        /// <summary>
        /// Sets the specified value with the specified key in session storage.
        /// </summary>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        public abstract void SetString(string key, string value);

        /// <summary>
        /// Creates new unique session identifier.
        /// </summary>
        /// <returns>A session identifier.</returns>
        protected virtual string CreateSessionIdentifier()
        {
            var provider = new Base64SHA1Provider();
            return provider.CreateHash(Guid.NewGuid().ToString("N"));
        }

        /// <summary>
        /// Raises <see cref="Cleared"/> event.
        /// </summary>
        protected virtual void OnCleared()
        {
            var sessionIdentifier = GetSessionIdentifier();
            Cleared?.Invoke(this, new SessionIdentifierEventArgs(sessionIdentifier));
        }
    }
}
