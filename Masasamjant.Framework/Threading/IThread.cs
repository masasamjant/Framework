using System.Globalization;

namespace Masasamjant.Threading
{
    /// <summary>
    /// Represents a thread in the system.
    /// </summary>
    public interface IThread
    {
        /// <summary>
        /// Gets the unique identifier for the thread.
        /// </summary>
        int ManagedThreadId { get; }

        /// <summary>
        /// Gets the current culture used by the thread.
        /// </summary>
        CultureInfo CurrentCulture { get; }

        /// <summary>
        /// Gets the current UI culture used by the thread.
        /// </summary>
        CultureInfo CurrentUICulture { get; }

        /// <summary>
        /// Gets or sets the name of the thread.
        /// </summary>
        string? Name { get; set; }

        /// <summary>
        /// Start thread.
        /// </summary>
        void Start();

        /// <summary>
        /// Start thread with argument.
        /// </summary>
        /// <param name="arg">The thread start argument.</param>
        void Start(object? arg);

        /// <summary>
        /// Blocks the calling thread until the thread represented by this instance terminates.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <c>null</c> to wait indefinitely.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="millisecondsTimeout"/> is less than -1.</exception>
        void Join(int? millisecondsTimeout);
    }
}
