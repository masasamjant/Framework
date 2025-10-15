namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents factory that creates <see cref="Base64StringHashProvider"/> instances.
    /// </summary>
    public interface IBase64StringHashProviderFactory
    {
        /// <summary>
        /// Creates <see cref="Base64StringHashProvider"/> instance for specified hash algorithm.
        /// </summary>
        /// <param name="algorithm">The name of algorithm.</param>
        /// <returns>A <see cref="Base64StringHashProvider"/>.</returns>
        /// <exception cref="NotSupportedException">If value of <paramref name="algorithm"/> is name of not supported algorithm.</exception>
        Base64StringHashProvider CreateBase64StringHashProvider(string algorithm);
    }
}
