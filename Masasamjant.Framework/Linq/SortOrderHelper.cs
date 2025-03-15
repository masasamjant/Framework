namespace Masasamjant.Linq
{
    /// <summary>
    /// Provides helper methods to <see cref="SortOrder"/> enumeration.
    /// </summary>
    public static class SortOrderHelper
    {
        /// <summary>
        /// Gets SQL string of specified <see cref="SortOrder"/> value.
        /// </summary>
        /// <param name="order">The <see cref="SortOrder"/>.</param>
        /// <returns>A SQL string.</returns>
        public static string ToSql(this SortOrder order)
        {
            switch (order)
            {
                case SortOrder.Ascending:
                    return "ASC";
                case SortOrder.Descending:
                    return "DESC";
                default:
                    return string.Empty;
            }
        }
    }
}
