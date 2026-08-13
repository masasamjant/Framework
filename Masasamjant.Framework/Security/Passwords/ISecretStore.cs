namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents storage of the secrets used for password hashing and verification.
    /// </summary>
    public interface ISecretStore
    {
        /// <summary>
        /// Gets the secret for specified application in specified environment.
        /// </summary>
        /// <param name="application">The application name.</param>
        /// <param name="environment">The password environment.</param>
        /// <returns>A secret or <c>null</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="application"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="environment"/> is not defined.</exception>
        /// <exception cref="InvalidOperationException">If exception occurs when getting secret.</exception>
        Task<string?> GetSecretAsync(string application, PasswordEnvironment environment);

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
        Task StoreSecretAsync(string application, PasswordEnvironment environment, string secret, bool overwrite);
    }
}
