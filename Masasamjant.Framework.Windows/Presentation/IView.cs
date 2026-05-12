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
        IPresentationCommand ViewLoadingCommand { get; }

        /// <summary>
        /// Shows message in view.
        /// </summary>
        /// <param name="message">The message to show.</param>
        /// <returns>A dialog result.</returns>
        DialogResult ShowMessage(PresentationMessage message);
    }
}
