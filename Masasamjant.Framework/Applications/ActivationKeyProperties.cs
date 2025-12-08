namespace Masasamjant.Applications
{
    /// <summary>
    /// Represents properties of activation key.
    /// </summary>
    public sealed class ActivationKeyProperties : IActivationKeyProperties
    {
        /// <summary>
        /// Minimum component count.
        /// </summary>
        public const int MinComponentCount = 2;

        /// <summary>
        /// Maximum component count.
        /// </summary>
        public const int MaxComponentCount = 8;

        /// <summary>
        /// Minimum component length.
        /// </summary>
        public const int MinComponentLength = 3;

        /// <summary>
        /// Maximum component length.
        /// </summary>
        public const int MaxComponentLength = 6;

        /// <summary>
        /// Minimum prefix length.
        /// </summary>
        public const int MinPrefixLength = 0;

        /// <summary>
        /// Maximum prefix length.
        /// </summary>
        public const int MaxPrefixLength = 6;

        /// <summary>
        /// Initializes new instance of the <see cref="ActivationKeyProperties"/> class.
        /// </summary>
        /// <param name="componentSeparator">The component separator character.</param>
        /// <param name="componentLength">The length of single component from 3 to 6 characters.</param>
        /// <param name="componentCount">The count of components from 2 to 8 components.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="componentSeparator"/> is white-space.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="componentLength"/> or <paramref name="componentCount"/> is not greater than 0.</exception>
        public ActivationKeyProperties(char componentSeparator, int componentLength, int componentCount)
            : this(componentSeparator, componentLength, componentCount, 0)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="ActivationKeyProperties"/> class.
        /// </summary>
        /// <param name="componentSeparator">The component separator character.</param>
        /// <param name="componentLength">The length of single component from 3 to 6 characters.</param>
        /// <param name="componentCount">The count of components from 2 to 8 components.</param>
        /// <param name="prefixLength">The prefix length from 0 to 6 characters.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="componentSeparator"/> is white-space.</exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="componentLength"/> is not in range from <see cref="MinComponentLength"/> to <see cref="MaxComponentLength"/>.
        /// -or-
        /// If value of <paramref name="componentCount"/> is not in range from <see cref="MinComponentCount"/> to <see cref="MaxComponentCount"/>.
        /// -or-
        /// If value of <paramref name="prefixLength"/> is not in range from <see cref="MinPrefixLength"/> to <see cref="MaxPrefixLength"/>.
        /// </exception>
        public ActivationKeyProperties(char componentSeparator, int componentLength, int componentCount, int prefixLength)
        {
            if (char.IsWhiteSpace(componentSeparator))
                throw new ArgumentException("The component separator cannot be white-space character.", nameof(componentSeparator));

            if (componentLength < MinComponentLength || componentLength > MaxComponentLength)
                throw new ArgumentOutOfRangeException(nameof(componentLength), componentLength, $"The component of activation key can be from {MinComponentLength} to {MaxComponentLength} characters long.");

            if (componentCount < MinComponentCount || componentCount > MaxComponentCount)
                throw new ArgumentOutOfRangeException(nameof(componentCount), componentCount, $"The activation key can have {MinComponentCount} to {MaxComponentCount} components.");

            if (prefixLength < MinPrefixLength || prefixLength > MaxPrefixLength)
                throw new ArgumentOutOfRangeException(nameof(prefixLength), prefixLength, $"The prefix of activation key can be from {MinPrefixLength} to {MaxPrefixLength} characters long.");

            ComponentSeparator = componentSeparator;
            ComponentLength = componentLength;
            ComponentCount = componentCount;
            PrefixLength = prefixLength;
            NumberToNumberMap = DefaultNumberToNumberMap();
            NumberToLetterMap = DefaultNumberToLetterMap();
        }

        /// <summary>
        /// Gets the component separator character.
        /// </summary>
        public char ComponentSeparator { get; }

        /// <summary>
        /// Gets the length of single component.
        /// </summary>
        public int ComponentLength { get; }

        /// <summary>
        /// Gets the count of components.
        /// </summary>
        public int ComponentCount { get; }

        /// <summary>
        /// Gets the length of prefix string. 0, if prefix should not be used.
        /// </summary>
        public int PrefixLength { get; }

        /// <summary>
        /// Gets if prefix should be used.
        /// </summary>
        public bool UsePrefix
        {
            get { return PrefixLength > 0; }
        }

        /// <summary>
        /// Gets the number to number character map. Default value is result of <see cref="DefaultNumberToNumberMap"/> method.
        /// </summary>
        /// <remarks>The map is in read-only state.</remarks>
        public CharacterMap NumberToNumberMap { get; private set; }

        /// <summary>
        /// Gets the number to letter character map. Default value is result of <see cref="DefaultNumberToLetterMap"/> method.
        /// </summary>
        /// <remarks>The map is in read-only state.</remarks>
        public CharacterMap NumberToLetterMap { get; private set; }

        /// <summary>
        /// Gets the new instance of default activation key properties 
        /// where the component separator is '-', component length is 4, component count is 4, and prefix length is 4.
        /// </summary>
        public static ActivationKeyProperties Default 
        {
            get { return new ActivationKeyProperties('-', 4, 4, 4); } 
        }

        /// <summary>
        /// Gets the default number to number map.
        /// </summary>
        /// <returns>A number to number map of <see cref="Default"/> properties.</returns>
        public static AsciiCharacterMap DefaultNumberToNumberMap()
        {
            return CreateReadOnlyMap(new Dictionary<char, char>()
            {
                { '0', '5' },
                { '1', '8' },
                { '2', '6' },
                { '3', '9' },
                { '4', '7' },
                { '5', '0' },
                { '6', '2' },
                { '7', '4' },
                { '8', '1' },
                { '9', '3' }
            });
        }

        /// <summary>
        /// Gets the default number to letter map.
        /// </summary>
        /// <returns>A number to letter map of <see cref="Default"/> properties.</returns>
        public static AsciiCharacterMap DefaultNumberToLetterMap()
        {
            return CreateReadOnlyMap(new Dictionary<char, char>()
            {
                { '0', 'K' },
                { '1', 'C' },
                { '2', 'E' },
                { '3', 'M' },
                { '4', 'B' },
                { '5', 'H' },
                { '6', 'F' },
                { '7', 'U' },
                { '8', 'A' },
                { '9', 'V' }
            });
        }

        /// <summary>
        /// Change <see cref="NumberToNumberMap"/> to specified ASCII character map.
        /// </summary>
        /// <param name="numberToNumberMap">The number-to-number map.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="numberToNumberMap"/> has source character that is not ASCII digit character (0-9).
        /// -or-
        /// If <paramref name="numberToNumberMap"/> does not have mapping for each ASCII digit character (0-9).
        /// -or-
        /// If <paramref name="numberToNumberMap"/> has destination character that is not ASCII digit character (0-9).
        /// </exception>
        public void ChangeNumberToNumberMap(AsciiCharacterMap numberToNumberMap)
        {
            ValidateNumberToNumberMap(numberToNumberMap);
            NumberToNumberMap = CreateReadOnlyMap(numberToNumberMap.ToDictionary());
        }

        /// <summary>
        /// Change <see cref="NumberToLetterMap"/> to specified ASCII character map.
        /// </summary>
        /// <param name="numberToLetterMap">The number-to-letter map.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="numberToLetterMap"/> has source character that is not ASCII digit character (0-9).
        /// -or-
        /// If <paramref name="numberToLetterMap"/> does not have mapping for each ASCII digit character (0-9).
        /// -or-
        /// If <paramref name="numberToLetterMap"/> has destination character that is not ASCII letter character (a-z or A-Z).
        /// </exception>
        public void ChangeNumberToLetterMap(AsciiCharacterMap numberToLetterMap)
        {
            ValidateNumberToLetterMap(numberToLetterMap);
            NumberToLetterMap = CreateReadOnlyMap(numberToLetterMap.ToDictionary());
        }   

        private static AsciiCharacterMap CreateReadOnlyMap(Dictionary<char, char> values)
        {
            var map = new AsciiCharacterMap(values);
            map.SetReadOnly();
            return map;
        }

        private static void ValidateNumberToNumberMap(CharacterMap numberToNumberMap)
        {
            if (numberToNumberMap.Count != 10)
                throw new ArgumentException("The number to number map must contain exactly 10 mappings for ASCII digit characters (0-9).", nameof(numberToNumberMap));

            if (numberToNumberMap.Sources.Any(c => !char.IsAsciiDigit(c)))
                throw new ArgumentException("The number to number map can only contain ASCII digit characters as source characters.", nameof(numberToNumberMap));

            if (numberToNumberMap.Destinations.Any(c => !char.IsAsciiDigit(c)))
                throw new ArgumentException("The number to number map can only contain ASCII digit characters as destination characters.", nameof(numberToNumberMap));
        }

        private static void ValidateNumberToLetterMap(CharacterMap numberToLetterMap)
        {
            if (numberToLetterMap.Count != 10)
                throw new ArgumentException("The number to letter map must contain exactly 10 mappings for ASCII digit characters (0-9).", nameof(numberToLetterMap));

            if (numberToLetterMap.Sources.Any(c => !char.IsAsciiDigit(c)))
                throw new ArgumentException("The number to letter map can only contain ASCII digit characters as source characters.", nameof(numberToLetterMap));

            if (numberToLetterMap.Destinations.Any(c => !char.IsAsciiLetter(c)))
                throw new ArgumentException("The number to letter map can only contain ASCII letter characters as destination characters.", nameof(numberToLetterMap));
        }
    }
}
