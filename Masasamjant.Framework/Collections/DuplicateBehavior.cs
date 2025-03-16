namespace Masasamjant.Collections
{
    /// <summary>
    /// Defines how duplicate item added or found from collection is handled.
    /// </summary>
    public enum DuplicateBehavior : int
    {
        /// <summary>
        /// Duplicate item is ignored.
        /// </summary>
        Ignore = 0,

        /// <summary>
        /// Insert duplicate item if collection can include duplicates.
        /// </summary>
        Insert = 1,

        /// <summary>
        /// Replace existing item with duplicate.
        /// </summary>
        Replace = 2,

        /// <summary>
        /// Throws one of <see cref="DuplicateException"/>s.
        /// </summary>
        Error = 3
    }
}
