namespace Masasamjant.Web
{
    /// <summary>
    /// Represents <see cref="ISessionStorageProvider"/> that provides <see cref="HttpSessionStorage"/>.    
    /// </summary>
    public sealed class HttpSessionStorageProvider : ISessionStorageProvider
    {
        private readonly IHttpContextAccessor? contextAccessor;
        private readonly HttpContext? context;

        /// <summary>
        /// Initializes new instance of the <see cref="HttpSessionStorageProvider"/> class.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="context"/> is null.</exception>
        public HttpSessionStorageProvider(HttpContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
            this.contextAccessor = null;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="HttpSessionStorageProvider"/> class.
        /// </summary>
        /// <param name="contextAccessor">The HTTP context accessor.</param>
        /// <exception cref="ArgumentNullException">If the <paramref name="contextAccessor"/> is null.</exception>
        public HttpSessionStorageProvider(IHttpContextAccessor contextAccessor)
        {
            this.context = null;
            this.contextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        }

        /// <summary>
        /// Gets the <see cref="HttpSessionStorage"/>.
        /// </summary>
        /// <returns>A <see cref="HttpSessionStorage"/> instance.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available.</exception>
        public HttpSessionStorage GetSessionStorage()
        {
            var httpContext = GetHttpContext();
            
            if (httpContext == null)
                throw new InvalidOperationException("HTTP context is not available.");
            
            return new HttpSessionStorage(httpContext.Session);
        }

        private HttpContext? GetHttpContext()
        {
            if (context != null)
                return context;

            if (contextAccessor != null)
                return contextAccessor.HttpContext;

            return null;
        }

        ISessionStorage ISessionStorageProvider.GetSessionStorage()
        {
            return GetSessionStorage();
        }
    }
}
