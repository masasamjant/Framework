namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents abstract file storage of the secrets used for password hashing and verification.
    /// </summary>
    public abstract class FileSecretStore : ISecretStore
    {
        /// <summary>
        /// Initializes new instance of the <see cref="FileSecretStore"/> class.
        /// </summary>
        /// <param name="storeDirectory">The store directory path.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="storeDirectory"/> is <c>null</c>, empty or only whitespace.</exception>
        protected FileSecretStore(string storeDirectory)
        {
            if (string.IsNullOrWhiteSpace(storeDirectory))
                throw new ArgumentNullException(nameof(storeDirectory), "The value cannot be null, empty or whitespace.");
            
            StoreDirectory = storeDirectory;
        }

        /// <summary>
        /// Gets the store directory path.
        /// </summary>
        protected string StoreDirectory { get; }

        /// <summary>
        /// Gets the secret for specified application in specified environment.
        /// </summary>
        /// <param name="application">The application name.</param>
        /// <param name="environment">The password environment.</param>
        /// <returns>A secret or <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="application"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="environment"/> is not defined.</exception>
        /// <exception cref="InvalidOperationException">If exception occurs when getting secret.</exception>
        public abstract Task<string?> GetSecretAsync(string application, PasswordEnvironment environment);

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
        public abstract Task StoreSecretAsync(string application, PasswordEnvironment environment, string secret, bool overwrite);

        /// <summary>
        /// Check if store file exist and delete it if <paramref name="overwrite"/> is <c>true</c>. 
        /// Otherwise, throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <param name="secretFilePath">The path of secrets file.</param>
        /// <param name="overwrite"><c>true</c> if can overwrite file; <c>false</c> otherwise.</param>
        /// <param name="application">The application name.</param>
        /// <param name="environment">The password environment.</param>
        /// <exception cref="InvalidOperationException">If file specified by <paramref name="secretFilePath"/> exist and <paramref name="overwrite"/> is <c>false</c>.</exception>
        protected static void CheckStoreFile(string secretFilePath, bool overwrite, string application, PasswordEnvironment environment)
        {
            if (File.Exists(secretFilePath))
            {
                if (overwrite)
                    File.Delete(secretFilePath);
                else
                    throw new InvalidOperationException($"A secret already exists for {application} in {environment} environment.");
            }
        }

        /// <summary>
        /// Ensures that <see cref="StoreDirectory"/> exist.
        /// </summary>
        protected void EnsureStoreDirectoryExist()
        {
            if (!Directory.Exists(StoreDirectory))
                Directory.CreateDirectory(StoreDirectory);
        }

        /// <summary>
        /// Gets full path to secrets file.
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="environment">The password environment.</param>
        /// <returns>A full path to secrets file.</returns>
        protected string GetSecretFilePath(string application, PasswordEnvironment environment)
        {
            var key = GetKey(application, environment);
            return Path.Combine(StoreDirectory, $"{key}.sec");
        }

        /// <summary>
        /// Validate application and password environment arguments.
        /// </summary>
        /// <param name="application">The application name.</param>
        /// <param name="environment">The password environment.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="application"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="environment"/> is not defined.</exception>
        protected static void ValidateApplicationEnvironment(string application, PasswordEnvironment environment)
        {
            if (string.IsNullOrWhiteSpace(application))
                throw new ArgumentNullException(nameof(application), "The value cannot be null, empty or whitespace.");

            if (!Enum.IsDefined(environment))
                throw new ArgumentException("The value is not defined.", nameof(environment));
        }

        private static string GetKey(string application, PasswordEnvironment environment)
            => $"{application}-{environment}-SCRT".ToUpperInvariant();
    }
}
