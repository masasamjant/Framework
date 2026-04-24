namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Defines password complexity requirements
    /// </summary>
    [Flags]
    public enum PasswordComplexity : int
    {
        /// <summary>
        /// None.
        /// </summary>
        None = 0,

        /// <summary>
        /// Password must have lower-case letter character.
        /// </summary>
        LowerCaseLetters = 1,

        /// <summary>
        /// Password must have upper-case letter character.
        /// </summary>
        UpperCaseLetters = 2,

        /// <summary>
        /// Password must have number character.
        /// </summary>
        Numbers = 4,

        /// <summary>
        /// Password must have special character that is no number or letter.
        /// </summary>
        Specials = 8
    }
}
