namespace Masasamjant
{
    /// <summary>
    /// Represents one-to-one character map that accepts only ASCII character mappings.
    /// </summary>
    public sealed class AsciiCharacterMap : CharacterMap
    {
        /// <summary>
        /// Initializes new instance of the <see cref="AsciiCharacterMap"/> class with specified characters.
        /// </summary>
        /// <param name="characters">The mapped ASCII characters.</param>
        /// <exception cref="ArgumentException">If <paramref name="characters"/> contains invalid character mappings.</exception>
        public AsciiCharacterMap(IDictionary<char, char> characters)
            : base(characters)
        { }

        /// <summary>
        /// Initializes new empty instance of the <see cref="AsciiCharacterMap"/> class.
        /// </summary>
        public AsciiCharacterMap()
            : base()
        { }

        /// <summary>
        /// Create copy from this character map.
        /// </summary>
        /// <returns>A copy from this character map.</returns>
        /// <remarks>If this map is in read-only state, the copy is not.</remarks>
        public override CharacterMap Clone()
        {
            var map = new AsciiCharacterMap();
            foreach (var mapping in this)
                map.Add(mapping.Source, mapping.Destination);
            return map;
        }

        /// <summary>
        /// Validate that source and destination characters are ASCII characters.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <exception cref="CharacterMappingException">If <paramref name="source"/> or <paramref name="destination"/> is not ASCII character.</exception>
        protected override void ValidateCharacters(char source, char destination)
        {
            if (!char.IsAscii(source))
                throw new CharacterMappingException(source, destination, "The source character must be an ASCII character.", null);

            if (!char.IsAscii(destination))
                throw new CharacterMappingException(source, destination, "The destination character must be an ASCII character.", null);
        }
    }
}
