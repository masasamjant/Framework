using Masasamjant.Http.Abstractions;
using Masasamjant.Web.Http.Interceptors;

namespace Masasamjant.Web.Http
{
    /// <summary>
    /// Provides extension methods to <see cref="IHttpClient"/> interface.
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Add <see cref="SessionIdentifierHeaderInterceptor"/> to specified <see cref="IHttpClient"/>.
        /// </summary>
        /// <param name="client">The HTTP client to which the interceptor will be added.</param>
        /// <param name="sessionStorageProvider">The session storage provider.</param>
        /// <param name="sessionIdentifierHeaderName">The name of the session identifier header.</param>
        /// <returns>The HTTP client with the added interceptor.</returns>
        /// <remarks>If <paramref name="sessionIdentifierHeaderName"/> is <c>null</c> or empty, or whitespace the interceptor will not add the session identifier header.</remarks>
        public static IHttpClient AddSessionIdentifierHeaderInterceptor(this IHttpClient client, ISessionStorageProvider sessionStorageProvider, string? sessionIdentifierHeaderName)
        {
            ArgumentNullException.ThrowIfNull(client);
            var interceptor = new SessionIdentifierHeaderInterceptor(sessionStorageProvider, sessionIdentifierHeaderName);
            client.HttpGetRequestInterceptors.Add(interceptor);
            client.HttpPostRequestInterceptors.Add(interceptor);
            return client;
        }
    }
}
