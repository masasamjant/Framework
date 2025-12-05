using System.Globalization;

namespace Masasamjant.Threading
{
    /// <summary>
    /// Represents an adapter for the <see cref="Thread"/> class, implementing the <see cref="IThread"/> interface.
    /// </summary>
    public sealed class ThreadAdapter : IThread
    {
        private readonly Thread thread;

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadAdapter"/> class.
        /// </summary>
        /// <param name="thread">The thread to adapt.</param>
        public ThreadAdapter(Thread thread)
        {
            this.thread = thread;
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
    }
}
