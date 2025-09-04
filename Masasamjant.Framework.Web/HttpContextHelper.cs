using Microsoft.Extensions.Primitives;

namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods related to HTTP context.
    /// </summary>
    public static class HttpContextHelper
    {
        /// <summary>
        /// Get item stored in <see cref="HttpContext.Items"/> as <typeparamref name="T"/> instance or 
        /// if not exist or not expected type, then <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="key">The item key.</param>
        /// <param name="defaultValue">The default value to return if not exist or not expected type.</param>
        /// <returns>A value from <see cref="HttpContext.Items"/> or <paramref name="defaultValue"/>.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static T? GetItemAs<T>(this IHttpContextAccessor httpContextAccessor, string key, T? defaultValue = default)
            => TryGetItemAs<T>(httpContextAccessor, key, out var item) ? item : defaultValue;

        /// <summary>
        /// Get item stored in <see cref="HttpContext.Items"/> as <typeparamref name="T"/> instance or 
        /// if not exist or not expected type, then <paramref name="defaultValue"/>.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="context">The HTTP context.</param>
        /// <param name="key">The item key.</param>
        /// <param name="defaultValue">The default value to return if not exist or not expected type.</param>
        /// <returns>A value from <see cref="HttpContext.Items"/> or <paramref name="defaultValue"/>.</returns>
        public static T? GetItemAs<T>(this HttpContext context, string key, T? defaultValue = default)
            => TryGetItemAs<T>(context, key, out var item) ? item : defaultValue;

        /// <summary>
        /// Try get item stored in <see cref="HttpContext.Items"/> as <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="key">The item key.</param>
        /// <param name="item">The actual item, if returns <c>true</c>; otherwise default value.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was get from <see cref="HttpContext.Items"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static bool TryGetItemAs<T>(this IHttpContextAccessor httpContextAccessor, string key, out T? item)
            => TryGetItemAs(GetHttpContext(httpContextAccessor), key, out item);

        /// <summary>
        /// Try get item stored in <see cref="HttpContext.Items"/> as <typeparamref name="T"/> instance.
        /// </summary>
        /// <typeparam name="T">The expected type.</typeparam>
        /// <param name="context">The HTTP context.</param>
        /// <param name="key">The item key.</param>
        /// <param name="item">The actual item, if returns <c>true</c>; otherwise default value.</param>
        /// <returns><c>true</c> if <paramref name="item"/> was get from <see cref="HttpContext.Items"/>; <c>false</c> otherwise.</returns>
        public static bool TryGetItemAs<T>(this HttpContext context, string key, out T? item)
        {
            if (context.Items.TryGetValue(key, out var value) && value is T actual)
            {
                item = actual;
                return true;
            }

            item = default;
            return false;
        }

        /// <summary>
        /// Get values of HTTP request header specified by name.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="name">The header name.</param>
        /// <returns>A value of header or empty.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static IEnumerable<string> GetRequestHeaderValues(this IHttpContextAccessor httpContextAccessor, string name)
            => GetRequestHeaderValues(GetHttpContext(httpContextAccessor), name);

        /// <summary>
        /// Get values of HTTP request header specified by name.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="name">The header name.</param>
        /// <returns>A value of header or empty.</returns>
        public static IEnumerable<string> GetRequestHeaderValues(this HttpContext context, string name)
            => GetHeaderValues(context.Request.Headers, name);

        /// <summary>
        /// Get values of HTTP response header specified by name.
        /// </summary>
        /// <param name="httpContextAccessor">The HTTP context accessor.</param>
        /// <param name="name">The header name.</param>
        /// <returns>A value of header or empty.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static IEnumerable<string> GetResponseHeaderValues(this IHttpContextAccessor httpContextAccessor, string name)
            => GetResponseHeaderValues(GetHttpContext(httpContextAccessor), name);

        /// <summary>
        /// Get values of HTTP response header specified by name.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="name">The header name.</param>
        /// <returns>A value of header or empty.</returns>
        public static IEnumerable<string> GetResponseHeaderValues(this HttpContext context, string name)
            => GetHeaderValues(context.Response.Headers, name);

        /// <summary>
        /// Get values of HTTP header specified by name.
        /// </summary>
        /// <param name="headers">The <see cref="IHeaderDictionary"/>.</param>
        /// <param name="name">The header name.</param>
        /// <returns>A values of header or empty.</returns>
        public static IEnumerable<string> GetHeaderValues(this IHeaderDictionary headers, string name)
        {
            if (headers.TryGetValue(name, out var values) && values.Count > 0)
            {
                foreach (var value in values)
                    yield return value ?? string.Empty;
            }
        }

        /// <summary>
        /// Try get value of HTTP request header specified by name.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="name">The header name.</param>
        /// <param name="values">The headers values if returns <c>true</c>; empty otherwise.</param>
        /// <returns><c>true</c> if get values of specified header; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static bool TryGetRequestHeaderValue(this IHttpContextAccessor httpContextAccessor, string name, out IEnumerable<string> values)
            => TryGetRequestHeaderValue(GetHttpContext(httpContextAccessor), name, out values);

        /// <summary>
        /// Try get value of HTTP request header specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="name">The header name.</param>
        /// <param name="values">The headers values if returns <c>true</c>; empty otherwise.</param>
        /// <returns><c>true</c> if get values of specified header; <c>false</c> otherwise.</returns>
        public static bool TryGetRequestHeaderValue(this HttpContext context, string name, out IEnumerable<string> values)
            => TryGetHeaderValue(context.Request.Headers, name, out values);

        /// <summary>
        /// Try get value of HTTP response header specified by name.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="name">The header name.</param>
        /// <param name="values">The headers values if returns <c>true</c>; empty otherwise.</param>
        /// <returns><c>true</c> if get values of specified header; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static bool TryGetResponseHeaderValue(this IHttpContextAccessor httpContextAccessor, string name, out IEnumerable<string> values)
            => TryGetResponseHeaderValue(GetHttpContext(httpContextAccessor), name, out values);

        /// <summary>
        /// Try get value of HTTP response header specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="name">The header name.</param>
        /// <param name="values">The headers values if returns <c>true</c>; empty otherwise.</param>
        /// <returns><c>true</c> if get values of specified header; <c>false</c> otherwise.</returns>
        public static bool TryGetResponseHeaderValue(this HttpContext context, string name, out IEnumerable<string> values)
            => TryGetHeaderValue(context.Response.Headers, name, out values);

        /// <summary>
        /// Try get value of HTTP header specified by name.
        /// </summary>
        /// <param name="headers">The <see cref="IHeaderDictionary"/>.</param>
        /// <param name="name">The header name.</param>
        /// <param name="values">The headers values if returns <c>true</c>; empty otherwise.</param>
        /// <returns><c>true</c> if get values of specified header; <c>false</c> otherwise.</returns>
        public static bool TryGetHeaderValue(this IHeaderDictionary headers, string name, out IEnumerable<string> values)
        {
            if (headers.TryGetValue(name, out StringValues headerValue))
            {
                if (headerValue.Count > 0)
                    values = headerValue.ToArray<string>();
                else
                    values = [];
                
                return true;
            }

            values = [];
            return false;
        }

        /// <summary>
        /// Gets session identifier string stored to HTTP context items.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="sessionIdentifierKey">The session identifier key. Default value is <see cref="HttpDefaultKeys.SessionIdentifierKey"/>.</param>
        /// <returns>A session identifier string or <c>null</c>, if not exist or not string.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sessionIdentifierKey"/> is empty or only whitespace.</exception>
        public static string? GetSessionIdentifier(this HttpContext context, string sessionIdentifierKey = HttpDefaultKeys.SessionIdentifierKey)
        {
            if (string.IsNullOrWhiteSpace(sessionIdentifierKey))
                throw new ArgumentNullException(nameof(sessionIdentifierKey), "The session identifier key is empty or only whitespace.");

            return context.Items[sessionIdentifierKey] as string;
        }

        /// <summary>
        /// Gets the value of request cookie specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <returns>A cookie value or <c>null</c>.</returns>
        public static string? GetCookieValue(this HttpContext context, string cookieName)
            => TryGetCookieValue(context, cookieName, out var cookieValue) ? cookieValue : null;

        /// <summary>
        /// Gets the value of request cookie specified by name.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <returns>A cookie value or <c>null</c>.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static string? GetCookieValue(this IHttpContextAccessor httpContextAccessor, string cookieName)
            => GetCookieValue(GetHttpContext(httpContextAccessor), cookieName);

        /// <summary>
        /// Try get the value of request cookie specified by name.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value if returns <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if cookie value exist; <c>false</c> otherwise.</returns>
        public static bool TryGetCookieValue(this HttpContext context, string cookieName, out string? cookieValue)
            =>  TryGetCookieValue(context.Request.Cookies, cookieName, out cookieValue);

        /// <summary>
        /// Try get the value of request cookie specified by name.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value if returns <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if cookie value exist; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static bool TryGetCookieValue(this IHttpContextAccessor httpContextAccessor, string cookieName, out string? cookieValue)
            => TryGetCookieValue(GetHttpContext(httpContextAccessor), cookieName, out cookieValue);

        /// <summary>
        /// Try get the value of request cookie specified by name.
        /// </summary>
        /// <param name="requestCookies">The <see cref="IRequestCookieCollection"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value if returns <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if cookie value exist; <c>false</c> otherwise.</returns>
        public static bool TryGetCookieValue(IRequestCookieCollection requestCookies, string cookieName, out string? cookieValue)
            => requestCookies.TryGetValue(cookieName, out cookieValue);

        /// <summary>
        /// Set response cookie with specified name and value using provided settings.
        /// </summary>
        /// <param name="context">The <see cref="HttpContext"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <param name="path">The cookie path.</param>
        /// <param name="expires">The expiration date and time.</param>
        /// <param name="httpOnly"><c>true</c> if HTTP only; <c>false</c> otherwise.</param>
        /// <param name="secure"><c>true</c> if only transmit in HTTPS; <c>false</c> otherwise.</param>
        /// <param name="essential"><c>true</c> if cookie is essential; <c>false</c> otherwise.</param>
        /// <param name="domain">The domain associated with cookie.</param>
        public static void SetCookieValue(this HttpContext context, string cookieName, string cookieValue, string? path = null, DateTimeOffset? expires = null, bool httpOnly = true, bool secure = false, bool essential = false, string? domain = null)
            => SetCookieValue(context.Response.Cookies, cookieName, cookieValue, path, expires, httpOnly, secure, essential, domain);

        /// <summary>
        /// Set response cookie with specified name and value using provided settings.
        /// </summary>
        /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <param name="path">The cookie path.</param>
        /// <param name="expires">The expiration date and time.</param>
        /// <param name="httpOnly"><c>true</c> if HTTP only; <c>false</c> otherwise.</param>
        /// <param name="secure"><c>true</c> if only transmit in HTTPS; <c>false</c> otherwise.</param>
        /// <param name="essential"><c>true</c> if cookie is essential; <c>false</c> otherwise.</param>
        /// <param name="domain">The domain associated with cookie.</param>
        /// <exception cref="InvalidOperationException">If HTTP context is not available via <paramref name="httpContextAccessor"/>.</exception>
        public static void SetCookieValue(this IHttpContextAccessor httpContextAccessor, string cookieName, string cookieValue, string? path = null, DateTimeOffset? expires = null, bool httpOnly = true, bool secure = false, bool essential = false, string? domain = null)
            => SetCookieValue(GetHttpContext(httpContextAccessor), cookieName, cookieValue, path, expires, httpOnly, secure, essential, domain);

        /// <summary>
        /// Set response cookie with specified name and value using provided settings.
        /// </summary>
        /// <param name="responseCookies">The <see cref="IResponseCookies"/>.</param>
        /// <param name="cookieName">The cookie name.</param>
        /// <param name="cookieValue">The cookie value.</param>
        /// <param name="path">The cookie path.</param>
        /// <param name="expires">The expiration date and time.</param>
        /// <param name="httpOnly"><c>true</c> if HTTP only; <c>false</c> otherwise.</param>
        /// <param name="secure"><c>true</c> if only transmit in HTTPS; <c>false</c> otherwise.</param>
        /// <param name="essential"><c>true</c> if cookie is essential; <c>false</c> otherwise.</param>
        /// <param name="domain">The domain associated with cookie.</param>
        public static void SetCookieValue(IResponseCookies responseCookies, string cookieName, string cookieValue, string? path = null, DateTimeOffset? expires = null, bool httpOnly = true, bool secure = false, bool essential = false, string? domain = null)
        {
            var options = new CookieOptions()
            {
                Domain = domain,
                Expires = expires,
                HttpOnly = httpOnly,
                Secure = secure,
                IsEssential = essential
            };

            if (!string.IsNullOrWhiteSpace(path))
                options.Path = path;

            responseCookies.Append(cookieName, cookieValue, options);
        }

        private static HttpContext GetHttpContext(IHttpContextAccessor httpContextAccessor)
        {
            return httpContextAccessor.HttpContext ?? throw new InvalidOperationException("HTTP context is not available.");
        }
    }
}
