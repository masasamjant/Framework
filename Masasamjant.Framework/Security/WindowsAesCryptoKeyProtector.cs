using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    /// <summary>
    /// Provides methods to protect and unprotect <see cref="AesCryptoKey"/> using Windows Data Protection API (DPAPI).
    /// </summary>
    /// <remarks>This class is only supported on Windows platforms.</remarks>
    [SupportedOSPlatform("windows")]
    public static class WindowsAesCryptoKeyProtector
    {
        /// <summary>
        /// Protects the specified <see cref="AesCryptoKey"/> using Windows Data Protection API (DPAPI) with the specified scope.
        /// </summary>
        /// <param name="key">The <see cref="AesCryptoKey"/> to protect.</param>
        /// <param name="scope">The <see cref="DataProtectionScope"/>.</param>
        /// <returns>A byte array containing the protected data.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="key"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="scope"/> is not defined.</exception>
        /// <exception cref="PlatformNotSupportedException">If the current platform is not Windows.</exception>"
        public static byte[] Protect(AesCryptoKey key, DataProtectionScope scope)
        {
            PlatformHelper.EnsureIsWindows();
            ArgumentNullException.ThrowIfNull(key);
            var cryptography = new WindowsDataCryptography(scope);
            var data = key.Key;
            return cryptography.EncryptData(data);
        }

        /// <summary>
        /// Unprotects the specified byte array containing protected data and returns the original <see cref="AesCryptoKey"/> using Windows Data Protection API (DPAPI) with the specified scope.
        /// </summary>
        /// <param name="data">The byte array containing the protected data.</param>
        /// <param name="scope">The <see cref="DataProtectionScope"/>.</param>
        /// <returns>A original <see cref="AesCryptoKey"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="data"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="scope"/> is not defined.</exception>
        /// <exception cref="PlatformNotSupportedException">If the current platform is not Windows.</exception>
        public static AesCryptoKey Unprotect(byte[] data, DataProtectionScope scope)
        {
            PlatformHelper.EnsureIsWindows();
            ArgumentNullException.ThrowIfNull(data);
            var cryptography = new WindowsDataCryptography(scope);
            var key = cryptography.DecryptData(data);
            return new AesCryptoKey(key);
        }
    }
}
