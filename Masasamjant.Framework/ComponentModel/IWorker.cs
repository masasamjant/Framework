namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents component that performs work when started. 
    /// It should be possible to start and stop worker multiple times unless worker implements <see cref="IDisposable"/> and is disposed. 
    /// If worker implements <see cref="IDisposable"/>, it should be disposed after use.
    /// </summary>
    public interface IWorker : IProvideError
    {
        /// <summary>
        /// Gets if worker is started.
        /// </summary>
        bool Started { get; }

        /// <summary>
        /// Starts the worker. If worker is already started, this method does nothing.
        /// </summary>
        /// <exception cref="InvalidOperationException">If exception occurs when starting worker.</exception>
        /// <exception cref="ObjectDisposedException">If worker implements <see cref="IDisposable"/> and is disposed.</exception>
        void Start();

        /// <summary>
        /// Stops the worker. If worker is already stopped, this method does nothing.
        /// </summary>
        /// <exception cref="InvalidOperationException">If exception occurs when stopping worker.</exception>
        /// <exception cref="ObjectDisposedException">If worker implements <see cref="IDisposable"/> and is disposed.</exception>
        void Stop();
    }
}
