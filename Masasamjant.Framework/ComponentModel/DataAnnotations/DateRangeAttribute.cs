namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to validate that date part of <see cref="DateTime"/> or <see cref="DateTimeOffset"/> is within a specified date range.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class DateRangeAttribute : NullableValidationAttribute
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DateRangeAttribute"/> with specified begin and end dates.
        /// </summary>
        /// <param name="begin">The beginning date of the range.</param>
        /// <param name="end">The ending date of the range.</param>
        public DateRangeAttribute(DateTime begin, DateTime end)
            : this(new DateRange(begin, end))
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="DateRangeAttribute"/> with specified <see cref="DateRange"/>.
        /// </summary>
        /// <param name="range">The date range to validate against.</param>
        public DateRangeAttribute(DateRange range)
        {
            Range = range;
        }

        /// <summary>
        /// Gets the date range.
        /// </summary>
        public DateRange Range { get; }

        /// <summary>
        /// Gets whether or not requires validation context.
        /// </summary>
        public override bool RequiresValidationContext => false;

        /// <summary>
        /// Validates that <paramref name="value"/>, if <see cref="DateTime"/> or <see cref="DateTimeOffset"/>, falls within the specified date range.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            var type = value.GetType();

            if (type.Equals(typeof(DateTime)))
                return Range.Contains(((DateTime)value).Date);
            else if (type.Equals(typeof(DateTimeOffset)))
                return Range.Contains(((DateTimeOffset)value).Date);
            else if (type.Equals(typeof(DateTime?)))
            {
                DateTime? dt = (DateTime?)value;
                return dt.HasValue ? Range.Contains(dt.Value.Date) : AllowNullable;
            }
            else if (type.Equals(typeof(DateTimeOffset?)))
            {
                DateTimeOffset? dt = (DateTimeOffset?)value;
                return dt.HasValue ? Range.Contains(dt.Value.Date) : AllowNullable;
            }

            return true;
        }
    }
}
