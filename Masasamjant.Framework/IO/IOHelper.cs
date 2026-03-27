namespace Masasamjant.IO
{
    /// <summary>
    /// Provides helper methods for IO operations.
    /// </summary>
    public static class IOHelper
    {
        /// <summary>
        /// Defines the small buffer size for file operations. 
        /// </summary>
        public const int SmallBufferSize = 4096;

        /// <summary>
        /// Defines the medium buffer size for file operations.
        /// </summary>
        public const int MediumBufferSize = 16384;

        /// <summary>
        /// Defines the large buffer size for file operations.
        /// </summary>
        public const int LargeBufferSize = 131072;

        /// <summary>
        /// Limit in bytes for small buffer size.
        /// </summary>
        public const int SmallBufferSizeLimit = 1024 * 1024;

        /// <summary>
        /// Limit in bytes for medium buffer size.
        /// </summary>
        public const int MediumBufferSizeLimit = 50 * 1024 * 1024;

        /// <summary>
        /// Tries to determine optimal buffer size based on file size.
        /// If file does not exist, returns default small buffer size.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>A buffer size.</returns>
        public static int GetBufferSize(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "File path is null, empty or only whitespace.");

            return GetBufferSize(new FileInfo(filePath));
        }

        /// <summary>
        /// Tries to determine optimal buffer size based on file size. 
        /// If file does not exist, returns default small buffer size.
        /// </summary>
        /// <param name="fileInfo">The file information.</param>
        /// <returns>A buffer size.</returns>
        public static int GetBufferSize(FileInfo fileInfo)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);

            if (!fileInfo.Exists)
                return SmallBufferSize;

            return GetBufferSize(fileInfo.Length);
        }

        /// <summary>
        /// Tries to determine optimal buffer size based on stream length. 
        /// If stream does not support length, returns default small buffer size.
        /// </summary>
        /// <param name="stream">The stream.</param>
        /// <returns>A buffer size.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="stream"/> is <c>null</c>.</exception>
        public static int GetBufferSize(Stream stream)
        {
            ArgumentNullException.ThrowIfNull(stream);
            long len = Math.Max(0, stream.TryGetLength());
            return GetBufferSize(len);
        }

        /// <summary>
        /// Tries to determine optimal buffer size based on data length.
        /// </summary>
        /// <param name="length">The data length.</param>
        /// <returns>A buffer size. </returns>
        public static int GetBufferSize(long length)
        {
            return length switch
            {
                < SmallBufferSizeLimit => SmallBufferSize,
                < MediumBufferSizeLimit => MediumBufferSize,
                _ => LargeBufferSize
            };
        }
    }
}
