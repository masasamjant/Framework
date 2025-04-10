using Masasamjant.Collections;

namespace Masasamjant.Text
{
    /// <summary>
    /// Represents concatenated string of key-value pairs.
    /// </summary>
    public sealed class KeyValueConcatString : IConcatString, IEquatable<KeyValueConcatString>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="KeyValueConcatString"/> class.
        /// </summary>
        /// <param name="itemSeparator">The item separator character to separate each key-value pair.</param>
        /// <param name="keyValueSeparator">The key-value separator character to separate key and value in each key-value pair.</param>
        /// <param name="values">The dictionary of key-value pairs to concatenate.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="itemSeparator"/> op <paramref name="keyValueSeparator"/> is <see cref="ConcatString.NoSeparator"/>.
        /// -or-
        /// If <paramref name="keyValueSeparator"/> is same as <paramref name="itemSeparator"/>.
        /// -or-
        /// If keys or values of <paramref name="values"/> contains <paramref name="itemSeparator"/> or <paramref name="keyValueSeparator"/>.
        /// </exception>
        public KeyValueConcatString(char itemSeparator, char keyValueSeparator, IDictionary<string, string> values)
        {
            if (itemSeparator == ConcatString.NoSeparator)
                throw new ArgumentException("The value cannot be no separator character.", nameof(itemSeparator));

            if (keyValueSeparator == ConcatString.NoSeparator)
                throw new ArgumentException("The value cannot be no separator character.", nameof(keyValueSeparator));

            if (itemSeparator == keyValueSeparator)
                throw new ArgumentException("The value cannot be same as item separator.", nameof(keyValueSeparator));

            if (values.Keys.Any(key => key.Contains(itemSeparator) || key.Contains(keyValueSeparator)))
                throw new ArgumentException("The key cannot contain separator.", nameof(values));

            if (values.Values.Any(value => value.Contains(itemSeparator) || value.Contains(keyValueSeparator)))
                throw new ArgumentException("The value cannot contain separator.", nameof(values));

            ItemSeparator = itemSeparator;
            KeyValueSeparator = keyValueSeparator;
            Values = values.AsReadOnly();
        }

        /// <summary>
        /// Initializes new empty instance of the <see cref="KeyValueConcatString"/> class.
        /// </summary>
        public KeyValueConcatString()
        {
            ItemSeparator = ConcatString.NoSeparator;
            KeyValueSeparator = ConcatString.NoSeparator;
            Values = DictionaryHelper.CreateReadOnly<string, string>();
        }

        /// <summary>
        /// Gets the item separator character to separate each key-value pair.
        /// </summary>
        public char ItemSeparator { get; private set; }

        /// <summary>
        /// Gets the key-value separator character to separate key and value in each key-value pair.
        /// </summary>
        public char KeyValueSeparator { get; private set; }

        /// <summary>
        /// Gets the dictionary of key-value pairs to concatenate.
        /// </summary>
        public IReadOnlyDictionary<string, string> Values { get; private set; }

        /// <summary>
        /// Gets if represents empty concat string.
        /// </summary>
        public bool IsEmpty
        {
            get { return ItemSeparator == ConcatString.NoSeparator || KeyValueSeparator == ConcatString.NoSeparator; }
        }

        /// <summary>
        /// Concatenates values into single string. In concated string the first character is <see cref="ItemSeparator"/> and second 
        /// character is <see cref="KeyValueSeparator"/>, rest of the string is the concatenated key-value pairs.
        /// </summary>
        /// <returns>
        /// A empty string, if <see cref="IsEmpty"/>.
        /// -or-
        /// A concated string.
        /// </returns>
        public string Concat()
        {
            if (IsEmpty)
                return string.Empty;

            if (Values.Count == 0)
                return ItemSeparator.ToString() + KeyValueSeparator.ToString();

            var value = string.Join(ItemSeparator, ConcatKeyValues());

            return $"{ItemSeparator}{KeyValueSeparator}{value}";
        }

        /// <summary>
        /// Compares this instance with another instance of <see cref="KeyValueConcatString"/> for equality.
        /// </summary>
        /// <param name="other">The other <see cref="KeyValueConcatString"/>.</param>
        /// <returns><c>true</c> if equal; <c>false</c> otherwise.</returns>
        public bool Equals(KeyValueConcatString? other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other) || (other.IsEmpty && IsEmpty))
                return true;

            if (other.IsEmpty != IsEmpty || other.ItemSeparator != ItemSeparator || other.KeyValueSeparator != KeyValueSeparator)
                return false;

            return string.Equals(Concat(), other.Concat(), StringComparison.Ordinal);
        }

        /// <summary>
        /// Check if object instance is <see cref="KeyValueConcatString"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="KeyValueConcatString"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as KeyValueConcatString);
        }

        /// <summary>
        /// Gets the hash code of this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return Concat().GetHashCode();
        }

        /// <summary>
        /// Gets the string representation of this instance, <see cref="Concat"/>.
        /// </summary>
        /// <returns>A concat string.</returns>
        public override string ToString()
        {
            return Concat();
        }

        /// <summary>
        /// Parses concat string from specified value.
        /// </summary>
        /// <param name="value">The value to parse concat string.</param>
        /// <exception cref="FormatException">If the format of <paramref name="value"/> is not correct.</exception>
        public void ParseFrom(string value)
        {
            if (value.Length == 0)
            {
                ItemSeparator = ConcatString.NoSeparator;
                KeyValueSeparator = ConcatString.NoSeparator;
                Values = DictionaryHelper.CreateReadOnly<string, string>();
            }
            else if (value.Length < 2)
            {
                throw new FormatException("The value has invalid count of characters. Expected 0, 2 or more.");
            }
            else if (value.Length == 2)
            {
                var itemSeparator = value[0];
                var keyValueSeparator = value[1];
                ValidateSeparators(itemSeparator, keyValueSeparator);
                ItemSeparator = itemSeparator;
                KeyValueSeparator = keyValueSeparator;
                Values = DictionaryHelper.CreateReadOnly<string, string>();
            }
            else
            {
                var itemSeparator = value[0];
                var keyValueSeparator = value[1];
                ValidateSeparators(itemSeparator, keyValueSeparator);
                var items = value.Remove(0, 2).Split(itemSeparator, StringSplitOptions.None);
                var values = new Dictionary<string, string>(items.Length);
                if (items.Length > 0 && items.All(x => x.Contains(keyValueSeparator)))
                    values = ParseKeyValues(keyValueSeparator, items);
                ItemSeparator = itemSeparator;
                KeyValueSeparator = keyValueSeparator;
                Values = values.AsReadOnly();
            }
        }

        /// <summary>
        /// Parses a <see cref="KeyValueConcatString"/> from the specified value.
        /// </summary>
        /// <param name="value">The value to parse.</param>
        /// <returns>A <see cref="KeyValueConcatString"/>.</returns>
        /// <exception cref="FormatException">If the format of <paramref name="value"/> is not correct.</exception>
        public static KeyValueConcatString Parse(string value)
        {
            var cs = new KeyValueConcatString();
            if (value.Length > 0)
                cs.ParseFrom(value);
            return cs;
        }

        private IEnumerable<string> ConcatKeyValues()
        {
            foreach (var keyValue in Values)
                yield return $"{keyValue.Key}{KeyValueSeparator}{keyValue.Value}";
        }

        private static Dictionary<string, string> ParseKeyValues(char separator, IEnumerable<string> items)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var item in items)
            {
                var pair = item.Split(separator, 2, StringSplitOptions.None);
                dictionary.Add(pair[0], pair[1]);
            }
            return dictionary;
        }

        private static void ValidateSeparators(char itemSeparator, char keyValueSeparator)
        {
            if (itemSeparator == ConcatString.NoSeparator)
                throw new FormatException($"The value '{itemSeparator}' is not allowed separator.");
            if (keyValueSeparator == ConcatString.NoSeparator)
                throw new FormatException($"The value '{keyValueSeparator}' is not allowed separator.");
            if (itemSeparator == keyValueSeparator)
                throw new FormatException("The key-value separator cannot be same as item separator.");
        }
    }
}
