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
        private const int KeyLength = 32;
        private const int IVLength = 16;

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

        internal AesCryptoKey(byte[] key, byte[] iv)
            : base(key, iv) 
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
            byte[] data = Rfc2898DeriveBytes.Pbkdf2(password, salt.ToBytes(), iterations, hashAlgorithmName, KeyLength + IVLength);
            var key = data.Take(KeyLength).ToArray();
            var iv = data.Skip(KeyLength).Take(IVLength).ToArray();
            return (key, iv);
        }

        /// <summary>
        /// Export <see cref="AesCryptoKey"/> to specified file.
        /// </summary>
        /// <param name="key">The key to export.</param>
        /// <param name="filePath">The file to save exported key.</param>
        /// <returns>A task representing export.</returns>
        /// <exception cref="ArgumentException">If file specified by <paramref name="filePath"/> already exist.</exception>
        /// <exception cref="InvalidOperationException">If export operation fails.</exception>
        /// <remarks>It is responsibility of the caller to ensure file is secured.</remarks>
        public static async Task ExportAsync(AesCryptoKey key, string filePath)
        {
            if (File.Exists(filePath))
                throw new ArgumentException("The file already exist.", nameof(filePath));

            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                var export = new AesCryptoKeyExport();
                await export.ExportAsync(key, stream);
                await stream.FlushAsync();
            }
        }

        /// <summary>
        /// Import <see cref="AesCryptoKey"/> from specified file.
        /// </summary>
        /// <param name="filePath">The file to import.</param>
        /// <returns>A imported <see cref="AesCryptoKey"/>.</returns>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="filePath"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If import operation fails.</exception>
        public static async Task<AesCryptoKey> ImportAsync(string filePath)
        {
            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.", filePath);

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            { 
                var import = new AesCryptoKeyImport();
                return await import.ImportAsync(stream);
            }
        }
    }
}
