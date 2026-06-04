using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents a date time value with a specified accuracy.
    /// </summary>
    public readonly struct AccurateDateTimeOffset : IEquatable<AccurateDateTimeOffset>
    {
        /// <summary>
        /// Initializes new instance of <see cref="AccurateDateTimeOffset"/> with the specified value and accuracy.
        /// </summary>
        /// <param name="value">The date time value.</param>
        /// <param name="accuracy">The accuracy of the date time value.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="accuracy"/> is not defined.</exception>
        public AccurateDateTimeOffset(DateTimeOffset value, DateTimeAccuracy accuracy)
        {
            if (!Enum.IsDefined(accuracy))
                throw new ArgumentException("The value is not defined.", nameof(accuracy));
            
            Value = value;
            Accuracy = accuracy;
        }

        /// <summary>
        /// Gets the date time offset value. 
        /// The accuracy of this value is determined by the <see cref="Accuracy"/> property.
        /// </summary>
        public DateTimeOffset Value { get; }

        /// <summary>
        /// Gets how accurate the <see cref="Value"/> is. 
        /// For example, if the accuracy is <see cref="DateTimeAccuracy.Day"/>, 
        /// the time part of the <see cref="Value"/> should be ignored.
        /// </summary>
        public DateTimeAccuracy Accuracy { get; }

        /// <summary>
        /// Check if other <see cref="AccurateDateTimeOffset"/> is equal with this.
        /// </summary>
        /// <param name="other">The other <see cref="AccurateDateTimeOffset"/> to compare.</param>
        /// <returns><c>true</c> if the other <see cref="AccurateDateTimeOffset"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(AccurateDateTimeOffset other)
        {
            return Value.Equals(other.Value) && Accuracy == other.Accuracy;
        }

        /// <summary>
        /// Check if object instance is <see cref="AccurateDateTimeOffset"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns><c>true</c> if the object is <see cref="AccurateDateTimeOffset"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is AccurateDateTimeOffset other && Equals(other);
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Value, Accuracy);
        }

        /// <summary>
        /// Operator to compare if two <see cref="AccurateDateTimeOffset"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="AccurateDateTimeOffset"/> to compare.</param>
        /// <param name="right">The second <see cref="AccurateDateTimeOffset"/> to compare.</param>
        /// <returns><c>true</c> if the two <see cref="AccurateDateTimeOffset"/> instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(AccurateDateTimeOffset left, AccurateDateTimeOffset right)
        {
            return left.Equals(right);
        }

        /// <summary>
        /// Operator to compare if two <see cref="AccurateDateTimeOffset"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="AccurateDateTimeOffset"/> to compare.</param>
        /// <param name="right">The second <see cref="AccurateDateTimeOffset"/> to compare.</param>
        /// <returns><c>true</c> if the two <see cref="AccurateDateTimeOffset"/> instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(AccurateDateTimeOffset left, AccurateDateTimeOffset right)
        {
            return !left.Equals(right);
        }

        /// <summary>
        /// Operator to implicitly convert <see cref="AccurateDateTimeOffset"/> to <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="accurateDateTimeOffset">The <see cref="AccurateDateTimeOffset"/> to convert.</param>
        public static implicit operator DateTimeOffset(AccurateDateTimeOffset value)
            => value.Value;
    }
}
