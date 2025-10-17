using Masasamjant.Security.Abstractions;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Provides encryption and decryption of streams using AES algorithm.
    /// </summary>
    public sealed class AesStreamCryptography : IStreamCryptography, IStreamCryptography<AesCryptoKey>
    {
        private static readonly HashAlgorithmName DefaultHashAlgorithmName = HashAlgorithmName.SHA384;
        private static readonly int DefaultIterations = 1000000;
        private static readonly int MinIterations = 1000;

        private readonly HashAlgorithmName hashAlgorithmName;
        private readonly int iterations;

        /// <summary>
        /// Initializes new instance of the <see cref="AesStreamCryptography"/> class.
        /// </summary>
        /// <param name="hashAlgorithmName">The hash algorithm name. If <c>null</c>, the use default of SHA384.</param>
        /// <param name="iterations">The number of iterations. If <c>null</c>, then use default value of one million iterations.</param>
        /// <remarks>If value of <paramref name="iterations"/> is less than 1000, then 1000 iterations is used.</remarks>
        public AesStreamCryptography(HashAlgorithmName? hashAlgorithmName = null, int? iterations = null)
        {
            this.hashAlgorithmName = hashAlgorithmName.GetValueOrDefault(DefaultHashAlgorithmName);
            this.iterations = Math.Max(MinIterations, iterations.GetValueOrDefault(DefaultIterations));
        }

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
        public async Task EncryptAsync(Stream sourceStream, Stream destinationStream, string password, Salt salt, CancellationToken cancellationToken = default)
        {
            await EncryptAsync(sourceStream, destinationStream, CreateCryptoKey(password, salt), cancellationToken);
        }

        /// <summary>
        /// Encrypt data from specified source stream to specified destination stream.
        /// </summary>
        /// <param name="sourceStream">The source stream containing data to encrypt.</param>
        /// <param name="destinationStream">The destination stream to contain encrypted data.</param>
        /// <param name="key">The crypto key.</param>
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
        /// <exception cref="InvalidOperationException">If stream encryption fails.</exception>
        public async Task EncryptAsync(Stream sourceStream, Stream destinationStream, AesCryptoKey key, CancellationToken cancellationToken = default)
        {
            ValidateStreams(sourceStream, destinationStream);

            try
            {
                using (var aes = Aes.Create())
                using (var encryptor = aes.CreateEncryptor(key.Key, key.IV))
                {
                    CryptoStream? cs = null;

                    try
                    {
                        cs = new CryptoStream(destinationStream, encryptor, CryptoStreamMode.Write);
                        var buffer = new byte[4096];
                        var read = 0;

                        while ((read = await sourceStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                        {
                            await cs.WriteAsync(buffer, 0, read, cancellationToken);
                        }

                        await cs.FlushFinalBlockAsync(cancellationToken);
                    }
                    finally
                    {
                        if (cs != null)
                            await cs.DisposeAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Stream encryption failed. See inner exception for details.", exception);
            }
        }

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
        /// <exception cref="InvalidOperationException">If stream decryption fails.</exception>
        public async Task DecryptAsync(Stream sourceStream, Stream destinationStream, string password, Salt salt, CancellationToken cancellationToken = default)
        {
            await DecryptAsync(sourceStream, destinationStream, CreateCryptoKey(password, salt), cancellationToken);
        }

        /// <summary>
        /// Decrypt data from specified source stream to specified destination stream.
        /// </summary>
        /// <param name="sourceStream">The source stream containing encrypted data.</param>
        /// <param name="destinationStream">The destination stream to contain decrypted data.</param>
        /// <param name="key">The crypto key.</param>
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
        /// <exception cref="InvalidOperationException">If stream encryption fails.</exception>
        public async Task DecryptAsync(Stream sourceStream, Stream destinationStream, AesCryptoKey key, CancellationToken cancellationToken = default)
        {
            ValidateStreams(sourceStream, destinationStream);

            try
            {
                using (var aes = Aes.Create())
                using (var decryptor = aes.CreateDecryptor(key.Key, key.IV))
                {
                    CryptoStream? cs = null;

                    try
                    {
                        cs = new CryptoStream(sourceStream, decryptor, CryptoStreamMode.Read);
                        var buffer = new byte[4096];
                        var read = 0;

                        while ((read = await cs.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                        {
                            await destinationStream.WriteAsync(buffer, 0, read, cancellationToken);
                        }

                        await destinationStream.FlushAsync(cancellationToken);
                    }
                    finally
                    {
                        if (cs != null)
                            await cs.DisposeAsync();
                    }
                }
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("Stream decryption failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Creates new <see cref="AesCryptoKey"/> instance from specified password and salt.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="salt">The salt.</param>
        /// <returns>A <see cref="AesCryptoKey"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="password"/> is empty or only whitespace.</exception>
        public AesCryptoKey CreateCryptoKey(string password, Salt salt) => new(password, salt, iterations, hashAlgorithmName);

        private static void ValidateStreams(Stream sourceStream, Stream destinationStream) 
        {
            if (ReferenceEquals(sourceStream, destinationStream))
                throw new ArgumentException("The destination stream is same as source stream.", nameof(destinationStream));

            if (sourceStream is CryptoStream)
                throw new ArgumentException("The source stream is cryptography stream.", nameof(sourceStream));

            if (destinationStream is CryptoStream)
                throw new ArgumentException("The destination stream is cryptography stream.", nameof(destinationStream));

            if (!sourceStream.CanRead)
                throw new ArgumentException("The source stream is not readable.", nameof(sourceStream));

            if (!destinationStream.CanWrite)
                throw new ArgumentException("The destination stream is not writable.", nameof(destinationStream));
        }
    }
}
