namespace Masasamjant.Text
{
    /// <summary>
    /// Represents simple concat strings where values are concated using specified separator character.
    /// </summary>
    public sealed class ConcatString : IConcatString, IEquatable<ConcatString>
    {
        /// <summary>
        /// Represents no separator character.
        /// </summary>
        public const char NoSeparator = '\0';

        /// <summary>
        /// Initializes new instance of the <see cref="ConcatString"/> class.
        /// </summary>
        /// <param name="separator">The separator character.</param>
        /// <param name="values">The values to concate.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="separator"/> is <see cref="NoSeparator"/> or any value in <paramref name="values"/> contains <paramref name="separator"/>.
        /// </exception>
        public ConcatString(char separator, IEnumerable<string> values)
        {
            if (separator == NoSeparator)
                throw new ArgumentException("The value cannot be no separator character.", nameof(separator));

            if (values.Any(value => value.Contains(separator)))
                throw new ArgumentException("The value cannot contain separator.", nameof(values));

            Separator = separator;
            Values = new List<string>(values).AsReadOnly();
        }

        /// <summary>
        /// Initializes new empty instance of the <see cref="ConcatString"/> class.
        /// </summary>
        public ConcatString()
        {
            Separator = NoSeparator;
            Values = new List<string>().AsReadOnly();
        }

        /// <summary>
        /// Gets the separator character.
        /// </summary>
        public char Separator { get; private set; }

        /// <summary>
        /// Gets the values to concatenate.
        /// </summary>
        public IEnumerable<string> Values { get; private set; }

        /// <summary>
        /// Gets if represents empty concat string.
        /// </summary>
        public bool IsEmpty => Separator == NoSeparator;

        /// <summary>
        /// Concatenates values into single string. If <see cref="IsEmpty"/> is <c>true</c>, then returns empty string; otherwise 
        /// returns concated string. The first character of string is <see cref="Separator"/> followed by concated values.
        /// </summary>
        /// <returns>A concated string.</returns>
        public string Concat()
        {
            if (Separator == NoSeparator)
                return string.Empty;

            if (!Values.Any())
                return Separator.ToString();

            var value = string.Join(Separator, Values);
            return Separator + value;
        }

        /// <summary>
        /// Check if other <see cref="ConcatString"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="ConcatString"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(ConcatString? other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other) || (other.IsEmpty && IsEmpty))
                return true;

            if (Separator != other.Separator || IsEmpty != other.IsEmpty)
                return false;

            return string.Equals(Concat(), other.Concat(), StringComparison.Ordinal);
        }

        /// <summary>
        /// Check if object instance is <see cref="ConcatString"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="ConcatString"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as ConcatString);
        }

        /// <summary>
        /// Gets hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return Concat().GetHashCode();
        }

        /// <summary>
        /// Gets string presentation, <see cref="Concat"/>.
        /// </summary>
        /// <returns>A concated string.</returns>
        public override string ToString()
        {
            return Concat();
        }

        /// <summary>
        /// Parse concat string from specified value.
        /// </summary>
        /// <param name="value">The value to parse concat string.</param>
        /// <exception cref="FormatException">If the format of <paramref name="value"/> is not correct.</exception>
        public void ParseFrom(string value)
        {
            if (value.Length == 0)
            {
                Separator = NoSeparator;
                Values = new List<string>().AsReadOnly();
            }
            else
            {
                try
                {
                    var separator = value[0];
                    if (separator == NoSeparator)
                        throw new FormatException("The separator character cannot be no separator character.");
                    var values = separator.ToString() == value 
                        ? Enumerable.Empty<string>() 
                        : value.Remove(0, 1).Split(separator, StringSplitOptions.None);
                    Separator = separator;
                    Values = new List<string>(values).AsReadOnly();
                }
                catch (Exception exception)
                {
                    throw new FormatException("The concat string value has incorrect format.", exception);
                }
            }
        }

        /// <summary>
        /// Parse <see cref="ConcatString"/> instance from specified value.
        /// </summary>
        /// <param name="value">The concat string value.</param>
        /// <returns>A parsed <see cref="ConcatString"/> instance.</returns>
        /// <exception cref="FormatException">If <paramref name="value"/> has invalid format.</exception>
        public static ConcatString Parse(string value)
        {
            var cs = new ConcatString();
            if (value.Length > 0)
                cs.ParseFrom(value);
            return cs;
        }
    }
}
