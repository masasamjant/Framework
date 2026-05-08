namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents view.
    /// </summary>
    public interface IView : IPresentationCommands
    {
        /// <summary>
        /// Gets the presentation command that is executed when the view is loading.
        /// </summary>
        IPresentationCommand ViewLoading { get; }
    }
}
