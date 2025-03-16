namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to <see cref="TimeSpan"/> values.
    /// </summary>
    public static class TimeSpanHelper
    {
        /// <summary>
        /// Check if specified <see cref="TimeSpan"/> represents zero.
        /// </summary>
        /// <param name="value">The <see cref="TimeSpan"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="value"/> represents zero; <c>false</c> otherwise.</returns>
        public static bool IsZero(this TimeSpan value)
            => value == TimeSpan.Zero;

        /// <summary>
        /// Create <see cref="TimeSpan"/> based on specified <see cref="TimeComponent"/> and value. If <paramref name="value"/> is 
        /// <see cref="double.NaN"/> or positive or negative infinity, then returns <see cref="TimeSpan"/> that represents infinity.
        /// </summary>
        /// <param name="component">The <see cref="TimeComponent"/>.</param>
        /// <param name="value">The value of <paramref name="component"/>.</param>
        /// <returns>A <see cref="TimeSpan"/> value.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="component"/> is not defined.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="value"/> is less than 0.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static TimeSpan CreateTimeSpan(this TimeComponent component, double value)
        {
            if (!Enum.IsDefined(component))
                throw new ArgumentException("The value is not defined.", nameof(component));

            if (double.IsNaN(value) || double.IsInfinity(value))
                return Timeout.InfiniteTimeSpan;

            if (value < 0D)
                throw new ArgumentOutOfRangeException(nameof(value), value, "The value must be greater than or equal to 0.");

            if (value == 0.0D)
                return TimeSpan.Zero;

            switch (component)
            {
                case TimeComponent.Hour: return TimeSpan.FromHours(value);
                case TimeComponent.Minute: return TimeSpan.FromMinutes(value);
                case TimeComponent.Second: return TimeSpan.FromSeconds(value);
                case TimeComponent.Millisecond: return TimeSpan.FromMilliseconds(value);
                case TimeComponent.Microsecond: return TimeSpan.FromMicroseconds(value);
                default:
                    throw new NotSupportedException($"The component '{component}' is not supported.");
            }
        }

        /// <summary>
        /// Create <see cref="TimeSpan"/> based on specified <see cref="DateTimeComponent"/> and value.
        /// </summary>
        /// <param name="component">The <see cref="DateTimeComponent"/>.</param>
        /// <param name="value">The value of <paramref name="component"/>.</param>
        /// <returns>A <see cref="TimeSpan"/> value.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="component"/> is not defined.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="value"/> is less than 0.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static TimeSpan CreateTimeSpan(this DateTimeComponent component, double value)
        {
            if (!Enum.IsDefined(component))
                throw new ArgumentException("The value is not defined.", nameof(component));

            if (double.IsNaN(value) || double.IsInfinity(value))
                return Timeout.InfiniteTimeSpan;

            if (value < 0D)
                throw new ArgumentOutOfRangeException(nameof(value), value, "The value must be greater than or equal to 0.");

            if (value == 0D)
                return TimeSpan.Zero;

            switch (component)
            {
                case DateTimeComponent.Day: return TimeSpan.FromDays(value);
                case DateTimeComponent.Hour: return TimeSpan.FromHours(value);
                case DateTimeComponent.Minute: return TimeSpan.FromMinutes(value);
                case DateTimeComponent.Second: return TimeSpan.FromSeconds(value);
                case DateTimeComponent.Millisecond: return TimeSpan.FromMilliseconds(value);
                case DateTimeComponent.Microsecond: return TimeSpan.FromMicroseconds(value);
                case DateTimeComponent.Month:
                    int months = Convert.ToInt32(value);
                    return FromMonths(months, DateTime.Today);
                case DateTimeComponent.Year:
                    int years = Convert.ToInt32(value);
                    return FromYears(years, DateTime.Today);
                default:
                    throw new NotSupportedException($"The component '{component}' is not supported.");
            }
        }

        /// <summary>
        /// Create <see cref="TimeSpan"/> from specified months using <see cref="DateTime.Today"/> as reference. 
        /// This is same as invoking <see cref="FromMonths(int, DateTime)"/> with <see cref="DateTime.Today"/>.
        /// </summary>
        /// <returns>A <see cref="TimeSpan"/> value from months.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="months"/> is less than 0.</exception>
        public static TimeSpan FromMonths(int months) => FromMonths(months, DateTime.Today);

        /// <summary>
        /// Create <see cref="TimeSpan"/> from specified months.
        /// </summary>
        /// <param name="months">The months.</param>
        /// <param name="refDate">The reference date.</param>
        /// <returns>A <see cref="TimeSpan"/> value from months.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="months"/> is less than 0.</exception>
        public static TimeSpan FromMonths(int months, DateTime refDate)
        {
            if (months < 0)
                throw new ArgumentOutOfRangeException(nameof(months), months, "The value must be greater than or equal to 0.");

            if (months == 0)
                return TimeSpan.Zero;
            else
            {
                var end = refDate.AddMonths(months);
                return end.Subtract(refDate);
            }
        }

        /// <summary>
        /// Create <see cref="TimeSpan"/> from specified years using <see cref="DateTime.Today"/> as reference. 
        /// This is same as invoking <see cref="FromYears(int, DateTime)"/> with <see cref="DateTime.Today"/>.
        /// </summary>
        /// <param name="years">The years.</param>
        /// <returns>A <see cref="TimeSpan"/> value from years.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="years"/> is less than 0.</exception>
        public static TimeSpan FromYears(int years) => FromYears(years, DateTime.Today);

        /// <summary>
        /// Create <see cref="TimeSpan"/> from specified years.
        /// </summary>
        /// <param name="years">The years.</param>
        /// <param name="refDate">The reference date.</param>
        /// <returns>A <see cref="TimeSpan"/> value from years.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="years"/> is less than 0.</exception>
        public static TimeSpan FromYears(int years, DateTime refDate)
        {
            if (years < 0)
                throw new ArgumentOutOfRangeException(nameof(years), years, "The value must be greater than or equal to 0.");

            if (years == 0)
                return TimeSpan.Zero;
            else
            {
                var end = refDate.AddYears(years);
                return end.Subtract(refDate);
            }
        }

        /// <summary>
        /// Checks if <see cref="TimeSpan"/> represents negative value.
        /// </summary>
        /// <param name="span">The <see cref="TimeSpan"/>.</param>
        /// <returns><c>true</c> if <paramref name="span"/> represents negative value; <c>false</c> otherwise.</returns>
        public static bool IsNegative(this TimeSpan span) => span.Ticks < 0;
    }
}
