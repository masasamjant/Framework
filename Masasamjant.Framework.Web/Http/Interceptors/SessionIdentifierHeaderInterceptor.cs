using Masasamjant.Http;
using Masasamjant.Http.Abstractions;
using HttpRequest = Masasamjant.Http.Abstractions.HttpRequest;

namespace Masasamjant.Web.Http.Interceptors
{
    /// <summary>
    /// Represents <see cref="HttpRequestInterceptor"/> that adds a session identifier header to HTTP requests.
    /// </summary>
    public sealed class SessionIdentifierHeaderInterceptor : HttpRequestInterceptor
    {
        private readonly ISessionStorageProvider sessionStorageProvider;

        /// <summary>
        /// Initializes new instance of the <see cref="SessionIdentifierHeaderInterceptor"/> class.
        /// </summary>
        /// <param name="sessionStorageProvider">The session storage provider.</param>
        /// <param name="sessionIdentifierHeaderName">The name of the session identifier header.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="sessionStorageProvider"/> is <c>null</c>.</exception>
        public SessionIdentifierHeaderInterceptor(ISessionStorageProvider sessionStorageProvider, string? sessionIdentifierHeaderName)
        {
            this.sessionStorageProvider = sessionStorageProvider ?? throw new ArgumentNullException(nameof(sessionStorageProvider));
            SessionIdentifierHeaderName = sessionIdentifierHeaderName;
        }

        /// <summary>
        /// Gets the name of HTTP header to which session identifier will be added.
        /// </summary>
        /// <remarks>If <c>null</c>, empty or only whitespace, then session identifier header is not added.</remarks>
        public string? SessionIdentifierHeaderName { get; }

        /// <summary>
        /// Intercepts specified <see cref="HttpGetRequest"/> before it is sent and adds session identifier header.
        /// </summary>
        /// <param name="request">The HTTP GET request to intercept.</param>
        /// <returns>A task representing the asynchronous operation, containing the interception result.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <c>null</c>.</exception>
        public override Task<HttpRequestInterception> InterceptAsync(HttpGetRequest request)
        {
            return Task.FromResult(AddSessionIdentifierHader(request));
        }

        /// <summary>
        /// Intercepts specified <see cref="HttpPostRequest"/> before it is sent and adds session identifier header.
        /// </summary>
        /// <param name="request">The HTTP POST request to intercept.</param>
        /// <returns>A task representing the asynchronous operation, containing the interception result.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="request"/> is <c>null</c>.</exception>
        public override Task<HttpRequestInterception> InterceptAsync(HttpPostRequest request)
        {
            return Task.FromResult(AddSessionIdentifierHader(request));
        }

        private HttpRequestInterception AddSessionIdentifierHader(HttpRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (!string.IsNullOrWhiteSpace(SessionIdentifierHeaderName) &&
                !request.Headers.Contains(SessionIdentifierHeaderName))
            {
                var sessionIdentifier = GetSessionIdentifier();
                request.Headers.Add(SessionIdentifierHeaderName, sessionIdentifier);
            }

            return HttpRequestInterception.Continue;
        }

        private string GetSessionIdentifier()
        {
            var storage = sessionStorageProvider.GetSessionStorage();
            return storage.GetSessionIdentifier();
        }
    }
}
