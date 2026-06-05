using Masasamjant.Security.Abstractions;
using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;

namespace Masasamjant.Security
{
    /// <summary>
    /// Encryption and decryption of data and strings secured with Windows Data Protection API (DPAPI).
    /// </summary>
    /// <remarks>Only available on Windows platforms.</remarks>
    [SupportedOSPlatform("windows")]
    public sealed class WindowsDataCryptography : IDataCryptography
    {
        /// <summary>
        /// Initializes new instance of <see cref="WindowsDataCryptography"/> with specified <see cref="DataProtectionScope"/>.
        /// </summary>
        /// <param name="scope">The data protection scope.</param>
        /// <exception cref="ArgumentException">If <paramref name="scope"/> is not defined.</exception>
        /// <exception cref="PlatformNotSupportedException">If not executed in Windows operating system.</exception>
        public WindowsDataCryptography(DataProtectionScope scope) 
        {
            PlatformHelper.EnsureIsWindows();

            if (!Enum.IsDefined(scope))
                throw new ArgumentException("The value is not defined.", nameof(scope));

            Scope = scope;
        }

        /// <summary>
        /// Gets the <see cref="DataProtectionScope"/> used by this instance.
        /// </summary>
        public DataProtectionScope Scope { get; }

        /// <summary>
        /// Decrypt data.
        /// </summary>
        /// <param name="cipherData">The data to decrypt.</param>
        /// <param name="entropy">The optional entropy used for additional security.</param>
        /// <returns>A decrypted data.</returns>
        public byte[] DecryptData(byte[] cipherData, byte[]? entropy = null)
        {
            if (cipherData is null || cipherData.Length == 0)
                return [];
            return ProtectedData.Unprotect(cipherData, entropy, Scope);
        }

        /// <summary>
        /// Decrypt string.
        /// </summary>
        /// <param name="cipherData">The data to decrypt.</param>
        /// <param name="encoding">The optional encoding; <see cref="Encoding.Unicode"/> by default.</param>
        /// <param name="entropy">The optional entropy user for additional security.</param>
        /// <returns>A decrypted string.</returns>
        public string DecryptString(string cipherData, Encoding? encoding = null, byte[]? entropy = null)
        {
            if (string.IsNullOrEmpty(cipherData))
                return cipherData ?? string.Empty;
            
            encoding = EnsureEncoding(encoding);
            byte[] buffer = Convert.FromBase64String(cipherData);
            byte[] clear = DecryptData(buffer, entropy);
            return encoding.GetString(clear);
        }

        /// <summary>
        /// Encrypt data.
        /// </summary>
        /// <param name="clearData">The data to encrypt.</param>
        /// <param name="entropy">The optional entropy used for additional security.</param>
        /// <returns>An encrypted data.</returns>
        public byte[] EncryptData(byte[] clearData, byte[]? entropy = null)
        {
            if (clearData is null || clearData.Length == 0)
                return [];
            return ProtectedData.Protect(clearData, entropy, Scope);
        }

        /// <summary>
        /// Encrypt string.
        /// </summary>
        /// <param name="clearData">The data to encrypt.</param>
        /// <param name="encoding">The optional encoding; <see cref="Encoding.Unicode"/> by default.</param>
        /// <param name="entropy">The optional entropy used for additional security.</param>
        /// <returns>An encrypted string.</returns>
        public string EncryptString(string clearData, Encoding? encoding = null, byte[]? entropy = null)
        {
            if (string.IsNullOrEmpty(clearData))
                return clearData ?? string.Empty;

            encoding = EnsureEncoding(encoding);
            byte[] buffer = clearData.GetByteArray(encoding);
            byte[] cipher = EncryptData(buffer, entropy);
            return Convert.ToBase64String(cipher);
        }

        private static Encoding EnsureEncoding(Encoding? encoding)
        {
            return encoding ?? Encoding.Unicode;
        }

        Task<byte[]> IDataCryptography.DecryptDataAsync(byte[] cipherData, string password, Salt salt, CancellationToken cancellationToken)
        {
            byte[] entropy = CryptographyHelper.GetPseudoRandomBytes(password, salt);
            return Task.FromResult(DecryptData(cipherData, entropy));
        }

        Task<string> IDataCryptography.DecryptStringAsync(string cipherData, string password, Salt salt, Encoding? encoding, CancellationToken cancellationToken)
        {
            byte[] entropy = CryptographyHelper.GetPseudoRandomBytes(password, salt);
            return Task.FromResult(DecryptString(cipherData, encoding, entropy));
        }

        Task<byte[]> IDataCryptography.EncryptDataAsync(byte[] clearData, string password, Salt salt, CancellationToken cancellationToken)
        {
            byte[] entropy = CryptographyHelper.GetPseudoRandomBytes(password, salt);
            return Task.FromResult(EncryptData(clearData, entropy));
        }

        Task<string> IDataCryptography.EncryptStringAsync(string clearData, string password, Salt salt, Encoding? encoding, CancellationToken cancellationToken)
        {
            byte[] entropy = CryptographyHelper.GetPseudoRandomBytes(password, salt);
            return Task.FromResult(EncryptString(clearData, encoding, entropy));
        }
    }
}
