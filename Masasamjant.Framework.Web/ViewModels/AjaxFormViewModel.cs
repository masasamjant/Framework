namespace Masasamjant.Web.ViewModels
{
    /// <summary>
    /// Represents view model that implements <see cref="IAjaxForm{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the form data object.</typeparam>
    public class AjaxFormViewModel<T> : ViewModel, IAjaxForm<T> where T : class
    {
        /// <summary>
        /// Gets or sets form data.
        /// </summary>
        public T? Data { get; set; }

        /// <summary>
        /// Gets or sets how target element is updated.
        /// </summary>
        public AjaxUpdate AjaxUpdate { get; set; }

        /// <summary>
        /// Gets or sets how ajax error is displayed.
        /// </summary>
        public AjaxErrorDisplay ErrorDisplay { get; set; }

        /// <summary>
        /// Gets or sets ID of update element.
        /// </summary>
        public string AjaxUpdateElementId { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets ID of error element.
        /// </summary>
        public string AjaxErrorElementId { get; set; } = string.Empty;
    }
}
