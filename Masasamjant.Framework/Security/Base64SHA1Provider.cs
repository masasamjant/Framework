using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="Base64StringHashProvider"/> that computes Base-64 string from SHA1 hash of specified string.
    /// </summary>
    public sealed class Base64SHA1Provider : Base64StringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Base64SHA1Provider"/> class.
        /// </summary>
        public Base64SHA1Provider()
            : base(new SHA1HashProvider())
        { }
    }
}
