using System.Timers;

namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// A <see cref="ITimer"/> implementation that internally uses <see cref="System.Timers.Timer"/> to provide timer functionality.
    /// </summary>
    public sealed class FrameworkTimer : ITimer, IDisposable
    {
        private readonly System.Timers.Timer internalTimer;
        private long disposed = 0L;

        /// <summary>
        /// Initializes a new instance of the <see cref="FrameworkTimer"/> class with an optional interval.
        /// </summary>
        /// <param name="interval">The interval at which the timer elapses. Default is 100 milliseconds.</param>
        public FrameworkTimer(TimeSpan? interval = null)
        {
            double? totalMilliseconds = null;

            if (interval.HasValue)
            {
                totalMilliseconds = interval.Value.TotalMilliseconds;

                if (totalMilliseconds < 1.0D || totalMilliseconds > int.MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(interval), totalMilliseconds, "Total milliseconds of the interval must be between 1 and Int32.MaxValue.");
            }

            internalTimer = new System.Timers.Timer();
            internalTimer.Elapsed += OnTimerElapsed;

            if (totalMilliseconds.HasValue)
                internalTimer.Interval = totalMilliseconds.Value;
            else
                internalTimer.Interval = 100.0D;
        }

        /// <summary>
        /// Finalizes current instance.
        /// </summary>
        ~FrameworkTimer()
        {
            Dispose(false);
        }

        /// <summary>
        /// Notifies when the timer has elapsed.
        /// </summary>
        public event ElapsedEventHandler? Elapsed;

        private bool IsDisposed => Interlocked.Read(ref disposed) != 0L;

        /// <summary>
        /// Gets or sets a value indicating whether the timer should automatically reset after it has elapsed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        public bool AutoReset
        {
            get => internalTimer.AutoReset;
            set
            {
                ObjectDisposedException.ThrowIf(IsDisposed, this);
                internalTimer.AutoReset = value;
            }
        }

        /// <summary>
        /// Gets or sets the interval, in milliseconds, at which the timer elapses. Default is 100 milliseconds.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If the value is less than 1 or greater than Int32.MaxValue.</exception>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        public double Interval
        {
            get => internalTimer.Interval;
            set
            {
                if (value < 1.0D || value > int.MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(Interval), value, "Interval must be between 1 and Int32.MaxValue milliseconds.");

                ObjectDisposedException.ThrowIf(IsDisposed, this);
                internalTimer.Interval = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the timer is enabled and actively counting down to its next elapsed event.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        public bool Enabled
        {
            get => internalTimer.Enabled;
            set
            {
                ObjectDisposedException.ThrowIf(IsDisposed, this);
                internalTimer.Enabled = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the timer is allowed to raise the elapsed event manually
        /// by calling the <see cref="RaiseElapsed(DateTime?)"/> method. If set to <c>false</c>, the timer will not raise the elapsed event 
        /// when <see cref="RaiseElapsed(DateTime?)"/> is called.
        /// </summary>
        /// <remarks>Default value is <c>true</c>.</remarks>
        public bool AllowRaiseElapsed { get; set; } = true;

        /// <summary>
        /// Disposes the timer, releasing all resources used by it.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        public void Start()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            internalTimer.Start();
        }

        /// <summary>
        /// Stops the timer.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        public void Stop()
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            internalTimer.Stop();
        }

        /// <summary>
        /// Raises the elapsed event, signaling that the timer has elapsed. 
        /// This method can be called to manually trigger the elapsed event, optionally providing a specific signal time.
        /// </summary>
        /// <param name="signalTime">The time at which the elapsed event is signaled. If <c>null</c>, the current local time is used.</param>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        public void RaiseElapsed(DateTime? signalTime = null)
        {
            ObjectDisposedException.ThrowIf(IsDisposed, this);
            if (AllowRaiseElapsed)
                OnTimerElapsed(this, new ElapsedEventArgs(signalTime ?? DateTime.Now));
        }

        private void Dispose(bool disposing)
        {
            if (Interlocked.CompareExchange(ref disposed, 1L, 0L) == 0L)
            {
                if (Enabled)
                {
                    internalTimer.Stop();
                }

                internalTimer.Dispose();
            }
        }

        private void OnTimerElapsed(object? sender, ElapsedEventArgs e)
        {
            Elapsed?.Invoke(this, e);
        }
    }
}
