namespace Masasamjant.Web
{
    /// <summary>
    /// Represents descriptor of MVC action.
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
    }

    internal sealed class InternalActionDescriptor : IActionDescriptor
    {
        public InternalActionDescriptor(string actionName, string controllerName, string? areaName = null)
        {
            ActionName = actionName;
            ControllerName = controllerName;
            AreaName = areaName;
        }

        public string? AreaName { get; }

        public string ControllerName { get; }

        public string ActionName { get; }
    }
}
