namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="HttpContextValueAccessor"/> that gets and sets value session.
    /// </summary>
    public class HttpContextSessionValueAccessor : HttpContextValueAccessor
    {
        /// <summary>
        /// Gets the value stored in session.
        /// </summary>
        /// <param name="context">The HTTP context from which to retrieve the value.</param>
        /// <param name="key">The key associated with the value to retrieve.</param>
        /// <returns>The value associated with the specified key, or <c>null</c> if not found.</returns>
        protected override string? GetHttpContextValue(HttpContext context, string key)
        {
            return GetSession(context).GetString(key);
        }

        /// <summary>
        /// Sets the value to session.
        /// </summary>
        /// <param name="context">The HTTP context in which to set the value.</param>
        /// <param name="key">The key associated with the value to set.</param>
        /// <param name="value">The value to set.</param>
        protected override void SetHttpContextValue(HttpContext context, string key, string value)
        {
            GetSession(context).SetString(key, value);
        }

        /// <summary>
        /// Gets the session storage for the specified HTTP context.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <returns>The session storage.</returns>
        protected virtual ISessionStorage GetSession(HttpContext context)
        {
            return new HttpSessionStorage(context.Session);
        }
    }
}
