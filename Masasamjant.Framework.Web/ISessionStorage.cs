namespace Masasamjant.Web
{
    /// <summary>
    /// Represents storage for session data.
    /// </summary>
    public interface ISessionStorage
    {
        /// <summary>
        /// Notifies when session is cleared.
        /// </summary>
        event EventHandler<SessionIdentifierEventArgs>? Cleared;

        /// <summary>
        /// Gets the value associated with the specified key from session storage.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key, or <c>null</c>, if the key does not exist.</returns>
        string? GetString(string key);

        /// <summary>
        /// Sets the specified value with the specified key in session storage.
        /// </summary>
        /// <param name="key">The key of the value to set.</param>
        /// <param name="value">The value to set.</param>
        void SetString(string key, string value);

        /// <summary>
        /// Removes the value associated with the specified key from session storage.
        /// </summary>
        /// <param name="key">The key of the value to remove.</param>
        void Remove(string key);

        /// <summary>
        /// Clears all values from session storage.
        /// </summary>
        void Clear();

        /// <summary>
        /// Gets the unique session identifier.
        /// </summary>
        /// <returns>A unique string to identify session.</returns>
        string GetSessionIdentifier();
    }
}
