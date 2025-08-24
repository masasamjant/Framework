namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Defines how ajax target element is updated.
    /// </summary>
    public enum AjaxUpdate : int
    {
        /// <summary>
        /// Replace existing content.
        /// </summary>
        Replace = 0,

        /// <summary>
        /// Append to existing content.
        /// </summary>
        Append = 1
    }
}
