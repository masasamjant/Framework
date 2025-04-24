namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents null type in reflection. This can be used to specify type of something that actually is null reference.
    /// </summary>
    public sealed class NullType : ReflectionType, IEquatable<NullType>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="NullType"/> class.
        /// </summary>
        /// <param name="type">The actual type.</param>
        /// <exception cref="ArgumentException">If <paramref name="type"/> is <see cref="NullType"/>.</exception>
        public NullType(Type type)
            : base(type)
        {
            if (type is NullType)
                throw new ArgumentException("The type cannot be null type.", nameof(type));
        }

        /// <summary>
        /// Check if other <see cref="NullType"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="NullType"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(NullType? other)
        {
            return other != null && GetActualType().Equals(other.GetActualType());
        }

        /// <summary>
        /// Check if object instance is <see cref="NullType"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="NullType"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as NullType);
        }

        /// <summary>
        /// Check if <see cref="Type"/> is <see cref="NullType"/> and equal to this.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is <see cref="NullType"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(Type? type)
        {
            return type != null && type is NullType other && Equals(other);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return GetActualType().GetHashCode();
        }
    }
}
