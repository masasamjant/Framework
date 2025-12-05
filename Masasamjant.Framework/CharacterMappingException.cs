namespace Masasamjant
{
    /// <summary>
    /// Exception that is thrown when character mapping is invalid.
    /// </summary>
    public class CharacterMappingException : Exception
    {
        /// <summary>
        /// Initializes new instance of the <see cref="CharacterMappingException"/> class.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <param name="message">The exception message.</param>
        public CharacterMappingException(char source, char destination, string message)
            : this(source, destination, message, null)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="CharacterMappingException"/> class.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <param name="message">The exception message.</param>
        /// <param name="innerException">The inner exception.</param>
        public CharacterMappingException(char source, char destination, string message, Exception? innerException)
            : base(message, innerException)
        {
            SourceCharacter = source;
            DestinationCharacter = destination;
        }

        /// <summary>
        /// Gets the source character.
        /// </summary>
        public char SourceCharacter { get; }

        /// <summary>
        /// Gets the destination character.
        /// </summary>
        public char DestinationCharacter { get; }
    }
}
