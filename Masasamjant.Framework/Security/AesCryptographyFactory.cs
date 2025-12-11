using Masasamjant.Security.Abstractions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Masasamjant.Security
{
    public sealed class AesCryptographyFactory : ICryptographyFactory<AesCryptoKey>, ICryptographyFactory
    {
        private readonly HashAlgorithmName? hashAlgorithmName;
        private readonly int? iterations;

        public AesCryptographyFactory(HashAlgorithmName? hashAlgorithmName = null, int? iterations = null)
        {
            this.hashAlgorithmName = hashAlgorithmName;
            this.iterations = iterations;
        }

        public CryptoKeyExport<AesCryptoKey> CreateCryptoKeyExport()
        {
            return new AesCryptoKeyExport();
        }

        public CryptoKeyImport<AesCryptoKey> CreateCryptoKeyImport()
        {
            return new AesCryptoKeyImport();
        }

        public IDataCryptography<AesCryptoKey> CreateDataCryptography()
        {
            return new AesDataCryptography(hashAlgorithmName, iterations);
        }

        public IFileCryptography<AesCryptoKey> CreateFileCryptography()
        {
            return new AesFileCryptography(hashAlgorithmName, iterations);
        }

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
