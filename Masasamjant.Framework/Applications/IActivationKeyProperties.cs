namespace Masasamjant.Applications
{
    /// <summary>
    /// Represents properties of activation key.
    /// </summary>
    public interface IActivationKeyProperties
    {
        /// <summary>
        /// Gets the component separator character.
        /// </summary>
        char ComponentSeparator { get; }

        /// <summary>
        /// Gets the length of single component.
        /// </summary>
        int ComponentLength { get; }

        /// <summary>
        /// Gets the count of components.
        /// </summary>
        int ComponentCount { get; }

        /// <summary>
        /// Gets the length of prefix string. 0, if prefix should not be used.
        /// </summary>
        int PrefixLength { get; }

        /// <summary>
        /// Gets if prefix should be used.
        /// </summary>
        bool UsePrefix { get; }

        /// <summary>
        /// Gets the number to number character map.
        /// </summary>
        /// <remarks>The map is in read-only state.</remarks>
        CharacterMap NumberToNumberMap { get; }

        /// <summary>
        /// Gets the number to letter character map.
        /// </summary>
        /// <remarks>The map is in read-only state.</remarks>
        CharacterMap NumberToLetterMap { get; }
    }
}
