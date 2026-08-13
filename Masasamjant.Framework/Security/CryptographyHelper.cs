using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Provides cryptography helper methods.
    /// </summary>
    public static class CryptographyHelper
    {
        /// <summary>
        /// Minimum iterations.
        /// </summary>
        public const int MinIterations = 1000;
        
        /// <summary>
        /// Default key length in bytes.
        /// </summary>
        public const int DefaultKeyLength = 32;

        private static readonly HashAlgorithmName DefaultHashAlgorithm = HashAlgorithmName.SHA256;

        /// <summary>
        /// Gets pseudo-random bytes derived from the password and salt using PBKDF2 with the default parameters.
        /// This method uses low iteration count of <see cref="MinIterations"/>, key length of <see cref="DefaultKeyLength"/> and SHA256 hash algorithm.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>A pseudo-random bytes derived from the password and salt.</returns>
        public static byte[] GetPseudoRandomBytes(string password, Salt salt)
        {
            return GetPseudoRandomBytes(password, salt, MinIterations, DefaultHashAlgorithm, DefaultKeyLength);
        }

        /// <summary>
        /// Gets pseudo-random bytes derived from the password and salt using PBKDF2.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="iterations">The iterations. If less than <see cref="MinIterations"/>, <see cref="MinIterations"/> is used.</param>
        /// <param name="hashAlgorithm">The hash algorithm.</param>
        /// <param name="keyLength">The key length.</param>
        /// <returns>A pseudo-random bytes derived from the password and salt.</returns>
        public static byte[] GetPseudoRandomBytes(string password, Salt salt, int iterations, HashAlgorithmName hashAlgorithm, int keyLength)
        {
            ArgumentNullException.ThrowIfNull(password);
            ArgumentNullException.ThrowIfNull(salt);
            
            return Rfc2898DeriveBytes.Pbkdf2(password, salt.ToBytes(), Math.Max(MinIterations, iterations), hashAlgorithm, keyLength);
        }
    }
}
