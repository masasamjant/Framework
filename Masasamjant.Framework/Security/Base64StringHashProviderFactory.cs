using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents factory that creates <see cref="Base64StringHashProvider"/> instances from implementations in this framework.
    /// </summary>
    public sealed class Base64StringHashProviderFactory : IBase64StringHashProviderFactory
    {
        /// <summary>
        /// Creates <see cref="Base64StringHashProvider"/> instance for specified hash algorithm.
        /// </summary>
        /// <param name="algorithm">The name of algorithm.</param>
        /// <returns>A <see cref="Base64StringHashProvider"/>.</returns>
        /// <exception cref="NotSupportedException">If value of <paramref name="algorithm"/> is name of not supported algorithm.</exception>
        public Base64StringHashProvider CreateBase64StringHashProvider(string algorithm)
        {
            return algorithm.ToUpperInvariant() switch
            {
                HashAlgorithms.SHA1 => new Base64SHA1Provider(),
                HashAlgorithms.SHA256 => new Base64SHA256Provider(),
                HashAlgorithms.SHA384 => new Base64SHA384Provider(),
                HashAlgorithms.SHA512 => new Base64SHA512Provider(),
                _ => throw new NotSupportedException($"Hash algorithm '{algorithm}' is not supported.")
            };
        }
    }
}
