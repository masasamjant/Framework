namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents <see cref="StringHashProvider"/> that computes hexadecimal lower-case string.
    /// </summary>
    public abstract class HexStringHashProvider : StringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="HexStringHashProvider"/> class.
        /// </summary>
        /// <param name="hashProvider">The <see cref="IHashProvider"/>.</param>
        protected HexStringHashProvider(IHashProvider hashProvider)
            : base(hashProvider)
        { }

        /// <summary>
        /// Encode hash bytes to lower-case hexadecimal string.
        /// </summary>
        /// <param name="hash">A hash bytes.</param>
        /// <returns>A lower-case hexadecimal hash string.</returns>
        protected override string EncodeHash(byte[] hash)
        {
            return Convert.ToHexStringLower(hash);
        }
    }
}
