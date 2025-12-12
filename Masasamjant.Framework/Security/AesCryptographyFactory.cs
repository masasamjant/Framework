using Masasamjant.Security.Abstractions;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents factory of cryptography components for AES algorithm.
    /// </summary>
    public sealed class AesCryptographyFactory : ICryptographyFactory<AesCryptoKey>, ICryptographyFactory
    {
        private readonly HashAlgorithmName? hashAlgorithmName;
        private readonly int? iterations;

        /// <summary>
        /// Initializes new instance of the <see cref="AesCryptographyFactory"/> class.
        /// </summary>
        /// <param name="hashAlgorithmName">The hash algorithm name. If <c>null</c>, the use default of SHA384.</param>
        /// <param name="iterations">The number of iterations. If <c>null</c>, then use default value of one million iterations.</param>
        public AesCryptographyFactory(HashAlgorithmName? hashAlgorithmName = null, int? iterations = null)
        {
            this.hashAlgorithmName = hashAlgorithmName;
            this.iterations = iterations;
        }

        /// <summary>
        /// Creates new instance of the <see cref="ICryptoKeyExport{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="ICryptoKeyExport{TCryptoKey}"/> instance.</returns>
        /// <remarks>Returned instance export key so that <see cref="ICryptoKeyImport{TCryptoKey}"/> obtained from <see cref="CreateCryptoKeyImport"/> can import it.</remarks>
        public ICryptoKeyExport<AesCryptoKey> CreateCryptoKeyExport()
        {
            return new AesCryptoKeyExport();
        }

        /// <summary>
        /// Creates new instance of the <see cref="ICryptoKeyImport{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="ICryptoKeyImport{TCryptoKey}"/> instance.</returns>
        /// <remarks>Returned instance import key exported from <see cref="ICryptoKeyExport{TCryptoKey}"/> obtained from <see cref="CreateCryptoKeyExport"/>.</remarks>
        public ICryptoKeyImport<AesCryptoKey> CreateCryptoKeyImport()
        {
            return new AesCryptoKeyImport();
        }

        /// <summary>
        /// Creates new instance of the <see cref="IDataCryptography{TCryptoKey}"/> impelementation.
        /// </summary>
        /// <returns>A <see cref="IDataCryptography{TCryptoKey}"/> instance.</returns>
        public IDataCryptography<AesCryptoKey> CreateDataCryptography()
        {
            return new AesDataCryptography(hashAlgorithmName, iterations);
        }

        /// <summary>
        /// Creates new instance of the <see cref="IFileCryptography{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="IFileCryptography{TCryptoKey}"/> instance.</returns>
        public IFileCryptography<AesCryptoKey> CreateFileCryptography()
        {
            return new AesFileCryptography(hashAlgorithmName, iterations);
        }

        /// <summary>
        /// Creates new instance of the <see cref="IStreamCryptography{TCryptoKey}"/> implementation.
        /// </summary>
        /// <returns>A <see cref="IStreamCryptography{TCryptoKey}"/> instance.</returns>
        public IStreamCryptography<AesCryptoKey> CreateStreamCryptography()
        {
            return new AesStreamCryptography(hashAlgorithmName, iterations);
        }

        IDataCryptography ICryptographyFactory.CreateDataCryptography()
        {
            return CreateDataCryptography();
        }

        IFileCryptography ICryptographyFactory.CreateFileCryptography()
        {
            return CreateFileCryptography();
        }

        IStreamCryptography ICryptographyFactory.CreateStreamCryptography()
        {
            return CreateStreamCryptography();
        }
    }
}
