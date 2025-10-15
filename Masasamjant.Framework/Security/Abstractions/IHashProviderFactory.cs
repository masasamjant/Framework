namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents factory that creates instances of <see cref="IHashProvider"/> impelementations.
    /// </summary>
    public interface IHashProviderFactory
    {
        /// <summary>
        /// Creates <see cref="IHashProvider"/> instance for specified hash algorithm.
        /// </summary>
        /// <param name="algorithm">The name of algorithm.</param>
        /// <returns>A <see cref="IHashProvider"/>.</returns>
        /// <exception cref="NotSupportedException">If value of <paramref name="algorithm"/> is name of not supported algorithm.</exception>
        IHashProvider CreateHashProvider(string algorithm);
    }
}
