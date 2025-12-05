namespace Masasamjant.Threading
{
    /// <summary>
    /// Represents a provider for obtaining the current thread.
    /// </summary>
    public interface IThreadProvider
    {
        /// <summary>
        /// Gets the current thread.
        /// </summary>
        /// <returns>A current thread.</returns>
        IThread GetCurrentThread();
    }
}
