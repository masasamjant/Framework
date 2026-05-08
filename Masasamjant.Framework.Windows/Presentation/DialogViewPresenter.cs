namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents base class for presenter associated with specified dialog view.
    /// </summary>
    /// <typeparam name="TView">The type of the dialog view.</typeparam>
    public class DialogViewPresenter<TView> : FormViewPresenter<TView>, IDialogViewPresenter<TView>
        where TView : class, IDialogView
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DialogViewPresenter{TView}"/> class.
        /// </summary>
        /// <param name="view">The view associated with presenter.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="view"/> is <c>null</c>.</exception>
        protected DialogViewPresenter(TView view) 
            : base(view)
        { }
    }
}
