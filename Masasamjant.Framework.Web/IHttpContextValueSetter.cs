namespace Masasamjant.Web
{
    /// <summary>
    /// Represents component that store value associated with <see cref="HttpContext"/>.
    /// </summary>
    public interface IHttpContextValueSetter
    {
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
        void SetHttpValue(HttpContext context, string key, string value);
    }
}
