namespace Masasamjant.Web.Mvc.Ajax
{
    /// <summary>
    /// Defines how ajax error should be displayed.
    /// </summary>
    public enum AjaxErrorDisplay : int
    {
        /// <summary>
        /// Ajax error not displayed at all.
        /// </summary>
        None = 0,

        /// <summary>
        /// Ajax error is displayed in browser console.
        /// </summary>
        Console = 1,

        /// <summary>
        /// Ajax error is display in browser alert box.
        /// </summary>
        Alert = 2,

        /// <summary>
        /// Ajax error is displayed in specified element.
        /// </summary>
        Element = 3
    }
}
