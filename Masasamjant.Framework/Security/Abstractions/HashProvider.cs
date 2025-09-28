namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents abstract <see cref="IHashProvider"/>.
    /// </summary>
    public abstract class HashProvider : IHashProvider
    {
        /// <summary>
        /// Gets the name of implemented algorithm like 'SHA-1'.
        /// </summary>
        public abstract string Algorithm { get; }

        /// <summary>
        /// Hash data using algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>A hashed data.</returns>
        public abstract byte[] HashData(byte[] data);

        /// <summary>
        /// Hash data from byte array using algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>A task that, when completed, contains hashed data.</returns>
        public async Task<byte[]> HashDataAsync(byte[] data)
        {
            if (data.Length == 0)
                return [];

            var source = new MemoryStream(data);
            return await HashDataAsync(source);
        }

        /// <summary>
        /// Hash data from stream using algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="source">The data to hash.</param>
        /// <returns>A task that, when completed, contains hashed data.</returns>
        public abstract Task<byte[]> HashDataAsync(Stream source);
    }
}
