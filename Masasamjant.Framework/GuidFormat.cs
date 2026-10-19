namespace Masasamjant
{
    /// <summary>
    /// Define format strings of <see cref="Guid.ToString(string?)"/>.
    /// </summary>
    public static class GuidFormat
    {
        /// <summary>
        /// 32 digits: 
        /// 00000000000000000000000000000000
        /// </summary>
        public const string N = nameof(N);

        /// <summary>
        /// 32 digits separated by hyphens: 
        /// 00000000-0000-0000-0000-000000000000
        /// </summary>
        public const string D = nameof(D);

        /// <summary>
        /// 32 digits separated by hyphens, enclosed in braces:
        /// {00000000-0000-0000-0000-000000000000}
        /// </summary>
        public const string B = nameof(B);

        /// <summary>
        /// 32 digits separated by hyphens, enclosed in parentheses:
        /// (00000000-0000-0000-0000-000000000000)
        /// </summary>
        public const string P = nameof(P);

        /// <summary>
        /// Four hexadecimal values enclosed in braces, where the fourth value is a subset of eight hexadecimal values that is also enclosed in braces:
        /// {0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}
        /// </summary>
        public const string X = nameof(X);

        /// <summary>
        /// Check if the provided format string is a valid <see cref="Guid"/> format.
        /// </summary>
        /// <param name="format">The format string to check.</param>
        /// <returns><c>true</c> if the format string is valid; otherwise, <c>false</c>.</returns>
        public static bool IsValidFormat(string? format)
        {
            return format != null && (format == N || format == D || format == B || format == P || format == X);
        }

        /// <summary>
        /// Get an example representation of a <see cref="Guid"/> in the specified format.
        /// </summary>
        /// <param name="format">The format string.</param>
        /// <returns>An example representation of a <see cref="Guid"/> in the specified format.</returns>
        public static string GetFormatExample(string format)
        {
            return format switch
            {
                N => "00000000000000000000000000000000",
                D => "00000000-0000-0000-0000-000000000000",
                B => "{00000000-0000-0000-0000-000000000000}",
                P => "(00000000-0000-0000-0000-000000000000)",
                X => "{0x00000000,0x0000,0x0000,{0x00,0x00,0x00,0x00,0x00,0x00,0x00,0x00}}",
                _ => string.Empty
            };
        }
    }
}
