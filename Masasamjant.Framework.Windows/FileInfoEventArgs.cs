namespace Masasamjant.Windows
{
    /// <summary>
    /// Represents the event data for a file information event.
    /// </summary>
    public sealed class FileInfoEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FileInfoEventArgs"/> class.
        /// </summary>
        /// <param name="file">The <see cref="FileInfo"/> associated with the event.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="file"/> is null.</exception>
        public FileInfoEventArgs(FileInfo file)
        {
            File = file ?? throw new ArgumentNullException(nameof(file));
        }

        /// <summary>
        /// Gets the <see cref="FileInfo"/> associated with the event.
        /// </summary>
        public FileInfo File { get; }
    }
}
