namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents abstract <see cref="IWorker"/> implementation.
    /// </summary>
    public abstract class Worker : IWorker, IProvideError
    {
        private long started = 0L;

        /// <summary>
        /// Gets if worker is started.
        /// </summary>
        protected Worker()
        { }

        /// <summary>
        /// Notifies about error in work execution.
        /// </summary>
        public event EventHandler<ErrorEventArgs>? Error;

        /// <summary>
        /// Gets if worker is started.
        /// </summary>
        public bool Started
        {
            get { return Interlocked.Read(ref started) == 1L; }
        }

        /// <summary>
        /// Starts the worker. If worker is already started, this method does nothing.
        /// </summary>
        /// <exception cref="InvalidOperationException">If exception occurs when starting worker.</exception>
        /// <exception cref="ObjectDisposedException">If worker implements <see cref="IDisposable"/> and is disposed.</exception>
        public void Start()
        {
            if (Interlocked.CompareExchange(ref started, 1L, 0L) == 0L)
            {
                try
                {
                    OnStart();
                }
                catch (Exception exception)
                {
                    if (exception is ObjectDisposedException)
                        throw;

                    throw new InvalidOperationException("An error occurred while starting the worker. See inner exception for details.", exception);
                }
            }
        }

        /// <summary>
        /// Stops the worker. If worker is already stopped, this method does nothing.
        /// </summary>
        /// <exception cref="InvalidOperationException">If exception occurs when stopping worker.</exception>
        /// <exception cref="ObjectDisposedException">If worker implements <see cref="IDisposable"/> and is disposed.</exception>
        public void Stop()
        {
            if (Interlocked.CompareExchange(ref started, 0L, 1L) == 1L)
            {
                try
                {
                    OnStop();
                }
                catch (Exception exception)
                {
                    if (exception is ObjectDisposedException)
                        throw;

                    throw new InvalidOperationException("An error occurred while stopping the worker. See inner exception for details.", exception);
                }
            }
        }

        /// <summary>
        /// Derived classes must override to initialize and start the worker.
        /// </summary>
        protected abstract void OnStart();

        /// <summary>
        /// Derived classes must override to stop and clear the worker.
        /// </summary>
        protected abstract void OnStop();

        /// <summary>
        /// Raises <see cref="Error"/> event.
        /// </summary>
        /// <param name="args">The error event args.</param>
        protected void OnError(ErrorEventArgs args)
        {
            Error?.Invoke(this, args);
        }

        /// <summary>
        /// Derived classes must override to perform the actual work. The implementation must check <paramref name="cancellationToken"/> to determine if worker is stopped 
        /// and exit the work immediately when possible from loop. Also in case of exception, the implementation must raise <see cref="Error"/> event and work as defined in error behavior.
        /// </summary>
        /// <remarks>
        /// Depending on worker implementation this method might be invoked on a separate thread, only once when worker is started or several times during worker lifetime.
        /// Derived classes must document the behavior of this method and how it is invoked in their implementation.
        /// </remarks>
        /// <param name="cancellationToken">The cancellation token that signals if worker is stopped.</param>
        protected abstract void Run(CancellationToken cancellationToken);

        /// <summary>
        /// Sets worker to stopped state. This can be used if worker implements <see cref="IDisposable"/> 
        /// and it is disposed while worker is started and invoking <see cref="Stop"/> would throw <see cref="ObjectDisposedException"/>.
        /// </summary>
        protected void SetStopped()
        {
            Interlocked.Exchange(ref started, 0L);
        }
    }
}
