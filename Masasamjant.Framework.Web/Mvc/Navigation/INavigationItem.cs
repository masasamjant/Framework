namespace Masasamjant.Web.Mvc.Navigation
{
    /// <summary>
    /// Represents navigation item view model.
    /// </summary>
    public interface INavigationItem : ISupportCssClass, ISupportDisabledCssClass, ISupportEnabledState, ISupportHtmlAttributes
    {
        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the URL to navigate.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets the route parameters to append to the navigation URL. 
        /// The key is the parameter name, and the value is the parameter value.
        /// </summary>
        IDictionary<string, object?> RouteParameters { get; }

        /// <summary>
        /// Gets the full navigation URL with route parameters.
        /// </summary>
        /// <returns>The full navigation URL.</returns>
        string GetNavigationUrl();
    }
}
