namespace Masasamjant.Web
{
    /// <summary>
    /// Represents component to get value associated with <see cref="HttpContext"/>.
    /// </summary>
    public interface IHttpContextValueGetter
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
        string? GetHttpValue(HttpContext context, string key);
    }
}
