using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents <see cref="IEqualityComparer{DateTime}"/> that supports ignoring or not ignoring <see cref="DateTime.Kind"/> in comparison.
    /// </summary>
    public class DateTimeEqualityComparer : IEqualityComparer<DateTime>
    {
        /// <summary>
        /// Initializes new default instance of the <see cref="DateTimeEqualityComparer"/> class 
        /// where <see cref="DateTime.Kind"/> is ignored in comparison.
        /// </summary>
        public DateTimeEqualityComparer()
            : this(true)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DateTimeEqualityComparer"/> class.
        /// </summary>
        /// <param name="ignoreKind"><c>true</c> if <see cref="DateTime.Kind"/> should be ignored in comparison; <c>false</c> otherwise.</param>
        public DateTimeEqualityComparer(bool ignoreKind)
        {
            IgnoreDateTimeKind = ignoreKind;
        }

        /// <summary>
        /// Gets whether or not <see cref="DateTime.Kind"/> should be ignored in comparison.
        /// </summary>
        public bool IgnoreDateTimeKind { get; }

        /// <summary>
        /// Check if <paramref name="x"/> and <paramref name="y"/> are equal.
        /// </summary>
        /// <param name="x">The first <see cref="DateTime"/>.</param>
        /// <param name="y">The second <see cref="DateTime"/>.</param>
        /// <returns><c>true</c> if <paramref name="x"/> and <paramref name="y"/> are considered as equal; <c>false</c> otherwise.</returns>
        public virtual bool Equals(DateTime x, DateTime y)
        {
            if (!IgnoreDateTimeKind && x.Kind != y.Kind)
                return false;

            return x.Year == y.Year && x.Month == y.Month && x.Day == y.Day && EqualTimes(x, y);
        }

        /// <summary>
        /// Gets hash code for specified <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="obj">The <see cref="DateTime"/>.</param>
        /// <returns>A hash code.</returns>
        public virtual int GetHashCode([DisallowNull] DateTime obj)
        {
            int code = HashCode.Combine(obj.Year, obj.Month, obj.Day) ^ GetTimeHashCode(obj);

            if (!IgnoreDateTimeKind)
                code ^= obj.Kind.GetHashCode();

            return code;
        }

        /// <summary>
        /// Check if times of <paramref name="x"/> and <paramref name="y"/> are equal. Default implementation 
        /// checks equality to the milliseconds, but derived classes can override to change time comparison.
        /// </summary>
        /// <param name="x">The first <see cref="DateTime"/>.</param>
        /// <param name="y">The second <see cref="DateTime"/>.</param>
        /// <returns><c>true</c> if <paramref name="x"/> and <paramref name="y"/> has equal times; <c>false</c> otherwise.</returns>
        protected virtual bool EqualTimes(DateTime x, DateTime y)
        {
            return x.Hour == y.Hour && x.Minute == y.Minute && x.Second == y.Second && x.Millisecond == y.Millisecond;
        }

        /// <summary>
        /// Gets hash code for time of specified <see cref="DateTime"/> value. Default implementation 
        /// calculates hash code to the milliseconds, but derived classes can override to change hash calculation.
        /// </summary>
        /// <remarks>Hash should be calculated based on same time components as equality in <see cref="EqualTimes(DateTime, DateTime)"/>.</remarks>
        /// <param name="obj">The <see cref="DateTime"/>.</param>
        /// <returns>A hash code for time of <paramref name="obj"/>.</returns>
        protected virtual int GetTimeHashCode(DateTime obj)
        {
            return HashCode.Combine(obj.Hour, obj.Minute, obj.Second, obj.Millisecond);
        }
    }
}
