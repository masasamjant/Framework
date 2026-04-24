using System.Collections.ObjectModel;

namespace Masasamjant.Security.Passwords
{
    /// <summary>
    /// Represents characters used by <see cref="PasswordGenerator"/> class.
    /// </summary>
    public static class PasswordCharacters
    {
        /// <summary>
        /// Gets numbers character set.
        /// </summary>
        public static IList<char> Numbers { get; } = CreateList("1234567890");

        /// <summary>
        /// Gets lower case letters character set.
        /// </summary>
        public static IList<char> LowerCaseLetters { get; } = CreateList("abcdefghijklmnopqrstuvwxyz");

        /// <summary>
        /// Gets upper case letters character set.
        /// </summary>
        public static IList<char> UpperCaseLetters { get; } = CreateList("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        /// <summary>
        /// Gets special character set.
        /// </summary>
        public static IList<char> Specials { get; } = CreateList("!@#$¤%&=?|*+-_^~£");

        private static ReadOnlyCollection<char> CreateList(string value)
            => new List<char>(value.ToCharArray()).AsReadOnly();
    }
}
