namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents component that provides encryption and decryption of files.
    /// </summary>
    public interface IFileCryptography
    {
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
        Task EncryptAsync(string sourceFile, string destinationFile, string password, Salt salt, bool overwriteDestination = false, CancellationToken cancellationToken = default);

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
        Task DecryptAsync(string sourceFile, string destinationFile, string password, Salt salt, bool overwriteDestination = false, CancellationToken cancellationToken = default);
    }

    /// <summary>
    /// Represents component that provides encryption and decryption of files using <typeparamref name="TCryptoKey"/> key.
    /// </summary>
    /// <typeparam name="TCryptoKey">The type of the <see cref="CryptoKey"/>.</typeparam>
    public interface IFileCryptography<TCryptoKey> : IFileCryptography, ICryptography<TCryptoKey>
        where TCryptoKey : CryptoKey
    {
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
        Task EncryptAsync(string sourceFile, string destinationFile, TCryptoKey key, bool overwriteDestination = false, CancellationToken cancellationToken = default);

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
        Task DecryptAsync(string sourceFile, string destinationFile, TCryptoKey key, bool overwriteDestination = false, CancellationToken cancellationToken = default);
    }
}
