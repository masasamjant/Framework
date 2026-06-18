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
        internal const int KeyLength = 32;
        internal const int IVLength = 16;

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

        internal AesCryptoKey(byte[] key)
            : base(key) 
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
            using (Aes aes = Aes.Create())
            {
                byte[] key = CryptographyHelper.GetPseudoRandomBytes(password, salt, iterations, hashAlgorithmName, KeyLength);
                aes.GenerateIV();
                byte[] iv = aes.IV;
                return (key, iv);
            }
        }

        /// <summary>
        /// Creates key and initialization vector bytes for AES algorithm
        /// </summary>
        /// <param name="data">The data to generate key and initialization vector.</param>
        /// <returns>A tuple of key and initialization vector bytes for AES algorithm.</returns>
        protected override (byte[] Key, byte[] IV) GenerateKey(byte[] data)
        {
            using (Aes aes = Aes.Create())
            {
                byte[] key = new byte[KeyLength];
                Array.Copy(data, 0, key, 0, KeyLength);
                aes.GenerateIV();
                byte[] iv = aes.IV;
                return (key, iv);
            }
        }

        /// <summary>
        /// Export <see cref="AesCryptoKey"/> to specified file.
        /// </summary>
        /// <param name="key">The key to export.</param>
        /// <param name="filePath">The file to save exported key.</param>
        /// <returns>A task representing export.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If file specified by <paramref name="filePath"/> already exist.</exception>
        /// <exception cref="InvalidOperationException">If export operation fails.</exception>
        /// <remarks>It is responsibility of the caller to ensure file is secured.</remarks>
        public static Task ExportAsync(AesCryptoKey key, string filePath)
        {
            return ExportAsync(key, filePath, new AesCryptoKeyExport());
        }

        /// <summary>
        /// Export <see cref="AesCryptoKey"/> to specified file using specified <see cref="AesCryptoKeyExport"/> instance.
        /// </summary>
        /// <param name="key">The key to export.</param>
        /// <param name="filePath">The file to save exported key.</param>
        /// <param name="exporter">The exporter instance to use.</param>
        /// <returns>A task representing export.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> or <paramref name="exporter"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If file specified by <paramref name="filePath"/> already exist.</exception>
        /// <exception cref="InvalidOperationException">If export operation fails.</exception>
        public static async Task ExportAsync(AesCryptoKey key, string filePath, AesCryptoKeyExport exporter)
        {
            ArgumentNullException.ThrowIfNull(key);
            ArgumentNullException.ThrowIfNull(exporter);

            if (File.Exists(filePath))
                throw new ArgumentException("The file already exist.", nameof(filePath));

            using (var stream = new FileStream(filePath, FileMode.CreateNew, FileAccess.Write))
            {
                await exporter.ExportAsync(key, stream);
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
        public static Task<AesCryptoKey> ImportAsync(string filePath)
        {
            return ImportAsync(filePath, new AesCryptoKeyImport());
        }

        /// <summary>
        /// Import <see cref="AesCryptoKey"/> from specified file using specified <see cref="AesCryptoKeyImport"/> instance.
        /// </summary>
        /// <param name="filePath">The file to import.</param>
        /// <param name="importer">The <see cref="AesCryptoKeyImport"/> instance to use.</param>
        /// <returns>A imported <see cref="AesCryptoKey"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="importer"/> is <c>null</c>.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="filePath"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If import operation fails.</exception>
        public static async Task<AesCryptoKey> ImportAsync(string filePath, AesCryptoKeyImport importer)
        {
            ArgumentNullException.ThrowIfNull(importer);

            if (!File.Exists(filePath))
                throw new FileNotFoundException("File not found.", filePath);

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                return await importer.ImportAsync(stream);
            }
        }
    }
}
