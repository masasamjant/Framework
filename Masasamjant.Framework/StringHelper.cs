using System.Text;

namespace Masasamjant
{
    /// <summary>
    /// Provides extension and helper methods to strings.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        /// Remove all other characters from <paramref name="value"/> except those in <paramref name="except"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="except">The characters not to remove.</param>
        /// <returns>A new string.</returns>
        public static string Remove(this string? value, char[] except)
        {
            if (value == null || value.Length == 0)
                return string.Empty;

            if (except.Length == 0)
                return value;

            var result = new List<char>(value.Length);

            for (int index = 0; index < value.Length; index++)
            {
                if (except.Contains(value[index]))
                    result.Add(value[index]);
            }

            return new string(result.ToArray());
        }

        /// <summary>
        /// Replace characters in string value using specified character map.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="map">The character map of replaced characters.</param>
        /// <returns>A string value with replacements.</returns>
        public static string Replace(this string value, CharacterMap map)
        {
            if (map.Count == 0 || value.Length == 0)
                return value;

            var sb = new StringBuilder(value);

            foreach (var mapping in map.Mappings)
                sb.Replace(mapping.Source, mapping.Destination);

            return sb.ToString();
        }

        /// <summary>
        /// Takes substring from left, at the start, of the string. If <paramref name="value"/> is <c>null</c> or empty or 
        /// <paramref name="length"/> is 0 or negative, then returns empty string. 
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="length">The length of the substring to take.</param>
        /// <returns>A substring from left of <paramref name="value"/> or empty string.</returns>
        public static string Left(this string? value, int length) => Substring(value, length, true);

        /// <summary>
        /// Takes substring from right, at the end, of the string. If <paramref name="value"/> is <c>null</c> or empty or 
        /// <paramref name="length"/> is 0 or negative, then returns empty string.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="length">The length of the substring to take.</param>
        /// <returns>A substring from right of <paramref name="value"/> or empty string.</returns>
        public static string Right(this string? value, int length) => Substring(value, length, false);

        /// <summary>
        /// Counts the number of continuous occurence of <paramref name="c"/> from left, at the start, of the string.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to count.</param>
        /// <returns>A continuous count of <paramref name="c"/> at the left of <paramref name="value"/>.</returns>
        public static int LeftCount(this string? value, char c) => Count(value, c, true);

        /// <summary>
        /// Counts the number of continuous occurence of <paramref name="c"/> from right, at the end, of the string.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to count.</param>
        /// <returns>A continuous count of <paramref name="c"/> at the right of <paramref name="value"/>.</returns>
        public static int RightCount(this string? value, char c) => Count(value, c, false);

        /// <summary>
        /// Count how many times a specified character appears in the string.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to count.</param>
        /// <returns>A count of <paramref name="c"/> ins <paramref name="value"/>.</returns>
        public static int Count(this string? value, char c)
        {
            if (value == null || value.Length == 0)
                return 0;

            var firstIndex = value.IndexOf(c);

            if (firstIndex < 0)
                return 0;

            int count = 1;
            int index = firstIndex + 1;
            for (; index < value.Length; index++)
            {
                if (value[index] == c)
                    count++;
            }

            return count;
        }

        /// <summary>
        /// Takes substring from specified string after first occurrence of specified character.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to search.</param>
        /// <returns>
        /// A empty string, if <paramref name="value"/> is <c>null</c> or empty or, if <paramref name="c"/> does not exist.
        /// -or-
        /// A substring from <paramref name="value"/> after first occurrence of <paramref name="c"/>.
        /// </returns>
        public static string AfterFirst(this string? value, char c) => After(value, c, true);

        /// <summary>
        /// Takes substring from specified string after last occurrence of specified character.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to search.</param>
        /// <returns>
        /// A empty string, if <paramref name="value"/> is <c>null</c> or empty or, if <paramref name="c"/> does not exist.
        /// -or-
        /// A substring from <paramref name="value"/> after last occurrence of <paramref name="c"/>.
        /// </returns>
        public static string AfterLast(this string? value, char c) => After(value, c, false);

        /// <summary>
        /// Takes substring from specified string before first occurrence of specified character.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to search.</param>
        /// <returns>
        /// A empty string, if <paramref name="value"/> is <c>null</c> or empty or, if <paramref name="c"/> does not exist.
        /// -or-
        /// A substring from <paramref name="value"/> before first occurrence of <paramref name="c"/>.
        /// </returns>
        public static string BeforeFirst(this string? value, char c) => Before(value, c, true);

        /// <summary>
        /// Takes substring from specified string before last occurrence of specified character.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to search.</param>
        /// <returns>
        /// A empty string, if <paramref name="value"/> is <c>null</c> or empty or, if <paramref name="c"/> does not exist.
        /// -or-
        /// A substring from <paramref name="value"/> before last occurrence of <paramref name="c"/>.
        /// </returns>
        public static string BeforeLast(this string? value, char c) => Before(value, c, false);

        /// <summary>
        /// Take substring from specified string between first and last occurrence of specified character. The characters at indexes are not
        /// included to substring.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="c">The character to search.</param>
        /// <returns>A substring from <paramref name="value"/> between first and last occurence of <paramref name="c"/>; or empty string.</returns>
        public static string Between(this string? value, char c) => Between(value, c, c);

        /// <summary>
        /// Take substring from specified string between first and last occurrence of specified characters. The characters at indexes are not
        /// included to substring.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="first">The first character.</param>
        /// <param name="last">The second character.</param>
        /// <returns>A substring from <paramref name="value"/> between first occurrence of <paramref name="first"/> and last occurence of <paramref name="last"/>; or empty string.</returns>
        public static string Between(this string? value, char first, char last)
        {
            if (value == null || value.Length == 0)
                return string.Empty;

            int firstIndex = value.IndexOf(first);
            int lastIndex = value.LastIndexOf(last);

            // If neither is present, return empty.
            if (firstIndex < 0 && lastIndex < 0)
                return string.Empty;

            // If both are present and first is after last, then return empty.
            if (firstIndex >= 0 && lastIndex >= 0 && firstIndex > lastIndex)
                return string.Empty;

            // If indexes are same or next to each other, return empty.
            if (firstIndex == lastIndex || (firstIndex + 1) == lastIndex)
                return string.Empty;

            // If first is not present and last index is first character,
            // or if last is not present and first index is last character, then return empty.
            if ((firstIndex < 0 && lastIndex == 0) || (firstIndex == value.Length - 1 && lastIndex < 0))
                return string.Empty;

            // If first is not present, then take from start to last index.
            if (firstIndex < 0)
                return value.Substring(0, lastIndex);

            firstIndex += 1; // Skip first character.

            // If last is not present, then take from first index + 1 to end.
            if (lastIndex < 0)
                return value.Substring(firstIndex);

            // Take between first and last index.
            return value.Substring(firstIndex, lastIndex - firstIndex);
        }

        /// <summary>
        /// Trim specified character from string using specified <see cref="StringTrimOptions"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="option">The <see cref="StringTrimOptions"/>.</param>
        /// <param name="trimChar">The character to trim.</param>
        /// <returns>
        /// A empty string, if <paramref name="value"/> is <c>null</c> or empty.
        /// -or-
        /// A <paramref name="value"/> if <paramref name="option"/> is <see cref="StringTrimOptions.None"/>.
        /// -or-
        /// A <paramref name="value"/> trimmed based on <paramref name="option"/> and <paramref name="trimChar"/>.
        /// </returns>
        public static string Trim(this string? value, StringTrimOptions option, char trimChar) => Trim(value, option, [trimChar]);

        /// <summary>
        /// Trim specified characters from string using specified <see cref="StringTrimOptions"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="option">The <see cref="StringTrimOptions"/>.</param>
        /// <param name="trimChars">The characters to trim.</param>
        /// <returns>
        /// A empty string, if <paramref name="value"/> is <c>null</c> or empty.
        /// -or-
        /// A <paramref name="value"/> if <paramref name="trimChars"/> is empty or <paramref name="option"/> is <see cref="StringTrimOptions.None"/>.
        /// -or-
        /// A <paramref name="value"/> trimmed based on <paramref name="option"/> and <paramref name="trimChars"/>.
        /// </returns>
        public static string Trim(this string? value, StringTrimOptions option, char[] trimChars)
        {
            if (value == null || value.Length == 0)
                return string.Empty;

            if (trimChars.Length == 0 || option.Equals(StringTrimOptions.None))
                return value;

            if (option.HasFlag(StringTrimOptions.Start))
                value = value.TrimStart(trimChars);

            if (option.HasFlag(StringTrimOptions.End))
                value = value.TrimEnd(trimChars);

            return value;
        }

        /// <summary>
        /// Trim start of <paramref name="value"/> until it stars with <paramref name="start"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="start">The allowed start character.</param>
        /// <returns>A <paramref name="value"/> trimmed from start so that it starts with <paramref name="start"/> or empty string.</returns>
        public static string TrimUntilStartsWith(this string? value, char start) => TrimUntilStartsWith(value, [start]);

        /// <summary>
        /// Trim start of <paramref name="value"/> until it starts with one of the characters in <paramref name="starts"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="starts">The allowed start characters.</param>
        /// <returns>A <paramref name="value"/> trimmed from start so that is starts with one of the <paramref name="starts"/> or empty string.</returns>
        public static string TrimUntilStartsWith(this string? value, char[] starts)
        {
            if (starts.Length == 0)
                throw new ArgumentException("At least one character must be specified.", nameof(starts));

            if (value == null || value.Length == 0)
                return string.Empty;

            int index;

            do
            {
                index = Array.IndexOf(starts, value[0]);
                if (index < 0)
                    value = value.TrimStart(value[0]);
            }
            while (index < 0 && value.Length > 0);

            return value;
        }

        /// <summary>
        /// Trim end of <paramref name="value"/> until it ends with <paramref name="end"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="end">The allowed end character.</param>
        /// <returns>A <paramref name="value"/> trimmed from end so that it ends with <paramref name="end"/> or empty string.</returns>
        public static string TrimUntilEndsWith(this string? value, char end) => TrimUntilEndsWith(value, [end]);

        /// <summary>
        /// Trim end of <paramref name="value"/> until it ends with one of the characters in <paramref name="ends"/>.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="ends">The allowed end characters.</param>
        /// <returns>A <paramref name="value"/> trimmed from end so that is ends with one of the <paramref name="ends"/> or empty string.</returns>
        /// <exception cref="ArgumentException">If <paramref name="ends"/> is empty.</exception>
        public static string TrimUntilEndsWith(this string? value, char[] ends)
        {
            if (ends.Length == 0)
                throw new ArgumentException("At least one character must be specified.", nameof(ends));

            if (value == null || value.Length == 0)
                return string.Empty;

            int index;

            do
            {
                index = Array.IndexOf(ends, value[^1]);
                if (index < 0)
                    value = value.TrimEnd(value[^1]);
            }
            while (index < 0 && value.Length > 0);

            return value;
        }

        /// <summary>
        /// Gets invalid characters in specified string.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="invalid">The characters considered invalid.</param>
        /// <returns>A dictionary of indexes and characters from <paramref name="value"/> that are in <paramref name="invalid"/>.</returns>
        public static IReadOnlyDictionary<int, char> GetInvalidCharacters(string value, char[] invalid)
        {
            var result = new Dictionary<int, char>();

            if (value.Length == 0 || invalid.Length == 0)
                return result.AsReadOnly();

            for (int index = 0; index < value.Length; index++)
            {
                if (Array.IndexOf(invalid, value[index]) >= 0)
                    result.Add(index, value[index]);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Converts string to string array where each character is one item.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A string array of characters from <paramref name="value"/>.</returns>
        public static string[] ToStringArray(this string? value)
        {
            if (value == null || value.Length == 0)
                return Array.Empty<string>();

            return value.Select(c => c.ToString()).ToArray();
        }

        /// <summary>
        /// Check if string starts with any of the specified starting strings.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="starts">The possible starts.</param>
        /// <param name="comparison">The <see cref="StringComparison"/> or <c>null</c> to use <see cref="StringComparison.CurrentCulture"/>.</param>
        /// <returns><c>true</c> if <paramref name="value"/> starts with any of <paramref name="starts"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If <paramref name="comparison"/> has value and that value is not defined.</exception>
        public static bool StartsWithAny(this string? value, IEnumerable<string>? starts, StringComparison? comparison = null)
        {
            if (comparison.HasValue && !Enum.IsDefined(comparison.Value))
                throw new ArgumentException("The value is not defined.", nameof(comparison));

            if (starts == null || value == null || !starts.Any())
                return false;

            if (!comparison.HasValue)
                comparison = StringComparison.CurrentCulture;

            return starts.Any(start => !string.IsNullOrEmpty(start) && value.StartsWith(start, comparison.Value));
        }

        /// <summary>
        /// Check if string ends with any of the specified ending strings.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="ends">The possible ends.</param>
        /// <param name="comparison">The <see cref="StringComparison"/> or <c>null</c> to use <see cref="StringComparison.CurrentCulture"/>.</param>
        /// <returns><c>true</c> if <paramref name="value"/> ends with any of <paramref name="ends"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If <paramref name="comparison"/> has value and that value is not defined.</exception>
        public static bool EndsWithAny(this string? value, IEnumerable<string>? ends, StringComparison? comparison = null)
        {
            if (comparison.HasValue && !Enum.IsDefined(comparison.Value))
                throw new ArgumentException("The value is not defined.", nameof(comparison));

            if (ends == null || value == null || !ends.Any())
                return false;

            if (!comparison.HasValue)
                comparison = StringComparison.CurrentCulture;

            return ends.Any(end => !string.IsNullOrEmpty(end) && value.EndsWith(end, comparison.Value));
        }

        /// <summary>
        /// Truncate string value to specified length.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <param name="length">The length to truncate to.</param>
        /// <param name="postfix">The postfix to append to truncated string or <c>null</c>.</param>
        /// <returns>A truncated string or original value if it is not longer than <paramref name="length"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="length"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">If <paramref name="postfix"/> is set and its length is more than <paramref name="length"/>.</exception>
        public static string Truncate(this string value, int length, string? postfix = null)
        {
            if (length < 0)
                throw new ArgumentOutOfRangeException(nameof(length), length, "The value must be greater than or equal to 0.");

            if (value.Length == 0 || length == 0)
                return string.Empty;

            if (value.Length <= length)
                return value;

            if (postfix != null && postfix.Length > 0 && postfix.Length >= length)
                throw new ArgumentException("The postfix must be shorter than truncate length.", nameof(postfix));

            var result = value.Substring(0, length);

            if (postfix != null && postfix.Length > 0)
                result = result.Substring(0, result.Length - postfix.Length) + postfix;

            return result;
        }

        /// <summary>
        /// Check if specified string value contains any whitespace characters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> contains whitespace character; <c>false</c> otherwise.</returns>
        public static bool HasWhiteSpace(this string? value)
        {
            if (value == null || value.Length == 0)
                return false;

            return value.Any(char.IsWhiteSpace);
        }

        /// <summary>
        /// Check if specified string value contains any leading whitespace characters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> contains leading whitespace character; <c>false</c> otherwise.</returns>
        public static bool HasLeadingWhiteSpace(this string? value)
        {
            if (value == null || value.Length == 0)
                return false;

            return char.IsWhiteSpace(value[0]);
        }

        /// <summary>
        /// Check if specified string value contains any trailing whitespace characters.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> contains trailing whitespace character; <c>false</c> otherwise.</returns>
        public static bool HasTrailingWhiteSpace(this string? value)
        {
            if (value == null || value.Length == 0)
                return false;

            return char.IsWhiteSpace(value[^1]);
        }

        /// <summary>
        /// Reads specified string value to lines.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A lines read from <paramref name="value"/>.</returns>
        public static IEnumerable<string> Lines(this string? value)
        {
            if (value == null)
                yield break;
            var reader = new StringReader(value);
            string? line;
            while ((line = reader.ReadLine()) != null)
                yield return line;
        }

        /// <summary>
        /// Reads specified string value to lines.
        /// </summary>
        /// <param name="value">The string value.</param>
        /// <returns>A lines read from <paramref name="value"/>.</returns>
        public static async Task<IEnumerable<string>> LinesAsync(this string? value)
        {
            var lines = new List<string>();
            if (value == null)
                return lines.AsEnumerable();
            var reader = new StringReader(value);
            string? line;
            while ((line = await reader.ReadLineAsync()) != null)
                lines.Add(line);
            return lines.AsEnumerable();
        }

        private static string Substring(string? value, int length, bool fromLeft)
        {
            if (value == null || value.Length == 0 || length <= 0)
                return string.Empty;

            if (value.Length < length)
                return value;

            return fromLeft ? value.Substring(0, length) : value.Substring(value.Length - length, length);
        }

        private static string After(string? value, char c, bool afterFirst)
        {
            if (value == null || value.Length == 0)
                return string.Empty;

            int index = afterFirst ? value.IndexOf(c) : value.LastIndexOf(c);

            if (index < 0 || index == value.Length - 1)
                return string.Empty;

            return value.Substring(index + 1);
        }

        private static string Before(string? value, char c, bool beforeFirst)
        {
            if (value == null || value.Length == 0)
                return string.Empty;

            int index = beforeFirst ? value.IndexOf(c) : value.LastIndexOf(c);

            if (index <= 0)
                return string.Empty;

            return value.Substring(0, index);
        }

        private static int Count(string? value, char c, bool fromLeft)
        {
            if (value == null || value.Length == 0)
                return 0;

            int count = 0;

            if (fromLeft)
            {
                for (int index = 0; index < value.Length; index++)
                {
                    if (value[index] == c)
                        count++;
                    else
                        break;
                }
            }
            else
            {
                for (int index = value.Length - 1; index >= 0; index--)
                {
                    if (value[index] == c)
                        count++;
                    else
                        break;
                }
            }

            return count;
        }
    }
}
