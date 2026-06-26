namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="HttpContextValueAccessor"/> that gets value from HTTP context request headers 
    /// and sets value to HTTP context response headers.
    /// </summary>
    public sealed class HttpContextHeaderValueAccessor : HttpContextValueAccessor
    {
        /// <summary>
        /// Gets the value stored in HTTP context request headers.
        /// </summary>
        /// <param name="context">The HTTP context from which to retrieve the value.</param>
        /// <param name="key">The key associated with the value to retrieve.</param>
        /// <returns>The value associated with the specified key, or <c>null</c> if not found.</returns>
        protected override string? GetHttpContextValue(HttpContext context, string key)
        {
            return context.TryGetRequestHeaderValue(key, out var values) ? values.FirstOrDefault() : null;
        }

        /// <summary>
        /// Sets the value to HTTP context response headers.
        /// </summary>
        /// <param name="context">The HTTP context in which to set the value.</param>
        /// <param name="key">The key associated with the value to set.</param>
        /// <param name="value">The value to set.</param>
        protected override void SetHttpContextValue(HttpContext context, string key, string value)
        {
            context.Response.Headers.Append(key, value);
        }
    }
}
