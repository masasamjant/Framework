namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents service that will export <typeparamref name="TCryptoKey"/> to stream.
    /// </summary>
    /// <typeparam name="TCryptoKey">The type of the crypto key.</typeparam>
    public abstract class CryptoKeyExport<TCryptoKey> : ICryptoKeyExport<TCryptoKey> where TCryptoKey : CryptoKey
    {
        /// <summary>
        /// Exports specified <typeparamref name="TCryptoKey"/> to specified stream.
        /// </summary>
        /// <param name="key">The crypto key.</param>
        /// <param name="stream">The stream to export key.</param>
        /// <returns>A task representing export.</returns>
        /// <remarks>It is responsibility of caller to secure data exported to stream.</remarks>
        /// <exception cref="InvalidOperationException">If export fails.</exception>
        public abstract Task ExportAsync(TCryptoKey key, Stream stream);

        /// <summary>
        /// Validate that specified stream is writable.
        /// </summary>
        /// <param name="stream">The stream to write.</param>
        /// <exception cref="ArgumentException">If <paramref name="stream"/> is not writable stream.</exception>
        protected static void ValidateCanWrite(Stream stream)
        {
            if (!stream.CanWrite)
                throw new ArgumentException("The stream is not writable.", nameof(stream));
        }
    }
}
