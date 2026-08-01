namespace Masasamjant.Windows
{
    /// <summary>
    /// Represents the event data for a directory information event.
    /// </summary>
    public sealed class DirectoryInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryInfoEventArgs"/> class.
        /// </summary>
        /// <param name="directory">The <see cref="DirectoryInfo"/> associated with the event.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="directory"/> is null.</exception>
        public DirectoryInfoEventArgs(DirectoryInfo directory)
        {
            Directory = directory ?? throw new ArgumentNullException(nameof(directory));
        }

        /// <summary>
        /// Gets the <see cref="DirectoryInfo"/> associated with the event.
        /// </summary>
        public DirectoryInfo Directory { get; }
    }
}
