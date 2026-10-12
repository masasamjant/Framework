using System.Timers;

namespace Masasamjant.ComponentModel
{
    /// <summary>
    /// Represents timer that supports raising elapsed events at specified intervals or manually, if allowed.
    /// </summary>
    public interface ITimer : IDisposable
    {
        /// <summary>
        /// Notifies when the timer has elapsed.
        /// </summary>
        event ElapsedEventHandler? Elapsed;

        /// <summary>
        /// Gets or sets a value indicating whether the timer should automatically reset after it has elapsed.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        bool AutoReset { get; set; }

        /// <summary>
        /// Gets or sets the interval, in milliseconds, at which the timer elapses.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        double Interval { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the timer is enabled and actively counting down to its next elapsed event.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        bool Enabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the timer is allowed to raise the elapsed event manually
        /// by calling the <see cref="RaiseElapsed(DateTime?)"/> method. If set to <c>false</c>, the timer will not raise the elapsed event 
        /// when <see cref="RaiseElapsed(DateTime?)"/> is called.
        /// </summary>
        bool AllowRaiseElapsed { get; set; }

        /// <summary>
        /// Starts the timer.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        void Start();

        /// <summary>
        /// Stops the timer.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        void Stop();

        /// <summary>
        /// Raises the elapsed event, signaling that the timer has elapsed. 
        /// This method can be called to manually trigger the elapsed event, optionally providing a specific signal time.
        /// </summary>
        /// <param name="signalTime">The time at which the elapsed event is signaled. If <c>null</c>, the current local time is used.</param>
        /// <exception cref="ObjectDisposedException">If the timer has been disposed.</exception>
        void RaiseElapsed(DateTime? signalTime = null);
    }
}
