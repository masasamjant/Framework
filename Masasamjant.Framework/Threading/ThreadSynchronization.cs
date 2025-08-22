using Masasamjant.ComponentModel;

namespace Masasamjant.Threading
{
    /// <summary>
    /// Represents thread syncronization.
    /// </summary>
    public abstract class ThreadSynchronization : ISynchronization, IDisposable, ISupportIsDisposed
    {
        /// <summary>
        /// Gets whether or not instance is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Begin synchronization.
        /// </summary>
        public abstract void Begin();

        /// <summary>
        /// End synchronization.
        /// </summary>
        public abstract void End();

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
    }
}
