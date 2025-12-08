using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Masasamjant
{
    /// <summary>
    /// Represents date range from begin date to end date.
    /// </summary>
    /// <remarks>Time value is not used at all so only date part of any <see cref="DateTime"/> is used.</remarks>
    public readonly struct DateRange : IEquatable<DateRange>, ICloneable
    {
        private readonly DateTime? begin;
        private readonly DateTime? end;
        private readonly int days;

        /// <summary>
        /// Initializes new <see cref="DateRange"/> from specified begin and end date.
        /// </summary>
        /// <param name="begin">The begin date.</param>
        /// <param name="end">The end date.</param>
        /// <exception cref="ArgumentOutOfRangeException">If date of <paramref name="end"/> is earlier than date of <paramref name="begin"/>.</exception>
        public DateRange(DateTime begin, DateTime end)
        {
            if (end.Date < begin.Date)
                throw new ArgumentOutOfRangeException(nameof(end), end, "The end date must later than or equal to begin date.");

            if (begin.Date.Equals(end.Date))
            {
                this.begin = begin.Date;
                this.end = begin.Date;
                this.days = 1;
            }
            else
            {
                this.begin = begin.Date;
                this.end = end.Date;
                this.days = end.Subtract(begin).Days + 1;
            }
        }

        /// <summary>
        /// Initializes new <see cref="DateRange"/> where begin and end date is same.
        /// </summary>
        /// <param name="date">The begin and end date.</param>
        public DateRange(DateTime date)
        {
            begin = date.Date;
            end = date.Date;
            days = 1;
        }

        /// <summary>
        /// Represents empty date range.
        /// </summary>
        public static DateRange Empty { get; } = new DateRange();

        /// <summary>
        /// Gets the begin date of the range.
        /// </summary>
        public DateTime Begin
        {
            get { return begin.GetValueOrDefault(DateTime.MinValue); }
        }

        /// <summary>
        /// Gets the end date of the range.
        /// </summary>
        public DateTime End
        {
            get { return end.GetValueOrDefault(DateTime.MinValue); }
        }

        /// <summary>
        /// Gets whether or not this represents empty range.
        /// </summary>
        public bool IsEmpty
        {
            get { return !begin.HasValue || !end.HasValue; }
        }

        /// <summary>
        /// Gets how many days thare is in range. 
        /// If begin and end are same, then there is 1 day.
        /// If begin is today and end tomorrow, then there is 2 days and so on.
        /// </summary>
        public int Days
        {
            get
            {
                if (IsEmpty)
                    return 0;

                return days;
            }
        }

        /// <summary>
        /// Check if range begins after specified date.
        /// </summary>
        /// <param name="date">The date to compare.</param>
        /// <returns><c>true</c> if range begin after <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool BeginsAfter(DateTime date)
        {
            return !IsEmpty && Begin > date.Date;
        }

        /// <summary>
        /// Check if range begins after beginning of specified other range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if begin of this range is after begin of <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool BeginsAfter(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return Begin > other.Begin;
        }

        /// <summary>
        /// Check if range begins before specified date.
        /// </summary>
        /// <param name="date">The date to compare.</param>
        /// <returns><c>true</c> if range begin before <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool BeginsBefore(DateTime date)
        {
            return !IsEmpty && Begin < date.Date;
        }

        /// <summary>
        /// Check if range begins before beginning of specified other range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if begin of this range is before begin of <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool BeginsBefore(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return Begin < other.Begin;
        }

        /// <summary>
        /// Combines this range with specified other range. When combined, then begin date of result will be the earliest begin date of both ranges 
        /// and end date will be the latest end date of both ranges.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns>A combined range.</returns>
        public DateRange Combine(DateRange other)
        {
            if (IsEmpty)
                return other;

            if (other.IsEmpty || Equals(other))
                return this;

            var begin = ComparableHelper.Min(Begin, other.Begin);
            var end = ComparableHelper.Max(End, other.End);
            return new DateRange(begin, end);
        }

        /// <summary>
        /// Combine this range with several other ranges. When combined, then begin date of result will be the earliest begin date of all ranges 
        /// and end date will be the latest end date of all ranges.
        /// </summary>
        /// <param name="ranges">The ranges to combine with.</param>
        /// <returns>A combined range.</returns>
        public DateRange Combine(IEnumerable<DateRange> ranges)
        {
            DateRange combination = this;

            foreach (var range in ranges)
                combination = combination.Combine(range);

            return combination;
        }

        /// <summary>
        /// Check if range contains specified date.
        /// </summary>
        /// <param name="date">The date to check.</param>
        /// <returns><c>true</c> if range contains <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool Contains(DateTime date)
        {
            return !IsEmpty && Begin <= date.Date && End >= date.Date;
        }

        /// <summary>
        /// Creates range from today and number of days. If <paramref name="days"/> is less than 0, then range will end at today and begin will be in past. 
        /// If <paramref name="days"/> is greater than 0, then range will begin at today and end will be in future. If <paramref name="days"/> is 0, then begin and end is today.
        /// </summary>
        /// <param name="days">The number of how many days range should contain.</param>
        /// <returns>A create range.</returns>
        public static DateRange FromDays(int days)
        {
            return FromDays(DateTime.Today, days);
        }

        /// <summary>
        /// Creates range from specified date and number of days. If <paramref name="days"/> is less than 0, then range will end at <paramref name="date"/> and begin will be in past.
        /// If <paramref name="days"/> is greater than 0, then range will begin at <paramref name="date"/> and end will be in future. If <paramref name="days"/> is 0, then begin and end is <paramref name="date"/>.
        /// </summary>
        /// <example>If <paramref name="date"/> is 1.1.2000 and <paramref name="days"/> is 3, then range is from 1.1.2000 to 4.1.2000.</example>
        /// <param name="date">The reference date.</param>
        /// <param name="days">The number of how many days range should contain.</param>
        /// <returns>A create range.</returns>
        public static DateRange FromDays(DateTime date, int days)
        {
            if (days < 0)
                return FromNegativeDays(date, days);
            else if (days > 0)
                return FromPositiveDays(date, days);
            else
                return new DateRange(date);
        }

        private static DateRange FromNegativeDays(DateTime date, int days)
        {
            var end = date;
            var begin = end.AddDays(days);
            return new DateRange(begin, end);
        }

        private static DateRange FromPositiveDays(DateTime date, int days)
        {
            var begin = date;
            var end = begin.AddDays(days);
            return new DateRange(begin, end);
        }

        /// <summary>
        /// Gets all dates in range.
        /// </summary>
        /// <returns>A enumerable of all dates in range.</returns>
        public IEnumerable<DateTime> GetDates()
        {
            if (IsEmpty)
                yield break;

            if (Days == 1)
                yield return Begin;
            else
            { 
                for (int value = 0; value < Days; value++)
                    yield return Begin.AddDays(value);
            }
        }

        /// <summary>
        /// Gets all dates in range that are on specified week day.
        /// </summary>
        /// <param name="weekDay">The week day.</param>
        /// <returns>A enumerable of all days in range that are on <paramref name="weekDay"/>.</returns>
        public IEnumerable<DateTime> GetDates(DayOfWeek weekDay)
            => GetDates().Where(date => date.DayOfWeek == weekDay);

        /// <summary>
        /// Gets all dates in range that are on specified week days.
        /// </summary>
        /// <param name="weekDays">The week days.</param>
        /// <returns>A enumerable of all days in range that are on <paramref name="weekDays"/>.</returns>
        public IEnumerable<DateTime> GetDates(IEnumerable<DayOfWeek> weekDays)
            => GetDates().Where(date => weekDays.Contains(date.DayOfWeek));

        /// <summary>
        /// Gets all dates in range that are on specified week day and year and month.
        /// </summary>
        /// <param name="weekDay">The week day or <c>null</c>, if all.</param>
        /// <param name="year">The year or <c>null</c>, if all.</param>
        /// <param name="month">The month or <c>null</c>, if all.</param>
        /// <returns>A enumerable of all days in range that match specified criteria.</returns>
        public IEnumerable<DateTime> GetDates(DayOfWeek? weekDay, int? year, int? month)
        {
            var dates = GetDates();

            if (year.HasValue)
                dates = dates.Where(date => date.Year == year.Value);

            if (month.HasValue)
                dates = dates.Where(date => date.Month == month.Value);

            if (weekDay.HasValue)
                dates = dates.Where(date => date.DayOfWeek == weekDay.Value);

            return dates;
        }

        /// <summary>
        /// Gets count of days specifying if weekends should be included or not.
        /// </summary>
        /// <param name="includeWeekends"><c>true</c> to include weekends, same as <see cref="Days"/> or <c>false</c> to exclude weekends i.e. saturdays and sundays.</param>
        /// <returns>A count of days with or without saturdays and sundays.</returns>
        public int GetTotalDays(bool includeWeekends)
        {
            if (includeWeekends)
                return Days;

            return GetDates([DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday]).Count();
        }

        /// <summary>
        /// Gets count of days of specified week day.
        /// </summary>
        /// <param name="weekDay">The week day.</param>
        /// <returns>A count of how many <paramref name="weekDay"/> there is in range.</returns>
        public int GetTotalDays(DayOfWeek weekDay)
            => GetDates(weekDay).Count();

        /// <summary>
        /// Gets count of days of specified week days.
        /// </summary>
        /// <param name="weekDays">The week days.</param>
        /// <returns>A count of how many <paramref name="weekDays"/> there is in range.</returns>
        public int GetTotalDays(IEnumerable<DayOfWeek>? weekDays)
        {
            if (weekDays == null)
                return Days;

            return GetDates(weekDays).Count();
        }

        /// <summary>
        /// Check if range ends after specified date.
        /// </summary>
        /// <param name="date">The date to compare.</param>
        /// <returns><c>true</c> if end of range is after <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool EndsAfter(DateTime date)
        {
            return !IsEmpty && End > date.Date;
        }

        /// <summary>
        /// Check if range ends after end of specified other range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if end of this range is after end of <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool EndsAfter(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return End > other.End;
        }

        /// <summary>
        /// Check if range ends before specified date.
        /// </summary>
        /// <param name="date">The date to compare.</param>
        /// <returns><c>true</c> if end of range is before <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool EndsBefore(DateTime date)
        {
            return !IsEmpty && End < date.Date;
        }

        /// <summary>
        /// Check if range ends before end of specified other range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if end of this range is before end of <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool EndsBefore(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;
            
            return End < other.End;
        }

        /// <summary>
        /// Check if this range contains other range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if other range is included in this range; <c>false</c> otherwise.</returns>
        public bool Includes(DateRange other)
        {
            return !IsEmpty && !other.IsEmpty 
                && Begin <= other.Begin 
                && End >= other.End;
        }

        /// <summary>
        /// Check if this range is adjacent to other range. So this range either ends day before other begin or
        /// other ends day after this begin.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if this range is adjacent to <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool IsAdjacentTo(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return End.AddDays(1) == other.Begin ||
                other.End.AddDays(1) == Begin;
        }

        /// <summary>
        /// Check if this range begins after other range ends.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if begin of this range is after end of <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool IsAfter(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return Begin > other.End;
        }

        /// <summary>
        /// Check if this range begins after specified date.
        /// </summary>
        /// <param name="date">The date to compare.</param>
        /// <returns><c>true</c> if begin of this range is after <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool IsAfter(DateTime date)
        {
            return !IsEmpty && Begin > date.Date;
        }

        /// <summary>
        /// Check is this range begin day after other ends.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if end of <paramref name="other"/> is day before this begins; <c>false</c> otherwise.</returns>
        public bool IsImmediatelyAfter(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return other.End.AddDays(1) == Begin;
        }

        /// <summary>
        /// Check is this range begin day after specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns><c>true</c> if begin of this is day after <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool IsImmediatelyAfter(DateTime date)
        {
            return !IsEmpty && date.AddDays(1).Date == Begin;
        }

        /// <summary>
        /// Check if this range ends before other range begins.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if end of this range is before end of <paramref name="other"/>; <c>false</c> otherwise.</returns>
        public bool IsBefore(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return End < other.Begin;
        }

        /// <summary>
        /// Check if this range ends before specified date.
        /// </summary>
        /// <param name="date">The date to compare.</param>
        /// <returns><c>true</c> if end of this range is before <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool IsBefore(DateTime date)
        {
            return !IsEmpty && End < date.Date;
        }

        /// <summary>
        /// Check if end of this is immediately before other begins.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if end of this is one day earlier than other begins; <c>false</c> otherwise.</returns>
        public bool IsImmediatelyBefore(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            return End.AddDays(1) == other.Begin;
        }

        /// <summary>
        /// Check if end of this is immediately before specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns><c>true</c> if end of this is one day earlier than <paramref name="date"/>; <c>false</c> otherwise.</returns>
        public bool IsImmediatelyBefore(DateTime date)
        {
            return !IsEmpty && End.AddDays(1) == date.Date;
        }

        /// <summary>
        /// Gets the dates between end of <paramref name="first"/> and begin of <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first range.</param>
        /// <param name="second">The second range.</param>
        /// <returns>A dates between <paramref name="first"/> and <paramref name="second"/>.</returns>
        public static IEnumerable<DateTime> GetDatesBetween(DateRange first, DateRange second)
        {
            var range = GetRangeBetween(first, second);
            return range.IsEmpty ? Enumerable.Empty<DateTime>() : range.GetDates();
        }

        /// <summary>
        /// Gets <see cref="DateRange"/> of dates between end of <paramref name="first"/> and begin of <paramref name="second"/>.
        /// </summary>
        /// <param name="first">The first range.</param>
        /// <param name="second">The second range.</param>
        /// <returns>A <see cref="DateRange"/> of dates between <paramref name="first"/> and <paramref name="second"/>.</returns>
        public static DateRange GetRangeBetween(DateRange first, DateRange second)
        {
            if (first.IsEmpty || second.IsEmpty || first.Equals(second) || first.End >= second.Begin)
                return Empty;

            var begin = first.End.AddDays(1);
            var end = second.Begin.AddDays(-1);
            return new DateRange(begin, end);
        }

        /// <summary>
        /// Gets range of overlapping days between this and other range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns>A <see cref="DateRange"/> of overlapping days.</returns>
        public DateRange Overlap(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return Empty;

            if (Equals(other))
                return this;

            if (IsBefore(other) || IsAfter(other) || other.IsBefore(this) || other.IsAfter(this))
                return Empty;

            var begin = ComparableHelper.Max(Begin, other.Begin);
            var end = ComparableHelper.Min(End, other.End);
            return new DateRange(begin, end);
        }

        /// <summary>
        /// Gets range of overlapping days between this and several other ranges.
        /// </summary>
        /// <param name="ranges">The other ranges.</param>
        /// <returns>A <see cref="DateRange"/> of overlapping days.</returns>
        public DateRange Overlap(IEnumerable<DateRange> ranges)
        {
            DateRange overlap = this;
            foreach (var range in ranges)
                overlap = overlap.Overlap(range);
            return overlap;
        }

        /// <summary>
        /// Check if this range overlaps with other range.
        /// </summary>
        /// <param name="other">The other range.</param>
        /// <returns><c>true</c> if this range and <paramref name="other"/> overlap; <c>false</c> otherwise.</returns>
        public bool Overlaps(DateRange other)
        {
            if (IsEmpty || other.IsEmpty)
                return false;

            if (Equals(other))
                return true;

            return Begin <= other.End &&
                End >= other.Begin;
        }

        /// <summary>
        /// Shift this range by specified number of years, months and days.
        /// </summary>
        /// <param name="part">The <see cref="DateRangePart"/> to shift.</param>
        /// <param name="years">The years to shift.</param>
        /// <param name="months">The months to shift.</param>
        /// <param name="days">The days to shift.</param>
        /// <returns>A result range of the shift.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="part"/> is not defined.</exception>
        public DateRange Shift(DateRangePart part, int years, int months, int days)
        {
            if (!Enum.IsDefined(part))
                throw new ArgumentException("The value is not defined.", nameof(part));

            var newBegin = part != DateRangePart.End ? Begin.Shift(years, months, days) : Begin;
            var newEnd = part != DateRangePart.Begin ? End.Shift(years, months, days) : End;
            return new DateRange(newBegin, newEnd);
        }

        /// <summary>
        /// Split range to several ranges each having specified count of days.
        /// </summary>
        /// <param name="days">The count of days in each range.</param>
        /// <returns>A enumerable of split date ranges.</returns>
        public IEnumerable<DateRange> Split(int days)
        {
            if (IsEmpty)
                yield break;

            if (days <= 1)
                yield return this;
            else
            {
                int total = 0;
                int skip = 0;
                int take = days;

                while (total < Days)
                {
                    var dates = GetDates().Skip(skip).Take(take).ToArray();
                    total += take;
                    skip += take;
                    take = (Days - total > days) ? days : Days - total;
                    if (dates.Length > 1)
                        yield return new DateRange(dates[0], dates[dates.Length - 1]);
                    else if (dates.Length == 1)
                        yield return new DateRange(dates[0]);
                }
            }
        }

        /// <summary>
        /// Split range to several ranges by month.
        /// </summary>
        /// <returns>A enumerable of date ranges by month.</returns>
        public IEnumerable<DateRange> SplitByMonth()
        {
            var groups = GetDates().GroupBy(
                dt => { return dt.Month; },
                dt => { return dt; });

            foreach (var group in groups)
                yield return ToDateRange(group);
        }

        /// <summary>
        /// Split range to several ranges by year.
        /// </summary>
        /// <returns>A enumerable of date ranges by year.</returns>
        public IEnumerable<DateRange> SplitByYear()
        {
            var groups = GetDates().GroupBy(
                dt => { return dt.Year; },
                dt => { return dt; });

            foreach (var group in groups)
                yield return ToDateRange(group);
        }

        /// <summary>
        /// Check if other <see cref="DateRange"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="DateRange"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(DateRange other)
        {
            return IsEmpty == other.IsEmpty &&
                Begin.Equals(other.Begin) &&
                End.Equals(other.End) &&
                Days == other.Days;
        }

        /// <summary>
        /// Check if object is <see cref="DateRange"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="DateRange"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is DateRange other)
                return Equals(other);

            return false;
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            if (IsEmpty)
                return 0;

            return Begin.GetHashCode() ^ End.GetHashCode() ^ Days;
        }

        /// <summary>
        /// Creates copy.
        /// </summary>
        /// <returns>A copy date range.</returns>
        public DateRange Clone() => new DateRange(this);

        /// <summary>
        /// Gets string representation of this range.
        /// </summary>
        /// <returns>A string representation.</returns>
        public override string ToString()
        {
            if (IsEmpty)
                return string.Empty;

            return $"{Begin.ToShortDateString()} {End.ToShortDateString()}";
        }

        /// <summary>
        /// Gets range from week of specified date using current culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A <see cref="DateRange"/> of week where <paramref name="date"/> is.</returns>
        public static DateRange GetWeek(DateTime date)
            => GetWeek(date, CultureInfo.CurrentCulture);

        /// <summary>
        /// Gets range from week of specified date using specified culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>A <see cref="DateRange"/> of week where <paramref name="date"/> is.</returns>
        public static DateRange GetWeek(DateTime date, CultureInfo culture)
            => GetWeek(date, culture.DateTimeFormat.FirstDayOfWeek);

        /// <summary>
        /// Gets range from week of specified date using specified first day of week.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="firstDayOfWeek">The first day of week.</param>
        /// <returns>A <see cref="DateRange"/> of week where <paramref name="date"/> is.</returns>
        /// <exception cref="ArgumentException">If <paramref name="firstDayOfWeek"/> is not sunday or monday.</exception>
        public static DateRange GetWeek(DateTime date, DayOfWeek firstDayOfWeek)
        {
            if (firstDayOfWeek == DayOfWeek.Sunday || firstDayOfWeek == DayOfWeek.Monday)
            {
                var begin = date.DayOfWeek == firstDayOfWeek ? date : date.GetPreviousWeekDay(firstDayOfWeek);
                var end = begin.AddDays(6);
                return new DateRange(begin, end);
            }
            else
                throw new ArgumentException("The week must begin either sunday or monday.", nameof(firstDayOfWeek));
        }

        /// <summary>
        /// Gets range from month of specified date using calendar of specified culture.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="culture">The culture.</param>
        /// <returns>A <see cref="DateRange"/> of month where <paramref name="date"/> is.</returns>
        public static DateRange GetMonth(DateTime date, CultureInfo culture)
        {
            var days = culture.Calendar.GetDaysInMonth(date.Year, date.Month);
            return GetMonth(date, days);
        }

        /// <summary>
        /// Gets range from month of specified date.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <returns>A <see cref="DateRange"/> of month where <paramref name="date"/> is.</returns>
        public static DateRange GetMonth(DateTime date)
        {
            var days = DateTime.DaysInMonth(date.Year, date.Month);
            return GetMonth(date, days);
        }

        /// <summary>
        /// Gets random date from range.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <returns>A random date from range or <c>null</c>, if this represents empty range.</returns>
        public DateTime? GetRandomDate(Random random)
        {
            if (IsEmpty)
                return null;

            if (Days == 1)
                return Begin;

            var dates = GetDates().ToArray();
            var index = random.Next(0, dates.Length);
            return dates[index];
        }

        /// <summary>
        /// Gets random date from specified date range.
        /// </summary>
        /// <param name="random">The random.</param>
        /// <param name="min">The minimum date.</param>
        /// <param name="max">The maximum date.</param>
        /// <returns>A random date from range specified by <paramref name="min"/> and <paramref name="max"/>.</returns>
        public static DateTime GetRandomDate(Random random, DateTime min, DateTime max)
        {
            if (min == max)
                return min;

            if (min > max)
                ObjectHelper.Swap(ref min, ref max);

            return new DateRange(min, max).GetRandomDate(random).GetValueOrDefault();
        }

        /// <summary>
        /// Check if two ranges are equal.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns><c>true</c> if ranges are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(DateRange left, DateRange right) => left.Equals(right);

        /// <summary>
        /// Check if two ranges are not equal.
        /// </summary>
        /// <param name="left">The left range.</param>
        /// <param name="right">The right range.</param>
        /// <returns><c>true</c> if ranges are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(DateRange left, DateRange right)
        {
            return !(left == right);
        }

        private DateRange(DateRange other)
        {
            begin = other.begin;
            end = other.end;
            days = other.days;
        }

        private static DateRange ToDateRange(IGrouping<int, DateTime> group)
        {
            var begin = group.First();
            var end = group.Last();
            if (begin.Equals(end))
                return new DateRange(begin);
            else
                return new DateRange(begin, end);
        }

        private static DateRange GetMonth(DateTime date, int days)
        {
            var begin = new DateTime(date.Year, date.Month, 1);
            var end = begin.AddDays(days - 1);
            return new DateRange(begin, end);
        }

        object ICloneable.Clone() => Clone();
    }
}
