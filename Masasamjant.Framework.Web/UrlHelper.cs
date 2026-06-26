namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods to work with URL and URI.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Check if specified URL is absolute URI and optionally one of the specified schemes. If <paramref name="schemes"/> is <c>null</c> 
        /// or empty, then <paramref name="url"/> must be absolute URI. Otherwise <see cref="Uri.Scheme"/> must be one of the specified schemes.
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <param name="schemes">The allowed schemes.</param>
        /// <returns><c>true</c> if the URL is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidAbsoluteUrl(string url, IEnumerable<string>? schemes = null)
        {
            if (Uri.TryCreate(url, UriKind.Absolute, out var uri))
            {
                if (schemes != null && schemes.Any())
                    return schemes.Contains(uri.Scheme);
            
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if specified URL is absolute URI with HTTP or HTTPS scheme.
        /// </summary>
        /// <param name="url">The URL to validate.</param>
        /// <returns><c>true</c> if the URL is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidHttpUrl(string url) => IsValidAbsoluteUrl(url, new[] { Uri.UriSchemeHttp, Uri.UriSchemeHttps });
    }
}
