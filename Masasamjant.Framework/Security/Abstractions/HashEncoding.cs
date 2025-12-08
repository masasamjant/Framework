namespace Masasamjant.Security.Abstractions
{
    /// <summary>
    /// Defines how hash value is encoded.
    /// </summary>
    public enum HashEncoding : int
    {
        /// <summary>
        /// Base64 encoding.
        /// </summary>
        Base64 = 0,

        /// <summary>
        /// Hexadecimal encoding.
        /// </summary>
        Hex = 1
    }
}
