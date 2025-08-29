
namespace Masasamjant.Web.Middlewares
{
    /// <summary>
    /// Middleware that reads incoming HTTP request header containing session identifier and 
    /// saves it to HTTP context items.
    /// </summary>
    public sealed class HttpHeaderSessionIdentifierMiddleware : Middleware
    {
        private string sessionIdentifierHttpHeader;
        private string sessionIdentifierKey;

        /// <summary>
        /// Initializes new instance of the <see cref="HttpHeaderSessionIdentifierMiddleware"/> class.
        /// </summary>
        /// <param name="next">The <see cref="RequestDelegate"/> of the next action.</param>
        public HttpHeaderSessionIdentifierMiddleware(RequestDelegate next)
            : base(next)
        {
            sessionIdentifierHttpHeader = HttpDefaultKeys.SessionIdentifierKey;
            sessionIdentifierKey = HttpDefaultKeys.SessionIdentifierKey;
        }

        /// <summary>
        /// Gets or sets name of HTTP header of session identifier. Default value is <see cref="HttpDefaultKeys.SessionIdentifierKey"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">If value to set is empty or only whitespace.</exception>
        public string SessionIdentifierHttpHeader
        {
            get { return sessionIdentifierHttpHeader; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(SessionIdentifierHttpHeader), "The value cannot be empty or only whitespace.");

                sessionIdentifierHttpHeader = value;
            }
        }

        /// <summary>
        /// Gets or sets key of HTTP item to save session identifier. Default value is <see cref="HttpDefaultKeys.SessionIdentifierKey"/>.
        /// </summary>
        /// <exception cref="ArgumentNullException">If value to set is empty or only whitespace.</exception>
        public string SessionIdentifierKey
        {
            get { return sessionIdentifierKey; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(SessionIdentifierKey), "The value cannot be empty or only whitespace.");

                sessionIdentifierKey = value;
            }
        }

        /// <summary>
        /// Executed middleware action. Reads session identifier from HTTP header if available and saves to context items.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/> where middleware is executed.</param>
        /// <returns>A task.</returns>
        public override async Task InvokeAsync(HttpContext context)
        {
            if (context.TryGetRequestHeaderValue(SessionIdentifierHttpHeader, out var values) && values.Any())
            {
                var sessionIdentifier = values.First();
                context.Items[SessionIdentifierKey] = sessionIdentifier;
            }

            await Next(context);
        }
    }
}
