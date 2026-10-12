namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents a component that performs some cleanup or resource release when disposed.
    /// </summary>
    public abstract class DisposableComponent : IDisposable
    {
        private long disposed = 0L;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposableComponent"/> class.
        /// </summary>
        protected DisposableComponent()
        { }

        /// <summary>
        /// Finalizes current instance.
        /// </summary>
        ~DisposableComponent()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets a value indicating whether the component has been disposed.
        /// </summary>
        public bool IsDisposed => Interlocked.Read(ref disposed) != 0L;

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref disposed, 1L, 0L) == 0L)
            { 
                if (disposing)
                    OnDisposing();
            }
        }

        /// <summary>
        /// Can be used to check if the component has already been disposed and throw an exception if it has.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If <see cref="IsDisposed"/> is <c>true</c>.</exception>
        protected void CheckDisposed()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
        }

        /// <summary>
        /// Derived classes should override this method to implement their specific disposal logic.
        /// </summary>
        protected abstract void OnDisposing();
    }
}
