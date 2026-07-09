namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to validate that date part of <see cref="DateTime"/> or <see cref="DateTimeOffset"/> is specified date.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class RequiredDateAttribute : NullableValidationAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredDateAttribute"/> class.
        /// </summary>
        /// <param name="requiredDate">The required date.</param>
        /// <exception cref="ArgumentException">If value of <paramref name="requiredDate"/> is not defined.</exception>
        public RequiredDateAttribute(RequiredDate requiredDate)
        {
            if (!Enum.IsDefined(requiredDate))
                throw new ArgumentException("The value is not defined.", nameof(requiredDate));

            RequiredDate = requiredDate;
        }

        /// <summary>
        /// Gets whether or not validation context is required.
        /// </summary>
        public override bool RequiresValidationContext => false;

        /// <summary>
        /// Gets what date the date part of <see cref="DateTime"/> or <see cref="DateTimeOffset"/> is required to be.
        /// </summary>
        public RequiredDate RequiredDate { get; }

        /// <summary>
        /// Validates that value is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <returns><c>true</c> if the value is valid; otherwise, <c>false</c>.</returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
                return false;

            var type = value.GetType();

            if (type.Equals(typeof(DateTime)))
                return IsTodaysDate((DateTime)value);

            if (type.Equals(typeof(DateTime?)))
                return IsTodaysDate((DateTime?)value);

            if (type.Equals(typeof(DateTimeOffset)))
                return IsTodaysDate((DateTimeOffset)value);

            if (type.Equals(typeof(DateTimeOffset?)))
                return IsTodaysDate((DateTimeOffset?)value);

            return true;
        }

        private bool IsTodaysDate(DateTime? value)
        {
            if (value.HasValue)
            {
                switch (RequiredDate)
                {
                    case RequiredDate.Today:
                        return DateTimeHelper.IsTodaysDate(value.Value);
                    case RequiredDate.Yesterday:
                        return DateTimeHelper.IsYesterdaysDate(value.Value);
                    case RequiredDate.Tomorrow:
                        return DateTimeHelper.IsTomorrowsDate(value.Value);
                    default:
                        return false;
                }
            }

            return AllowNullable;
        }

        private bool IsTodaysDate(DateTimeOffset? value)
        {
            if (value.HasValue)
            {
                switch (RequiredDate)
                {
                    case RequiredDate.Today:
                        return DateTimeHelper.IsTodaysDate(value.Value.Date);
                    case RequiredDate.Yesterday:
                        return DateTimeHelper.IsYesterdaysDate(value.Value.Date);
                    case RequiredDate.Tomorrow:
                        return DateTimeHelper.IsTomorrowsDate(value.Value.Date);
                    default:
                        return false;
                }
            }

            return AllowNullable;
        }
    }
}
