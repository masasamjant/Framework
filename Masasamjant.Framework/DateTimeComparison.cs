namespace Masasamjant
{
    /// <summary>
    /// Defines how two <see cref="DateTime"/> and <see cref="DateTimeOffset"/> is compared.
    /// </summary>
    public enum DateTimeComparison : int
    {
        /// <summary>
        /// Compare date and time.
        /// </summary>
        DateTime = 0,

        /// <summary>
        /// Compare only dates.
        /// </summary>
        Date = 1,

        /// <summary>
        /// Compare only times.
        /// </summary>
        Time = 2
    }
}
