using Masasamjant.Security.Abstractions;
using System.Security.Cryptography;
using System.Text;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents component that provides encryption and decryption of data using AES algorithm.
    /// </summary>
    public sealed class AesDataCryptography : IDataCryptography
    {
        private readonly AesStreamCryptography cryptography;

        /// <summary>
        /// Initializes new instance of the <see cref="AesDataCryptography"/> class.
        /// </summary>
        /// <param name="hashAlgorithmName">The hash algorithm name. If <c>null</c>, the use default of SHA384.</param>
        /// <param name="iterations">The number of iterations. If <c>null</c>, then use default value of one million iterations.</param>
        /// <remarks>If value of <paramref name="iterations"/> is less than 1000, then 1000 iterations is used.</remarks>
        public AesDataCryptography(HashAlgorithmName? hashAlgorithmName = null, int? iterations = null)
        {
            cryptography = new AesStreamCryptography(hashAlgorithmName, iterations);
        }

        /// <summary>
        /// Decrypt specified cipher data.
        /// </summary>
        /// <param name="cipherData">The cipher data to decrypt.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing decryption and when completed contains decrypted data.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If data decryption fails.</exception>
        public async Task<byte[]> DecryptDataAsync(byte[] cipherData, string password, Salt salt, CancellationToken cancellationToken = default)
        {
            if (cipherData.Length == 0)
                return [];

            try
            {
                using (var sourceStream = new MemoryStream(cipherData))
                using (var destinationStream = new MemoryStream()) 
                {
                    await cryptography.DecryptAsync(sourceStream, destinationStream, password, salt, cancellationToken);
                    return destinationStream.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("The data decryption failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Decrypt specified cipher string.
        /// </summary>
        /// <param name="cipherData">The cipher string to decrypt.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use unicode encoding.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing decryption and when completed contains decrypted string.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If string encryption fails.</exception>
        public async Task<string> DecryptStringAsync(string cipherData, string password, Salt salt, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(cipherData))
                return cipherData;

            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherData);
                byte[] clearBytes = await DecryptDataAsync(cipherBytes, password, salt, cancellationToken);
                if (encoding == null)
                    encoding = Encoding.Unicode;
                return encoding.GetString(clearBytes);
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("The string decryption failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Encrypt specified clear data.
        /// </summary>
        /// <param name="clearData">The clear data to encrypt.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption and when completed contains encrypted data.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If data encryption fails.</exception>
        public async Task<byte[]> EncryptDataAsync(byte[] clearData, string password, Salt salt, CancellationToken cancellationToken = default)
        {
            if (clearData.Length == 0)
                return [];

            try
            {
                using (var sourceStream = new MemoryStream(clearData))
                using (var destinationStream = new MemoryStream())
                {
                    await cryptography.EncryptAsync(sourceStream, destinationStream, password, salt, cancellationToken);
                    return destinationStream.ToArray();
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("The data encryption failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Encrypt specified clear string.
        /// </summary>
        /// <param name="clearData">The clear string to encrypt.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use unicode encoding.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption and when completed contains encrypted string.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If string encryption fails.</exception>
        public async Task<string> EncryptStringAsync(string clearData, string password, Salt salt, Encoding? encoding = null, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(clearData))
                return clearData;

            try
            {
                byte[] clearBytes = clearData.GetByteArray(encoding);
                byte[] cipherBytes = await EncryptDataAsync(clearBytes, password, salt, cancellationToken);
                return Convert.ToBase64String(cipherBytes);
            }
            catch (Exception exception)
            {
                throw new NotImplementedException("The string encryption failed. See inner exception for details.", exception);
            }
        }
    }
}
