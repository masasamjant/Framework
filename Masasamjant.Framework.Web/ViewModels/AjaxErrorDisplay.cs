namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Defines how ajax error is displayed.
    /// </summary>
    public enum AjaxErrorDisplay : int
    {
        /// <summary>
        /// Ajax error not displayed at all.
        /// </summary>
        None = 0,

        /// <summary>
        /// Ajax error displayed in alert box.
        /// </summary>
        Alert = 1,

        /// <summary>
        /// Ajax error displayed in specified element.
        /// </summary>
        Element = 2
    }
}
