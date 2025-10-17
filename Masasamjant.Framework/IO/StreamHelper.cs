namespace Masasamjant.IO
{
    /// <summary>
    /// Provides helper methods to work with streams.
    /// </summary>
    public static class StreamHelper
    {
        /// <summary>
        /// Stream zero position.
        /// </summary>
        public const long ZeroPosition = 0L;

        /// <summary>
        /// Default buffer size of operations.
        /// </summary>
        public const int DefaultBufferSize = 1024;

        /// <summary>
        /// Create <see cref="MemoryStream"/> from specified <see cref="Stream"/>. If <paramref name="stream"/> is <see cref="MemoryStream"/>,
        /// then return that.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to create memory stream.</param>
        /// <returns>A <see cref="MemoryStream"/> created from <paramref name="stream"/>.</returns>
        /// <exception cref="InvalidOperationException">If cannot create memory stream from <paramref name="stream"/>.</exception>
        public static MemoryStream ToMemoryStream(this Stream stream)
        {
            if (stream is MemoryStream ms)
                return ms;
            else
            {
                try
                {
                    ms = new MemoryStream();
                    var position = stream.Position;
                    stream.Position = ZeroPosition;
                    stream.CopyTo(ms);
                    stream.Position = position;
                    ms.Position = ZeroPosition;
                    return ms;
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException($"Creating memory stream from {stream.GetType()} failed.", exception);
                }
            }
        }
        
        /// <summary>
        /// Create <see cref="MemoryStream"/> from specified <see cref="Stream"/>. If <paramref name="stream"/> is <see cref="MemoryStream"/>,
        /// then return that.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to create memory stream.</param>
        /// <returns>A <see cref="MemoryStream"/> created from <paramref name="stream"/>.</returns>
        /// <exception cref="InvalidOperationException">If cannot create memory stream from <paramref name="stream"/>.</exception>
        public static async Task<MemoryStream> ToMemoryStreamAsync(this Stream stream)
        {
            if (stream is MemoryStream ms)
                return ms;
            else
            {
                try
                {
                    ms = new MemoryStream();
                    var position = stream.Position;
                    stream.Position = ZeroPosition;
                    await stream.CopyToAsync(ms);
                    stream.Position = position;
                    ms.Position = ZeroPosition;
                    return ms;
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException($"Creating memory stream from {stream.GetType()} failed.", exception);
                }
            }
        }

        /// <summary>
        /// Read data from specified <see cref="Stream"/> to byte array.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to read.</param>
        /// <returns>A byte array of data from <paramref name="stream"/>.</returns>
        public static byte[] ToBytes(this Stream stream)
        {
            if (stream is MemoryStream ms)
                return ms.ToArray();

            using (ms = ToMemoryStream(stream))
                return ms.ToArray();
        }

        /// <summary>
        /// Read data from specified <see cref="Stream"/> to byte array.
        /// </summary>
        /// <param name="stream">The <see cref="Stream"/> to read.</param>
        /// <returns>A byte array of data from <paramref name="stream"/>.</returns>
        public static async Task<byte[]> ToBytesAsync(this Stream stream)
        {
            if (stream is MemoryStream ms)
                return ms.ToArray();

            using (ms = await ToMemoryStreamAsync(stream))
                return ms.ToArray();
        }

        /// <summary>
        /// Copy data from specified source stream to specified destination stream.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="destination">The destination stream.</param>
        /// <param name="progressHandler">The update progress handler.</param>
        /// <param name="bufferSize">The buffer size of each read.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="destination"/> is same as <paramref name="source"/>.
        /// -or-
        /// If cannot read from <paramref name="source"/>.
        /// -or-
        /// If cannot write to <paramref name="destination"/>.
        /// </exception>
        public static void CopyStream(Stream source, Stream destination, Action<StreamCopyProgress>? progressHandler = null, int bufferSize = DefaultBufferSize)
        {
            ValidateStreamCopy(source, destination);
            bufferSize = EnsureBufferSize(bufferSize);

            var copied = 0L;
            var buffer = new byte[bufferSize];
            var read = source.Read(buffer, 0, bufferSize);

            while (read > 0)
            {
                destination.Write(buffer, 0, read);
                copied += read;
                progressHandler?.Invoke(new StreamCopyProgress(source, destination, copied));
                read = source.Read(buffer, 0, bufferSize);
            }
        }

        /// <summary>
        /// Copy data from specified source stream to specified destination stream.
        /// </summary>
        /// <param name="source">The source stream.</param>
        /// <param name="destination">The destination stream.</param>
        /// <param name="progressHandler">The update progress handler.</param>
        /// <param name="bufferSize">The buffer size of each read.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="destination"/> is same as <paramref name="source"/>.
        /// -or-
        /// If cannot read from <paramref name="source"/>.
        /// -or-
        /// If cannot write to <paramref name="destination"/>.
        /// </exception>
        public static async Task CopyStreamAsync(Stream source, Stream destination, Func<StreamCopyProgress, Task>? progressHandler = null, int bufferSize = DefaultBufferSize)
        {
            ValidateStreamCopy(source, destination);
            bufferSize = EnsureBufferSize(bufferSize);

            var copied = 0L;
            var buffer = new byte[bufferSize];
            var read = await source.ReadAsync(buffer, 0, bufferSize);

            while (read > 0)
            {
                await destination.WriteAsync(buffer, 0, read);
                copied += read;
                if (progressHandler != null)
                    await progressHandler(new StreamCopyProgress(source, destination, copied));
                read = await source.ReadAsync(buffer, 0, bufferSize);
            }
        }

        private static void ValidateStreamCopy(Stream source, Stream destination)
        {
            if (ReferenceEquals(source, destination))
                throw new ArgumentException("The destination stream cannot be same as source.", nameof(destination));

            if (!source.CanRead)
                throw new ArgumentException("Cannot read from source stream.", nameof(source));

            if (!destination.CanWrite)
                throw new ArgumentException("Cannot write to destination stream.", nameof(destination));
        }

        private static int EnsureBufferSize(int bufferSize) => bufferSize <= 0 ? DefaultBufferSize : bufferSize;
    }
}
