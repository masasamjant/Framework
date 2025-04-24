namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents type of <c>this</c> parameter of an extension method.
    /// </summary>
    public sealed class ThisType : ReflectionType, IEquatable<ThisType>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ThisType"/> class.
        /// </summary>
        /// <param name="type">The actual type.</param>
        /// <exception cref="ArgumentException">If <paramref name="type"/> is <see cref="ThisType"/>.</exception>
        public ThisType(Type type)
            : base(type)
        {
            if (type is ThisType)
                throw new ArgumentException("The type cannot be 'ThisType'.", nameof(type));
        }

        /// <summary>
        /// Check if other <see cref="ThisType"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="ThisType"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(ThisType? other)
        {
            return other != null && GetActualType().Equals(other.GetActualType());
        }

        /// <summary>
        /// Check if object instance is <see cref="ThisType"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="ThisType"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as ThisType);
        }

        /// <summary>
        /// Check if <see cref="Type"/> is <see cref="ThisType"/> and equal to this.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns><c>true</c> if <paramref name="type"/> is <see cref="ThisType"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(Type? type)
        {
            return type != null && type is ThisType other && Equals(other);
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
