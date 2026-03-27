using System.Security.Cryptography;

namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Reprsents abstract <see cref="IPasswordSecurity"/> that use PBKDF2 algorithm to create and verify password hashes.
    /// </summary>
    public abstract class Pbkdf2PasswordSecurity : IPasswordSecurity
    {
        private const char Separator = ':';

        /// <summary>
        /// Intializes new instance of the <see cref="Pbkdf2PasswordSecurity"/> class.
        /// </summary>
        /// <param name="algorithm">The hash algorithm name,</param>
        /// <param name="iterations">The iterations.</param>
        /// <param name="minSecretLength">The minimum secret length.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="minSecretLength"/> is less than 16.</exception>
        protected Pbkdf2PasswordSecurity(HashAlgorithmName algorithm, int iterations, int minSecretLength)
        {
            if (minSecretLength < 16)
                throw new ArgumentOutOfRangeException(nameof(minSecretLength), minSecretLength, "Minimum secret length must be at least 16 characters.");

            Algorithm = algorithm;
            Iterations = Math.Max(MinIterations, iterations);
            MinSecretLength = minSecretLength;
        }

        /// <summary>
        /// Gets the hash algorithm name.
        /// </summary>
        protected HashAlgorithmName Algorithm { get; }

        /// <summary>
        /// Gets the calculation iterations.
        /// </summary>
        protected int Iterations { get; }

        /// <summary>
        /// Gets the minimum iterations for password hashing.
        /// </summary>
        protected abstract int MinIterations { get; }

        /// <summary>
        /// Gets the size of salt.
        /// </summary>
        protected abstract int SaltSize { get; }

        /// <summary>
        /// Gets the size of output.
        /// </summary>
        protected abstract int KeySize { get; }

        /// <summary>
        /// Gets the minimum required length for a secret value.
        /// </summary>
        protected int MinSecretLength { get; }

        /// <summary>
        /// Compute hash from password using specified secret value.
        /// </summary>
        /// <param name="password">The password value.</param>
        /// <param name="secret">The secret to strengthen the password.</param>
        /// <returns>A password hash.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="password"/> or <paramref name="secret"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="password"/> is equal to <paramref name="secret"/>.</exception>
        public string HashPassword(string password, string secret)
        {
            ValidateParameters(password, secret);
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            string secretPassword = password + secret;
            byte[] hash = Rfc2898DeriveBytes.Pbkdf2(secretPassword, salt,Iterations, Algorithm, KeySize);
            return $"{Convert.ToBase64String(salt)}{Separator}{Convert.ToBase64String(hash)}";
        }

        /// <summary>
        /// Verify password hash.
        /// </summary>
        /// <param name="password">The password value.</param>
        /// <param name="secret">The secret to strengthen the password.</param>
        /// <param name="hash">The password hash to verify against.</param>
        /// <returns><c>true</c> if the password is valid; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="password"/>, <paramref name="secret"/> or <paramref name="hash"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If any value is equal to other.</exception>
        public bool VerifyPassword(string password, string secret, string hash)
        {
            ValidateParameters(password, secret, hash);

            var parts = hash.Split(Separator);

            if (parts.Length != 2)
                return false;

            string secretPassword = password + secret;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] originalHash = Convert.FromBase64String(parts[1]);
            byte[] currentHash = Rfc2898DeriveBytes.Pbkdf2(secretPassword, salt, Iterations, Algorithm, KeySize);
            return CryptographicOperations.FixedTimeEquals(originalHash, currentHash);
        }

        private void ValidateParameters(string password, string secret)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentNullException(nameof(password), "Password is null, empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(secret))
                throw new ArgumentNullException(nameof(secret), "Secret is null, empty or only whitespace.");

            if (secret.Length < MinSecretLength)
                throw new ArgumentException($"Secret must be at least {MinSecretLength} characters long.", nameof(secret));

            if (string.Equals(password, secret, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Password cannot be equal to secret.", nameof(password));
        }

        private void ValidateParameters(string password, string secret, string hash)
        {
            ValidateParameters(password, secret);

            if (string.Equals(password, hash, StringComparison.OrdinalIgnoreCase) || string.Equals(secret, hash, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("Hash cannot be equal to password or secret.", nameof(hash));
        }
    }
}
