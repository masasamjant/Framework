using Microsoft.AspNetCore.Mvc.Controllers;

namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Describes an MVC action.
    /// </summary>
    public sealed class MvcActionDescriptor : IActionDescriptor
    {
        private readonly Dictionary<string, string?> routeValues = new Dictionary<string, string?>();

        /// <summary>
        /// Initializes a new instance of the <see cref="MvcActionDescriptor"/> class.
        /// </summary>
        /// <param name="actionName">The name of the action.</param>
        /// <param name="controllerName">The name of the controller.</param>
        /// <param name="areaName">The name of the area.</param>
        /// <param name="routeValues">The route values.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="actionName"/> or <paramref name="controllerName"/> is <c>null</c>, empty or only whitespace.</exception>
        public MvcActionDescriptor(string actionName, string controllerName, string? areaName = null, IDictionary<string, string?>? routeValues = null)
        {
            if (string.IsNullOrWhiteSpace(actionName))
                throw new ArgumentNullException(nameof(actionName), "Action name is null, empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(controllerName))
                throw new ArgumentNullException(nameof(controllerName), "Controller name is null, empty or only whitespace.");
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = string.IsNullOrWhiteSpace(areaName) ? null : areaName;

            if (routeValues != null)
                AppendRouteValues(routeValues);
        }

        /// <summary>
        /// Initializes new default instance of the <see cref="MvcActionDescriptor"/> class.
        /// </summary>
        internal MvcActionDescriptor()
        { }

        /// <summary>
        /// Gets the area name or <c>null</c>, if not in area.
        /// </summary>
        public string? AreaName { get; internal set; }

        /// <summary>
        /// Gets the controller name.
        /// </summary>
        public string ControllerName { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the action name.
        /// </summary>
        public string ActionName { get; internal set; } = string.Empty;

        /// <summary>
        /// Gets the route values.
        /// </summary>
        public IReadOnlyDictionary<string, string?> RouteValues
        {
            get { return routeValues.AsReadOnly(); }
        }

        /// <summary>
        /// Creates <see cref="MvcActionDescriptor"/> from specified <see cref="ControllerActionDescriptor"/>.
        /// </summary>
        /// <param name="controllerActionDescriptor">The controller action descriptor.</param>
        /// <returns>The created <see cref="MvcActionDescriptor"/>.</returns>
        public static MvcActionDescriptor Create(ControllerActionDescriptor controllerActionDescriptor)
        {
            var actionDescriptor = new MvcActionDescriptor
            {
                ActionName = ActionDescriptorHelper.GetActionName(controllerActionDescriptor),
                ControllerName = ActionDescriptorHelper.GetControllerName(controllerActionDescriptor),
                AreaName = ActionDescriptorHelper.GetAreaName(controllerActionDescriptor)
            };

            actionDescriptor.AppendRouteValues(controllerActionDescriptor.RouteValues);

            return actionDescriptor;
        }

        private void AppendRouteValues(IDictionary<string, string?> routeValues)
        {
            foreach (var routeValue in routeValues)
            {
                this.routeValues[routeValue.Key] = routeValue.Value;
            }
        }
    }
}
