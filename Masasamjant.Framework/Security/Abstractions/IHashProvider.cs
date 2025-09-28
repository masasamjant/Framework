namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents provides that creates cryptography hash.
    /// </summary>
    public interface IHashProvider
    {
        /// <summary>
        /// Gets the name of implemented algorithm like 'SHA-1'.
        /// </summary>
        string Algorithm { get; }

        /// <summary>
        /// Hash data using algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>A hashed data.</returns>
        byte[] HashData(byte[] data);

        /// <summary>
        /// Hash data from byte array using algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="data">The data to hash.</param>
        /// <returns>A task that, when completed, contains hashed data.</returns>
        Task<byte[]> HashDataAsync(byte[] data);

        /// <summary>
        /// Hash data from stream using algorithm specified by <see cref="Algorithm"/>.
        /// </summary>
        /// <param name="source">The data to hash.</param>
        /// <returns>A task that, when completed, contains hashed data.</returns>
        Task<byte[]> HashDataAsync(Stream source);
    }
}
