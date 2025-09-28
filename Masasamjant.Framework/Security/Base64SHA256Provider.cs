using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="Base64StringHashProvider"/> that computes Base-64 string from SHA256 hash of specified string.
    /// </summary>
    public sealed class Base64SHA256Provider : Base64StringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Base64SHA256Provider"/> class.
        /// </summary>
        public Base64SHA256Provider()
            : base(new SHA256HashProvider())
        { }
    }
}
