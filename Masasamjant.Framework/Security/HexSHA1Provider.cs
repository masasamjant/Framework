using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="HexStringHashProvider"/> that computes hexadecimal string from SHA1 hash of specified string.
    /// </summary>
    public sealed class HexSHA1Provider : HexStringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="HexSHA1Provider"/> class.
        /// </summary>
        public HexSHA1Provider()
            : base(new SHA1HashProvider())
        { }
    }
}
