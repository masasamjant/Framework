using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Represents timing.
    /// </summary>
    [TypeConverter(typeof(TimingConverter))]
    public readonly struct Timing : IEquatable<Timing>, ICloneable
    {
        /// <summary>
        /// Initializes new value of the <see cref="Timing"/> within single day.
        /// </summary>
        /// <param name="date">The date of the timing.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endTime">The end time.</param>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="endTime"/> is earlier than value of <paramref name="startTime"/>.</exception>
        public Timing(DateOnly date, TimeOnly startTime, TimeOnly endTime)
            : this(date, startTime, date, endTime)
        { }

        /// <summary>
        /// Initializes new value of the <see cref="Timing"/> with specified dates and times.
        /// </summary>
        /// <param name="startDate">The start date.</param>
        /// <param name="startTime">The start time.</param>
        /// <param name="endDate">The end date.</param>
        /// <param name="endTime">The end time.</param>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If value of <paramref name="endDate"/> is earlier than value of <paramref name="startDate"/>.
        /// -or-
        /// If <paramref name="startDate"/> and <paramref name="endDate"/> are same and value of <paramref name="endTime"/> is earlier than value of <paramref name="startTime"/>.
        /// </exception>
        public Timing(DateOnly startDate, TimeOnly startTime, DateOnly endDate, TimeOnly endTime)
        {
            if (endDate < startDate)
                throw new ArgumentOutOfRangeException(nameof(endDate), endDate, "End date is earlier than start date.");

            if (endDate == startDate && endTime < startTime)
                throw new ArgumentOutOfRangeException(nameof(endTime), endTime, "When in same date, then end time must be same or later than start time.");

            StartDate = startDate;
            StartTime = startTime;
            EndDate = endDate;
            EndTime = endTime;
        }

        /// <summary>
        /// Initializes new value of the <see cref="Timing"/> with specified start and end <see cref="DateTime"/>.
        /// </summary>
        /// <param name="startDateTime">The start date and time.</param>
        /// <param name="endDateTime">The end date and time.</param>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="endDateTime"/> is earlier than value of <paramref name="startDateTime"/>.</exception>
        public Timing(DateTime startDateTime, DateTime endDateTime)
            : this(DateOnly.FromDateTime(startDateTime), TimeOnly.FromDateTime(startDateTime), DateOnly.FromDateTime(endDateTime), TimeOnly.FromDateTime(endDateTime))
        { }

        /// <summary>
        /// Get the start date.
        /// </summary>
        public DateOnly StartDate { get; }

        /// <summary>
        /// Gets the start time.
        /// </summary>
        public TimeOnly StartTime { get; }

        /// <summary>
        /// Gets the end date.
        /// </summary>
        public DateOnly EndDate { get; }

        /// <summary>
        /// Gets the end time.
        /// </summary>
        public TimeOnly EndTime { get; }

        /// <summary>
        /// Gets the <see cref="StartDate"/> and <see cref="StartTime"/> as local <see cref="DateTime"/>.
        /// </summary>
        public DateTime StartDateTime
        {
            get { return new DateTime(StartDate, StartTime, DateTimeKind.Local); }
        }

        /// <summary>
        /// Gets the <see cref="EndDate"/> and <see cref="EndTime"/> as local <see cref="DateTime"/>.
        /// </summary>
        public DateTime EndDateTime
        {
            get { return new DateTime(EndDate, EndTime, DateTimeKind.Local); }
        }

        /// <summary>
        /// Gets the duration of timing.
        /// </summary>
        public TimeSpan Duration => EndDateTime.Subtract(StartDateTime);

        /// <summary>
        /// Check if other <see cref="Timing"/> equal with this.
        /// </summary>
        /// <param name="other">The other timing.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(Timing other)
        {
            return StartDateTime.Equals(other.StartDateTime) &&
                EndDateTime.Equals(other.EndDateTime);
        }

        /// <summary>
        /// Check if object instance is <see cref="Timing"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="Timing"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals([NotNullWhen(true)] object? obj)
        {
            if (obj is Timing other)
                return Equals(other); 
            return false;
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(StartDateTime, EndDateTime);
        }

        /// <summary>
        /// Check if this timing contains specified local <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="value">The value to check.</param>
        /// <returns><c>true</c> if contains <paramref name="value"/>; <c>false</c> otherwise.</returns>
        public bool Contains(DateTime value)
        {
            return value >= StartDateTime && value <= EndDateTime;
        }

        /// <summary>
        /// Check if this timing contains specified date and time.
        /// </summary>
        /// <param name="date">The date.</param>
        /// <param name="time">The time.</param>
        /// <returns><c>true</c> if contains specified date and time; <c>false</c> otherwise.</returns>
        public bool Contains(DateOnly date, TimeOnly time)
            => Contains(new DateTime(date, time, DateTimeKind.Local));

        /// <summary>
        /// Gets string presentation.
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            if (StartDate == EndDate)
                return $"{StartDate}: {StartTime} - {EndTime}";
            else
                return $"{StartDate}: {StartTime} - {EndDate}: {EndTime}";
        }

        /// <summary>
        /// Check if timings are equal.
        /// </summary>
        /// <param name="left">The left timing.</param>
        /// <param name="right">The right timing.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are equal; <c>false</c> otherwise.</returns>
        public static bool operator ==(Timing left, Timing right) => left.Equals(right);

        /// <summary>
        /// Check if timings are not equal.
        /// </summary>
        /// <param name="left">The left timing.</param>
        /// <param name="right">The right timing.</param>
        /// <returns><c>true</c> if <paramref name="left"/> and <paramref name="right"/> are not equal; <c>false</c> otherwise.</returns>
        public static bool operator !=(Timing left, Timing right) => !(left == right);

        /// <summary>
        /// Creates timing from hours beginning from specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="startDateTime">The start date and time of timing.</param>
        /// <param name="hours">The duration in hours.</param>
        /// <returns>A <see cref="Timing"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="hours"/> is less than 0.</exception>
        public static Timing FromHours(DateTime startDateTime, int hours)
        {
            if (hours < 0)
                throw new ArgumentOutOfRangeException(nameof(hours), hours, "The value must be greater than or equal to 0.");
            else if (hours == 0)
                return new Timing(startDateTime, startDateTime);
            else
                return new Timing(startDateTime, startDateTime.AddHours(hours));
        }

        /// <summary>
        /// Creates timing from minutes beginning from specified <see cref="DateTime"/>.
        /// </summary>
        /// <param name="startDateTime">The start date and time of timing.</param>
        /// <param name="minutes">The duration in minutes.</param>
        /// <returns>A <see cref="Timing"/>.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="minutes"/> is less than 0.</exception>
        public static Timing FromMinutes(DateTime startDateTime, int minutes)
        {
            if (minutes < 0)
                throw new ArgumentOutOfRangeException(nameof(minutes), minutes, "The value must be greater than or equal to 0.");
            else if (minutes == 0)
                return new Timing(startDateTime, startDateTime);
            else
                return new Timing(startDateTime, startDateTime.AddMinutes(minutes));
        }

        /// <summary>
        /// Extend current timing by specified duration. This means that end date and time of current timing change and start remains same.
        /// </summary>
        /// <param name="duration">The duration to extend.</param>
        /// <returns>A extended timing.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="duration"/> represents negative time interval.</exception>
        public Timing Extend(TimeSpan duration)
        {
            if (duration < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(duration), duration, "The extend duration cannot be negative.");

            if (duration == TimeSpan.Zero)
                return new Timing(StartDateTime, EndDateTime);

            DateTime startDateTime = StartDateTime;
            DateTime endDateTime = EndDateTime.Add(duration);
            return new Timing(startDateTime, endDateTime);
        }

        /// <summary>
        /// Shorten current timing by specified duration. This means that end date and time of current timing change and start remains same.
        /// </summary>
        /// <param name="duration">The duration to shorten.</param>
        /// <returns>A shortened timing.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// If <paramref name="duration"/> represents negative time interval.
        /// -or-
        /// If value of <paramref name="duration"/> is longer than <see cref="Duration"/>.
        /// </exception>
        public Timing Shorten(TimeSpan duration)
        {
            if (duration < TimeSpan.Zero)
                throw new ArgumentOutOfRangeException(nameof(duration), duration, "The shorten duration cannot be negative.");

            if (duration == TimeSpan.Zero)
                return new Timing(StartDateTime, EndDateTime);

            if (duration > Duration)
                throw new ArgumentOutOfRangeException(nameof(duration), duration, "The shorten duration cannot be greater than current duration.");

            DateTime startDateTime = StartDateTime;
            DateTime endDateTime = EndDateTime.Subtract(duration);
            return new Timing(startDateTime, endDateTime);
        }

        /// <summary>
        /// Create copy from current timing.
        /// </summary>
        /// <returns>A copy from current timing.</returns>
        public Timing Clone() => new(StartDateTime, EndDateTime);

        object ICloneable.Clone()
        {
            return this.Clone();
        }
    }
}
