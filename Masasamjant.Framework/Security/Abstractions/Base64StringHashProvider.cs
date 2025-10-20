namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Represents <see cref="StringHashProvider"/> that computes Base-64 string.
    /// </summary>
    public abstract class Base64StringHashProvider : StringHashProvider
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Base64StringHashProvider"/> class.
        /// </summary>
        /// <param name="hashProvider">The <see cref="IHashProvider"/>.</param>
        protected Base64StringHashProvider(IHashProvider hashProvider)
            : base(hashProvider)
        { }

        /// <summary>
        /// Encode hash bytes to base-64 string.
        /// </summary>
        /// <param name="hash">A hash bytes.</param>
        /// <returns>A base-64 hash string.</returns>
        protected override string EncodeHash(byte[] hash)
        {
            return Convert.ToBase64String(hash);
        }
    }
}
