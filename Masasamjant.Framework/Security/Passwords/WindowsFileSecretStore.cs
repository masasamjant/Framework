using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;

namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Windows file storage of the secrets used for password hashing and verification secured with Windows Data Protection API (DPAPI).
    /// </summary>
    [SupportedOSPlatform("windows")]
    public sealed class WindowsFileSecretStore : FileSecretStore
    {
        private readonly DataProtectionScope scope;

        /// <summary>
        /// Initializes new instance of the <see cref="WindowsFileSecretStore"/> class with specified data protection scope and default store directory.
        /// </summary>
        /// <param name="scope">The data protection scope.</param>
        /// <exception cref="ArgumentException">If <paramref name="scope"/> is not defined.</exception>
        /// <exception cref="PlatformNotSupportedException">If not executed in Windows operating system.</exception>
        /// <remarks>The default directory is "[Local Application Data]\Masasamjant\Framework\Store".</remarks>
        public WindowsFileSecretStore(DataProtectionScope scope)
            : this(scope, Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Masasamjant", "Framework", "Store"))
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="WindowsFileSecretStore"/> class with specified data protection scope and store directory.
        /// </summary>
        /// <param name="scope">The data protection scope.</param>
        /// <param name="storeDirectory">The directory so store secret file.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="storeDirectory"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="scope"/> is not defined.</exception>
        /// <exception cref="PlatformNotSupportedException">If not executed in Windows operating system.</exception>
        public WindowsFileSecretStore(DataProtectionScope scope, string storeDirectory)
            : base(storeDirectory)
        {
            PlatformHelper.EnsureIsWindows();

            if (!Enum.IsDefined(scope))
                throw new ArgumentException("The value is not defined.", nameof(scope));

            this.scope = scope;
        }

        /// <summary>
        /// Gets the secret for specified application in specified environment.
        /// </summary>
        /// <param name="application">The application name.</param>
        /// <param name="environment">The password environment.</param>
        /// <returns>A secret or <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="application"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="environment"/> is not defined.</exception>
        /// <exception cref="InvalidOperationException">If exception occurs when getting secret.</exception>
        public override async Task<string?> GetSecretAsync(string application, PasswordEnvironment environment)
        {
            ValidateApplicationEnvironment(application, environment);

            try
            {
                EnsureStoreDirectoryExist();

                var secretFilePath = GetSecretFilePath(application, environment);

                if (!File.Exists(secretFilePath))
                    return null;

                byte[] encrypted = await File.ReadAllBytesAsync(secretFilePath);
                byte[] decrypted = ProtectedData.Unprotect(encrypted, null, scope);

                return Encoding.UTF8.GetString(decrypted);
            }
            catch (CryptographicException exception)
            {
                throw new InvalidOperationException("Failed to decrypt the secret.", exception);
            }
            catch (IOException exception)
            {
                throw new InvalidOperationException("IO error occurred with secret store file.", exception);
            }
        }

        /// <summary>
        /// Store specified secret for specified application in specified environment. 
        /// If <paramref name="overwrite"/> is <c>false</c> and the secret already exists, an exception will be thrown.
        /// </summary>
        /// <param name="application">The application name.</param>
        /// <param name="environment">The password environment.</param>
        /// <param name="secret">The secret to store.</param>
        /// <param name="overwrite"><c>true</c> to overwrite the existing secret; otherwise, <c>false</c>.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="application"/> or <paramref name="secret"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="environment"/> is not defined.</exception>
        /// <exception cref="InvalidOperationException">If exception occurs when storing secret.</exception>
        public override async Task StoreSecretAsync(string application, PasswordEnvironment environment, string secret, bool overwrite)
        {
            ValidateApplicationEnvironment(application, environment);

            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentException("The value cannot be null, empty or whitespace.", nameof(secret));

            try
            {
                EnsureStoreDirectoryExist();

                var secretFilePath = GetSecretFilePath(application, environment);

                CheckStoreFile(secretFilePath, overwrite, application, environment);

                byte[] clear = secret.GetByteArray(Encoding.UTF8);
                byte[] encrypted = ProtectedData.Protect(clear, null, scope);
                await File.WriteAllBytesAsync(secretFilePath, encrypted);
            }
            catch (CryptographicException exception)
            {
                throw new InvalidOperationException("Failed to encrypt the secret.", exception);
            }
            catch (IOException exception)
            {
                throw new InvalidOperationException("IO error occurred with secret store file.", exception);
            }
        }
    }
}
