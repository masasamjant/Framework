namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents a view that is based on form.
    /// </summary>
    public interface IFormView : IView
    {
        /// <summary>
        /// Gets presentation command that is executed when the form is closing.
        /// </summary>
        IPresentationCommand<FormClosingEventArgs> FormClosing { get; }

        /// <summary>
        /// Gets presentation command that is executed when the form is closed.
        /// </summary>
        IPresentationCommand<FormClosedEventArgs> FormClosed { get; }

        /// <summary>
        /// Show form.
        /// </summary>
        void Show();

        /// <summary>
        /// Close form.
        /// </summary>
        void Close();
    }
}
