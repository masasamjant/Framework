namespace Masasamjant.Windows.Input
{
    /// <summary>
    /// Represents a base class for input devices.
    /// </summary>
    public abstract class InputDevice : IInputDevice, IDisposable
    {
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
        /// Disposes current instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected virtual void Dispose(bool disposing) 
        {
            if (IsDisposed)
                return;

            IsDisposed = true;
        }

        /// <summary>
        /// Check if <see cref="IsDisposed"/> is <c>true</c> and throw an <see cref="ObjectDisposedException"/> if it is.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If <see cref="IsDisposed"/> is <c>true</c>.</exception>
        protected void CheckDisposed()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
        }
    }
}
