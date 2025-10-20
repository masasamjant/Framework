namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents factory to create instance of <see cref="IStringHashProvider"/> implementation.
    /// </summary>
    public interface IStringHashProviderFactory
    {
        /// <summary>
        /// Creates instance of <see cref="IStringHashProvider"/> implementation for specified algorithm and hash encoding.
        /// </summary>
        /// <param name="algorithm">The hash algorithm</param>
        /// <param name="encoding">The hash encoding.</param>
        /// <returns>A <see cref="IStringHashProvider"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="algorithm"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="encoding"/> is not defined.</exception>
        /// <exception cref="NotSupportedException">If algorithm specified by <paramref name="algorithm"/> is not supported.</exception>
        public IStringHashProvider CreateStringHashProvider(string algorithm, HashEncoding encoding);
    }
}
