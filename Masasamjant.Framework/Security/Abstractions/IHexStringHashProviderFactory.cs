namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents factory that creates <see cref="HexStringHashProvider"/> instances.
    /// </summary>
    public interface IHexStringHashProviderFactory
    {
        /// <summary>
        /// Creates <see cref="HexStringHashProvider"/> instance for specified hash algorithm.
        /// </summary>
        /// <param name="algorithm">The name of algorithm.</param>
        /// <returns>A <see cref="HexStringHashProvider"/>.</returns>
        /// <exception cref="NotSupportedException">If value of <paramref name="algorithm"/> is name of not supported algorithm.</exception>
        HexStringHashProvider CreateHexStringHashProvider(string algorithm);
    }
}
