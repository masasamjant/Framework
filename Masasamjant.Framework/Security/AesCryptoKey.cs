using Masasamjant.Security.Abstractions;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents cryptography key for AES algorithm.
    /// </summary>
    public sealed class AesCryptoKey : CryptoKey
    {
        private static readonly HashAlgorithmName DefaultHashAlgorithmName = HashAlgorithmName.SHA384;
        private static readonly int DefaultIterations = 1000000;

        /// <summary>
        /// Initializes new instance of the <see cref="AesCryptoKey"/> class.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The computing iterations. Default is one million iteration.</param>
        /// <param name="hashAlgorithmName">The hash algorithm name. Default is SHA384.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <remarks>If value of <paramref name="iterations"/> is less than 1000, then minimum iterations value of 1000 is used.</remarks>
        public AesCryptoKey(string password, Salt salt, int? iterations = null, HashAlgorithmName? hashAlgorithmName = null)
            : base(password, salt, iterations.GetValueOrDefault(DefaultIterations), hashAlgorithmName.GetValueOrDefault(DefaultHashAlgorithmName))
        { }

        /// <summary>
        /// Creates key and initialization vector bytes for AES algorithm
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The computing iterations.</param>
        /// <param name="hashAlgorithmName">The hash algorithm name.</param>
        /// <returns>A tuple of key and initialization vector bytes for AES algorithm.</returns>
        protected override (byte[] Key, byte[] IV) GenerateKey(string password, Salt salt, int iterations, HashAlgorithmName hashAlgorithmName)
        {
            using (var derivedBytes = new Rfc2898DeriveBytes(password, salt.ToBytes(), iterations, hashAlgorithmName))
            {
                var key = derivedBytes.GetBytes(32);
                var iv = derivedBytes.GetBytes(16);
                return (key, iv);
            }
        }
    }
}
