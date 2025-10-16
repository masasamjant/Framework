namespace Masasamjant.IO
{
    /// <summary>
    /// Represents stream that only support reading.
    /// </summary>
    /// <remarks>
    /// This class takes the ownership of the specified source stream. So if this stream is disposed, 
    /// then also underlying source stream is disposed.
    /// </remarks>
    public sealed class ReadOnlyStream : Stream
    {
        private readonly Stream source;
        private bool disposed;

        /// <summary>
        /// Initializes new instance of the <see cref="ReadOnlyStream"/> class.
        /// </summary>
        /// <param name="source">The source stream that is read.</param>
        /// <exception cref="ArgumentException">If <paramref name="source"/> do not support reading.</exception>
        public ReadOnlyStream(Stream source)
        {
            if (!source.CanRead)
                throw new ArgumentException("The source stream must be readable.", nameof(source));

            this.source = source;
            this.disposed = false;
        }

        /// <summary>
        /// Gets if or not support reading. Always <c>true</c>.
        /// </summary>
        public override bool CanRead => true;

        /// <summary>
        /// Gets if or not support seeking.
        /// </summary>
        public override bool CanSeek => source.CanSeek;

        /// <summary>
        /// Gets if or not support writing. Always <c>false</c>.
        /// </summary>
        public override bool CanWrite => false;

        /// <summary>
        /// Gets the length of the stream.
        /// </summary>
        /// <exception cref="ObjectDisposedException">If instance is disposed.</exception>
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
        /// Gets or sets the position.
        /// </summary>
        public override long Position 
        {
            get 
            {
                if (disposed)
                    throw new ObjectDisposedException(GetType().FullName);

                return source.Position; 
            }
            set 
            {
                if (disposed)
                    throw new ObjectDisposedException(GetType().FullName);

                source.Position = value; 
            }
        }

        /// <summary>
        /// Flushes data written. This is not supported.
        /// </summary>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override void Flush()
        {
            throw new NotSupportedException("Flush is not supported in read stream.");
        }

        /// <summary>
        /// Read stream.
        /// </summary>
        /// <param name="buffer">The buffer to read data.</param>
        /// <param name="offset">The offset of <paramref name="buffer"/> to write data.</param>
        /// <param name="count">The count to read.</param>
        /// <returns>Actual read count.</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);

            return source.Read(buffer, offset, count);
        }

        /// <summary>
        /// Seeek stream.
        /// </summary>
        /// <param name="offset">The offset.</param>
        /// <param name="origin">The origin.</param>
        /// <returns>A new position.</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            if (disposed)
                throw new ObjectDisposedException(GetType().FullName);

            return source.Seek(offset, origin);
        }

        /// <summary>
        /// Set length. This is not supported.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override void SetLength(long value)
        {
            throw new NotSupportedException("Setting length is not supported.");
        }

        /// <summary>
        /// Write stream. This is not supported.
        /// </summary>
        /// <param name="buffer">The buffer.</param>
        /// <param name="offset">The buffer offset.</param>
        /// <param name="count">The count.</param>
        /// <exception cref="NotSupportedException">Always.</exception>
        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException("Write is not supported.");
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
