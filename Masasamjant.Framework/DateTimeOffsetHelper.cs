using Masasamjant.Configuration;
using System.Globalization;

namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to <see cref="DateTimeOffset"/> values.
    /// </summary>
    public static class DateTimeOffsetHelper
    {
        /// <summary>
        /// Gets <see cref="DateTimeOffset"/> representing tomorrows date.
        /// </summary>
        /// <returns>A <see cref="DateTimeOffset"/> of tomorrows date.</returns>
        public static DateTimeOffset GetTomorrow()
        {
            var today = GetToday();

            if (today.Date.Equals(DateTime.MaxValue.Date))
                return today;

            return today.AddDays(1);
        }

        /// <summary>
        /// Gets <see cref="DateTimeOffset"/> representing yesterdays date.
        /// </summary>
        /// <returns>A <see cref="DateTimeOffset"/> of yesterdays date.</returns>
        public static DateTimeOffset GetYesterday()
        {
            var today = GetToday();

            if (today.Date.Equals(DateTime.MaxValue.Date))
                return today;

            return today.AddDays(-1);
        }

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is <see cref="DateTime.Today"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is <see cref="DateTime.Today"/>; <c>false</c> otherwise.</returns>
        public static bool IsToday(this DateTimeOffset datetime)
        {
            return GetToday().Equals(datetime);
        }

        /// <summary>
        /// Check if date of specified <see cref="DateTimeOffset"/> is <see cref="DateTime.Today"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if date of <paramref name="datetime"/> is <see cref="DateTime.Today"/>; <c>false</c> otherwise.</returns>
        public static bool IsTodaysDate(this DateTimeOffset datetime)
        {
            return GetToday().Equals(datetime.Date);
        }

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is tomorrows date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is tomorrows date; <c>false</c> otherwise.</returns>
        public static bool IsTomorrow(this DateTimeOffset datetime)
        {
            return GetTomorrow().Equals(datetime);
        }

        /// <summary>
        /// Check if date of specified <see cref="DateTimeOffset"/> is tomorrows date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if date of <paramref name="datetime"/> is tomorrows date; <c>false</c> otherwise.</returns>
        public static bool IsTomorrowsDate(this DateTimeOffset datetime)
        {
            return GetTomorrow().Equals(datetime.Date);
        }

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is yesterdays date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is yesterdays date; <c>false</c> otherwise.</returns>
        public static bool IsYesterday(this DateTimeOffset datetime)
        {
            return GetYesterday().Equals(datetime);
        }

        /// <summary>
        /// Check if date of specified <see cref="DateTimeOffset"/> is yesterdays date.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if date of <paramref name="datetime"/> is yesterdays date; <c>false</c> otherwise.</returns>
        public static bool IsYesterdaysDate(this DateTimeOffset datetime)
        {
            return GetYesterday().Equals(datetime.Date);
        }

        /// <summary>
        /// Gets minimum value between specified values.
        /// </summary>
        /// <param name="left">The left <see cref="DateTimeOffset"/>.</param>
        /// <param name="right">The right <see cref="DateTimeOffset"/>.</param>
        /// <returns>A minimum value between <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static DateTimeOffset Min(DateTimeOffset left, DateTimeOffset right)
            => ComparableHelper.Min(left, right);

        /// <summary>
        /// Gets maximum value between specified values.
        /// </summary>
        /// <param name="left">The left <see cref="DateTimeOffset"/>.</param>
        /// <param name="right">The right <see cref="DateTimeOffset"/>.</param>
        /// <returns>A maximum value between <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static DateTimeOffset Max(DateTimeOffset left, DateTimeOffset right)
            => ComparableHelper.Max(left, right);

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in future.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTimeOffset datetime) => IsFuture(datetime, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in future using specified time offset. For example if <paramref name="offset"/> is 5 minutes, 
        /// then <paramref name="datetime"/> must be at least 5 minutes ahead of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be ahead to be in future.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTimeOffset datetime, TimeSpan offset)
        {
            datetime = ToUniversalOrLocalTime(datetime, out var kind);
            var futureTime = GetNow(kind);
            return IsFuture(datetime, futureTime, offset);
        }

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in future using current time provided by specified <see cref="IDateTimeConfiguration"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTimeOffset datetime, IDateTimeConfiguration configuration) => IsFuture(datetime, configuration, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in future using current time provided by specified <see cref="IDateTimeConfiguration"/> and using 
        /// specified time offset. For example if <paramref name="offset"/> is 5 minutes, then <paramref name="datetime"/> must be at least 5 minutes ahead of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be ahead to be in future.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in future; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this DateTimeOffset datetime, IDateTimeConfiguration configuration, TimeSpan offset)
        {
            var futureTime = configuration.GetDateTimeOffsetNow();
            datetime = ToUniversalOrLocalTime(datetime, out var _);
            return IsFuture(datetime, futureTime, offset);
        }

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in past.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTimeOffset datetime) => IsPast(datetime, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in past using specified time offset. For example if <paramref name="offset"/> is 5 minutes, 
        /// then <paramref name="datetime"/> must be at least 5 minutes behind of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be behind to be in past.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTimeOffset datetime, TimeSpan offset)
        {
            datetime = ToUniversalOrLocalTime(datetime, out var kind);
            var currentTime = GetNow(kind);
            return IsPast(datetime, currentTime, offset);
        }

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in past using current time provided by specified <see cref="IDateTimeConfiguration"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTimeOffset datetime, IDateTimeConfiguration configuration) => IsPast(datetime, configuration, TimeSpan.Zero);

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> is in past using current time provided by specified <see cref="IDateTimeConfiguration"/> and using 
        /// specified time offset. For example if <paramref name="offset"/> is 5 minutes, then <paramref name="datetime"/> must be at least 5 minutes behind of current time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <param name="configuration">The <see cref="IDateTimeConfiguration"/>.</param>
        /// <param name="offset">The offset of how much <paramref name="datetime"/> must be behind to be in past.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is in past; <c>false</c> otherwise.</returns>
        public static bool IsPast(this DateTimeOffset datetime, IDateTimeConfiguration configuration, TimeSpan offset)
        {
            var currentTime = configuration.GetDateTimeNow();
            datetime = ToUniversalOrLocalTime(datetime, out var _);
            return IsPast(datetime, currentTime, offset);
        }

        /// <summary>
        /// Check value of <paramref name="datetime"/> is near to <paramref name="compare"/> using specified time offset. If <paramref name="offset"/> is zero, 
        /// then <paramref name="datetime"/> must be equal to <paramref name="compare"/>. If <paramref name="offset"/> is for example 3 seconds, then difference 
        /// between <paramref name="datetime"/> and <paramref name="compare"/>, must be less or equal to offset.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/> to check.</param>
        /// <param name="compare">The <see cref="DateTimeOffset"/> to compare to.</param>
        /// <param name="offset">The offset of values considered to be near.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is near <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If offset of <paramref name="compare"/> is not same as offset of <paramref name="datetime"/>.</exception>
        public static bool IsNear(this DateTimeOffset datetime, DateTimeOffset compare, TimeSpan offset)
        {
            if (!datetime.Offset.Equals(compare.Offset))
                throw new ArgumentException("The comparison value must have some offset.", nameof(compare));

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
        /// Check if specified <see cref="DateTimeOffset"/> represents UTC time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> represents UTC time; <c>false</c> otherwise.</returns>
        public static bool IsUniveralTime(this DateTimeOffset datetime) => datetime.Offset.IsZero();

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> represents local time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> represents local time; <c>false</c> otherwise.</returns>
        public static bool IsLocalTime(this DateTimeOffset datetime) => datetime.Offset.Equals(DateTimeOffset.Now.Offset);

        /// <summary>
        /// Converts specified <see cref="DateTimeOffset"/> to local time if it does not represent UTC time.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTime"/>.</param>
        /// <param name="kind">The <see cref="DateTimeKind"/> of returned <see cref="DateTimeOffset"/>.</param>
        /// <returns></returns>
        public static DateTimeOffset ToUniversalOrLocalTime(this DateTimeOffset datetime, out DateTimeKind kind)
        {
            if (IsUniveralTime(datetime))
            {
                kind = DateTimeKind.Utc;
                return datetime;
            }
            else
            {
                kind = DateTimeKind.Local;
                return IsLocalTime(datetime) ? datetime : datetime.ToLocalTime();
            }
        }

        /// <summary>
        /// Check if <paramref name="first"/> and <paramref name="second"/> are close to each other by specified <see cref="DateTimeComponent"/>. For example if 
        /// <paramref name="component"/> is <see cref="DateTimeComponent.Minute"/> and <paramref name="offset"/> is 5, then difference between <paramref name="first"/> and <paramref name="second"/> can 
        /// at most be 5 minutes to them to be close.
        /// </summary>
        /// <param name="first">The first <see cref="DateTimeOffset"/>.</param>
        /// <param name="second">The second <see cref="DateTimeOffset"/>.</param>
        /// <param name="component">The <see cref="DateTimeComponent"/>.</param>
        /// <param name="offset">The offset of difference like 5 minutes.</param>
        /// <returns><c>true</c> if <paramref name="first"/> and <paramref name="second"/> are considered to be close; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="offset"/> is less than 0.</exception>
        /// <exception cref="ArgumentException">
        /// If <paramref name="first"/> and <paramref name="second"/> do not have same <see cref="DateTimeOffset.Offset"/>.
        /// -or-
        /// If value of <paramref name="component"/> is not defined in <see cref="DateTimeComponent"/>.
        /// </exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static bool AreClose(DateTimeOffset first, DateTimeOffset second, DateTimeComponent component, int offset)
        {
            if (!first.Offset.Equals(second.Offset))
                throw new ArgumentException("The second value must have same offset as first.", nameof(second));

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
                    return max.DateTime.TotalMonths() - min.DateTime.TotalMonths() <= offset;
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
        /// Gets the noon of specified <see cref="DateTimeOffset"/> where time is 12.00.00.000.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/>.</param>
        /// <returns>A noon of <paramref name="datetime"/>.</returns>
        public static DateTimeOffset Noon(this DateTimeOffset datetime)
        {
            return new DateTimeOffset(datetime.Year, datetime.Month, datetime.Day, 12, 0, 0, 0, datetime.Offset);
        }

        /// <summary>
        /// Gets the midnight of specified <see cref="DateTimeOffset"/> where time is 23.59.59.999.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/>.</param>
        /// <returns>A midnight of <paramref name="datetime"/>.</returns>
        public static DateTimeOffset Midnight(this DateTimeOffset datetime)
        {
            return new DateTimeOffset(datetime.Year, datetime.Month, datetime.Day, 23, 59, 59, 999, datetime.Offset);
        }

        /// <summary>
        /// Shift specified <see cref="DateTimeOffset"/> using specified values. If value of component is 0, default, then 
        /// not shifted by that component.
        /// </summary>
        /// <param name="datetime">The initial <see cref="DateTimeOffset"/>.</param>
        /// <param name="years">The years to shift.</param>
        /// <param name="months">The months to shift.</param>
        /// <param name="days">The days to shift.</param>
        /// <param name="hours">The hours to shift.</param>
        /// <param name="minutes">The minutes to shift.</param>
        /// <param name="seconds">The seconds to shift.</param>
        /// <param name="milliseconds">The milliseconds to shift.</param>
        /// <param name="microseconds">The microseconds to shift.</param>
        /// <returns>A shifted <see cref="DateTimeOffset"/> or initial, if all components are 0.</returns>
        public static DateTimeOffset Shift(this DateTimeOffset datetime, int years = 0, int months = 0, int days = 0, int hours = 0, int minutes = 0, int seconds = 0, int milliseconds = 0, int microseconds = 0)
            => datetime.Shift(DateTimeComponent.Year, years)
                .Shift(DateTimeComponent.Month, months)
                .Shift(DateTimeComponent.Day, days)
                .Shift(DateTimeComponent.Hour, hours)
                .Shift(DateTimeComponent.Minute, minutes)
                .Shift(DateTimeComponent.Second, seconds)
                .Shift(DateTimeComponent.Millisecond, milliseconds)
                .Shift(DateTimeComponent.Microsecond, microseconds);

        /// <summary>
        /// Shift specified <see cref="DateTimeComponent"/> of specified <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="datetime">The initial <see cref="DateTimeOffset"/>.</param>
        /// <param name="component">The <see cref="DateTimeComponent"/> to shift.</param>
        /// <param name="value">The shift value.</param>
        /// <returns>A shifted <see cref="DateTimeOffset"/> or initial, if <paramref name="value"/> is 0.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="component"/> is not defined in <see cref="DateTimeComponent"/>.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static DateTimeOffset Shift(this DateTimeOffset datetime, DateTimeComponent component, int value)
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
        /// Gets specified <see cref="DateTimeComponent"/> value of specified <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/>.</param>
        /// <param name="component">The <see cref="DateTimeComponent"/> to get.</param>
        /// <returns>A component value.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="component"/> is not defined in <see cref="DateTimeComponent"/>.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="component"/> is not supported.</exception>
        public static long GetComponent(this DateTimeOffset datetime, DateTimeComponent component)
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
        /// Check if specified <see cref="DateTimeOffset"/> represents week day from <see cref="DayOfWeek.Monday"/> to <see cref="DayOfWeek.Friday"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/>.</param>
        /// <returns>
        /// <c>true</c> if <paramref name="datetime"/> is from week day from <see cref="DayOfWeek.Monday"/> to <see cref="DayOfWeek.Friday"/>.
        /// -or-
        /// <c>false</c> if <paramref name="datetime"/> is <see cref="DayOfWeek.Saturday"/> or <see cref="DayOfWeek.Sunday"/>.
        /// </returns>
        public static bool IsWeekDay(this DateTimeOffset datetime) => datetime.DateTime.IsWeekDay();

        /// <summary>
        /// Check if specified <see cref="DateTimeOffset"/> represents weekend from <see cref="DayOfWeek.Saturday"/> to <see cref="DayOfWeek.Sunday"/>.
        /// </summary>
        /// <param name="datetime">The <see cref="DateTimeOffset"/>.</param>
        /// <returns><c>true</c> if <paramref name="datetime"/> is <see cref="DayOfWeek.Saturday"/> or <see cref="DayOfWeek.Sunday"/>; <c>false</c> otherwise.</returns>
        public static bool IsWeekend(this DateTimeOffset datetime) => datetime.DateTime.IsWeekend();

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of first day of this week.
        /// </summary>
        /// <param name="weekBegin">The <see cref="DayOfWeek"/> of the first day of week; either <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of first day of this week.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekBegin"/> is not <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</exception>
        public static DateTimeOffset FirstDayOfWeek(this DateTimeOffset refDate, DayOfWeek weekBegin)
            => refDate.DateTime.FirstDayOfWeek(weekBegin);

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of first day of this week.
        /// </summary>
        /// <param name="culture">The <see cref="CultureInfo"/> of culture to use or <c>null</c> to use current culture.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of first day of this week.</returns>
        public static DateTimeOffset FirstDayOfWeek(this DateTimeOffset refDate, CultureInfo? culture = null)
            => refDate.DateTime.FirstDayOfWeek(culture);

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of last day of week from week of specified reference <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTimeOffset"/>.</param>
        /// <param name="weekBegin">The <see cref="DayOfWeek"/> of the first day of week; either <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of last day of week.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="weekBegin"/> is not <see cref="DayOfWeek.Sunday"/> or <see cref="DayOfWeek.Monday"/>.</exception>
        public static DateTimeOffset LastDayOfWeek(this DateTimeOffset refDate, DayOfWeek weekBegin)
            => FirstDayOfWeek(refDate, weekBegin).AddDays(6);

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of last day of week from week of specified reference <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTimeOffset"/>.</param>
        /// <param name="culture">The <see cref="CultureInfo"/> of culture to use or <c>null</c> to use current culture.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of last day of week.</returns>
        public static DateTimeOffset LastDayOfWeek(this DateTimeOffset refDate, CultureInfo? culture = null)
            => FirstDayOfWeek(refDate, culture).AddDays(6);

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of first day of month.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTimeOffset"/>.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of first day of month.<</returns>
        public static DateTimeOffset FirstDayOfMonth(this DateTimeOffset refDate)
            => refDate.DateTime.FirstDayOfMonth();

        /// <summary>
        /// Gets the <see cref="DateTimeOffset"/> of last day of month.
        /// </summary>
        /// <param name="refDate">The reference <see cref="DateTimeOffset"/>.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of last day of month.<</returns>
        public static DateTimeOffset LastDayOfMonth(this DateTimeOffset refDate)
            => refDate.DateTime.LastDayOfMonth();

        /// <summary>
        /// Gets previous week day of specified <see cref="DayOfWeek"/> from specified <see cref="DateTimeOffset"/>. If <paramref name="datetime"/> is wednesday and 
        /// <paramref name="weekDay"/> is <see cref="DayOfWeek.Thursday"/>, then returns <see cref="DateTimeOffset"/> of previous thursday.
        /// </summary>
        /// <param name="datetime">The reference <see cref="DateTimeOffset"/>.</param>
        /// <param name="weekDay">The previous <see cref="DayOfWeek"/> of to get.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of previous <see cref="DayOfWeek"/>.</returns>
        public static DateTimeOffset GetPreviousWeekDay(this DateTimeOffset datetime, DayOfWeek weekDay) => GetWeekDay(datetime, weekDay, true);

        /// <summary>
        /// Gets next week day of specified <see cref="DayOfWeek"/> from specified <see cref="DateTimeOffset"/>. If <paramref name="datetime"/> is wednesday and 
        /// <paramref name="weekDay"/> is <see cref="DayOfWeek.Thursday"/>, then returns <see cref="DateTimeOffset"/> of next thursday.
        /// </summary>
        /// <param name="datetime">The reference <see cref="DateTimeOffset"/>.</param>
        /// <param name="weekDay">The next <see cref="DayOfWeek"/> of to get.</param>
        /// <returns>A <see cref="DateTimeOffset"/> of next <see cref="DayOfWeek"/>.</returns>
        public static DateTimeOffset GetNextWeekDay(this DateTimeOffset datetime, DayOfWeek weekDay) => GetWeekDay(datetime, weekDay, false);

        private static DateTimeOffset GetToday()
        {
            return DateTime.Today;
        }

        private static bool IsFuture(DateTimeOffset datetime, DateTimeOffset futureTime, TimeSpan offset)
        {
            if (!offset.IsZero())
                futureTime = futureTime.AddTicks(Math.Abs(offset.Ticks));

            return datetime > futureTime;
        }

        private static bool IsPast(DateTimeOffset datetime, DateTimeOffset currentTime, TimeSpan offset)
        {
            var comparisonTime = offset.IsZero() ? datetime : datetime.AddTicks(Math.Abs(offset.Ticks));
            return comparisonTime < currentTime;
        }

        private static DateTimeOffset GetNow(DateTimeKind kind)
        {
            return kind == DateTimeKind.Utc ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
        }

        private static DateTimeOffset GetWeekDay(DateTimeOffset datetime, DayOfWeek weekDay, bool previous)
        {
            if (!Enum.IsDefined(weekDay))
                throw new ArgumentException("The value is not defined.", nameof(weekDay));

            int diff = previous ? datetime.DayOfWeek - weekDay : weekDay - datetime.DayOfWeek;

            if (diff <= 0)
                diff += DateTimeHelper.DaysInWeek;

            return datetime.AddDays(previous ? -1 * diff : diff);
        }
    }
}
