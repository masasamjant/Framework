using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;

namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods to <see cref="IUrlHelper"/> interface.
    /// </summary>
    public static class UrlHelper
    {
        /// <summary>
        /// Generates URL with path for an action specified by <see cref="IActionDescriptor"/>.
        /// </summary>
        /// <param name="url">The <see cref="IUrlHelper"/>.</param>
        /// <param name="actionDescriptor">The <see cref="IActionDescriptor"/>.</param>
        /// <param name="routeValues">The other route values.</param>
        /// <returns>A URL with path for action descibed by <paramref name="actionDescriptor"/>.</returns>
        public static string Action(this IUrlHelper url, IActionDescriptor actionDescriptor, object? routeValues = null)
        {
            var routeValueDictionary = new RouteValueDictionary(routeValues);

            if (!string.IsNullOrWhiteSpace(actionDescriptor.AreaName))
                routeValueDictionary["area"] = actionDescriptor.AreaName;

            return url.Action(actionDescriptor.ActionName, actionDescriptor.ControllerName, routeValueDictionary) ?? string.Empty;
        }

        /// <summary>
        /// Generates URL with path for an action specified by <see cref="ActionDescriptor"/>.
        /// </summary>
        /// <param name="url">The <see cref="IUrlHelper"/>.</param>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <param name="routeValues">The other route values.</param>
        /// <returns>A URL with path for action descibed by <paramref name="actionDescriptor"/>.</returns>
        public static string Action(this IUrlHelper url, ActionDescriptor actionDescriptor, object? routeValues = null)
            => Action(url, actionDescriptor.AsInterface(), routeValues);

        /// <summary>
        /// Check is specified URL string is absolute URI and optionally one of the specified schemes. If <paramref name="schemes"/> is <c>null</c> 
        /// or empty, then <paramref name="url"/> must be absolute URI. Otherwise <see cref="Uri.Scheme"/> must be one of in <paramref name="schemes"/>.
        /// </summary>
        /// <param name="url">The URL string.</param>
        /// <param name="schemes">The valid schemes.</param>
        /// <returns><c>true</c> if <paramref name="url"/> is valid absolute URI and, optionally, one of the schemes;<c>false</c> otherwise.</returns>
        public static bool IsValidUrl(string url, IEnumerable<string>? schemes = null)
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
        /// Check is specified URL strig absolute URI with HTTP or HTTPS scheme.
        /// </summary>
        /// <param name="url">The URL string.</param>
        /// <returns><c>true</c> if <paramref name="url"/> is absolute URI with HTTP or HTTPS scheme; <c>false</c> otherwise.</returns>
        public static bool IsValidHttpUrl(string url)
            => IsValidUrl(url, [Uri.UriSchemeHttp, Uri.UriSchemeHttps]);
    }
}
