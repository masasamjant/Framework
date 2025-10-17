using Masasamjant.Security.Abstractions;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents component that provides encryption and decryption of files using AES algorithm.
    /// </summary>
    public sealed class AesFileCryptography : IFileCryptography, IFileCryptography<AesCryptoKey>
    {
        private readonly AesStreamCryptography cryptography;

        /// <summary>
        /// Initializes new instance of the <see cref="AesFileCryptography"/> class.
        /// </summary>
        /// <param name="hashAlgorithmName">The hash algorithm name. If <c>null</c>, the use default of SHA384.</param>
        /// <param name="iterations">The number of iterations. If <c>null</c>, then use default value of one million iterations.</param>
        /// <remarks>If value of <paramref name="iterations"/> is less than 1000, then 1000 iterations is used.</remarks>
        public AesFileCryptography(HashAlgorithmName? hashAlgorithmName = null, int? iterations = null)
        {
            cryptography = new AesStreamCryptography(hashAlgorithmName, iterations);
        }

        /// <summary>
        /// Encrypt specified source file and save encrypted data to specified destination file.
        /// </summary>
        /// <param name="sourceFile">The file path of source file to encrypt.</param>
        /// <param name="destinationFile">The file path of destination file to save encrypted data.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="overwriteDestination"><c>true</c> to overwrite destination file, if exist; <c>false</c> otherwise.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sourceFile"/> or <paramref name="destinationFile"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="destinationFile"/> is same as value of <paramref name="sourceFile"/>.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="sourceFile"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If file encryption fails.</exception>
        public async Task EncryptAsync(string sourceFile, string destinationFile, string password, Salt salt, bool overwriteDestination = false, CancellationToken cancellationToken = default)
        {
            await EncryptAsync(sourceFile, destinationFile, CreateCryptoKey(password, salt), overwriteDestination, cancellationToken);
        }

        /// <summary>
        /// Encrypt specified source file and save encrypted data to specified destination file.
        /// </summary>
        /// <param name="sourceFile">The file path of source file to encrypt.</param>
        /// <param name="destinationFile">The file path of destination file to save encrypted data.</param>
        /// <param name="key">The crypto key.</param>
        /// <param name="overwriteDestination"><c>true</c> to overwrite destination file, if exist; <c>false</c> otherwise.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sourceFile"/> or <paramref name="destinationFile"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="destinationFile"/> is same as value of <paramref name="sourceFile"/>.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="sourceFile"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If file encryption fails.</exception>
        public async Task EncryptAsync(string sourceFile, string destinationFile, AesCryptoKey key, bool overwriteDestination = false, CancellationToken cancellationToken = default)
        {
            ValidateFilePaths(sourceFile, destinationFile);

            try
            {
                var destinationMode = overwriteDestination ? FileMode.Create : FileMode.CreateNew;

                using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
                using (var destinationStream = new FileStream(destinationFile, destinationMode, FileAccess.Write))
                {
                    await cryptography.EncryptAsync(sourceStream, destinationStream, key, cancellationToken);
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("The file encryption failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Decrypt specified source file and save clear data to specified destination file.
        /// </summary>
        /// <param name="sourceFile">The file path of source file to decrypt.</param>
        /// <param name="destinationFile">The file path of destination file to save decrypted data.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="overwriteDestination"><c>true</c> to overwrite destination file, if exist; <c>false</c> otherwise.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sourceFile"/> or <paramref name="destinationFile"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="destinationFile"/> is same as value of <paramref name="sourceFile"/>.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="sourceFile"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If file decryption fails.</exception>
        public async Task DecryptAsync(string sourceFile, string destinationFile, string password, Salt salt, bool overwriteDestination = false, CancellationToken cancellationToken = default)
        {
            await DecryptAsync(sourceFile, destinationFile, CreateCryptoKey(password, salt), overwriteDestination, cancellationToken);
        }

        /// <summary>
        /// Decrypt specified source file and save clear data to specified destination file.
        /// </summary>
        /// <param name="sourceFile">The file path of source file to decrypt.</param>
        /// <param name="destinationFile">The file path of destination file to save decrypted data.</param>
        /// <param name="key">The crypto key.</param>
        /// <param name="overwriteDestination"><c>true</c> to overwrite destination file, if exist; <c>false</c> otherwise.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sourceFile"/> or <paramref name="destinationFile"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="destinationFile"/> is same as value of <paramref name="sourceFile"/>.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="sourceFile"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If file decryption fails.</exception>
        public async Task DecryptAsync(string sourceFile, string destinationFile, AesCryptoKey key, bool overwriteDestination = false, CancellationToken cancellationToken = default)
        {
            ValidateFilePaths(sourceFile, destinationFile);

            try
            {
                var destinationMode = overwriteDestination ? FileMode.Create : FileMode.CreateNew;

                using (var sourceStream = new FileStream(sourceFile, FileMode.Open, FileAccess.Read))
                using (var destinationStream = new FileStream(destinationFile, destinationMode, FileAccess.Write))
                {
                    await cryptography.DecryptAsync(sourceStream, destinationStream, key, cancellationToken);
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("The file decryption failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Creates new <see cref="AesCryptoKey"/> instance from specified password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>A <see cref="AesCryptoKey"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        public AesCryptoKey CreateCryptoKey(string password, Salt salt) => cryptography.CreateCryptoKey(password, salt);

        private static void ValidateFilePaths(string sourceFile, string destinationFile)
        {
            if (string.IsNullOrWhiteSpace(sourceFile))
                throw new ArgumentNullException(nameof(sourceFile), "The source file path is empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(destinationFile))
                throw new ArgumentNullException(nameof(destinationFile), "The destination file path is empty or only whitespace.");

            if (string.Equals(sourceFile, destinationFile, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The destination file path is same as source file path.", nameof(destinationFile));

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException("The source file not exist.", sourceFile);
        }
    }
}
