namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Represetnts view model that support changing enabled state.
    /// </summary>
    public interface ISupportEnabledState
    {
        /// <summary>
        /// Gets or sets whether view model is in enabled state or not.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
