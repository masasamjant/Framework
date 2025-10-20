using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="HexStringHashProvider"/> that computes hexadecimal string from SHA512 hash of specified string.
    /// </summary>
    public sealed class HexSHA512Provider : HexStringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="HexSHA512Provider"/> class.
        /// </summary>
        public HexSHA512Provider()
            : base(new SHA512HashProvider())
        { }
    }
}
