namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents view model that support defining root element CSS class when it is disabled.
    /// </summary>
    public interface ISupportDisabledCssClass
    {
        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to root element when it is disabled.
        /// </summary>
        string DisabledCssClass { get; set; }
    }
}
