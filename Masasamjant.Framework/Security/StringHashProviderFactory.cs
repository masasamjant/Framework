using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents factory to create instance of <see cref="IStringHashProvider"/> implementation.
    /// </summary>
    public sealed class StringHashProviderFactory : IStringHashProviderFactory
    {
        private readonly IBase64StringHashProviderFactory base64StringHashProviderFactory;
        private readonly IHexStringHashProviderFactory hexStringHashProviderFactory;

        /// <summary>
        /// Initializes new default instance of the <see cref="StringHashProviderFactory"/> class.
        /// </summary>
        public StringHashProviderFactory()
            : this(new Base64StringHashProviderFactory(), new HexStringHashProviderFactory())
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="StringHashProviderFactory"/> class.
        /// </summary>
        /// <param name="base64StringHashProviderFactory">The <see cref="IBase64StringHashProviderFactory"/>.</param>
        /// <param name="hexStringHashProviderFactory">The <see cref="IHexStringHashProviderFactory"/>.</param>
        public StringHashProviderFactory(IBase64StringHashProviderFactory base64StringHashProviderFactory, IHexStringHashProviderFactory hexStringHashProviderFactory)
        {
            this.base64StringHashProviderFactory = base64StringHashProviderFactory;
            this.hexStringHashProviderFactory = hexStringHashProviderFactory;
        }

        /// <summary>
        /// Creates instance of <see cref="IStringHashProvider"/> implementation for specified algorithm and hash encoding.
        /// </summary>
        /// <param name="algorithm">The hash algorithm</param>
        /// <param name="encoding">The hash encoding.</param>
        /// <returns>A <see cref="IStringHashProvider"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="algorithm"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="encoding"/> is not defined.</exception>
        /// <exception cref="NotSupportedException">If algorithm specified by <paramref name="algorithm"/> is not supported.</exception>
        public IStringHashProvider CreateStringHashProvider(string algorithm, HashEncoding encoding)
        {
            if (string.IsNullOrWhiteSpace(algorithm))
                throw new ArgumentNullException(nameof(algorithm), "The algorithm is empty or only whitespace.");

            return encoding switch
            {
                HashEncoding.Base64 => base64StringHashProviderFactory.CreateBase64StringHashProvider(algorithm),
                HashEncoding.Hex => hexStringHashProviderFactory.CreateHexStringHashProvider(algorithm),
                _ => throw new ArgumentException("The value is not defined.", nameof(encoding))
            };
        }
    }
}
