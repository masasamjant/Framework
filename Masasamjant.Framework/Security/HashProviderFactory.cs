using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents factory that creates instances of <see cref="IHashProvider"/> impelementations in this framework.
    /// </summary>
    public sealed class HashProviderFactory : IHashProviderFactory
    {
        /// <summary>
        /// Creates <see cref="IHashProvider"/> instance for specified hash algorithm.
        /// </summary>
        /// <param name="algorithm">The name of algorithm.</param>
        /// <returns>A <see cref="IHashProvider"/>.</returns>
        /// <exception cref="NotSupportedException">If value of <paramref name="algorithm"/> is name of not supported algorithm.</exception>
        public IHashProvider CreateHashProvider(string algorithm)
        {
            return algorithm.ToUpperInvariant() switch
            {
                HashAlgorithms.SHA1 => new SHA1HashProvider(),
                HashAlgorithms.SHA256 => new SHA256HashProvider(),
                HashAlgorithms.SHA384 => new SHA384HashProvider(),
                HashAlgorithms.SHA512 => new SHA512HashProvider(),
                _ => throw new NotSupportedException($"Hash algorithm '{algorithm}' is not supported.")
            };
        }
    }
}
