namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents a view that is based on dialog.
    /// </summary>
    public interface IDialogView : IFormView
    {
        /// <summary>
        /// Gets current dialog result.
        /// </summary>
        DialogResult DialogResult { get; }

        /// <summary>
        /// Sets dialog result.
        /// </summary>
        /// <param name="result">The dialog result.</param>
        void SetDialogResult(DialogResult result);
    }
}
