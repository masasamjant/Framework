using Masasamjant.Resources;
using Masasamjant.Resources.Strings;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Defines sort orders.
    /// </summary>
    public enum SortOrder : int
    {
        /// <summary>
        /// Not specified. Use initial or default sorting.
        /// </summary>
        [ResourceString(nameof(SortOrderResource.None), typeof(SortOrderResource), UseNonPublicResource = true)]
        None = 0,

        /// <summary>
        /// Ascending order.
        /// </summary>
        [ResourceString(nameof(SortOrderResource.Ascending), typeof(SortOrderResource), UseNonPublicResource = true)]
        Ascending = 1,

        /// <summary>
        /// Descending order.
        /// </summary>
        [ResourceString(nameof(SortOrderResource.Descending), typeof(SortOrderResource), UseNonPublicResource = true)]
        Descending = 2
    }
}
