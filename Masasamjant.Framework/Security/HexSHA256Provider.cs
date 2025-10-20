using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="HexStringHashProvider"/> that computes hexadecimal string from SHA256 hash of specified string.
    /// </summary>
    public sealed class HexSHA256Provider : HexStringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="HexSHA256Provider"/> class.
        /// </summary>
        public HexSHA256Provider()
            : base(new SHA256HashProvider())
        { }
    }
}
