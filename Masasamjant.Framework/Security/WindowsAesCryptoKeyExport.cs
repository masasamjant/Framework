using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// <see cref="AesCryptoKeyExport"/> that use local machine Windows Data Protection API (DPAPI) to protect exported <see cref="AesCryptoKey"/>.
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed class WindowsAesCryptoKeyExport : AesCryptoKeyExport
    {
        /// <summary>
        /// Initializes new instance of the <see cref="WindowsAesCryptoKeyExport"/> class.
        /// </summary>
        /// <exception cref="PlatformNotSupportedException">If the current platform is not Windows.</exception>
        public WindowsAesCryptoKeyExport()
        {
            PlatformHelper.EnsureIsWindows();
        }

        protected override byte[] GetExportData(AesCryptoKey key)
        {
            return WindowsAesCryptoKeyProtector.Protect(key, DataProtectionScope.LocalMachine);
        }
    }
}
