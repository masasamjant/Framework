using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents context of disposables that should be disposed when context is disposed.
    /// </summary>
    public sealed class DisposableContext : IDisposable
    {
        private readonly List<IDisposable> disposables;
        private bool disposed;

        /// <summary>
        /// Initializes new instance of the <see cref="DisposableContext"/> class.
        /// </summary>
        public DisposableContext()
        {
            this.disposables = new List<IDisposable>();
            this.disposed = false;
        }

        [ExcludeFromCodeCoverage]
        ~DisposableContext()
        {
            Dispose(false);
        }

        /// <summary>
        /// Add new <typeparamref name="TDisposable"/> to the context.
        /// </summary>
        /// <typeparam name="TDisposable">The type of the disposable.</typeparam>
        /// <param name="disposable">The disposable instance.</param>
        /// <returns>A <paramref name="disposable"/>.</returns>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
        public TDisposable Add<TDisposable>(TDisposable disposable) where TDisposable : IDisposable
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);

            disposables.Add(disposable);
            return disposable;
        }

        /// <summary>
        /// Disposes current instance and all disposables it contains.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
                return;

            disposed = true;

            if (disposing)
            {
                foreach (var disposable in disposables)
                    disposable.Dispose();
            }

            disposables.Clear();
        }
    }
}
