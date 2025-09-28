namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents abstract <see cref="IStringHashProvider"/> that computes Base-64 string.
    /// </summary>
    public abstract class Base64StringHashProvider : IStringHashProvider
    {
        private readonly IHashProvider hashProvider;

        /// <summary>
        /// Initializes new instance of the <see cref="Base64StringHashProvider"/> class.
        /// </summary>
        /// <param name="hashProvider">The <see cref="IHashProvider"/>.</param>
        protected Base64StringHashProvider(IHashProvider hashProvider)
        {
            this.hashProvider = hashProvider;
        }

        /// <summary>
        /// Gets the name of implemented algorithm like 'SHA-1'.
        /// </summary>
        public string Algorithm 
        {
            get { return hashProvider.Algorithm; } 
        }

        /// <summary>
        /// Create Base-64 string hash with algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        public string CreateHash(string value)
        {
            var bytes = value.GetByteArray();
            if (bytes.Length == 0)
                return string.Empty;
            var sha = hashProvider.HashData(bytes);
            return Convert.ToBase64String(sha);
        }

        /// <summary>
        /// Create Base-64 string hash with algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A hash string or empty, if value of <paramref name="value"/> is empty.</returns>
        public async Task<string> CreateHashAsync(string value)
        {
            var bytes = value.GetByteArray();
            if (bytes.Length == 0)
                return string.Empty;
            var sha = await hashProvider.HashDataAsync(bytes);
            return Convert.ToBase64String(sha);
        }
    }
}
