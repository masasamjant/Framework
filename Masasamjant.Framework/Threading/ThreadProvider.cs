namespace Masasamjant.Threading
{
    /// <summary>
    /// Represents a provider for obtaining the current thread.
    /// </summary>
    public sealed class ThreadProvider : IThreadProvider
    {
        /// <summary>
        /// Gets the current thread.
        /// </summary>
        /// <returns>A current thread.</returns>
        public IThread GetCurrentThread()
        {
            return new ThreadAdapter(Thread.CurrentThread);
        }
    }
}
