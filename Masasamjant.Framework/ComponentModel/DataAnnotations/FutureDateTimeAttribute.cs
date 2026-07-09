namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to validate that <see cref="DateTime"/> or <see cref="DateTimeOffset"/> value is in future.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class FutureDateTimeAttribute : NullableValidationAttribute
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
                return IsFuture((DateTime)value, OffsetMinutes, AllowNullable);

            if (type.Equals(typeof(DateTime?)))
                return IsFuture((DateTime?)value, OffsetMinutes, AllowNullable);

            if (type.Equals(typeof(DateTimeOffset)))
                return IsFuture((DateTimeOffset)value, OffsetMinutes, AllowNullable);

            if (type.Equals(typeof(DateTimeOffset?)))
                return IsFuture((DateTimeOffset?)value, OffsetMinutes, AllowNullable);

            return true;
        }

        private static bool IsFuture(DateTime? value, int? offset, bool allowNullable)
        {
            if (!value.HasValue)
                return allowNullable;

            if (offset.HasValue && offset.Value > 0)
                return DateTimeHelper.IsFuture(value.Value, TimeSpan.FromMinutes(offset.Value));

            return DateTimeHelper.IsFuture(value.Value);
        }

        private static bool IsFuture(DateTimeOffset? value, int? offset, bool allowNullable)
        {
            if (!value.HasValue)
                return allowNullable;
            
            if (offset.HasValue && offset.Value > 0)
                return DateTimeOffsetHelper.IsFuture(value.Value, TimeSpan.FromMinutes(offset.Value));
            
            return DateTimeOffsetHelper.IsFuture(value.Value);
        }
    }
}
