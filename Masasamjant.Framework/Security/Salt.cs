using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    /// <summary>
    /// Represents cryptography salt.
    /// </summary>
    public sealed class Salt : IEquatable<Salt>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Salt"/> class from new <see cref="Guid"/> using specified <see cref="IStringHashProvider"/>.
        /// </summary>
        /// <param name="provider">The <see cref="IStringHashProvider"/>.</param>
        public Salt(IStringHashProvider provider) 
            : this(Guid.NewGuid().ToString(), provider)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="Salt"/> class from specified value using specified <see cref="IStringHashProvider"/>.
        /// </summary>
        /// <param name="value">The clear value.</param>
        /// <param name="hashProvider">The <see cref="IStringHashProvider"/>.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="value"/> is empty or only whitespace.</exception>
        public Salt(string value, IStringHashProvider hashProvider)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(value), "The value is empty or only whitespace.");

            Value = hashProvider.CreateHash(value);
        }

        /// <summary>
        /// Gets the value as base-64 string.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets <see cref="Value"/> as bytes.
        /// </summary>
        /// <returns>A hash bytes.</returns>
        public byte[] ToBytes()
            => Convert.FromBase64String(Value);

        /// <summary>
        /// Creates <see cref="Salt"/> instance using SHA1 hash algorithm.
        /// </summary>
        /// <param name="value">The clear value.</param>
        /// <returns>A <see cref="Salt"/> with SHA1 base-64 coded value.</returns>
        /// <exception cref="ArgumentNullException">If value <paramref name="value"/> is empty or only whitespace.</exception>
        public static Salt SHA1(string value) => new Salt(value, new Base64SHA1Provider());

        /// <summary>
        /// Creates <see cref="Salt"/> instance using SHA256 hash algorithm.
        /// </summary>
        /// <param name="value">The clear value.</param>
        /// <returns>A <see cref="Salt"/> with SHA256 base-64 coded value.</returns>
        /// <exception cref="ArgumentNullException">If value <paramref name="value"/> is empty or only whitespace.</exception>
        public static Salt SHA256(string value) => new Salt(value, new Base64SHA256Provider());

        /// <summary>
        /// Check if specified <see cref="Salt"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="Salt"/>.</param>
        /// <returns><c>true</c> if this and <paramref name="other"/> has equal values; <c>false</c> otherwise.</returns>
        public bool Equals(Salt? other)
        {
            return other != null && string.Equals(Value, other.Value, StringComparison.Ordinal);
        }

        /// <summary>
        /// Check if specified object instance is <see cref="Salt"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="Salt"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Salt);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode() => Value.GetHashCode();

        /// <summary>
        /// Gets the string presentation; <see cref="Value"/>.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Value;
        }
    }
}
