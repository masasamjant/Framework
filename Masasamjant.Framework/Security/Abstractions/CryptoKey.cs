using System.Security.Cryptography;

namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents cryptography key created from password and salt.
    /// </summary>
    public abstract class CryptoKey
    {
        private static readonly int MinIterations = 1000;

        /// <summary>
        /// Initializes new instance of the <see cref="CryptoKey"/> class.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The computing iterations.</param>
        /// <param name="hashAlgorithmName">The hash algorithm name.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <remarks>If value of <paramref name="iterations"/> is less than 1000, then minimum iterations value of 1000 is used.</remarks>
        protected CryptoKey(string password, Salt salt, int iterations, HashAlgorithmName hashAlgorithmName)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "The password is empty or only whitespace.");

            var (key, iv) = GenerateKey(password, salt, Math.Max(MinIterations, iterations), hashAlgorithmName);
            Key = key;
            IV = iv;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="CryptoKey"/> class.
        /// </summary>
        /// <param name="key">The key bytes.</param>
        /// <param name="iv">The initialization vector bytes.</param>
        protected CryptoKey(byte[] key, byte[] iv)
        {
            Key = key;
            IV = iv;
        }

        /// <summary>
        /// Gets the key bytes.
        /// </summary>
        public byte[] Key { get; }

        /// <summary>
        /// Gets the initialization vector bytes.
        /// </summary>
        public byte[] IV { get; }

        /// <summary>
        /// Derived classes must override to create key and initialization vector bytes.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The computing iterations.</param>
        /// <param name="hashAlgorithmName">The hash algorithm name.</param>
        /// <returns>A tuple of key and initialization vector bytes.</returns>
        protected abstract (byte[] Key, byte[] IV) GenerateKey(string password, Salt salt, int iterations, HashAlgorithmName hashAlgorithmName);
    }
}
