namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Represents view model that supports HTML attributes dictionary.
    /// </summary>
    public interface ISupportHtmlAttributes
    {
        /// <summary>
        /// Gets or sets HTML attributes.
        /// </summary>
        IDictionary<string, object?> HtmlAttributes { get; }
    }
}
