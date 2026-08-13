namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Provides password security by creating and verifying password hashes.
    /// </summary>
    public interface IPasswordSecurity
    {
        /// <summary>
        /// Compute hash from password using specified secret value.
        /// </summary>
        /// <param name="password">The password value.</param>
        /// <param name="secret">The secret to strengthen the password.</param>
        /// <returns>A password hash.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="password"/> or <paramref name="secret"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="password"/> is equal to <paramref name="secret"/>.</exception>
        string HashPassword(string password, string secret);

        /// <summary>
        /// Verify password hash.
        /// </summary>
        /// <param name="password">The password value.</param>
        /// <param name="secret">The secret to strengthen the password.</param>
        /// <param name="hash">The password hash to verify against.</param>
        /// <returns><c>true</c> if the password is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="password"/>, <paramref name="secret"/> or <paramref name="hash"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If any value is equal to other.</exception>
        bool VerifyPassword(string password, string secret, string hash);
    }
}
