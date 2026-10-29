namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents <see cref="Worker"/> that performs work in dedicated thread. This class implements <see cref="IDisposable"/> so worker 
    /// cannot be started or stopped after it is disposed. This implementations invokes <see cref="Worker.Run(CancellationToken)"/> every time worker is 
    /// started with new thread. Derived classes must implement <see cref="Worker.Run(CancellationToken)"/> to perform the actual work 
    /// and check <paramref name="cancellationToken"/> to determine if worker is stopped.
    /// </summary>
    /// <remarks>It is expected that <see cref="Worker.Run(CancellationToken)"/> is implemented in thread-safe manner.</remarks>
    public abstract class ThreadWorker : Worker, IDisposable
    {
        private Thread? thread;
        private readonly string? threadName;
        private CancellationTokenSource? cancellationTokenSource;
        private long disposed = 0L;

        /// <summary>
        /// Initializes new instance of the <see cref="ThreadWorker"/> class.
        /// </summary>
        /// <param name="threadName">The name of worker thread.</param>
        protected ThreadWorker(string? threadName)
        {
            this.threadName = threadName;
        }

        /// <summary>
        /// Finalizes current instance.
        /// </summary>
        ~ThreadWorker()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets if current instance is disposed.
        /// </summary>
        protected bool IsDisposed
        {
            get { return Interlocked.Read(ref disposed) == 1L; }
        }

        /// <summary>
        /// Gets the timeout to wait for thread to complete when stopped.
        /// If returns <c>null</c>, then wait indefinitely until thread is completed. 
        /// Default implementation returns <c>null</c> and derived classes can override it to provide specific timeout.
        /// </summary>
        protected virtual TimeSpan? ThreadWaitTimeout
        {
            get { return null; }
        }

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
        /// <param name="disposing"><c>true</c> when disposing; <c>false</c> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref disposed, 1L, 0L) == 0L)
            {
                if (Started)
                {
                    ClearCancellationTokenSource();
                    ClearThread();
                    SetStopped();
                }
            }
        }

        /// <summary>
        /// Starts worker by creating new thread and executing work in it.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If current instance is disposed.</exception>
        /// <remarks>This is invoked by base class and derived classes should not invoke it directly.</remarks>
        protected override sealed void OnStart()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSource.Token;
            thread = CreateThread();
            thread.Start(cancellationToken);
        }

        /// <summary>
        /// Stops the worker and wait worker thread to end.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If current instance is disposed.</exception>
        /// <remarks>This is invoked by base class and derived classes should not invoke it directly.</remarks>
        protected override sealed void OnStop()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            ClearCancellationTokenSource();
            ClearThread();
        }

        private Thread CreateThread()
        {
            var thread = new Thread(StartWorker);
            if (!string.IsNullOrEmpty(threadName))
                thread.Name = threadName;
            return thread;
        }

        private void ClearCancellationTokenSource()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }

        }

        private void ClearThread()
        {
            if (thread != null)
            {
                var timeout = ThreadWaitTimeout;
                int? millisecondsTimeout = timeout.HasValue ? (int)timeout.Value.TotalMilliseconds : null;
                if (millisecondsTimeout.HasValue)
                    thread.Join(millisecondsTimeout.Value);
                else
                    thread.Join();
                thread = null;
            }
        }

        private void StartWorker(object? arg)
        {
            if (arg is CancellationToken cancellationToken)
                Run(cancellationToken);
        }
    }
}
