namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Represents view model that support CSS class.
    /// </summary>
    public interface ISupportCssClass
    {
        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to element.
        /// </summary>
        string CssClass { get; set; }
    }
}
