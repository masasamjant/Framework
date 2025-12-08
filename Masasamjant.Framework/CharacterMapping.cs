using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents one-to-one mapping between <see cref="char"/> values.
    /// </summary>
    public readonly struct CharacterMapping : IEquatable<CharacterMapping>, ICloneable
    {
        /// <summary>
        /// Initializes new instance of <see cref="CharacterMapping"/> value.
        /// </summary>
        /// <param name="source">The source character.</param>
        /// <param name="destination">The destination character.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is same as <paramref name="source"/>.</exception>
        public CharacterMapping(char source, char destination)
        {
            if (source == destination)
                throw new ArgumentException("The destination character cannot be same as source character.", nameof(destination));

            Source = source;
            Destination = destination;
        }

        /// <summary>
        /// Gets the source character.
        /// </summary>
        public char Source { get; }

        /// <summary>
        /// Gets the destination character.
        /// </summary>
        public char Destination { get; }

        /// <summary>
        /// Check if other <see cref="CharacterMapping"/> is equal with this.
        /// </summary>
        /// <param name="other">The other <see cref="CharacterMapping"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal with this; <c>false</c> otherwise.</returns>
        public bool Equals(CharacterMapping other)
        {
            return Source == other.Source && Destination == other.Destination;
        }

        /// <summary>
        /// Check if object instance is <see cref="CharacterMapping"/> and equal with this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="CharacterMapping"/> and equal with this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is CharacterMapping other)
                return Equals(other);
            return false;
        }

        /// <summary>
        /// Gets hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Source, Destination);
        }

        /// <summary>
        /// Creates copy from this mapping.
        /// </summary>
        /// <returns>A copy from this mappaing.</returns>
        public CharacterMapping Clone()
        {
            return new CharacterMapping(Source, Destination);
        }

        /// <summary>
        /// Operator to check if <see cref="CharacterMapping"/>s are equal.
        /// </summary>
        /// <param name="left">The left <see cref="CharacterMapping"/>.</param>
        /// <param name="right">The right <see cref="CharacterMapping"/>.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(CharacterMapping left, CharacterMapping right) => left.Equals(right);

        /// <summary>
        /// Operator to check if <see cref="CharacterMapping"/>s are not equal.
        /// </summary>
        /// <param name="left">The left <see cref="CharacterMapping"/>.</param>
        /// <param name="right">The right <see cref="CharacterMapping"/>.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(CharacterMapping left, CharacterMapping right) => !(left == right);

        object ICloneable.Clone()
        {
            return Clone();
        }
    }
}
