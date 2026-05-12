namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents base class for presenter associated with specified form view.
    /// </summary>
    /// <typeparam name="TView">The type of the form view.</typeparam>
    public class FormViewPresenter<TView> : ViewPresenter<TView>, IFormViewPresenter<TView>
        where TView : class, IFormView
    {
        /// <summary>
        /// Initializes new instance of the <see cref="FormViewPresenter{TView}"/> class.
        /// </summary>
        /// <param name="view">The view associated with presenter.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="view"/> is <c>null</c>.</exception>
        protected FormViewPresenter(TView view)
            : base(view)
        {
            View.FormClosedCommand.Executed += OnFormClosedExecuted;
            View.FormClosingCommand.Executed += OnFormClosingExecuted;
        }

        /// <summary>
        /// Invoked when form closing command is executed. This should handle form closing 
        /// and set <see cref="FormClosingEventArgs.Cancel"/> based on whether or not form should be closed. 
        /// Default implementation does not cancel form closing.
        /// </summary>
        /// <param name="args">The form closing event args.</param>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public virtual void OnFormClosing(FormClosingEventArgs args)
        {
            CheckDisposed();
            args.Cancel = false;
        }

        /// <summary>
        /// Invoked when form closed command is executed. This should handle form closed event.
        /// </summary>
        /// <param name="args">The form closed event args.</param>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public virtual void OnFormClosed(FormClosedEventArgs args)
        {
            CheckDisposed();
            return;
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                View.FormClosedCommand.Executed -= OnFormClosedExecuted;
                View.FormClosingCommand.Executed -= OnFormClosingExecuted;
                base.Dispose(disposing);
            }
        }

        private void OnFormClosingExecuted(object? sender, PresentationCommandEventArgs<FormClosingEventArgs> e)
        {
            if (IsEnabledCommand(e))
                OnFormClosing(e.Original);
        }

        private void OnFormClosedExecuted(object? sender, PresentationCommandEventArgs<FormClosedEventArgs> e)
        {
            if (IsEnabledCommand(e))
                OnFormClosed(e.Original);
        }
    }
}
