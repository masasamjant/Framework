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
        /// Gets child navigation items.
        /// </summary>
        IEnumerable<INavigationItem> Children { get; }
    }
}
