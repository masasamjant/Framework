using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Provides helper methods for working with action descriptors.
    /// </summary>
    public static class ActionDescriptorHelper
    {
        /// <summary>
        /// Gets the action name if <paramref name="actionDescriptor"/> is <see cref="ControllerActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The action name if the action descriptor is a controller action descriptor; otherwise, an empty string.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="actionDescriptor"/> is <c>null</c>.</exception>
        public static string GetActionName(this ActionDescriptor actionDescriptor)
        {
            ArgumentNullException.ThrowIfNull(actionDescriptor);

            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return controllerActionDescriptor.ActionName;

            return string.Empty;
        }

        /// <summary>
        /// Gets the controller name if <paramref name="actionDescriptor"/> is <see cref="ControllerActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The controller name if the action descriptor is a controller action descriptor; otherwise, an empty string.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="actionDescriptor"/> is <c>null</c>.</exception>
        public static string GetControllerName(this ActionDescriptor actionDescriptor)
        {
            ArgumentNullException.ThrowIfNull(actionDescriptor);

            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return controllerActionDescriptor.ControllerName;

            return string.Empty;
        }

        /// <summary>
        /// Gets the area name if <paramref name="actionDescriptor"/> is <see cref="ControllerActionDescriptor"/> or <see cref="PageActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor.</param>
        /// <returns>The area name if the action descriptor is a controller or page action descriptor; otherwise, <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="actionDescriptor"/> is <c>null</c>.</exception>
        public static string? GetAreaName(this ActionDescriptor actionDescriptor)
        {
            ArgumentNullException.ThrowIfNull(actionDescriptor);

            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
            {
                if (controllerActionDescriptor.RouteValues.ContainsKey("area"))
                    return controllerActionDescriptor.RouteValues["area"];
            }
            else if (actionDescriptor is PageActionDescriptor pageActionDescriptor)
                return pageActionDescriptor.AreaName;

            return null;
        }

        /// <summary>
        /// Converts specified <see cref="ActionDescriptor"/> to <see cref="IActionDescriptor"/> implementation.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor to convert.</param>
        /// <returns>The converted <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor AsInterface(this ActionDescriptor actionDescriptor)
            => new MvcActionDescriptor(actionDescriptor.GetActionName(), actionDescriptor.GetControllerName(), actionDescriptor.GetAreaName());

        /// <summary>
        /// Converts specified <see cref="ActionDescriptor"/> to <see cref="IActionDescriptor"/> implementation.
        /// </summary>
        /// <param name="actionDescriptor">The action descriptor to convert.</param>
        /// <param name="action">The action name to use, or <c>null</c> to use the action descriptor's action name.</param>
        /// <param name="controller">The controller name to use, or <c>null</c> to use the action descriptor's controller name.</param>
        /// <param name="area">The area name to use, or <c>null</c> to use the action descriptor's area name.</param>
        /// <returns>The converted <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor AsInterface(this ActionDescriptor actionDescriptor, string? action = null, string? controller = null, string? area = null)
        {
            if (area == null)
                area = actionDescriptor.GetAreaName();

            if (action == null)
                action = actionDescriptor.GetActionName();

            if (controller == null)
                controller = actionDescriptor.GetControllerName();

            return new MvcActionDescriptor(action, controller, area);
        }
    }
}
