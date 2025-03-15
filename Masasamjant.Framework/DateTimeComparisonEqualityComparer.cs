using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents <see cref="DateTimeEqualityComparer"/> that use <see cref="DateTimeComparison"/> in comparison.
    /// </summary>
    public class DateTimeComparisonEqualityComparer : DateTimeEqualityComparer
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DateTimeComparisonEqualityComparer"/> class that will 
        /// ignore <see cref="DateTime.Kind"/> in comparison.
        /// </summary>
        /// <param name="comparison">The <see cref="DateTimeComparison"/>.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="comparison"/> is not defined in <see cref="DateTimeComparison"/>.</exception>
        public DateTimeComparisonEqualityComparer(DateTimeComparison comparison)
            : this(comparison, true)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DateTimeComparisonEqualityComparer"/> class.
        /// </summary>
        /// <param name="comparison">The <see cref="DateTimeComparison"/>.</param>
        /// <param name="ignoreKind"><c>true</c> if <see cref="DateTime.Kind"/> should be ignored in comparison; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="comparison"/> is not defined in <see cref="DateTimeComparison"/>.</exception>
        public DateTimeComparisonEqualityComparer(DateTimeComparison comparison, bool ignoreKind)
            : base(ignoreKind)
        {
            if (!Enum.IsDefined(comparison))
                throw new ArgumentException("The value is not defined.", nameof(comparison));

            Comparison = comparison;
        }

        /// <summary>
        /// Gets the <see cref="DateTimeComparison"/> used when comparison values.
        /// </summary>
        public DateTimeComparison Comparison { get; }

        /// <summary>
        /// Check if <paramref name="x"/> and <paramref name="y"/> are equal using <see cref="Comparison"/>.
        /// </summary>
        /// <param name="x">The first <see cref="DateTime"/>.</param>
        /// <param name="y">The second <see cref="DateTime"/>.</param>
        /// <returns><c>true</c> if <paramref name="x"/> and <paramref name="y"/> are considered as equal; <c>false</c> otherwise.</returns>
        public override bool Equals(DateTime x, DateTime y)
        {
            if (!IgnoreDateTimeKind && x.Kind != y.Kind)
                return false;

            switch (Comparison)
            {
                case DateTimeComparison.Date:
                    return x.Year == y.Year && x.Month == y.Month && x.Day == y.Day;
                case DateTimeComparison.Time:
                    return EqualTimes(x, y);
                default:
                    return x.Year == y.Year && x.Month == y.Month && x.Day == y.Day && EqualTimes(x, y);
            }
        }

        /// <summary>
        /// Gets hash code for specified <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="obj">The <see cref="DateTime"/>.</param>
        /// <returns>A hash code.</returns>
        public override int GetHashCode([DisallowNull] DateTime obj)
        {
            int code;

            switch (Comparison)
            {
                case DateTimeComparison.Date:
                    code = HashCode.Combine(obj.Year, obj.Month, obj.Day);
                    break;
                case DateTimeComparison.Time:
                    code = GetTimeHashCode(obj);
                    break;
                default:
                    code = HashCode.Combine(obj.Year, obj.Month, obj.Day) ^ GetTimeHashCode(obj);
                    break;
            }

            if (!IgnoreDateTimeKind)
                code ^= obj.Kind.GetHashCode();

            return code;
        }
    }
}
