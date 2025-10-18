namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents service that will import <typeparamref name="TCryptoKey"/> from stream.
    /// </summary>
    /// <typeparam name="TCryptoKey">The type of the crypto key.</typeparam>
    public abstract class CryptoKeyImport<TCryptoKey> where TCryptoKey : CryptoKey  
    {
        /// <summary>
        /// Imports <typeparamref name="TCryptoKey"/> from specified stream.
        /// </summary>
        /// <param name="stream">The stream to read imported key.</param>
        /// <returns>A task representing import.</returns>
        /// <exception cref="InvalidOperationException">If import fails.</exception>
        public abstract Task<TCryptoKey> ImportAsync(Stream stream);
    }
}
