namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents presenter associated with specified view.
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    public interface IViewPresenter<TView>
        where TView : IView
    {
        /// <summary>
        /// Gets the view associated with this presenter.
        /// </summary>
        TView View { get; }
    }
}
