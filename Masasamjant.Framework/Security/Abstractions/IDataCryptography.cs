using System.Text;

namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents component that provides encryption and decryption of data.
    /// </summary>
    public interface IDataCryptography
    {
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
        Task<byte[]> EncryptDataAsync(byte[] clearData, string password, Salt salt, CancellationToken cancellationToken = default);

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
        Task<string> EncryptStringAsync(string clearData, string password, Salt salt, Encoding? encoding = null, CancellationToken cancellationToken = default);

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
        Task<byte[]> DecryptDataAsync(byte[] cipherData, string password, Salt salt, CancellationToken cancellationToken = default);

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
        Task<string> DecryptStringAsync(string cipherData, string password, Salt salt, Encoding? encoding = null, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents component that provides encryption and decryption of data using <typeparamref name="TCryptoKey"/> key.
    /// </summary>
    /// <typeparam name="TCryptoKey">The type of the <see cref="CryptoKey"/>.</typeparam>
    public interface IDataCryptography<TCryptoKey> : IDataCryptography, ICryptography<TCryptoKey>
        where TCryptoKey : CryptoKey
    {
        /// <summary>
        /// Encrypt specified clear data.
        /// </summary>
        /// <param name="clearData">The clear data to encrypt.</param>
        /// <param name="key">The crypto key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption and when completed contains encrypted data.</returns>
        /// <exception cref="InvalidOperationException">If data encryption fails.</exception>
        Task<byte[]> EncryptDataAsync(byte[] clearData, TCryptoKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Encrypt specified clear string.
        /// </summary>
        /// <param name="clearData">The clear string to encrypt.</param>
        /// <param name="key">The crypto key.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use unicode encoding.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption and when completed contains encrypted string.</returns>
        /// <exception cref="InvalidOperationException">If string encryption fails.</exception>
        Task<string> EncryptStringAsync(string clearData, TCryptoKey key, Encoding? encoding = null, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decrypt specified cipher data.
        /// </summary>
        /// <param name="cipherData">The cipher data to decrypt.</param>
        /// <param name="key">The crypto key.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing decryption and when completed contains decrypted data.</returns>
        /// <exception cref="InvalidOperationException">If data decryption fails.</exception>
        Task<byte[]> DecryptDataAsync(byte[] cipherData, TCryptoKey key, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decrypt specified cipher string.
        /// </summary>
        /// <param name="cipherData">The cipher string to decrypt.</param>
        /// <param name="key">The crypto key.</param>
        /// <param name="encoding">The encoding or <c>null</c> to use unicode encoding.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing decryption and when completed contains decrypted string.</returns>
        /// <exception cref="InvalidOperationException">If string encryption fails.</exception>
        Task<string> DecryptStringAsync(string cipherData, TCryptoKey key, Encoding? encoding = null, CancellationToken cancellationToken = default);
    }
}
