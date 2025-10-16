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
    }
}
