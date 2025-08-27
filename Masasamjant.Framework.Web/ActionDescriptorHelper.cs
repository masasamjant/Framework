using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Masasamjant.Web
{
    /// <summary>
    /// Provides helper methods to <see cref="IActionDescriptor"/> interface.
    /// </summary>
    public static class ActionDescriptorHelper
    {
        /// <summary>
        /// Gets the action name of specified <see cref="ActionDescriptor"/>, if it is <see cref="ControllerActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A action name or empty string.</returns>
        public static string GetActionName(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return controllerActionDescriptor.ActionName;

            return string.Empty;
        }

        /// <summary>
        /// Gets the controller name of specified <see cref="ActionDescriptor"/>, if it is <see cref="ControllerActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A controller name or empty string.</returns>
        public static string GetControllerName(this ActionDescriptor actionDescriptor)
        {
            if (actionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                return controllerActionDescriptor.ControllerName;

            return string.Empty;
        }

        /// <summary>
        /// Gets the area name of specified <see cref="ActionDescriptor"/>, if it is <see cref="PageActionDescriptor"/> 
        /// or <see cref="ControllerActionDescriptor"/> and route values has area.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A area name or <c>null</c>.</returns>
        public static string? GetAreaName(this ActionDescriptor actionDescriptor) 
        {
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
        /// Creates <see cref="IActionDescriptor"/> from specified <see cref="ActionDescriptor"/>.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <returns>A <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor AsInterface(this ActionDescriptor actionDescriptor)
            => new InternalActionDescriptor(GetActionName(actionDescriptor), GetControllerName(actionDescriptor), GetAreaName(actionDescriptor));

        /// <summary>
        /// Creates <see cref="IActionDescriptor"/> from specified <see cref="ActionDescriptor"/> or values set explicitly.
        /// </summary>
        /// <param name="actionDescriptor">The <see cref="ActionDescriptor"/>.</param>
        /// <param name="action">The action name or <c>null</c> to get action name from <paramref name="actionDescriptor"/>.</param>
        /// <param name="controller">The controller name or <c>null</c> to get controller name from <paramref name="actionDescriptor"/>.</param>
        /// <param name="area">The area name or <c>null</c> to get area name from <paramref name="actionDescriptor"/>.</param>
        /// <returns>A <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor AsInterface(this ActionDescriptor actionDescriptor, string? action = null, string? controller = null, string? area = null)
        {
            if (area == null)
                area = GetAreaName(actionDescriptor);

            if (controller == null)
                controller = GetControllerName(actionDescriptor);

            if (action == null)
                action = GetActionName(actionDescriptor);

            return new InternalActionDescriptor(action, controller, area);
        }

        /// <summary>
        /// Creates <see cref="IActionDescriptor"/> from specified values.
        /// </summary>
        /// <param name="actionName">The action name.</param>
        /// <param name="controllerName">The controller name.</param>
        /// <param name="areaName">The area name or <c>null</c>.</param>
        /// <returns>A <see cref="IActionDescriptor"/>.</returns>
        public static IActionDescriptor CreateActionDescriptor(string actionName, string controllerName, string? areaName = null)
            => new InternalActionDescriptor(actionName, controllerName, areaName);

        /// <summary>
        /// Gets <see cref="IActionDescriptor"/> saved to <see cref="HttpContext.Items"/>.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="actionDescriptorKey">The key of saved action descriptor.</param>
        /// <returns>A saved <see cref="IActionDescriptor"/> or <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="actionDescriptorKey"/> is empty or only whitespace.</exception>
        public static IActionDescriptor? GetActionDescriptor(this HttpContext context, string actionDescriptorKey)
        {
            ValidateActionDescriptorKey(actionDescriptorKey);

            if (context.Items.ContainsKey(actionDescriptorKey) && context.Items[actionDescriptorKey] is IActionDescriptor actionDescriptor)
                return actionDescriptor;

            return null;
        }

        /// <summary>
        /// Saves specified <see cref="IActionDescriptor"/> to <see cref="HttpContext.Items"/>.
        /// </summary>
        /// <param name="context">The HTTP context.</param>
        /// <param name="actionDescriptor">The action descriptor to save.</param>
        /// <param name="actionDescriptorKey">The key to save action descriptor.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="actionDescriptorKey"/> is empty or only whitespace.</exception>
        public static void SetActionDescriptor(this HttpContext context, IActionDescriptor actionDescriptor, string actionDescriptorKey)
        {
            ValidateActionDescriptorKey(actionDescriptorKey);
            context.Items[actionDescriptorKey] = actionDescriptor;
        }

        private static void ValidateActionDescriptorKey(string actionDescriptorKey)
        {
            if (string.IsNullOrWhiteSpace(actionDescriptorKey))
                throw new ArgumentNullException(nameof(actionDescriptorKey), "The action descriptor key is empty or only whitespace.");
        }
    }
}
