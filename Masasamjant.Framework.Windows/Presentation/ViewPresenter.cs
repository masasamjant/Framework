namespace Masasamjant.Windows.Presentation
{
    /// <summary>
    /// Represents base class for presenter associated with specified view.
    /// </summary>
    /// <typeparam name="TView">The type of the view.</typeparam>
    public class ViewPresenter<TView> : IViewPresenter<TView>, IDisposable
        where TView : class, IView
    {
        private readonly TView view;

        /// <summary>
        /// Initializes new instance of the <see cref="ViewPresenter{TView}"/> class.
        /// </summary>
        /// <param name="view">The view associated with presenter.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="view"/> is <c>null</c>.</exception>
        protected ViewPresenter(TView view)
        {
            this.view = view ?? throw new ArgumentNullException(nameof(view));
            this.view.ViewLoading.Executed += OnViewLoadingExecuted;
        }

        /// <summary>
        /// Gets the view associated with this presenter.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If instance if disposed.</exception>
        public TView View
        {
            get
            {
                CheckDisposed();
                return view;
            }
        }

        /// <summary>
        /// Gets whether or not instance is disposed.
        /// </summary>
        protected bool IsDisposed { get; private set; } 

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Invoked when view loading command is executed. 
        /// Default implementation only checks disposed state.
        /// </summary>
        /// <param name="args">The arguments of original event.</param>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public virtual void OnViewExecuted(EventArgs args)
        {
            CheckDisposed();
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            View.ViewLoading.Executed -= OnViewLoadingExecuted;

            IsDisposed = true;
        }

        /// <summary>
        /// Check if instance is disposed and if so, then throws <see cref="ObjectDisposedException"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If <see cref="IsDisposed"/> is <c>true</c>.</exception>
        protected void CheckDisposed()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
        }

        private void OnViewLoadingExecuted(object? sender, PresentationCommandEventArgs e)
        {
            OnViewExecuted(e.Original);
        }
    }
}
