namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="ISessionStorage"/> associated with HTTP session.
    /// </summary>
    public sealed class HttpSessionStorage : SessionStorage
    {
        private readonly ISession session;

        /// <summary>
        /// Initializes new instance of the <see cref="HttpSessionStorage"/> class.
        /// </summary>
        /// <param name="session">The HTTP session.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="session"/> is <c>null</c>.</exception>
        public HttpSessionStorage(ISession session)
        {
            this.session = session ?? throw new ArgumentNullException(nameof(session));
        }

        /// <summary>
        /// Clears all values from session storage.
        /// </summary>
        public override void Clear()
        {
            session.Clear();
            OnCleared();
        }

        /// <summary>
        /// Gets the value associated with the specified key from session storage.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key, or <c>null</c>, if the key does not exist.</returns>
        public override string? GetString(string key)
        {
            ArgumentNullException.ThrowIfNull(key);
            return session.GetString(key);
        }

        /// <summary>
        /// Removes the value associated with the specified key from session storage.
        /// </summary>
        /// <param name="key">The key of the value to remove.</param>
        public override void Remove(string key)
        {
            ArgumentNullException.ThrowIfNull(key);
            session.Remove(key);
        }

        /// <summary>
        /// Sets the specified value with the specified key in session storage.
        /// </summary>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        public override void SetString(string key, string value)
        {
            ArgumentNullException.ThrowIfNull(key);
            session.SetString(key, value);
        }
    }
}
