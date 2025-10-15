namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents component that provides encryption and decryption of streams.
    /// </summary>
    public interface IStreamCryptography
    {
        /// <summary>
        /// Encrypt data from specified source stream to specified destination stream.
        /// </summary>
        /// <param name="sourceStream">The source stream containing data to encrypt.</param>
        /// <param name="destinationStream">The destination stream to contain encrypted data.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing encryption.</returns>
        /// <exception cref="ArgumentException">
        /// If <paramref name="destinationStream"/> is same as <paramref name="sourceStream"/>.
        /// -or-
        /// If <paramref name="sourceStream"/> or <paramref name="destinationStream"/> is crypto stream.
        /// -or-
        /// If <paramref name="sourceStream"/> is not readable.
        /// -or-
        /// If <paramref name="destinationStream"/> is not writable.
        /// </exception>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If stream encryption fails.</exception>
        Task EncryptAsync(Stream sourceStream, Stream destinationStream, string password, Salt salt, CancellationToken cancellationToken = default);

        /// <summary>
        /// Decrypt data from specified source stream to specified destination stream.
        /// </summary>
        /// <param name="sourceStream">The source stream containing encrypted data.</param>
        /// <param name="destinationStream">The destination stream to contain decrypted data.</param>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing decryption.</returns>
        /// <exception cref="ArgumentException">
        /// If <paramref name="destinationStream"/> is same as <paramref name="sourceStream"/>.
        /// -or-
        /// If <paramref name="sourceStream"/> or <paramref name="destinationStream"/> is crypto stream.
        /// -or-
        /// If <paramref name="sourceStream"/> is not readable.
        /// -or-
        /// If <paramref name="destinationStream"/> is not writable.
        /// </exception>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        /// <exception cref="InvalidOperationException">If stream encryption fails.</exception>
        Task DecryptAsync(Stream sourceStream, Stream destinationStream, string password, Salt salt, CancellationToken cancellationToken = default);
    }
}
