using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents pair of min and max between two values of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the comparable.</typeparam>
    public readonly struct MinMaxPair<T> : IEquatable<MinMaxPair<T>>
        where T: struct, IComparable<T>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="MinMaxPair{T}"/> value.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        public MinMaxPair(T x, T y)
        {
            Min = ComparableHelper.Min(x, y);
            Max = ComparableHelper.Max(x, y);
        }

        /// <summary>
        /// Gets the minimum value.
        /// </summary>
        public T Min { get; }

        /// <summary>
        /// Gets the maximum value.
        /// </summary>
        public T Max { get; }

        /// <summary>
        /// Check if other <see cref="MinMaxPair{T}"/> has same min and max value with this.
        /// </summary>
        /// <param name="other">The other <see cref="MinMaxPair{T}"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> has same min and value value; <c>false</c> otherwise.</returns>
        public bool Equals(MinMaxPair<T> other)
        {
            return Min.Equals(other.Min) &&
                   Max.Equals(other.Max);
        }

        /// <summary>
        /// Check if object instance is <see cref="MinMaxPair{T}"/> and equal with this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="MinMaxPair{T}"/> and equal with this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is MinMaxPair<T> other)
                return Equals(other);

            return false;
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code value.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Min, Max);
        }

        /// <summary>
        /// Get string presentation.
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            return $"Min={Min}, Max={Max}";
        }

        /// <summary>
        /// Check if <paramref name="left"/> and <paramref name="right"/> are equal.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(MinMaxPair<T> left, MinMaxPair<T> right) => left.Equals(right);

        /// <summary>
        /// Check if <paramref name="left"/> and <paramref name="right"/> are not equal.
        /// </summary>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(MinMaxPair<T> left, MinMaxPair<T> right) => !(left == right);
    }
}
