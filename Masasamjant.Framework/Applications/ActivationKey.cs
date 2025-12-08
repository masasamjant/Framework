namespace Masasamjant.Applications
{
    /// <summary>
    /// Represents activation key.
    /// </summary>
    public sealed class ActivationKey : IEquatable<ActivationKey>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ActivationKey"/> class.
        /// </summary>
        /// <param name="value">The activation key value.</param>
        /// <param name="seed">The seed that was used to generate key.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="value"/> is empty or only whitespace.</exception>
        public ActivationKey(string value, ActivationKeySeed seed)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "The value cannot be empty or only whitespace.");

            Value = value;
            Seed = seed;
        }

        /// <summary>
        /// Gets the activation key value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets the seed that was used to generate this activation key.
        /// </summary>
        public ActivationKeySeed Seed { get; }

        /// <summary>
        /// Check if other activation key is equal to this.
        /// </summary>
        /// <param name="other">The other activation key.</param>
        /// <returns><c>true</c> if <paramref name="other"/> has same value and seed with this; <c>false</c> otherwise.</returns>
        public bool Equals(ActivationKey? other)
        {
            if (other is null)
                return false;

            return string.Equals(Value, other.Value, StringComparison.Ordinal) && Seed.Equals(other.Seed);
        }

        /// <summary>
        /// Check if object instance is activation key and equal with this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is activation key and equal with this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as ActivationKey);
        }

        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Seed);
        }

        /// <summary>
        /// Gets string presentation.
        /// </summary>
        /// <returns>A <see cref="Value"/>.</returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
