using Masasamjant.Resources;
using Masasamjant.Resources.Strings;

namespace Masasamjant
{
    /// <summary>
    /// Defines behavior of how error is handled.
    /// </summary>
    public enum ErrorBehavior : int
    {
        /// <summary>
        /// Throw exception.
        /// </summary>
        [ResourceString(nameof(ErrorBehaviorResource.Throw), typeof(ErrorBehaviorResource), UseNonPublicResource = true)]
        Throw = 0,

        /// <summary>
        /// Cancel operation, if possible.
        /// </summary>
        [ResourceString(nameof(ErrorBehaviorResource.Cancel), typeof(ErrorBehaviorResource), UseNonPublicResource = true)]
        Cancel = 1,

        /// <summary>
        /// Retry operation.
        /// </summary>
        [ResourceString(nameof(ErrorBehaviorResource.Retry), typeof(ErrorBehaviorResource), UseNonPublicResource = true)]
        Retry = 2,

        /// <summary>
        /// Ignore error and continue operation, if possible.
        /// </summary>
        [ResourceString(nameof(ErrorBehaviorResource.Ignore), typeof(ErrorBehaviorResource), UseNonPublicResource = true)]
        Ignore = 3
    }
}
