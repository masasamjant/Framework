namespace Masasamjant.Web.ViewModels.Navigation
{
    /// <summary>
    /// Represents navigation item.
    /// </summary>
    public interface INavigationItem : ISupportCssClass, ISupportDisabledCssClass, ISupportEnabledState, ISupportHtmlAttributes
    {
        /// <summary>
        /// Gets or sets the item text.
        /// </summary>
        string Text { get; set; }

        /// <summary>
        /// Gets or sets the URL to navigatate.
        /// </summary>
        string Url { get; set; }

        /// <summary>
        /// Gets route aka query string parameters.
        /// </summary>
        IDictionary<string, object?> RouteParameters { get; }

        /// <summary>
        /// Gets the full navigation URL with route parameters.
        /// </summary>
        /// <returns>A full navigation URL with route parameters.</returns>
        string GetFullUrl();
    }
}
