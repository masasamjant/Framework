using Masasamjant.Resources;
using Masasamjant.Resources.Strings;

namespace Masasamjant
{
    /// <summary>
    /// Defines components of time.
    /// </summary>
    public enum TimeComponent : int
    {
        /// <summary>
        /// Hour component.
        /// </summary>
        [ResourceString(nameof(TimeComponentResource.Hour), typeof(TimeComponentResource), UseNonPublicResource = true)]
        Hour = 0,

        /// <summary>
        /// Minute component.
        /// </summary>
        [ResourceString(nameof(TimeComponentResource.Minute), typeof(TimeComponentResource), UseNonPublicResource = true)]
        Minute = 1,

        /// <summary>
        /// Second component.
        /// </summary>
        [ResourceString(nameof(TimeComponentResource.Second), typeof(TimeComponentResource), UseNonPublicResource = true)]
        Second = 2,

        /// <summary>
        /// Millisecond component.
        /// </summary>
        [ResourceString(nameof(TimeComponentResource.Millisecond), typeof(TimeComponentResource), UseNonPublicResource = true)]
        Millisecond = 3,

        /// <summary>
        /// Microsecond component.
        /// </summary>
        [ResourceString(nameof(TimeComponentResource.Microsecond), typeof(TimeComponentResource), UseNonPublicResource = true)]
        Microsecond = 4
    }
}
