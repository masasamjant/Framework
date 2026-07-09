namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to validate that <see cref="DateTime"/> or <see cref="DateTimeOffset"/> value is in the past.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class PastDateTimeAttribute : NullableValidationAttribute
    {
        private int? offsetMinutes;

        /// <summary>
        /// Gets whether or not validation context is required.
        /// </summary>
        public override bool RequiresValidationContext => false;

        /// <summary>
        /// Gets or sets offset minutes of how many minutes time must be in future.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">If attempt to set value less than zero.</exception>
        public int? OffsetMinutes
        {
            get { return offsetMinutes; }
            set
            {
                if (value.HasValue && value.Value < 0)
                    throw new ArgumentOutOfRangeException(nameof(OffsetMinutes), value, "The offset minutes cannot be negative.");

                offsetMinutes = value;
            }
        }

        /// <summary>
        /// Validates that value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            var type = value.GetType();

            if (type.Equals(typeof(DateTime)))
                return IsPast((DateTime)value, OffsetMinutes, AllowNullable);

            if (type.Equals(typeof(DateTime?)))
                return IsPast((DateTime?)value, OffsetMinutes, AllowNullable);

            if (type.Equals(typeof(DateTimeOffset)))
                return IsPast((DateTimeOffset)value, OffsetMinutes, AllowNullable);

            if (type.Equals(typeof(DateTimeOffset?)))
                return IsPast((DateTimeOffset?)value, OffsetMinutes, AllowNullable);

            return true;
        }

        private static bool IsPast(DateTime? value, int? offsetMinutes, bool allowNullable)
        {
            if (!value.HasValue)
                return allowNullable;

            if (offsetMinutes.HasValue && offsetMinutes.Value > 0)
                return DateTimeHelper.IsPast(value.Value, TimeSpan.FromMinutes(offsetMinutes.Value));

            return DateTimeHelper.IsPast(value.Value);
        }

        private static bool IsPast(DateTimeOffset? value, int? offsetMinutes, bool allowNullable)
        {
            if (!value.HasValue)
                return allowNullable;

            if (offsetMinutes.HasValue && offsetMinutes.Value > 0)
                return DateTimeOffsetHelper.IsPast(value.Value, TimeSpan.FromMinutes(offsetMinutes.Value));

            return DateTimeOffsetHelper.IsPast(value.Value);
        }
    }
}
