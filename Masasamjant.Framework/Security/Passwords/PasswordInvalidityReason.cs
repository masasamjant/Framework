namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Defines reasons why a password is invalid.
    /// </summary>
    public enum PasswordInvalidityReason : int
    {
        /// <summary>
        /// None, password is valid.
        /// </summary>
        None = 0,

        /// <summary>
        /// Password starts with whitespace character.
        /// </summary>
        StartWithWhitespace = 1,

        /// <summary>
        /// Password ends with whitespace character.
        /// </summary>
        EndWithWhitespace = 2,

        /// <summary>
        /// Password is too short.
        /// </summary>
        TooShort = 3,

        /// <summary>
        /// Password is too long.
        /// </summary>
        TooLong = 4,

        /// <summary>
        /// Password does not contain enough special characters.
        /// </summary>
        NotEnoughSpecialCharacters = 5,

        /// <summary>
        /// Password does not contain enough number characters.
        /// </summary>
        NotEnoughNumbers = 6,

        /// <summary>
        /// Password does not contain any lowercase letters.
        /// </summary>
        NoLowerCaseLetters = 7,

        /// <summary>
        /// Password does not contain any uppercase letters.
        /// </summary>
        NoUpperCaseLetters = 8,

        /// <summary>
        /// All characters in the password are the same.
        /// </summary>
        AllCharactersAreSame = 9,

        /// <summary>
        /// Password is empty or every character is whitespace character.
        /// </summary>
        EmptyOrWhitespaceOnly = 10
    }
}
