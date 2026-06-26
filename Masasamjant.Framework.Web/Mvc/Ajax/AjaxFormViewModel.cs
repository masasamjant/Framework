namespace Masasamjant.Web.Mvc.Ajax
{
    /// <summary>
    /// Represents <see cref="ViewModel"/> that implements <see cref="IAjaxForm"/> interface.
    /// </summary>
    public class AjaxFormViewModel : ViewModel, IAjaxForm
    {
        private AjaxUpdate update = AjaxUpdate.Replace;
        private AjaxErrorDisplay errorDisplay = AjaxErrorDisplay.Console;
        private string updateElementId = string.Empty;
        private string errorElementId = string.Empty;

        /// <summary>
        /// Initializes new default instance of the <see cref="AjaxFormViewModel"/> class.
        /// </summary>
        public AjaxFormViewModel()
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="AjaxFormViewModel"/> class.
        /// </summary>
        /// <param name="updateElementId">The value of <c>id</c> attribute of updated HTML element.</param>
        /// <param name="errorElementId">The value of <c>id</c> attribute of HTML element where ajax error is displayed.</param>
        /// <param name="update">Specifies how target element is updated.</param>
        /// <param name="errorDisplay">Specifies how ajax error is displayed.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="updateElementId"/> or <paramref name="errorElementId"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="update"/> or <paramref name="errorDisplay"/> is not defined.</exception>
        public AjaxFormViewModel(string updateElementId, string errorElementId, AjaxUpdate update, AjaxErrorDisplay errorDisplay)
        {
            ArgumentNullException.ThrowIfNull(updateElementId);
            ArgumentNullException.ThrowIfNull(errorElementId);

            if (!Enum.IsDefined(update))
                throw new ArgumentException("The value is not defined.", nameof(update));

            if (!Enum.IsDefined(errorDisplay))
                throw new ArgumentException("The value is not defined.", nameof(errorDisplay));

            AjaxUpdate = update;
            ErrorDisplay = errorDisplay;
            UpdateElementId = updateElementId;
            ErrorElementId = errorElementId;
        }

        /// <summary>
        /// Gets or sets how target element is updated.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        public AjaxUpdate AjaxUpdate
        {
            get { return update; }
            set
            {
                if (!Enum.IsDefined(value))
                    throw new ArgumentException("Value is not defined.", nameof(AjaxUpdate));

                update = value;
            }
        }

        /// <summary>
        /// Gets or sets how ajax error is displayed.
        /// </summary>
        /// <exception cref="ArgumentException">If attempt to set undefined value.</exception>
        public AjaxErrorDisplay ErrorDisplay
        {
            get { return errorDisplay; }
            set
            {
                if (!Enum.IsDefined(value))
                    throw new ArgumentException("Value is not defined.", nameof(ErrorDisplay));

                errorDisplay = value;
            }
        }

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of updated HTML element.
        /// </summary>
        public string UpdateElementId
        {
            get { return updateElementId; }
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(UpdateElementId));
                updateElementId = value;
            }
        }

        /// <summary>
        /// Gets or sets value of <c>id</c> attribute of HTML element where ajax error is displayed, 
        /// when displayed in element. Otherwise value is ignored.
        /// </summary>
        public string ErrorElementId
        {
            get { return errorElementId; }
            set
            {
                ArgumentNullException.ThrowIfNull(value, nameof(ErrorElementId));
                errorElementId = value;
            }
        }   
    }

    /// <summary>
    /// Represents <see cref="AjaxFormViewModel"/> that implements <see cref="IAjaxForm{T}"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the form data.</typeparam>
    public class AjaxFormViewModel<T> : AjaxFormViewModel, IAjaxForm<T> where T : class
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="AjaxFormViewModel"/> class.
        /// </summary>
        public AjaxFormViewModel()
            : base()
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="AjaxFormViewModel"/> class.
        /// </summary>
        /// <param name="updateElementId">The value of <c>id</c> attribute of updated HTML element.</param>
        /// <param name="errorElementId">The value of <c>id</c> attribute of HTML element where ajax error is displayed.</param>
        /// <param name="update">Specifies how target element is updated.</param>
        /// <param name="errorDisplay">Specifies how ajax error is displayed.</param>
        /// <param name="data">The form data.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="updateElementId"/> or <paramref name="errorElementId"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="update"/> or <paramref name="errorDisplay"/> is not defined.</exception>
        public AjaxFormViewModel(string updateElementId, string errorElementId, AjaxUpdate update, AjaxErrorDisplay errorDisplay, T? data)
            : base(updateElementId, errorElementId, update, errorDisplay)
        {
            Data = data;
        }

        /// <summary>
        /// Gets or sets form data.
        /// </summary>
        public T? Data { get; set; }
    }
}
