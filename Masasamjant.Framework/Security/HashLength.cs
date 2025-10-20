namespace Masasamjant.Security
{
    /// <summary>
    /// Provides constant hash lengths.
    /// </summary>
    public static class HashLength
    {
        /// <summary>
        /// SHA1 hash length in bytes.
        /// </summary>
        public const int SHA1BytesLength = 20;

        /// <summary>
        /// SHA1 hash length when encoded to base-64 string.
        /// </summary>
        public const int SHA1Base64Length = 28;

        /// <summary>
        /// SHA1 hash length when encoded to hexadecimal string.
        /// </summary>
        public const int SHA1HexLength = 40;

        /// <summary>
        /// SHA256 hash length in bytes.
        /// </summary>
        public const int SHA256BytesLength = 32;

        /// <summary>
        /// SHA256 hash length when encoded to base-64 string.
        /// </summary>
        public const int SHA265Base64Length = 44;

        /// <summary>
        /// SHA256 hash length when encoded to hexadecimal string.
        /// </summary>
        public const int SHA256HexLength = 64;

        /// <summary>
        /// SHA384 hash length in bytes.
        /// </summary>
        public const int SHA384BytesLength = 48;

        /// <summary>
        /// SHA384 hash length when encoded to base-64 string.
        /// </summary>
        public const int SHA384Base64Length = 64;

        /// <summary>
        /// SHA384 hash length when encoded to hexadecimal string.
        /// </summary>
        public const int SHA384HexLength = 96;

        /// <summary>
        /// SHA512 hash length in bytes.
        /// </summary>
        public const int SHA512BytesLength = 64;

        /// <summary>
        /// SHA512 hash length when encoded to base-64 string.
        /// </summary>
        public const int SHA512Base64Length = 88;

        /// <summary>
        /// SHA512 hash length when encoded to hexadecimal string.
        /// </summary>
        public const int SHA512HexLength = 128;
    }
}
