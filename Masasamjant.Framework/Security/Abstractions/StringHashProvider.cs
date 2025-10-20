namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents abstract <see cref="IStringHashProvider"/>.
    /// </summary>
    public abstract class StringHashProvider : IStringHashProvider
    {
        private readonly IHashProvider hashProvider;

        /// <summary>
        /// Initializes new instance of the <see cref="StringHashProvider"/> class.
        /// </summary>
        /// <param name="hashProvider">The <see cref="IHashProvider"/>.</param>
        protected StringHashProvider(IHashProvider hashProvider)
        {
            this.hashProvider = hashProvider;
        }

        /// <summary>
        /// Gets the name of algorithm.
        /// </summary>
        public string Algorithm
        {
            get { return hashProvider.Algorithm; }
        }

        /// <summary>
        /// Create string hash with algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        public string CreateHash(string value)
        {
            var bytes = value.GetByteArray();
            if (bytes.Length == 0)
                return string.Empty;
            var hash = hashProvider.HashData(bytes);
            return EncodeHash(hash);
        }

        /// <summary>
        /// Create string hash with algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        public async Task<string> CreateHashAsync(string value)
        {
            var bytes = value.GetByteArray();
            if (bytes.Length == 0)
                return string.Empty;
            var hash = await hashProvider.HashDataAsync(bytes);
            return EncodeHash(hash);
        }

        /// <summary>
        /// Encode hash bytes to string.
        /// </summary>
        /// <param name="hash">A hash bytes.</param>
        /// <returns>A hash string.</returns>
        protected abstract string EncodeHash(byte[] hash);
    }
}
