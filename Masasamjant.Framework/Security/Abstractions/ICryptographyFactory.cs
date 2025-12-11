using System;
using System.Collections.Generic;
using System.Text;

namespace Masasamjant.Security.Abstractions
{
    public interface ICryptographyFactory
    {
        IDataCryptography CreateDataCryptography();

        IFileCryptography CreateFileCryptography();

        IStreamCryptography CreateStreamCryptography();
    }

    public interface ICryptographyFactory<TCryptoKey> where TCryptoKey : CryptoKey
    {
        IDataCryptography<TCryptoKey> CreateDataCryptography();

        IFileCryptography<TCryptoKey> CreateFileCryptography();

        IStreamCryptography<TCryptoKey> CreateStreamCryptography();

        CryptoKeyExport<TCryptoKey> CreateCryptoKeyExport();

        CryptoKeyImport<TCryptoKey> CreateCryptoKeyImport();
    }
}
