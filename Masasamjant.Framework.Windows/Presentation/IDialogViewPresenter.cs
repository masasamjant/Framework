namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents presenter associated with specified dialog view.
    /// </summary>
    /// <typeparam name="TView">The type of the dialog view.</typeparam>
    public interface IDialogViewPresenter<TView> : IFormViewPresenter<TView>
         where TView : IDialogView
    {
    }
}
