namespace Masasamjant.Web.Sessions
{
    /// <summary>
    /// Represents provider of <see cref="HttpContextSessionStorage"/>.
    /// </summary>
    public sealed class HttpContextSessionStorageProvider : ISessionStorageProvider
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        
        /// <summary>
        /// Initializes new instance of the <see cref="HttpContextSessionStorageProvider"/> class.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        public HttpContextSessionStorageProvider(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Gets session storage.
        /// </summary>
        /// <returns>A <see cref="ISessionStorage"/>.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context or session is not available.</exception>
        public HttpContextSessionStorage GetSessionStorage()
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null)
                throw new InvalidOperationException("HTTP context is not available.");

            if (!httpContext.Session.IsAvailable)
                throw new InvalidOperationException("HTTP session is not available.");

            return new HttpContextSessionStorage(httpContext);
        }

        ISessionStorage ISessionStorageProvider.GetSessionStorage()
        {
            return this.GetSessionStorage();
        }
    }
}
