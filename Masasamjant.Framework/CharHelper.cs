using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to work with characters.
    /// </summary>
    public static class CharHelper
    {
        private static readonly char[] commonSeparators = [',', ';', '|', ':', '+', '-'];

        /// <summary>
        /// Gets the characters commonly used to separate string values.
        /// </summary>
        public static char[] CommonSeparators => (char[])commonSeparators.Clone();

        /// <summary>
        /// Tries to resolve separator character that could be used to join specified string values.
        /// </summary>
        /// <param name="values">The string values.</param>
        /// <param name="separators">The possible separators.</param>
        /// <returns>A suitable separator character or <c>null</c>.</returns>
        public static char? GetSeparator(IEnumerable<string> values, IEnumerable<char> separators)
            => TryGetSeparator(values, separators, out var separator) ? separator : null;

        /// <summary>
        /// Tries to resolve separator character that could be used to join specified string values.
        /// </summary>
        /// <param name="values">The string values.</param>
        /// <param name="separators">The possible separators.</param>
        /// <param name="separator">The suitable separator, if returns <c>true</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if suitable separator was found; <c>false</c> otherwise.</returns>
        public static bool TryGetSeparator(IEnumerable<string> values, IEnumerable<char> separators, [NotNullWhen(true)] out char? separator)
        {
            separator = null;

            // No any separators.
            if (!separators.Any())
                return false;

            // No any values so first separator is valid.
            if (!values.Any())
            {
                separator = separators.First();
                return true;
            }

            // If any of the values does not contain separator, then it is valid.
            foreach (var c in separators)
            {
                if (!values.Any(value => value.Contains(c)))
                {
                    separator = c;
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Tries to resolve separator character from <see cref="CommonSeparators"/> that could be used to join specified string values.
        /// </summary>
        /// <param name="values">The string values.</param>
        /// <returns>A suitable separator character or <c>null</c>.</returns>
        public static char? GetCommonSeparator(IEnumerable<string> values) => TryGetCommonSeparator(values, out var separator) ? separator : null;

        /// <summary>
        /// Tries to resolve separator character from <see cref="CommonSeparators"/> that could be used to join specified string values.
        /// </summary>
        /// <param name="values">The string values.</param>
        /// <param name="separator">The suitable separator, if returns <c>true</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if suitable separator was found; <c>false</c> otherwise.</returns>
        public static bool TryGetCommonSeparator(IEnumerable<string> values, [NotNullWhen(true)] out char? separator)
            => TryGetSeparator(values, CommonSeparators, out separator);

        /// <summary>
        /// Check if character is unicode letter or ASCII digit from 0 to 9.
        /// </summary>
        /// <param name="c">The character.</param>
        /// <returns><c>true</c> if <paramref name="c"/> is unicode letter or ASCII digit; <c>false</c> otherwise.</returns>
        public static bool IsNumberOrLetter(this char c)
            => char.IsLetter(c) || char.IsAsciiDigit(c);
    }
}
