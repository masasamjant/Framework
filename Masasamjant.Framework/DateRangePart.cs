namespace Masasamjant
{
    /// <summary>
    /// Defines the part of the <see cref="DateRange"/> to operate on.
    /// </summary>
    public enum DateRangePart : int
    {
        /// <summary>
        /// Both begin and end date.
        /// </summary>
        Both = 0,

        /// <summary>
        /// Only begin date.
        /// </summary>
        Begin = 1,

        /// <summary>
        /// Only end date.
        /// </summary>
        End = 2
    }
}
