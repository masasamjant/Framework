namespace Masasamjant
{
    /// <summary>
    /// Defines how accurately <see cref="DateTime"/> and <see cref="DateTimeOffset"/> should be used.
    /// </summary>
    public enum DateTimeAccuracy
    {
        /// <summary>
        /// Only the year is accurate.
        /// </summary>
        Year = 0,

        /// <summary>
        /// Only the year and month are accurate.
        /// </summary>
        Month = 1,

        /// <summary>
        /// Only the year, month and day are accurate.
        /// </summary>
        Day = 2,

        /// <summary>
        /// Only date and hour is accurate.
        /// </summary>
        Hour = 3,

        /// <summary>
        /// Only date, hour and minute are accurate.
        /// </summary>
        Minute = 4,

        /// <summary>
        /// Only date, hour, minute, and second are accurate.
        /// </summary>
        Second = 5,

        /// <summary>
        /// Only date, hour, minute, second and millisecond are accurate.
        /// </summary>
        Millisecond = 6
    }
}
