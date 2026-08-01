namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents view model that support defining root element CSS class.
    /// </summary>
    public interface ISupportCssClass
    {
        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to root element.
        /// </summary>
        string CssClass { get; set; }
    }
}
