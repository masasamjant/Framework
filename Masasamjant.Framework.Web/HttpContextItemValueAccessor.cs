namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="HttpContextValueAccessor"/> that gets and sets value to <see cref="HttpContext.Items"/>.
    /// </summary>
    public sealed class HttpContextItemValueAccessor : HttpContextValueAccessor
    {
        /// <summary>
        /// Gets the value stored in <see cref="HttpContext.Items"/>.
        /// </summary>
        /// <param name="context">The HTTP context from which to retrieve the value.</param>
        /// <param name="key">The key associated with the value to retrieve.</param>
        /// <returns>The value associated with the specified key, or <c>null</c> if not found.</returns>
        protected override string? GetHttpContextValue(HttpContext context, string key)
        {
            if (context.Items.TryGetValue(key, out var value) && value is string s)
                return s;

            return null;
        }

        /// <summary>
        /// Sets the value to <see cref="HttpContext.Items"/>.
        /// </summary>
        /// <param name="context">The HTTP context in which to set the value.</param>
        /// <param name="key">The key associated with the value to set.</param>
        /// <param name="value">The value to set.</param>
        protected override void SetHttpContextValue(HttpContext context, string key, string value)
        {
            context.Items[key] = value;
        }
    }
}
