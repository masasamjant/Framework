using System.IO.Compression;
using System.Text;

namespace Masasamjant.IO
{
    /// <summary>
    /// Provides helper method to simple data compression.
    /// </summary>
    public static class CompressionHelper
    {
        /// <summary>
        /// Compress data using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="level">The <see cref="CompressionLevel"/>.</param>
        /// <returns>A compressed data.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static byte[] Compress(byte[] data, CompressionLevel level = CompressionLevel.Optimal)
        {
            ValidateCompressionLevel(level);

            if (data.Length == 0)
                return [];

            if (data.Length == 1)
                return [data[0]];

            using (var source = new MemoryStream(data))
            using (var destination = new MemoryStream())
            {
                using (var zip = new GZipStream(destination, level))
                {
                    source.CopyTo(zip);
                    zip.Flush();
                }

                return destination.ToArray();
            }
        }

        /// <summary>
        /// Compress data using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="level">The <see cref="CompressionLevel"/>.</param>
        /// <returns>A compressed data.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static async Task<byte[]> CompressAsync(byte[] data, CompressionLevel level = CompressionLevel.Optimal)
        {
            ValidateCompressionLevel(level);

            if (data.Length == 0)
                return [];

            if (data.Length == 1)
                return [data[0]];

            using (var source = new MemoryStream(data))
            using (var destination = new MemoryStream())
            {
                using (var zip = new GZipStream(destination, level))
                {
                    await source.CopyToAsync(zip);
                    await zip.FlushAsync();
                }

                return destination.ToArray();
            }
        }

        /// <summary>
        /// Compress string data using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="level">The <see cref="CompressionLevel"/>.</param>
        /// <param name="encoding">The <see cref="Encoding"/> or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        /// <returns>A compressed data as byte array.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static byte[] Compress(string data, CompressionLevel level = CompressionLevel.Optimal, Encoding? encoding = null)
        {
            if (data.Length == 0)
                return [];

            return Compress(data.GetByteArray(encoding), level);
        }

        /// <summary>
        /// Compress string data using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to compress.</param>
        /// <param name="level">The <see cref="CompressionLevel"/>.</param>
        /// <param name="encoding">The <see cref="Encoding"/> or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        /// <returns>A compressed data as byte array.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static async Task<byte[]> CompressAsync(string data, CompressionLevel level = CompressionLevel.Optimal, Encoding? encoding = null)
        {
            if (data.Length == 0)
                return [];

            return await CompressAsync(data.GetByteArray(encoding), level);
        }

        /// <summary>
        /// Decompress data using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="level">The <see cref="CompressionLevel"/>.</param>
        /// <returns>A decompressed data.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static byte[] Decompress(byte[] data)
        {
            if (data.Length == 0)
                return [];

            using (var source = new MemoryStream(data))
            using (var destination = new MemoryStream())
            {
                using (var zip = new GZipStream(source, CompressionMode.Decompress))
                {
                    zip.CopyTo(destination);
                    destination.Flush();
                }

                return destination.ToArray();
            }
        }

        /// <summary>
        /// Decompress data using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="level">The <see cref="CompressionLevel"/>.</param>
        /// <returns>A decompressed data.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static async Task<byte[]> DecompressAsync(byte[] data)
        {
            if (data.Length == 0)
                return [];

            using (var source = new MemoryStream(data))
            using (var destination = new MemoryStream())
            {
                using (var zip = new GZipStream(source, CompressionMode.Decompress))
                {
                    await zip.CopyToAsync(destination);
                    await destination.FlushAsync();
                }

                return destination.ToArray();
            }
        }

        /// <summary>
        /// Decompress data to string using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="encoding">The <see cref="Encoding"/> or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        /// <returns>A decompressed string.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static string Decompress(byte[] data, Encoding? encoding = null)
        {
            if (data.Length == 0)
                return string.Empty;

            var buffer = Decompress(data);
            var enc = encoding ?? Encoding.Unicode;
            return enc.GetString(buffer);   
        }

        /// <summary>
        /// Decompress data to string using <see cref="GZipStream"/>.
        /// </summary>
        /// <param name="data">The data to decompress.</param>
        /// <param name="encoding">The <see cref="Encoding"/> or <c>null</c> to use <see cref="Encoding.Unicode"/>.</param>
        /// <returns>A decompressed string.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="level"/> is not defined.</exception>
        public static async Task<string> DecompressAsync(byte[] data, Encoding? encoding = null)
        {
            if (data.Length == 0)
                return string.Empty;

            var buffer = await DecompressAsync(data);
            var enc = encoding ?? Encoding.Unicode;
            return enc.GetString(buffer);
        }

        private static void ValidateCompressionLevel(CompressionLevel level)
        {
            if (!Enum.IsDefined(level))
                throw new ArgumentException($"The compression level '{level}' is not defined.", nameof(level));
        }
    }
}
