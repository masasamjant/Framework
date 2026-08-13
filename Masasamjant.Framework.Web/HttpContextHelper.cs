namespace Masasamjant.Web
{
    /// <summary>
    /// Represents a helper class for accessing HTTP context related values.
    /// </summary>
    public static class HttpContextHelper
    {
        /// <summary>
        /// Gets the <see cref="HttpContext"/> from specified <see cref="IHttpContextAccessor"/>.
        /// </summary>
        /// <param name="contextAccessor">The HTTP context accessor.</param>
        /// <returns>The current HTTP context.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="contextAccessor"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">If the HTTP context is not available.</exception>
        public static HttpContext GetContext(this IHttpContextAccessor contextAccessor)
        {
            ArgumentNullException.ThrowIfNull(contextAccessor);

            var context = contextAccessor.HttpContext;

            if (context == null)
                throw new InvalidOperationException("HTTP context is not available.");

            return context;
        }

        /// <summary>
        /// Gets the values of HTTP request header from the specified header collection.
        /// </summary>
        /// <param name="headers">The HTTP headers collection.</param>
        /// <param name="name">The name of the HTTP header.</param>
        /// <returns>The values of the specified header.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="headers"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static IEnumerable<string> GetRequestHeaderValues(this HttpContext context, string name)
        {
            ArgumentNullException.ThrowIfNull(context);
            return GetHeaderValues(context.Request.Headers, name);
        }

        /// <summary>
        /// Gets the values of HTTP response header from the specified header collection.
        /// </summary>
        /// <param name="headers">The HTTP headers collection.</param>
        /// <param name="name">The name of the HTTP header.</param>
        /// <returns>The values of the specified header.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="headers"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static IEnumerable<string> GetResponseHeaderValues(this HttpContext context, string name)
        {
            ArgumentNullException.ThrowIfNull(context);
            return GetHeaderValues(context.Response.Headers, name);
        }

        /// <summary>
        /// Gets the values of HTTP header from the specified header collection.
        /// </summary>
        /// <param name="headers">The HTTP headers collection.</param>
        /// <param name="name">The name of the HTTP header.</param>
        /// <returns>The values of the specified header.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="headers"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static IEnumerable<string> GetHeaderValues(this IHeaderDictionary headers, string name)
        {
            ArgumentNullException.ThrowIfNull(headers);
            ValidateName(name);

            if (headers.TryGetValue(name, out var values) && values.Count > 0)
            {
                foreach (var value in values)
                    yield return value ?? string.Empty;
            }
        }

        /// <summary>
        /// Try get value of HTTP request header specified by name.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="name">The name of HTTP header.</param>
        /// <param name="values">The values of HTTP header, when returns <c>true</c>; otherwise empty collection.</param>
        /// <returns><c>true</c> if the header exists and has values; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="headers"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static bool TryGetRequestHeaderValue(this HttpContext context, string name, out IReadOnlyCollection<string> values)
        {
            ArgumentNullException.ThrowIfNull(context);
            return TryGetHeaderValue(context.Request.Headers, name, out values);
        }

        /// <summary>
        /// Try get value of HTTP response header specified by name.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="name">The name of HTTP header.</param>
        /// <param name="values">The values of HTTP header, when returns <c>true</c>; otherwise empty collection.</param>
        /// <returns><c>true</c> if the header exists and has values; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="headers"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static bool TryGetResponseHeaderValue(this HttpContext context, string name, out IReadOnlyCollection<string> values)
        {
            ArgumentNullException.ThrowIfNull(context);
            return TryGetHeaderValue(context.Response.Headers, name, out values);
        }

        /// <summary>
        /// Try get value of HTTP header specified by name.
        /// </summary>
        /// <param name="headers">The HTTP header collection.</param>
        /// <param name="name">The name of HTTP header.</param>
        /// <param name="values">The values of HTTP header, when returns <c>true</c>; otherwise empty collection.</param>
        /// <returns><c>true</c> if the header exists and has values; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="headers"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static bool TryGetHeaderValue(this IHeaderDictionary headers, string name, out IReadOnlyCollection<string> values)
        {
            ArgumentNullException.ThrowIfNull(headers);
            ValidateName(name);

            var result = new List<string>();
            var success = false;

            if (headers.TryGetValue(name, out var value))
            {
                if (value.Count > 0)
                    result.AddRange(value.Select(x => x ?? string.Empty));

                success = true;
            }

            values = result.AsReadOnly();
            return success;
        }

        /// <summary>
        /// Get value of the request cookie specified by name.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="name">The cookie name.</param>
        /// <returns>A value of the cookie or <c>null</c>, if the cookie does not exist.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="context"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static string? GetCookieValue(this HttpContext context, string name)
        {
            ArgumentNullException.ThrowIfNull(context);
            return GetCookieValue(context.Request.Cookies, name);
        }

        /// <summary>
        /// Get value of the request cookie specified by name.
        /// </summary>
        /// <param name="cookies">The request cookies.</param>
        /// <param name="name">The cookie name.</param>
        /// <returns>A value of the cookie or <c>null</c>, if the cookie does not exist.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="cookies"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static string? GetCookieValue(this IRequestCookieCollection cookies, string name)
            => TryGetCookieValue(cookies, name, out var value) ? value : null;

        /// <summary>
        /// Tries to get value of the request cookie specified by name.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value, if returns <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the cookie exists and has a value; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="context"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static bool TryGetCookieValue(this HttpContext context, string name, out string? value)
        {
            ArgumentNullException.ThrowIfNull(context);
            return TryGetCookieValue(context.Request.Cookies, name, out value);
        }

        /// <summary>
        /// Tries to get value of the request cookie specified by name.
        /// </summary>
        /// <param name="cookies">The request cookies.</param>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value, if returns <c>true</c>; otherwise <c>null</c>.</param>
        /// <returns><c>true</c> if the cookie exists and has a value; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="cookies"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        public static bool TryGetCookieValue(this IRequestCookieCollection cookies, string name, out string? value)
        {
            ArgumentNullException.ThrowIfNull(cookies);
            ValidateName(name);
            return cookies.TryGetValue(name, out value);
        }

        /// <summary>
        /// Set response cookie with specified name and value using provided options.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value.</param>
        /// <param name="path">The cookie path.</param>
        /// <param name="expires">The cookie expiration.</param>
        /// <param name="maxAge">The cookie max age.</param>
        /// <param name="httpOnly"><c>true</c> if the cookie is HTTP only; otherwise, <c>false</c>.</param>
        /// <param name="secure"><c>true</c> if the cookie is secure; otherwise, <c>false</c>.</param>
        /// <param name="isEssential"><c>true</c> if the cookie is essential; otherwise, <c>false</c>.</param>
        /// <param name="domain">The cookie domain.</param>
        /// <param name="sameSiteMode">The cookie <see cref="SameSiteMode"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="context"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        /// <exception cref="ArgumentException">If value of <paramref name="sameSiteMode"/> is not defined.</exception>
        public static void SetCookieValue(this HttpContext context, string name, string value,
            string? path = null, DateTimeOffset? expires = null, TimeSpan? maxAge = null, bool httpOnly = true, bool secure = true, bool isEssential = false,
            string? domain = null, SameSiteMode sameSiteMode = SameSiteMode.Unspecified)
        {
            ArgumentNullException.ThrowIfNull(context);
            SetCookieValue(context.Response.Cookies, name, value, path, expires, maxAge, httpOnly, secure, isEssential, domain, sameSiteMode);
        }

        /// <summary>
        /// Set response cookie with specified name and value using provided options.
        /// </summary>
        /// <param name="cookies">The response cookies.</param>
        /// <param name="name">The cookie name.</param>
        /// <param name="value">The cookie value.</param>
        /// <param name="path">The cookie path.</param>
        /// <param name="expires">The cookie expiration.</param>
        /// <param name="maxAge">The cookie max age.</param>
        /// <param name="httpOnly"><c>true</c> if the cookie is HTTP only; otherwise, <c>false</c>.</param>
        /// <param name="secure"><c>true</c> if the cookie is secure; otherwise, <c>false</c>.</param>
        /// <param name="isEssential"><c>true</c> if the cookie is essential; otherwise, <c>false</c>.</param>
        /// <param name="domain">The cookie domain.</param>
        /// <param name="sameSiteMode">The cookie <see cref="SameSiteMode"/>.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="cookies"/> is <c>null</c>.
        /// -or-
        /// If <paramref name="name"/> is <c>null</c>, empty or only whitespace.
        /// </exception>
        /// <exception cref="ArgumentException">If value of <paramref name="sameSiteMode"/> is not defined.</exception>
        public static void SetCookieValue(this IResponseCookies cookies, string name, string value,
            string? path = null, DateTimeOffset? expires = null, TimeSpan? maxAge = null, bool httpOnly = true, bool secure = true, bool isEssential = false,
            string? domain = null, SameSiteMode sameSiteMode = SameSiteMode.Unspecified)
        {
            ArgumentNullException.ThrowIfNull(cookies);
            ValidateName(name);
            if (!Enum.IsDefined(sameSiteMode))
                throw new ArgumentException("The value is not defined.", nameof(sameSiteMode));

            var options = new CookieOptions()
            {
                Domain = domain,
                Expires = expires,
                MaxAge = maxAge,
                HttpOnly = httpOnly,
                Secure = secure,
                IsEssential = isEssential,
                SameSite = sameSiteMode
            };

            if (!string.IsNullOrWhiteSpace(path))
                options.Path = path;

            cookies.Append(name, value, options);
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "Name cannot be null, empty or only whitespace.");
        }
    }
}
