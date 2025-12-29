namespace Masasamjant.Validation
{
    /// <summary>
    /// Represents context for validating string arguments.
    /// </summary>
    public sealed class StringArgumentValidationContext
    {
        /// <summary>
        /// Initializes new instance of the <see cref="StringArgumentValidationContext"/> class.
        /// </summary>
        /// <param name="allowEmpty"></param>
        /// <param name="allowWhiteSpace"></param>
        /// <param name="maxLength"></param>
        /// <param name="minLength"></param>
        /// <param name="emptyAsNull"></param>
        /// <param name="checkNoLeadingWhiteSpace"></param>
        /// <param name="checkNoTrailingWhiteSpace"></param>
        public StringArgumentValidationContext(bool allowEmpty = false, bool allowWhiteSpace = false, int? maxLength = null, int? minLength = null,
            bool emptyAsNull = true, bool checkNoLeadingWhiteSpace = false, bool checkNoTrailingWhiteSpace = false)
        {
            AllowEmpty = allowEmpty;
            AllowWhiteSpace = allowWhiteSpace;
            MaxLength = maxLength;
            MinLength = minLength;
            EmptyAsNull = emptyAsNull;
            CheckNoLeadingWhiteSpace = checkNoLeadingWhiteSpace;
            CheckNoTrailingWhiteSpace = checkNoTrailingWhiteSpace;
        }

        /// <summary>
        /// Gets the default instance of the <see cref="StringArgumentValidationContext"/> class.
        /// </summary>
        public static StringArgumentValidationContext Default { get; } = new StringArgumentValidationContext();

        /// <summary>
        /// Gets or sets if empty string is allowed. Default is <c>false</c>.
        /// </summary>
        public bool AllowEmpty { get; set; } = false;

        /// <summary>
        /// Gets or sets if white-space string is allowed. Default is <c>false</c>.
        /// </summary>
        public bool AllowWhiteSpace { get; set; } = false;

        /// <summary>
        /// Gets or sets maximum length. Default is <c>null</c>, meaning no maximum length.
        /// </summary>
        public int? MaxLength { get; set; } = null;

        /// <summary>
        /// Gets or sets minimum length. Default is <c>null</c>, meaning no minimum length.
        /// </summary>
        public int? MinLength { get; set; } = null;

        /// <summary>
        /// Gets or sets if empty string is retuned as <c>null</c>. Default is <c>true</c>.
        /// </summary>
        public bool EmptyAsNull { get; set; } = true;

        /// <summary>
        /// Gets or sets if check that string has no leading white-space. Default is <c>false</c>.
        /// </summary>
        public bool CheckNoLeadingWhiteSpace { get; set; } = false;

        /// <summary>
        /// Gets or sets if check that string has no trailing white-space. Default is <c>false</c>.
        /// </summary>
        public bool CheckNoTrailingWhiteSpace { get; set; } = false;
    }
}
