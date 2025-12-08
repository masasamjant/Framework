using System.Globalization;

namespace Masasamjant.Threading
{
    /// <summary>
    /// Represents a thread in the system.
    /// </summary>
    public interface IThread
    {
        /// <summary>
        /// Gets the unique identifier for the current thread.
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
    }
}
