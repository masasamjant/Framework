namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Defines what date part is required in <see cref="DateTime"/> or <see cref="DateTimeOffset"/> value.
    /// </summary>
    public enum RequiredDate : int
    {
        /// <summary>
        /// Date part must be todays date.
        /// </summary>
        Today = 0,

        /// <summary>
        /// Date part must be yesterdays date.
        /// </summary>
        Yesterday = 1,

        /// <summary>
        /// Date part must be tomorrows date.
        /// </summary>
        Tomorrow = 2
    }
}
