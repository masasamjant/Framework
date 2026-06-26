namespace Masasamjant.Web.Mvc.Ajax
{
    /// <summary>
    /// Represents HTML form that use AJAX.
    /// </summary>
    public interface IAjaxForm
    {
        /// <summary>
        /// Gets or sets how target element is updated.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        AjaxUpdate AjaxUpdate { get; set; }

        /// <summary>
        /// Gets or sets how ajax error is displayed.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        AjaxErrorDisplay ErrorDisplay { get; set; }

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of updated HTML element.
        /// </summary>
        string UpdateElementId { get; set; }

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of HTML element where ajax error is displayed, 
        /// when displayed in element. Otherwise value is ignored.
        /// </summary>
        string ErrorElementId { get; set; }
    }

    /// <summary>
    /// Represents HTML form that use AJAX.
    /// </summary>
    /// <typeparam name="T">Type of form data.</typeparam>
    public interface IAjaxForm<T> : IAjaxForm where T : class
    {
        /// <summary>
        /// Gets or sets form data.
        /// </summary>
        T? Data { get; set; }
    }
}
