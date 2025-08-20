namespace Masasamjant.IO
{
    /// <summary>
    /// Represents progress of copying data between streams.
    /// </summary>
    public sealed class StreamCopyProgress
    {
        /// <summary>
        /// Initializes new instance of the <see cref="StreamCopyProgress"/> class.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="destination">The destination stream.</param>
        /// <param name="copiedBytes">The count of copied bytes.</param>
        /// <exception cref="ArgumentException">If destination stream is same stream as source.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="copiedBytes"/> is less than 0.</exception>
        public StreamCopyProgress(Stream source, Stream destination, long copiedBytes)
        {
            if (ReferenceEquals(source, destination))
                throw new ArgumentException("The destination stream cannot be same as source stream.", nameof(destination));

            if (copiedBytes < 0L)
                throw new ArgumentOutOfRangeException(nameof(copiedBytes), copiedBytes, "The count of copied bytes must be greater than or equal to 0.");

            Source = source;
            Destination = destination;
            CopiedBytes = copiedBytes;
        }

        /// <summary>
        /// Gets the source stream.
        /// </summary>
        public Stream Source { get; }

        /// <summary>
        /// Gets the destination stream.
        /// </summary>
        public Stream Destination { get; }

        /// <summary>
        /// Gets the count of copied bytes.
        /// </summary>
        public long CopiedBytes { get; }
    }
}
