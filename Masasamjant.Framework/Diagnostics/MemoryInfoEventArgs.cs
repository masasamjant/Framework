namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents argument of events related to <see cref="MemoryInfo"/>.
    /// </summary>
    public sealed class MemoryInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes new instance of the <see cref="MemoryInfoEventArgs"/> class.
        /// </summary>
        /// <param name="memory">The <see cref="MemoryInfo"/>.</param>
        public MemoryInfoEventArgs(MemoryInfo memory)
        {
            Memory = memory;
        }

        /// <summary>
        /// Gets the related <see cref="MemoryInfo"/>.
        /// </summary>
        public MemoryInfo Memory { get; }
    }
}
