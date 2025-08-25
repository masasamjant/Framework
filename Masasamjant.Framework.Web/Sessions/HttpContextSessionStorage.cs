namespace Masasamjant.Web.Sessions
{
    /// <summary>
    /// Represents storage of session values using session provided by <see cref="HttpContext"/>.
    /// </summary>
    public sealed class HttpContextSessionStorage : ISessionStorage
    {
        private readonly ISession session;

        /// <summary>
        /// Initializes new instance of the <see cref="HttpContextSessionStorage"/> class.
        /// </summary>
        /// <param name="httpContext">The <see cref="HttpContext"/>.</param>
        public HttpContextSessionStorage(HttpContext httpContext)
        {
            session = httpContext.Session;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="HttpContextSessionStorage"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <exception cref="InvalidOperationException">If HTTP context is not available.</exception>
        public HttpContextSessionStorage(IHttpContextAccessor httpContextAccessor)
            : this(httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HTTP context is not available."))
        { }

        /// <summary>
        /// Clear session content.
        /// </summary>
        public void Clear()
        {
            session.Clear();
        }

        /// <summary>
        /// Gets value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <returns>A value in session or <c>null</c>.</returns>
        public string? GetString(string key)
        {
            return session.GetString(key);
        }

        /// <summary>
        /// Remove value from session.
        /// </summary>
        /// <param name="key">The session key.</param>
        public void Remove(string key)
        {
            session.Remove(key);
        }

        /// <summary>
        /// Set value to session.
        /// </summary>
        /// <param name="key">The session key.</param>
        /// <param name="value">The value to set.</param>
        public void SetString(string key, string value)
        {
            session.SetString(key, value);
        }
    }
}
