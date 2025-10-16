namespace Masasamjant.IO
{
    /// <summary>
    /// Represents stream that only support writing.
    /// </summary>
    /// <remarks>
    /// This class takes the ownership of the specified source stream. So if this stream is disposed, 
    /// then also underlying source stream is disposed.
    /// </remarks>
    public sealed class WriteOnlyStream : Stream
    {
        private readonly Stream source;
        private bool disposed;

        /// <summary>
        /// Initializes new instance of the <see cref="WriteOnlyStream"/> class.
        /// </summary>
        /// <param name="source">The source stream to write.</param>
        /// <exception cref="ArgumentException">If <paramref name="source"/> do not support writing.</exception>
        public WriteOnlyStream(Stream source)
        {
            if (!source.CanWrite)
                throw new ArgumentException("The source stream must be writable.", nameof(source));

            this.source = source;
            this.disposed = false;
        }

        /// <summary>
        /// Gets if or not support reading. Always <c>false</c>.
        /// </summary>
        public override bool CanRead => false;

        /// <summary>
        /// Gets if or not support seeking. Always <c>false</c>.
        /// </summary>
        public override bool CanSeek => false;

        /// <summary>
        /// Gets if or not support writing. Always <c>true</c>.
        /// </summary>
        public override bool CanWrite => true;

        /// <summary>
        /// Gets the length.
        /// </summary>
        public override long Length
        {
            get 
            {
                if (disposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return source.Length; 
            } 
        }

        /// <summary>
        /// Gets the position.
        /// </summary>
        /// <exception cref="NotSupportedException">If attempt to set value.</exception>
        public override long Position 
        {
            get
            {
                if (disposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return source.Position;
            }
            set => throw new NotSupportedException("Seeking is not supported"); 
        }

        /// <summary>
        /// Clear buffers and write buffered data.
        /// </summary>
        public override void Flush()
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);

            source.Flush();
        }

        /// <summary>
        /// Read stream. This is not supported.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The buffer offset.</param>
        /// <param name="count">The count.</param>
        /// <returns>Actual read count.</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override int Read(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Reading is not supported.");
        }

        /// <summary>
        /// Seek stream. This is not supported.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="origin">The origin.</param>
        /// <returns>A new position.</returns>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException("Seeking is not supported");
        }

        /// <summary>
        /// Set length. This is not supported.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("Setting length is not supported");
        }

        /// <summary>
        /// Write data.
        /// </summary>
        /// <param name="buffer">The data to write.</param>
        /// <param name="offset">The offset of <paramref name="buffer"/> to read.</param>
        /// <param name="count">The count of data to write.</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);

            source.Write(buffer, offset, count);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposed)
                return;

            disposed = true;

            if (disposing)
            {
                source.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}
