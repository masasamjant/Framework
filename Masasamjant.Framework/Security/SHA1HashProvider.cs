using Masasamjant.Security.Abstractions;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="HashProvider"/> that use SHA-1 algorithm.
    /// </summary>
    public sealed class SHA1HashProvider : HashProvider
    {
        /// <summary>
        /// Gets the name of implemented algorithm. Always 'SHA-1'.
        /// </summary>
        public override string Algorithm => "SHA-1";

        /// <summary>
        /// Hash data using SHA-1 algorihtm.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>A hashed data.</returns>
        public override byte[] HashData(byte[] data)
        {
            return SHA1.HashData(data);
        }

        /// <summary>
        /// Hash data from stream using SHA-1 algorithm.
        /// </summary>
        /// <param name="source">The data to hash.</param>
        /// <returns>A task that, when completed, contains hashed data.</returns>
        public override async Task<byte[]> HashDataAsync(Stream source)
        {
            return await SHA1.HashDataAsync(source);
        }
    }
}
