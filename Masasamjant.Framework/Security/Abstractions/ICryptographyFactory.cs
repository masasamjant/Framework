namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents factory of cryptography components. All created implementations must
    /// use same cryptography algorithm.
    /// </summary>
    public interface ICryptographyFactory
    {
        /// <summary>
        /// Creates new instance of the <see cref="IDataCryptography"/> impelementation.
        /// </summary>
        /// <returns>A <see cref="IDataCryptography"/> instance.</returns>
        IDataCryptography CreateDataCryptography();

        /// <summary>
        /// Creates new instance of the <see cref="IFileCryptography"/> implementation.
        /// </summary>
        /// <returns>A <see cref="IFileCryptography"/> instance.</returns>
        IFileCryptography CreateFileCryptography();

        /// <summary>
        /// Creates new instance of the <see cref="IStreamCryptography"/> implementation.
        /// </summary>
        /// <returns>A <see cref="IStreamCryptography"/> instance.</returns>
        IStreamCryptography CreateStreamCryptography();
    }

    /// <summary>
    /// Represents factory of cryptography components for <typeparamref name="TCryptoKey"/> cryptography key.
    /// </summary>
    /// <typeparam name="TCryptoKey">The type of the cryptography key.</typeparam>
    public interface ICryptographyFactory<TCryptoKey> where TCryptoKey : CryptoKey
    {
        /// <summary>
        /// Creates new instance of the <see cref="IDataCryptography{TCryptoKey}"/> impelementation.
        /// </summary>
        /// <returns>A <see cref="IDataCryptography{TCryptoKey}"/> instance.</returns>
        IDataCryptography<TCryptoKey> CreateDataCryptography();

        /// <summary>
        /// Creates new instance of the <see cref="IFileCryptography{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="IFileCryptography{TCryptoKey}"/> instance.</returns>
        IFileCryptography<TCryptoKey> CreateFileCryptography();

        /// <summary>
        /// Creates new instance of the <see cref="IStreamCryptography{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="IStreamCryptography{TCryptoKey}"/> instance.</returns>
        IStreamCryptography<TCryptoKey> CreateStreamCryptography();

        /// <summary>
        /// Creates new instance of the <see cref="ICryptoKeyExport{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="ICryptoKeyExport{TCryptoKey}"/> instance.</returns>
        /// <remarks>Returned instance export key so that <see cref="ICryptoKeyImport{TCryptoKey}"/> obtained from <see cref="CreateCryptoKeyImport"/> can import it.</remarks>
        ICryptoKeyExport<TCryptoKey> CreateCryptoKeyExport();

        /// <summary>
        /// Creates new instance of the <see cref="ICryptoKeyImport{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="ICryptoKeyImport{TCryptoKey}"/> instance.</returns>
        /// <remarks>Returned instance import key exported from <see cref="ICryptoKeyExport{TCryptoKey}"/> obtained from <see cref="CreateCryptoKeyExport"/>.</remarks>
        ICryptoKeyImport<TCryptoKey> CreateCryptoKeyImport();
    }
}
