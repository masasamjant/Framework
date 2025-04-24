using Masasamjant.Resources;
using Masasamjant.Resources.Strings;

namespace Masasamjant
{
    /// <summary>
    /// Defines logical operators between two values.
    /// </summary>
    public enum LogicalOperator : int
    {
        /// <summary>
        /// None
        /// </summary>
        [ResourceString(nameof(LogicalOperatorResource.None), typeof(LogicalOperatorResource), UseNonPublicResource = true)]
        None = 0,

        /// <summary>
        /// And
        /// </summary>
        [ResourceString(nameof(LogicalOperatorResource.And), typeof(LogicalOperatorResource), UseNonPublicResource = true)]
        And = 1,

        /// <summary>
        /// Or
        /// </summary>
        [ResourceString(nameof(LogicalOperatorResource.Or), typeof(LogicalOperatorResource), UseNonPublicResource = true)]
        Or = 2
    }
}
