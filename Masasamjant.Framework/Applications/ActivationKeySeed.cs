using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Applications
{
    /// <summary>
    /// Represents seed of the activation key.
    /// </summary>
    public readonly struct ActivationKeySeed : IEquatable<ActivationKeySeed>
    {
        /// <summary>
        /// Initializes new value of <see cref="ActivationKeySeed"/>.
        /// </summary>
        /// <param name="value">The seed value.</param>
        /// <exception cref="ArgumentException">If <paramref name="value"/> is not integral number.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="value"/> is not positive number.</exception>
        public ActivationKeySeed(decimal value)
        {
            if (!decimal.IsInteger(value))
                throw new ArgumentException("The value must be integral number.", nameof(value));

            if (!decimal.IsPositive(value))
                throw new ArgumentOutOfRangeException(nameof(value), value, "The value must be positive number.");
            Value = value;
        }

        /// <summary>
        /// Gets the seed value.
        /// </summary>
        public decimal Value { get; }

        /// <summary>
        /// Check if other activation key seed is equal to this meaning it has same value.
        /// </summary>
        /// <param name="other">The other activation key seed.</param>
        /// <returns><c>true</c> if <paramref name="other"/> and this has same value; <c>false</c> otherwise.</returns>
        public bool Equals(ActivationKeySeed other) => Value == other.Value;

        /// <summary>
        /// Check if object instance is activation key seed and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is activation key seed and has same value with this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is ActivationKeySeed other)
                return Equals(other);

            return false;
        }

        /// <summary>
        /// Gets hash code value.
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        /// <summary>
        /// Gets the next activation key seed.
        /// </summary>
        /// <returns>A next activation key seed.</returns>
        public ActivationKeySeed Next()
            => new ActivationKeySeed(Value + 1);

        /// <summary>
        /// Check if <paramref name="left"/> and <paramref name="right"/> are equal.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(ActivationKeySeed left, ActivationKeySeed right) => left.Equals(right);

        /// <summary>
        /// Check if <paramref name="left"/> and <paramref name="right"/> are not equal.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(ActivationKeySeed left, ActivationKeySeed right) => !(left == right);
    }
}
