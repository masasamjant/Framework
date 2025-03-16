using Masasamjant.Resources;
using Masasamjant.Resources.Strings;

namespace Masasamjant
{
    /// <summary>
    /// Defines components of date and time.
    /// </summary>
    public enum DateTimeComponent : int
    {
        /// <summary>
        /// Ticks
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Ticks), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Ticks = 0,

        /// <summary>
        /// Microsecond
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Microsecond), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Microsecond = 1,

        /// <summary>
        /// Millisecond
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Millisecond), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Millisecond = 2,

        /// <summary>
        /// Second
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Second), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Second = 3,

        /// <summary>
        /// Minute
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Minute), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Minute = 4,

        /// <summary>
        /// Hour
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Hour), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Hour = 5,

        /// <summary>
        /// Day
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Day), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Day = 6,

        /// <summary>
        /// Month
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Month), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Month = 7,

        /// <summary>
        /// Year
        /// </summary>
        [ResourceString(nameof(DateTimeComponentResource.Year), typeof(DateTimeComponentResource), UseNonPublicResource = true)]
        Year = 8
    }
}
