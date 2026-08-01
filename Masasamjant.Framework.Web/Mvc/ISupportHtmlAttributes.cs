namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents view model that supports HTML attributes dictionary.
    /// </summary>
    public interface ISupportHtmlAttributes
    {
        /// <summary>
        /// Gets the HTML attributes dictionary.
        /// </summary>
        IDictionary<string, object?> HtmlAttributes { get; }
    }
}
