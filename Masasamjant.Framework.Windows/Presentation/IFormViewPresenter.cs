namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents presenter associated with specified form view.
    /// </summary>
    /// <typeparam name="TView">The type of the form view.</typeparam>
    public interface IFormViewPresenter<TView> : IViewPresenter<TView>
        where TView : IFormView
    {
    }
}
