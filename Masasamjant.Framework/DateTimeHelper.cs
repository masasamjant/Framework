using Masasamjant.Configuration;
using System.Globalization;

namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to <see cref="DateTime"/> values.
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        /// How many days is in one week.
        /// </summary>
        public const int DaysInWeek = 7;

        /// <summary>
        /// Number of first quarter of year.
        /// </summary>
        public const int FirstQuarter = 1;

        /// <summary>
        /// Number of second quarter of year.
        /// </summary>
        public const int SecondQuarter = 2;

        /// <summary>
        /// Number of third quarter of year.
        /// </summary>
        public const int ThirdQuarter = 3;

        /// <summary>
        /// Number of fourth quarter of year.
        /// </summary>
        public const int FourthQuarter = 4;

        /// <summary>
        /// Gets <see cref="DateTime"/> representing tomorrows date.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> of tomorrows date.</returns>
        public static DateTime GetTomorrow()
        {
            var today = GetToday();

            if (today.Date.Equals(DateTime.MaxValue.Date))
                return today;

            return today.AddDays(1);
        }

        /// <summary>
        /// Gets <see cref="DateTime"/> representing yesterdays date.
        /// </summary>
        /// <returns>A <see cref="DateTime"/> of yesterdays date.</returns>
        public static DateTime GetYesterday()
        {
            var today = GetToday();

            if (today.Date.Equals(DateTime.MaxValue.Date))
                return today;

            return today.AddDays(-1);
        }

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is <see cref="DateTime.Today"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is <see cref="DateTime.Today"/>; <c>false</c> otherwise.</returns>
        public static bool IsToday(this DateTime datetime)
        {
            return GetToday().Equals(datetime);
        }

        /// <summary>
        /// Check if date of specified <see cref="DateTime"/> is <see cref="DateTime.Today"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if date of <paramref name="datetime"/> is <see cref="DateTime.Today"/>; <c>false</c> otherwise.</returns>
        public static bool IsTodaysDate(this DateTime datetime)
        {
            return GetToday().Equals(datetime.Date);
        }

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is tomorrows date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is tomorrows date; <c>false</c> otherwise.</returns>
        public static bool IsTomorrow(this DateTime datetime)
        {
            return GetTomorrow().Equals(datetime);
        }

        /// <summary>
        /// Check if date of specified <see cref="DateTime"/> is tomorrows date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if date of <paramref name="datetime"/> is tomorrows date; <c>false</c> otherwise.</returns>
        public static bool IsTomorrowsDate(this DateTime datetime)
        {
            return GetTomorrow().Equals(datetime.Date);
        }

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is yesterdays date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is yesterdays date; <c>false</c> otherwise.</returns>
        public static bool IsYesterday(this DateTime datetime)
        {
            return GetYesterday().Equals(datetime);
        }

        /// <summary>
        /// Check if date of specified <see cref="DateTime"/> is yesterdays date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if date of <paramref name="datetime"/> is yesterdays date; <c>false</c> otherwise.</returns>
        public static bool IsYesterdaysDate(this DateTime datetime)
        {
            return GetYesterday().Equals(datetime.Date);
        }

        /// <summary>
        /// Gets minimum value between specified values.
        /// </summary>
        /// <param name="left">The left <see cref="DateTime"/>.</param>
        /// <param name="right">The right <see cref="DateTime"/>.</param>
        /// <returns>A minimum value between <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static DateTime Min(DateTime left, DateTime right)
            => ComparableHelper.Min(left, right);

        /// <summary>
        /// Gets maximum value between specified values.
        /// </summary>
        /// <param name="left">The left <see cref="DateTime"/>.</param>
        /// <param name="right">The right <see cref="DateTime"/>.</param>
        /// <returns>A maximum value between <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static DateTime Max(DateTime left, DateTime right)
            => ComparableHelper.Max(left, right);

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in future.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTime datetime) => IsFuture(datetime, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in future using specified time offset. For example if <paramref name="offset"/> is 5 minutes, 
        /// then <paramref name="datetime"/> must be at least 5 minutes ahead of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be ahead to be in future.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTime datetime, TimeSpan offset)
        {
            var futureTime = GetNow(datetime.Kind);
            return IsFuture(datetime, futureTime, offset);
        }

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in future using current time provided by specified <see cref="IDateTimeConfiguration"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTime datetime, IDateTimeConfiguration configuration) => IsFuture(datetime, configuration, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in future using current time provided by specified <see cref="IDateTimeConfiguration"/> and using 
        /// specified time offset. For example if <paramref name="offset"/> is 5 minutes, then <paramref name="datetime"/> must be at least 5 minutes ahead of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be ahead to be in future.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTime datetime, IDateTimeConfiguration configuration, TimeSpan offset)
        {
            var futureTime = configuration.GetDateTimeNow();
            return IsFuture(datetime, futureTime, offset);
        }

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in past.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTime datetime) => IsPast(datetime, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in past using specified time offset. For example if <paramref name="offset"/> is 5 minutes, 
        /// then <paramref name="datetime"/> must be at least 5 minutes behind of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be behind to be in past.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTime datetime, TimeSpan offset)
        {
            var currentTime = GetNow(datetime.Kind);
            return IsPast(datetime, currentTime, offset);
        }

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in past using current time provided by specified <see cref="IDateTimeConfiguration"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTime datetime, IDateTimeConfiguration configuration) => IsPast(datetime, configuration, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTime"/> is in past using current time provided by specified <see cref="IDateTimeConfiguration"/> and using 
        /// specified time offset. For example if <paramref name="offset"/> is 5 minutes, then <paramref name="datetime"/> must be at least 5 minutes behind of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be behind to be in past.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTime datetime, IDateTimeConfiguration configuration, TimeSpan offset)
        {
            var currentTime = configuration.GetDateTimeNow();
            return IsPast(datetime, currentTime, offset);
        }

        /// <summary>
        /// Check value of <paramref name="datetime"/> is near to <paramref name="compare"/> using specified time offset. If <paramref name="offset"/> is zero, 
        /// then <paramref name="datetime"/> must be equal to <paramref name="compare"/>. If <paramref name="offset"/> is for example 3 seconds, then difference 
        /// between <paramref name="datetime"/> and <paramref name="compare"/>, must be less or equal to offset.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/> to check.</param>
        /// <param name="compare">The <see cref="DateTime"/> to compare to.</param>
        /// <param name="offset">The offset of values considered to be near.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is near <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsNear(this DateTime datetime, DateTime compare, TimeSpan offset)
        {
            if (offset.IsZero())
                return datetime.Equals(compare);
            else
            {
                if (datetime.Equals(compare))
                    return true;

                var min = Min(datetime, compare);
                var max = Max(datetime, compare);
                var diff = max - min;
                return diff <= offset.Duration();
            }
        }

        /// <summary>
        /// Gets how many days are left in current month.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> or <c>null</c> to use current culture.</param>
        /// <returns>A count of days left in current month.</returns>
        public static int DaysLeftInThisMonth(CultureInfo? culture = null) => DaysLeftInMonth(GetToday(), culture);

        /// <summary>
        /// Gets how many days are left in month of specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> or <c>null</c> to use current culture.</param>
        /// <returns>A count of days left in month of <paramref name="datetime"/>.</returns>
        public static int DaysLeftInMonth(this DateTime datetime, CultureInfo? culture = null)
        {
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            int month = culture.Calendar.GetMonth(datetime);
            int year = culture.Calendar.GetYear(datetime);
            int daysInMonth = culture.Calendar.GetDaysInMonth(year, month);
            return daysInMonth - culture.Calendar.GetDayOfMonth(datetime);
        }

        /// <summary>
        /// Gets how many days are left in current year.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> or <c>null</c> to use current culture.</param>
        /// <returns>A count of days left in current year.</returns>
        public static int DaysLeftInThisYear(CultureInfo? culture = null) => DaysLeftInYear(GetToday(), culture);

        /// <summary>
        /// Gets how many days are left in year of specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> or <c>null</c> to use current culture.</param>
        /// <returns>A count of days left in year of <paramref name="datetime"/>.</returns>
        public static int DaysLeftInYear(this DateTime datetime, CultureInfo? culture = null)
        {
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            int year = culture.Calendar.GetYear(datetime);
            int daysInYear = culture.Calendar.GetDaysInYear(year);
            return daysInYear - culture.Calendar.GetDayOfYear(datetime);
        }

        /// <summary>
        /// Gets how many days are left in current week.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> or <c>null</c> to use current culture.</param>
        /// <returns>A count of days left in current week.</returns>
        public static int DaysLeftInThisWeek(CultureInfo? culture = null) => DaysLeftInWeek(GetToday(), culture);

        /// <summary>
        /// Gets how many days are left in week of specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> or <c>null</c> to use current culture.</param>
        /// <returns>A count of days left in week of <paramref name="datetime"/>.</returns>
        public static int DaysLeftInWeek(this DateTime datetime, CultureInfo? culture = null)
        {
            if (culture == null)
                culture = CultureInfo.CurrentCulture;

            var weekDay = culture.Calendar.GetDayOfWeek(datetime);

            if (culture.DateTimeFormat.FirstDayOfWeek == DayOfWeek.Sunday)
                return DaysInWeek - ((int)weekDay) - 1;
            else
            {
                int value = weekDay == DayOfWeek.Sunday ? 7 : (int)weekDay;
                return DaysInWeek - value;
            }
        }

        /// <summary>
        /// Check if <paramref name="left"/> and <paramref name="right"/> are considered as equal using specified <see cref="DateTimeComparison"/>. 
        /// This is helper method that use <see cref="DateTimeComparisonEqualityComparer"/> to perform comparison.
        /// </summary>
        /// <param name="left">The left <see cref="DateTime"/>.</param>
        /// <param name="right">The right <see cref="DateTime"/>.</param>
        /// <param name="comparison">The <see cref="DateTimeComparison"/>. Default is <see cref="DateTimeComparison.DateTime"/>.</param>
        /// <param name="ignoreKind"><c>true</c>, default, to ignore <see cref="DateTime.Kind"/> in comparison; <c>false</c> otherwise.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are considered as equal; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="comparison"/> is not defined in <see cref="DateTimeComparison"/>.</exception>
        public static bool AreEqual(DateTime left, DateTime right, DateTimeComparison comparison = DateTimeComparison.DateTime, bool ignoreKind = true)
        {
            var comparer = new DateTimeComparisonEqualityComparer(comparison, ignoreKind);
            return comparer.Equals(left, right);
        }

        /// <summary>
        /// Check if <paramref name="first"/> and <paramref name="second"/> are close to each other by specified <see cref="DateTimeComponent"/>. For example if 
        /// <paramref name="component"/> is <see cref="DateTimeComponent.Minute"/> and <paramref name="offset"/> is 5, then difference between <paramref name="first"/> and <paramref name="second"/> can 
        /// at most be 5 minutes to them to be close.
        /// </summary>
        /// <param name="first">The first <see cref="DateTime"/>.</param>
        /// <param name="second">The second <see cref="DateTime"/>.</param>
        /// <param name="component">The <see cref="DateTimeComponent"/>.</param>
        /// <param name="offset">The offset of difference like 5 minutes.</param>
        /// <returns><c>true</c> if <paramref name="first"/> and <paramref name="second"/> are considered to be close; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="offset"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">If value of <paramref name="component"/> is not defined in <see cref="DateTimeComponent"/>.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static bool AreClose(DateTime first, DateTime second, DateTimeComponent component, int offset)
        {
            if (offset < 0)
                throw new ArgumentOutOfRangeException(nameof(offset), offset, "The offset must be greater than or equal to 0.");

            if (!Enum.IsDefined(component))
                throw new ArgumentException("The value is not defined.", nameof(component));

            if (first.Equals(second))
                return true;

            var min = Min(first, second);
            var max = Max(first, second);
            var subtract = max.Subtract(min);

            switch (component)
            {
                case DateTimeComponent.Year:
                    return max.Year - min.Year <= offset;
                case DateTimeComponent.Month:
                    return max.TotalMonths() - min.TotalMonths() <= offset;
                case DateTimeComponent.Day:
                    return subtract.Days <= offset;
                case DateTimeComponent.Hour:
                    return subtract.TotalHours <= offset;
                case DateTimeComponent.Minute:
                    return subtract.TotalMinutes <= offset;
                case DateTimeComponent.Second:
                    return subtract.TotalSeconds <= offset;
                case DateTimeComponent.Millisecond:
                    return subtract.TotalMilliseconds <= offset;
                case DateTimeComponent.Microsecond:
                    return subtract.TotalMicroseconds <= offset;
                case DateTimeComponent.Ticks:
                    return max.Ticks - min.Ticks <= offset;
                default:
                    throw new NotSupportedException($"The '{component}' is not supported.");
            }
        }

        /// <summary>
        /// Gets total count of months in specified <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="date">The <see cref="DateTime"/>.</param>
        /// <returns>A total count of months in <paramref name="date"/>.</returns>
        public static int TotalMonths(this DateTime date)
        {
            return ((date.Year - 1) * 12) + date.Month;
        }

        /// <summary>
        /// Gets the noon of specified <see cref="DateTime"/> where time is 12.00.00.000.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <returns>A noon of <paramref name="datetime"/>.</returns>
        public static DateTime Noon(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, 12, 0, 0, 0, datetime.Kind);
        }

        /// <summary>
        /// Gets the midnight of specified <see cref="DateTime"/> where time is 23.59.59.999.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <returns>A midnight of <paramref name="datetime"/>.</returns>
        public static DateTime Midnight(this DateTime datetime)
        {
            return new DateTime(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59, 999, datetime.Kind);
        }

        /// <summary>
        /// Shift specified <see cref="DateTime"/> using specified values. If value of component is 0, default, then 
        /// not shifted by that component.
        /// </summary>
        /// <param name="datetime">The initial <see cref="DateTime"/>.</param>
        /// <param name="years">The years to shift.</param>
        /// <param name="months">The months to shift.</param>
        /// <param name="days">The days to shift.</param>
        /// <param name="hours">The hours to shift.</param>
        /// <param name="minutes">The minutes to shift.</param>
        /// <param name="seconds">The seconds to shift.</param>
        /// <param name="milliseconds">The milliseconds to shift.</param>
        /// <param name="microseconds">The microseconds to shift.</param>
        /// <returns>A shifted <see cref="DateTime"/> or initial, if all components are 0.</returns>
        public static DateTime Shift(this DateTime datetime, int years = 0, int months = 0, int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0, int microseconds = 0)
            => datetime.Shift(DateTimeComponent.Year, years)
                .Shift(DateTimeComponent.Month, months)
                .Shift(DateTimeComponent.Day, days)
                .Shift(DateTimeComponent.Hour, hours)
                .Shift(DateTimeComponent.Minute, minutes)
                .Shift(DateTimeComponent.Second, seconds)
                .Shift(DateTimeComponent.Millisecond, milliseconds)
                .Shift(DateTimeComponent.Microsecond, microseconds);

        /// <summary>
        /// Shift specified <see cref="DateTimeComponent"/> of specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The initial <see cref="DateTime"/>.</param>
        /// <param name="component">The <see cref="DateTimeComponent"/> to shift.</param>
        /// <param name="value">The shift value.</param>
        /// <returns>A shifted <see cref="DateTime"/> or initial, if <paramref name="value"/> is 0.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="component"/> is not defined in <see cref="DateTimeComponent"/>.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static DateTime Shift(this DateTime datetime, DateTimeComponent component, int value)
        {
            if (!Enum.IsDefined(component))
                throw new ArgumentException("The value is not defined.", nameof(component));

            if (value == 0)
                return datetime;

            switch (component)
            {
                case DateTimeComponent.Year: return datetime.AddYears(value);
                case DateTimeComponent.Month: return datetime.AddMonths(value);
                case DateTimeComponent.Day: return datetime.AddDays(value);
                case DateTimeComponent.Hour: return datetime.AddHours(value);
                case DateTimeComponent.Minute: return datetime.AddMinutes(value);
                case DateTimeComponent.Second: return datetime.AddSeconds(value);
                case DateTimeComponent.Millisecond: return datetime.AddMilliseconds(value);
                case DateTimeComponent.Microsecond: return datetime.AddMicroseconds(value);
                case DateTimeComponent.Ticks: return datetime.AddTicks(value);
                default:
                    throw new NotSupportedException($"The '{component}' component is not supported.");
            }
        }

        /// <summary>
        /// Gets specified <see cref="DateTimeComponent"/> value of specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <param name="component">The <see cref="DateTimeComponent"/> to get.</param>
        /// <returns>A component value.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="component"/> is not defined in <see cref="DateTimeComponent"/>.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static long GetComponent(this DateTime datetime, DateTimeComponent component)
        {
            if (!Enum.IsDefined(component))
                throw new ArgumentException("The value is not defined.", nameof(component));

            switch (component)
            {
                case DateTimeComponent.Year: return datetime.Year;
                case DateTimeComponent.Month: return datetime.Month;
                case DateTimeComponent.Day: return datetime.Day;
                case DateTimeComponent.Hour: return datetime.Hour;
                case DateTimeComponent.Minute: return datetime.Minute;
                case DateTimeComponent.Second: return datetime.Second;
                case DateTimeComponent.Millisecond: return datetime.Millisecond;
                case DateTimeComponent.Microsecond: return datetime.Microsecond;
                case DateTimeComponent.Ticks: return datetime.Ticks;
                default:
                    throw new NotSupportedException($"The '{component}' component is not supported.");
            }
        }

        /// <summary>
        /// Check if specified <see cref="DayOfWeek"/> represents week day from <see cref="DayOfWeek.Monday"/> to <see cref="DayOfWeek.Friday"/>.
        /// </summary>
        /// <param name="weekDay">The <see cref="DayOfWeek"/>.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="weekDay"/> is from week day from <see cref="DayOfWeek.Monday"/> to <see cref="DayOfWeek.Friday"/>.
        /// -or-
        /// <c>false</c> if <paramref name="weekDay"/> is <see cref="DayOfWeek.Saturday"/> or <see cref="DayOfWeek.Sunday"/>.
        /// </returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekDay"/> is not defined in <see cref="DayOfWeek"/>.</exception>
        public static bool IsWeekDay(this DayOfWeek weekDay)
        {
            if (!Enum.IsDefined(weekDay))
                throw new ArgumentException("The value is not defined.", nameof(weekDay));

            int dayOfWeek = (int)weekDay;
            return dayOfWeek < 6 && dayOfWeek > 0;
        }

        /// <summary>
        /// Check if specified <see cref="DateTime"/> represents week day from <see cref="DayOfWeek.Monday"/> to <see cref="DayOfWeek.Friday"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="datetime"/> is from week day from <see cref="DayOfWeek.Monday"/> to <see cref="DayOfWeek.Friday"/>.
        /// -or-
        /// <c>false</c> if <paramref name="datetime"/> is <see cref="DayOfWeek.Saturday"/> or <see cref="DayOfWeek.Sunday"/>.
        /// </returns>
        public static bool IsWeekDay(this DateTime datetime) => IsWeekDay(datetime.DayOfWeek);

        /// <summary>
        /// Check if specified <see cref="DayOfWeek"/> represents weekend from <see cref="DayOfWeek.Saturday"/> to <see cref="DayOfWeek.Sunday"/>.
        /// </summary>
        /// <param name="weekDay">The <see cref="DayOfWeek"/>.</param>
        /// <returns><c>true</c> if <paramref name="weekDay"/> is <see cref="DayOfWeek.Saturday"/> or <see cref="DayOfWeek.Sunday"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekDay"/> is not defined in <see cref="DayOfWeek"/>.</exception>
        public static bool IsWeekend(this DayOfWeek weekDay) => !IsWeekDay(weekDay);

        /// <summary>
        /// Check if specified <see cref="DateTime"/> represents weekend from <see cref="DayOfWeek.Saturday"/> to <see cref="DayOfWeek.Sunday"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is <see cref="DayOfWeek.Saturday"/> or <see cref="DayOfWeek.Sunday"/>; <c>false</c> otherwise.</returns>
        public static bool IsWeekend(this DateTime datetime) => IsWeekend(datetime.DayOfWeek);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of first day of week from week of specified reference <see cref="DateTime"/>.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTime"/>.</param>
        /// <param name="weekBegin">The <see cref="DayOfWeek"/> of the first day of week; either <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</param>
        /// <returns>A <see cref="DateTime"/> of first day of week.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekBegin"/> is not <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</exception>
        public static DateTime FirstDayOfWeek(this DateTime refDate, DayOfWeek weekBegin)
        {
            if (weekBegin == DayOfWeek.Sunday || weekBegin == DayOfWeek.Monday)
            {
                if (refDate.DayOfWeek == weekBegin)
                    return refDate;

                if (weekBegin == DayOfWeek.Monday)
                    return refDate.AddDays((int)weekBegin - GetSundayValue(refDate.DayOfWeek));

                return refDate.AddDays(weekBegin - refDate.DayOfWeek);
            }
            else
                throw new ArgumentException("The weeb must begin either on sunday or monday.", nameof(weekBegin));
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> of first day of week from week of specified reference <see cref="DateTime"/>.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTime"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> of culture to use or <c>null</c> to use current culture.</param>
        /// <returns>A <see cref="DateTime"/> of first day of week.</returns>
        public static DateTime FirstDayOfWeek(this DateTime refDate, CultureInfo? culture = null)
            => FirstDayOfWeek(refDate, (culture ?? CultureInfo.CurrentCulture).DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of first day of this week.
        /// </summary>
        /// <param name="weekBegin">The <see cref="DayOfWeek"/> of the first day of week; either <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</param>
        /// <returns>A <see cref="DateTime"/> of first day of this week.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekBegin"/> is not <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</exception>
        public static DateTime FirstDayOfThisWeek(DayOfWeek weekBegin) => FirstDayOfWeek(DateTime.Today, weekBegin);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of first day of this week.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> of culture to use or <c>null</c> to use current culture.</param>
        /// <returns>A <see cref="DateTime"/> of first day of this week.</returns>
        public static DateTime FirstDayOfThisWeek(CultureInfo? culture = null) => FirstDayOfWeek(DateTime.Today, culture);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of last day of week from week of specified reference <see cref="DateTime"/>.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTime"/>.</param>
        /// <param name="weekBegin">The <see cref="DayOfWeek"/> of the first day of week; either <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</param>
        /// <returns>A <see cref="DateTime"/> of last day of week.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekBegin"/> is not <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</exception>
        public static DateTime LastDayOfWeek(this DateTime refDate, DayOfWeek weekBegin) => FirstDayOfWeek(refDate, weekBegin).AddDays(6);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of last day of week from week of specified reference <see cref="DateTime"/>.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTime"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> of culture to use or <c>null</c> to use current culture.</param>
        /// <returns>A <see cref="DateTime"/> of last day of week.</returns>
        public static DateTime LastDayOfWeek(this DateTime refDate, CultureInfo? culture = null)
            => LastDayOfWeek(refDate, (culture ?? CultureInfo.CurrentCulture).DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of last day of this week.
        /// </summary>
        /// <param name="weekBegin">The <see cref="DayOfWeek"/> of the first day of week; either <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</param>
        /// <returns>A <see cref="DateTime"/> of last day of this week.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekBegin"/> is not <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</exception>
        public static DateTime LastDayOfThisWeek(DayOfWeek weekBegin) => LastDayOfWeek(DateTime.Today, weekBegin);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of last day of this week.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> of culture to use or <c>null</c> to use current culture.</param>
        /// <returns>A <see cref="DateTime"/> of last day of this week.</returns>
        public static DateTime LastDayOfThisWeek(CultureInfo? culture = null) => LastDayOfWeek(DateTime.Today, culture);

        /// <summary>
        /// Gets the <see cref="DateTime"/> of first day of month.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTime"/>.</param>
        /// <returns>A <see cref="DateTime"/> of first day of month.<</returns>
        public static DateTime FirstDayOfMonth(this DateTime refDate)
        {
            if (refDate == DateTime.MinValue)
                return refDate;

            return new DateTime(refDate.Year, refDate.Month, 1, refDate.Hour, refDate.Minute, refDate.Second, refDate.Millisecond, refDate.Kind);
        }

        /// <summary>
        /// Gets the <see cref="DateTime"/> of last day of month.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTime"/>.</param>
        /// <returns>A <see cref="DateTime"/> of last day of month.<</returns>
        public static DateTime LastDayOfMonth(this DateTime refDate)
        {
            if (refDate == DateTime.MaxValue)
                return refDate;

            if (refDate.Year == DateTime.MaxValue.Year && refDate.Month == DateTime.MaxValue.Month)
                return DateTime.MaxValue;

            return refDate.AddMonths(1).FirstDayOfMonth().AddDays(-1);
        }

        /// <summary>
        /// Gets previous week day of specified <see cref="DayOfWeek"/> from specified <see cref="DateTime"/>. If <paramref name="datetime"/> is wednesday and 
        /// <paramref name="weekDay"/> is <see cref="DayOfWeek.Thursday"/>, then returns <see cref="DateTime"/> of previous thursday.
        /// </summary>
        /// <param name="datetime">The reference <see cref="DateTime"/>.</param>
        /// <param name="weekDay">The previous <see cref="DayOfWeek"/> of to get.</param>
        /// <returns>A <see cref="DateTime"/> of previous <see cref="DayOfWeek"/>.</returns>
        public static DateTime GetPreviousWeekDay(this DateTime datetime, DayOfWeek weekDay) => GetWeekDay(datetime, weekDay, true);

        /// <summary>
        /// Gets next week day of specified <see cref="DayOfWeek"/> from specified <see cref="DateTime"/>. If <paramref name="datetime"/> is wednesday and 
        /// <paramref name="weekDay"/> is <see cref="DayOfWeek.Thursday"/>, then returns <see cref="DateTime"/> of next thursday.
        /// </summary>
        /// <param name="datetime">The reference <see cref="DateTime"/>.</param>
        /// <param name="weekDay">The next <see cref="DayOfWeek"/> of to get.</param>
        /// <returns>A <see cref="DateTime"/> of next <see cref="DayOfWeek"/>.</returns>
        public static DateTime GetNextWeekDay(this DateTime datetime, DayOfWeek weekDay) => GetWeekDay(datetime, weekDay, false);

        /// <summary>
        /// Gets the quarter of the year of specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <returns>A quarter of the year.</returns>
        public static int Quarter(this DateTime datetime)
        {
            if (datetime.Month >= 1 && datetime.Month <= 3)
                return FirstQuarter;
            else if (datetime.Month >= 4 && datetime.Month <= 6)
                return SecondQuarter;
            else if (datetime.Month >= 7 && datetime.Month <= 9)
                return ThirdQuarter;
            else
                return FourthQuarter;
        }

        /// <summary>
        /// Add weeks to the specified <see cref="DateTime"/>. If <paramref name="value"/> is positive, then 
        /// weeks are added. If negative, then weeks are subtracted. If zero, then value is not changed.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <param name="value">The number of weeks to add.</param>
        /// <returns>A <see cref="DateTime"/> with added weeks.</returns>
        public static DateTime AddWeeks(this DateTime datetime, int value)
        {
            if (value == 0)
                return datetime;

            int days = Math.Abs(value) * DaysInWeek;
            return value < 0 ? datetime.AddDays(-days) : datetime.AddDays(days);
        }

        /// <summary>
        /// Sets time of specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The initial <see cref="DateTime"/>.</param>
        /// <param name="hour">The hour to set or <c>null</c>.</param>
        /// <param name="minute">The minute to set or <c>null</c>.</param>
        /// <param name="second">The second to set or <c>null</c>.</param>
        /// <param name="millisecond">The millisecond to set or <c>null</c>.</param>
        /// <returns>A <see cref="DateTime"/> with new time.</returns>
        public static DateTime SetTime(this DateTime datetime, int? hour = null, int? minute = null, int? second = null, int? millisecond = null)
        {
            if ((new[] { hour, minute, second, millisecond }).NoValue())
                return datetime;

            return new DateTime(datetime.Year, datetime.Month, datetime.Day,
                hour.GetValueOrDefault(datetime.Hour),
                minute.GetValueOrDefault(datetime.Minute),
                second.GetValueOrDefault(datetime.Second),
                millisecond.GetValueOrDefault(datetime.Millisecond),
                datetime.Kind);
        }

        /// <summary>
        /// Gets the <see cref="DateOnly"/> from specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <returns>A <see cref="DateOnly"/> of <paramref name="datetime"/>.</returns>
        public static DateOnly OnlyDate(this DateTime datetime) => new DateOnly(datetime.Year, datetime.Month, datetime.Day);

        /// <summary>
        /// Gets the <see cref="TimeOnly"/> from specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <returns>A <see cref="TimeOnly"/> of <paramref name="datetime"/>.</returns>
        public static TimeOnly OnlyTime(this DateTime datetime) => new TimeOnly(datetime.Hour, datetime.Minute, datetime.Second, datetime.Millisecond);

        /// <summary>
        /// Gets the nearest <see cref="DateTime"/> to specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/>.</param>
        /// <param name="others">The other <see cref="DateTime"/>s to search for nearest.</param>
        /// <returns>A nearest <see cref="DateTime"/> of <paramref name="value"/> from <paramref name="others"/> or <c>null</c>.</returns>
        public static DateTime? Nearest(this DateTime value, IEnumerable<DateTime> others)
        {
            if (!others.Any())
                return null;

            DateTime? nearest = null;
            TimeSpan offset = TimeSpan.MaxValue;

            foreach (var other in others)
            {
                var min = Min(value, other);
                var max = Max(value, other);
                var diff = max - min;

                if (diff.IsZero())
                    return other;
                else if (diff < offset)
                {
                    nearest = other;
                    offset = diff;
                }
            }

            return nearest;
        }

        /// <summary>
        /// Gets the farest <see cref="DateTime"/> to specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/>.</param>
        /// <param name="others">The other <see cref="DateTime"/>s to search for farest.</param>
        /// <returns>A farest <see cref="DateTime"/> of <paramref name="value"/> from <paramref name="others"/> or <c>null</c>.</returns>
        public static DateTime? Farest(this DateTime value, IEnumerable<DateTime> others)
        {
            if (!others.Any())
                return null;

            DateTime? farest = null;
            TimeSpan offset = TimeSpan.MinValue;

            foreach (var other in others)
            {
                var min = Min(value, other);
                var max = Max(value, other);
                var diff = max - min;

                if (diff > offset)
                {
                    farest = other;
                    offset = diff;
                }
            }

            return farest;
        }

        /// <summary>
        /// Gets the nearest <see cref="DateTime"/>s to specified <see cref="DateTime"/>. 
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/>.</param>
        /// <param name="others">The other <see cref="DateTime"/>s to search for nearest.</param>
        /// <param name="offset">The offset of consider other to be near.</param>
        /// <returns>A nearest <see cref="DateTime"/>s to <paramref name="value"/> from <paramref name="others"/>.</returns>
        public static IEnumerable<DateTime> Nearest(this DateTime value, IEnumerable<DateTime> others, TimeSpan offset)
        {
            offset = offset.Duration();
            var nearest = new List<DateTime>();

            foreach (var other in others)
            {
                var min = Min(value, other);
                var max = Max(value, other);
                var diff = max - min;
                if (diff <= offset)
                    nearest.Add(other);
            }

            return nearest.OrderBy(x => x);
        }

        /// <summary>
        /// Gets the farest <see cref="DateTime"/>s to specified <see cref="DateTime"/>. 
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/>.</param>
        /// <param name="others">The other <see cref="DateTime"/>s to search for farest.</param>
        /// <param name="offset">The offset of consider other to be far.</param>
        /// <returns>A farest <see cref="DateTime"/>s to <paramref name="value"/> from <paramref name="others"/>.</returns>
        public static IEnumerable<DateTime> Farest(this DateTime value, IEnumerable<DateTime> others, TimeSpan offset)
        {
            offset = offset.Duration();
            var farest = new List<DateTime>();

            foreach (var other in others)
            {
                var min = Min(value, other);
                var max = Max(value, other);
                var diff = max - min;
                if (diff >= offset)
                    farest.Add(other);
            }

            return farest.OrderBy(x => x);
        }

        internal static int GetSundayValue(DayOfWeek weekDay)
        {
            if (weekDay == DayOfWeek.Sunday)
                return 7;

            return (int)weekDay;
        }

        private static bool IsFuture(DateTime datetime, DateTime futureTime, TimeSpan offset)
        {
            if (!offset.IsZero())
                futureTime = futureTime.AddTicks(Math.Abs(offset.Ticks));

            return datetime > futureTime;
        }

        private static bool IsPast(DateTime datetime, DateTime currentTime, TimeSpan offset)
        {
            var comparisonTime = offset.IsZero() ? datetime : datetime.AddTicks(Math.Abs(offset.Ticks));
            return comparisonTime < currentTime;
        }

        private static DateTime GetToday()
        {
            return DateTime.Today;
        }

        private static DateTime GetNow(DateTimeKind kind)
        {
            return kind == DateTimeKind.Utc ? DateTime.UtcNow : DateTime.Now;
        }

        private static DateTime GetWeekDay(DateTime datetime, DayOfWeek weekDay, bool previous)
        {
            if (!Enum.IsDefined(weekDay))
                throw new ArgumentException("The value is not defined.", nameof(weekDay));

            int diff = previous ? datetime.DayOfWeek - weekDay : weekDay - datetime.DayOfWeek;

            if (diff <= 0)
                diff += DaysInWeek;

            return datetime.AddDays(previous ? -1 * diff : diff);
        }
    }
}
