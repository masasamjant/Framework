using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents factory that creates <see cref="HexStringHashProvider"/> instances.
    /// </summary>
    public sealed class HexStringHashProviderFactory : IHexStringHashProviderFactory
    {
        /// <summary>
        /// Creates <see cref="HexStringHashProvider"/> instance for specified hash algorithm.
        /// </summary>
        /// <param name="algorithm">The name of algorithm.</param>
        /// <returns>A <see cref="HexStringHashProvider"/>.</returns>
        /// <exception cref="NotSupportedException">If value of <paramref name="algorithm"/> is name of not supported algorithm.</exception>
        public HexStringHashProvider CreateHexStringHashProvider(string algorithm)
        {
            return algorithm.ToUpperInvariant() switch
            {
                HashAlgorithms.SHA1 => new HexSHA1Provider(),
                HashAlgorithms.SHA256 => new HexSHA256Provider(),
                HashAlgorithms.SHA384 => new HexSHA384Provider(),
                HashAlgorithms.SHA512 => new HexSHA512Provider(),
                _ => throw new NotSupportedException($"Hash algorithm '{algorithm}' is not supported.")
            };
        }
    }
}
