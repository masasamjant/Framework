namespace Masasamjant.Web.Mvc
{
    /// <summary>
    /// Represents view model that supports changing enabled state.
    /// </summary>
    public interface ISupportEnabledState
    {
        /// <summary>
        /// Gets or sets whether or not the view model is in enabled state. 
        /// If <c>false</c>, then HTML elements bound to this view model should be disabled, hidden or inactive, depending on the design.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
