namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents <see cref="Worker"/> that performs work in timer thread. This class implements <see cref="IDisposable"/> so worker cannot be started or stopped after it is disposed. 
    /// This implementations invokes <see cref="Worker.Run(CancellationToken)"/> every time timer is elapsed. So <see cref="Worker.Run(CancellationToken)"/> is invoked periodically when worker is running. 
    /// Derived classes must implement <see cref="Worker.Run(CancellationToken)"/> to perform the actual work 
    /// and check <paramref name="cancellationToken"/> to determine if worker is stopped.
    /// </summary>
    public abstract class TimerWorker : Worker, IDisposable
    {
        private readonly TimeSpan executionInterval;
        private FrameworkTimer? timer;
        private CancellationTokenSource? cancellationTokenSource;
        private long disposed = 0L;

        /// <summary>
        /// Initializes new instance of the <see cref="TimerWorker"/> class.
        /// </summary>
        /// <param name="executionInterval">The interval at which the worker should execute.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="executionInterval"/> is less than or equal to <see cref="TimeSpan.Zero"/>.</exception>
        protected TimerWorker(TimeSpan executionInterval)
        {
            if (executionInterval <= TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(executionInterval), executionInterval, "Execution interval must be greater than zero.");
            
            this.executionInterval = executionInterval;
        }

        /// <summary>
        /// Gets if current instance is disposed.
        /// </summary>
        protected bool IsDisposed
        {
            get { return Interlocked.Read(ref disposed) == 1L; }
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
                    ClearTimer();
                    SetStopped();
                }
            }
        }

        /// <summary>
        /// Starts worker by creating new timer and executing work when it elapses.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If current instance is disposed.</exception>
        /// <remarks>This is invoked by base class and derived classes should not invoke it directly.</remarks>
        protected sealed override void OnStart()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);

            cancellationTokenSource = new CancellationTokenSource();

            timer = new FrameworkTimer(executionInterval)
            {
                AutoReset = true,
                Tag = cancellationTokenSource.Token,
                AllowRaiseElapsed = true
            };

            timer.Elapsed += OnTimerElapsed;
            timer.Start();
        }

        /// <summary>
        /// Stops the worker and wait worker thread to end.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If current instance is disposed.</exception>
        /// <remarks>This is invoked by base class and derived classes should not invoke it directly.</remarks>
        protected sealed override void OnStop()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            if (timer != null)
                timer.Stop();
            ClearCancellationTokenSource();
            ClearTimer();
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

        private void ClearTimer()
        {
            if (timer != null)
            {
                timer.Stop();
                timer.Elapsed -= OnTimerElapsed;
                timer.Tag = null;
                timer.Dispose();
            }
        }

        private void OnTimerElapsed(object? sender, System.Timers.ElapsedEventArgs e)
        {
            if (IsDisposed)
                return;

            if (sender is FrameworkTimer timer && timer.Tag is CancellationToken cancellationToken)
            {
                if (cancellationToken.IsCancellationRequested)
                    return;

                Run(cancellationToken);
            }
        }
    }
}
