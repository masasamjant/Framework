using System.Security.Cryptography;

namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// <see cref="Pbkdf2PasswordSecurity"/> using SHA-512 hash algorithm.
    /// </summary>
    public sealed class SHA512PasswordSecurity : Pbkdf2PasswordSecurity
    {
        /// <summary>
        /// Default iterations.
        /// </summary>
        public const int DefaultIterations = 500000;

        /// <summary>
        /// Minimum iterations.
        /// </summary>
        public const int MinimumIterations = 300000;

        private const int DataSize = 64;

        /// <summary>
        /// Initializes new instance of the <see cref="SHA512PasswordSecurity"/> class. If value of <paramref name="iterations"/> is 
        /// less than <see cref="MinimumIterations"/>, then <see cref="MinimumIterations"/> is used.
        /// </summary>
        /// <param name="iterations">The iterations. Default value is <see cref="DefaultIterations"/>.</param>
        public SHA512PasswordSecurity(int iterations = DefaultIterations) 
            : base(HashAlgorithmName.SHA512, iterations, DataSize)
        { }

        /// <summary>
        /// Gets the minimum iterations for password hashing.
        /// </summary>
        protected override int MinIterations => MinimumIterations;

        /// <summary>
        /// Gets the size of salt.
        /// </summary>
        protected override int SaltSize => DataSize;

        /// <summary>
        /// Gets the size of output.
        /// </summary>
        protected override int KeySize => DataSize;
    }
}
