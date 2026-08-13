namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Describes an action of a controller.
    /// </summary>
    public interface IActionDescriptor
    {
        /// <summary>
        /// Gets the area name or <c>null</c>, if not in area.
        /// </summary>
        string? AreaName { get; }

        /// <summary>
        /// Gets the controller name.
        /// </summary>
        string ControllerName { get; }

        /// <summary>
        /// Gets the action name.
        /// </summary>
        string ActionName { get; }

        /// <summary>
        /// Gets the route values.
        /// </summary>
        IReadOnlyDictionary<string, string?> RouteValues { get; }
    }
}
