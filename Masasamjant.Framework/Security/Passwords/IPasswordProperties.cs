namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents properties of password.
    /// </summary>
    public interface IPasswordProperties
    {
        /// <summary>
        /// Gets the password minimum length.
        /// </summary>
        int MinLength { get; }

        /// <summary>
        /// Gets the password maximum length.
        /// </summary>
        int MaxLength { get; }

        /// <summary>
        /// Gets the password complexity requirements.
        /// </summary>
        PasswordComplexity Complexity { get; }

        /// <summary>
        /// Gets how many special characters are included in password.
        /// </summary>
        int? SpecialCharacterCount { get; }
    }
}
