namespace Masasamjant.Web.Sessions
{
    /// <summary>
    /// Represents storage of session values.
    /// </summary>
    public interface ISessionStorage
    {
        /// <summary>
        /// Gets value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <returns>A value in session or <c>null</c>.</returns>
        string? GetString(string key);

        /// <summary>
        /// Set value to session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        void SetString(string key, string value);

        /// <summary>
        /// Remove value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        void Remove(string key);

        /// <summary>
        /// Clear session content.
        /// </summary>
        void Clear();
    }
}
