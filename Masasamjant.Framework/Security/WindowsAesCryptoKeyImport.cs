using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// <see cref="AesCryptoKeyImport"/> that use local machine Windows Data Protection API (DPAPI) to import the <see cref="AesCryptoKey"/>.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed class WindowsAesCryptoKeyImport : AesCryptoKeyImport
    {
        /// <summary>
        /// Initializes new instance of the <see cref="WindowsAesCryptoKeyImport"/> class.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">If the current platform is not Windows.</exception>
        public WindowsAesCryptoKeyImport()
        {
            PlatformHelper.EnsureIsWindows();
        }

        protected override AesCryptoKey CreateCryptoKeyFromImportData(byte[] data)
        {
            return WindowsAesCryptoKeyProtector.Unprotect(data, DataProtectionScope.LocalMachine);
        }
    }
}
