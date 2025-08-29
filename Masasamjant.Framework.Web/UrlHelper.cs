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
            if (string.IsNullOrWhiteSpace(actionDescriptor.AreaName))
                return url.Action(actionDescriptor.ActionName, actionDescriptor.ControllerName, routeValues) ?? string.Empty;
            else
            {
                var routeValueDictionary = new RouteValueDictionary(routeValues);
                routeValueDictionary["area"] = actionDescriptor.AreaName;
                return url.Action(actionDescriptor.ActionName, actionDescriptor.ControllerName, routeValueDictionary) ?? string.Empty;
            }
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
    }
}
