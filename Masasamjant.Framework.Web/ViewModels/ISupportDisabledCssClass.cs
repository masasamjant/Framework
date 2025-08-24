namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Represents view model that support disabled CSS class.
    /// </summary>
    public interface ISupportDisabledCssClass
    {
        /// <summary>
        /// Gets or sets name(s) of CSS classes applied to element when element is disabled.
        /// </summary>
        string DisabledCssClass { get; }
    }
}
