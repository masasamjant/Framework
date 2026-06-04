using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents a date time value with a specified accuracy.
    /// </summary>
    public readonly struct AccurateDateTime : IEquatable<AccurateDateTime>
    {
        /// <summary>
        /// Initializes new instance of <see cref="AccurateDateTime"/> with the specified value and accuracy.
        /// </summary>
        /// <param name="value">The date time value.</param>
        /// <param name="accuracy">The accuracy of the date time value.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="accuracy"/> is not defined.</exception>
        public AccurateDateTime(DateTime value, DateTimeAccuracy accuracy)
        {
            if (!Enum.IsDefined(accuracy))
                throw new ArgumentException("The value is not defined.", nameof(accuracy));

            Value = value;
            Accuracy = accuracy;
        }

        /// <summary>
        /// Gets the date time value. 
        /// The accuracy of this value is determined by the <see cref="Accuracy"/> property.
        /// </summary>
        public DateTime Value { get; }

        /// <summary>
        /// Gets how accurate the <see cref="Value"/> is. 
        /// For example, if the accuracy is <see cref="DateTimeAccuracy.Day"/>, 
        /// the time part of the <see cref="Value"/> should be ignored.
        /// </summary>
        public DateTimeAccuracy Accuracy { get; }

        /// <summary>
        /// Check if other <see cref="AccurateDateTime"/> is equal with this.
        /// </summary>
        /// <param name="other">The other <see cref="AccurateDateTime"/> to compare.</param>
        /// <returns><c>true</c> if the other <see cref="AccurateDateTime"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(AccurateDateTime other) 
            => Value == other.Value && Accuracy == other.Accuracy;

        /// <summary>
        /// Check if object instance is <see cref="AccurateDateTime"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object to compare.</param>
        /// <returns><c>true</c> if the object is <see cref="AccurateDateTime"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            return obj is AccurateDateTime other && Equals(other);
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
        /// Operator to compare if two <see cref="AccurateDateTime"/> instances are equal.
        /// </summary>
        /// <param name="left">The first <see cref="AccurateDateTime"/> to compare.</param>
        /// <param name="right">The second <see cref="AccurateDateTime"/> to compare.</param>
        /// <returns><c>true</c> if the two <see cref="AccurateDateTime"/> instances are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(AccurateDateTime left, AccurateDateTime right) => left.Equals(right);

        /// <summary>
        /// Operator to compare if two <see cref="AccurateDateTime"/> instances are not equal.
        /// </summary>
        /// <param name="left">The first <see cref="AccurateDateTime"/> to compare.</param>
        /// <param name="right">The second <see cref="AccurateDateTime"/> to compare.</param>
        /// <returns><c>true</c> if the two <see cref="AccurateDateTime"/> instances are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(AccurateDateTime left, AccurateDateTime right) => !left.Equals(right);

        /// <summary>
        /// Operator to implicitly convert <see cref="AccurateDateTime"/> to <see cref="DateTime"/>.
        /// </summary>
        /// <param name="accurateDateTime">The <see cref="AccurateDateTime"/> to convert.</param>
        public static implicit operator DateTime(AccurateDateTime accurateDateTime) => accurateDateTime.Value;
    }
}
