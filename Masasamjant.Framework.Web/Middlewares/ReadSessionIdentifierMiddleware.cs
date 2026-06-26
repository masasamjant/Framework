namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Represents middleware that reads session identifier from HTTP header and stores it using specified <see cref="IHttpContextValueSetter"/>.
    /// </summary>
    public sealed class ReadSessionIdentifierMiddleware : Middleware
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReadSessionIdentifierMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> to process HTTP request in the pipeline.</param>
        /// <param name="sessionIdentifierHeaderName">The name of HTTP header to read session identifier.</param>
        /// <param name="sessionIdentifierKey">The key to store session identifier.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="next"/> is <c>null</c>.</exception>
        public ReadSessionIdentifierMiddleware(RequestDelegate next, string? sessionIdentifierHeaderName, string? sessionIdentifierKey)
            : base(next)
        {
            SessionIdentifierHeaderName = sessionIdentifierHeaderName;
            SessionIdentifierKey = sessionIdentifierKey;
        }

        /// <summary>
        /// Gets the name of HTTP header to read session identifier.
        /// If not specified, then header is not read.
        /// </summary>
        public string? SessionIdentifierHeaderName { get; }

        /// <summary>
        /// Gets the key to store session identifier. 
        /// If not specified, then value is not stored.
        /// </summary>
        public string? SessionIdentifierKey { get; }

        /// <summary>
        /// Invoked when middleware is executed. 
        /// Read HTTP header for session identifier and then stores it using <paramref name="sessionIdentifierSetter"/>.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="sessionIdentifierSetter">The setter to store the session identifier.</param>
        /// <returns>A task that represents the completion of request processing.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="context"/> or <paramref name="sessionIdentifierSetter"/> is <c>null</c>.</exception>
        public async Task InvokeAsync(HttpContext context, IHttpContextValueSetter sessionIdentifierSetter)
        {
            ArgumentNullException.ThrowIfNull(context);
            ArgumentNullException.ThrowIfNull(sessionIdentifierSetter);

            if (!string.IsNullOrWhiteSpace(SessionIdentifierHeaderName) && !string.IsNullOrWhiteSpace(SessionIdentifierKey))
            {
                if (context.TryGetRequestHeaderValue(SessionIdentifierHeaderName, out var values) && values.Count > 0)
                {
                    var sessionIdentifier = values.First();

                    if (!string.IsNullOrWhiteSpace(sessionIdentifier))
                        sessionIdentifierSetter.SetHttpValue(context, SessionIdentifierKey, sessionIdentifier);
                }
            }
        }
    }
}
