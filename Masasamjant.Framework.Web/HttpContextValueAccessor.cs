namespace Masasamjant.Web
{
    /// <summary>
    /// Represents abstract implementation of <see cref="IHttpContextValueGetter"/> and <see cref="IHttpContextValueSetter"/> interfaces.
    /// </summary>
    public abstract class HttpContextValueAccessor : IHttpContextValueGetter, IHttpContextValueSetter
    {
        /// <summary>
        /// Gets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The HTTP context from which to retrieve the value.</param>
        /// <param name="key">The key associated with the value to retrieve.</param>
        /// <returns>The value associated with the specified key, or <c>null</c> if not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="context"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="key"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public string? GetHttpValue(HttpContext context, string key)
        {
            ValidateAccessorParameters(context, key);
            return GetHttpContextValue(context, key);
        }

        /// <summary>
        /// When overridden in a derived class, gets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The HTTP context from which to retrieve the value.</param>
        /// <param name="key">The key associated with the value to retrieve.</param>
        /// <returns>The value associated with the specified key, or <c>null</c> if not found.</returns>
        protected abstract string? GetHttpContextValue(HttpContext context, string key);

        /// <summary>
        /// Sets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The HTTP context in which to set the value.</param>
        /// <param name="key">The key associated with the value to set.</param>
        /// <param name="value">The value to set.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="context"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="key"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public void SetHttpValue(HttpContext context, string key, string value)
        { 
            ValidateAccessorParameters(context, key);
            SetHttpContextValue(context, key, value);
        }

        /// <summary>
        /// When overridden in a derived class, sets the value associated with <see cref="HttpContext"/>.
        /// </summary>
        /// <param name="context">The HTTP context in which to set the value.</param>
        /// <param name="key">The key associated with the value to set.</param>
        /// <param name="value">The value to set.</param>
        protected abstract void SetHttpContextValue(HttpContext context, string key, string value);

        private static void ValidateAccessorParameters(HttpContext context, string key)
        {
            ArgumentNullException.ThrowIfNull(context);
            
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), "Key of the value cannot be null, empty or only whitespace.");
        }
    }
}
