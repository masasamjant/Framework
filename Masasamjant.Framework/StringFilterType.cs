namespace Masasamjant
{
    /// <summary>
    /// Defines supported string filter types.
    /// </summary>
    public enum StringFilterType : int
    {
        /// <summary>
        /// Exact match filter.
        /// </summary>
        Match = 0,

        /// <summary>
        /// Contains filter.
        /// </summary>
        Contains = 1,

        /// <summary>
        /// Starts with filter.
        /// </summary>
        StartsWith = 2,

        /// <summary>
        /// Ends with filter.
        /// </summary>
        EndsWith = 3
    }
}
