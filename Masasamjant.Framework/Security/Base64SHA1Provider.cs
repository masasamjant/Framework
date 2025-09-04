using Masasamjant.Security.Abstractions;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="IStringHashProvider"/> that computes Base-64 string from SHA1 hash.
    /// </summary>
    public sealed class Base64SHA1Provider : IStringHashProvider
    {
        /// <summary>
        /// Create Base-64 string of SHA1 hash from specified string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        public string CreateHash(string value)
        {
            var bytes = value.GetByteArray();
            if (bytes.Length == 0)
                return string.Empty;
            var sha = SHA1.HashData(bytes);
            return Convert.ToBase64String(sha);
        }

        /// <summary>
        /// Create Base-64 string of SHA1 hash from specified string value.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        public async Task<string> CreateHashAsync(string value)
        {
            var bytes = value.GetByteArray();
            if (bytes.Length == 0)
                return string.Empty;
            var source = new MemoryStream(bytes);
            var sha = await SHA1.HashDataAsync(source);
            return Convert.ToBase64String(sha);
        }
    }
}
