using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents <see cref="HexStringHashProvider"/> that computes hexadecimal string from SHA384 hash of specified string.
    /// </summary>
    public sealed class HexSHA384Provider : HexStringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="HexSHA384Provider"/> class.
        /// </summary>
        public HexSHA384Provider()
            : base(new SHA384HashProvider())
        { }
    }
}
