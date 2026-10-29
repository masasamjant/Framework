using System.Globalization;

namespace Masasamjant.Threading
{
    /// <summary>
    /// Represents an adapter for the <see cref="Thread"/> class, implementing the <see cref="IThread"/> interface.
    /// </summary>
    /// <remarks>This class does not prevent adapting current thread, but doing that might have unintended consequences.</remarks>
    public sealed class ThreadAdapter : IThread
    {
        private readonly Thread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadAdapter"/> class.
        /// </summary>
        /// <param name="thread">The thread to adapt.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="thread"/> is <c>null</c>.</exception>
        public ThreadAdapter(Thread thread)
        {
            this.thread = thread ?? throw new ArgumentNullException(nameof(thread));
        }

        /// <summary>
        /// Gets the unique identifier for the current thread.
        /// </summary>
        public int ManagedThreadId => thread.ManagedThreadId;

        /// <summary>
        /// Gets the current culture used by the thread.
        /// </summary>
        public CultureInfo CurrentCulture => thread.CurrentCulture;

        /// <summary>
        /// Gets the current UI culture used by the thread.
        /// </summary>
        public CultureInfo CurrentUICulture => thread.CurrentUICulture;

        /// <summary>
        /// Gets or sets the name of the thread.
        /// </summary>
        public string? Name
        {
            get => thread.Name;
            set => thread.Name = value;
        }

        /// <summary>
        /// Start thread.
        /// </summary>
        public void Start()
        {
            thread.Start();
        }

        /// <summary>
        /// Start thread with argument.
        /// </summary>
        /// <param name="arg">The thread start argument.</param>
        public void Start(object? arg)
        {
            thread.Start(arg);
        }

        /// <summary>
        /// Blocks the calling thread until the thread represented by this instance terminates.
        /// </summary>
        /// <param name="millisecondsTimeout">The number of milliseconds to wait, or <c>null</c> to wait indefinitely.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="millisecondsTimeout"/> is less than -1.</exception>
        public void Join(int? millisecondsTimeout)
        {
            if (millisecondsTimeout.HasValue)
            {
                thread.Join(millisecondsTimeout.Value);
            }
            else
            {
                thread.Join();
            }
        }
    }
}
