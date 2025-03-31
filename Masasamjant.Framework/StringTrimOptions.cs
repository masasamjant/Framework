namespace Masasamjant
{
    /// <summary>
    /// Defines how string value is trimmed.
    /// </summary>
    [Flags]
    public enum StringTrimOptions : int
    {
        /// <summary>
        /// No trimming.
        /// </summary>
        None = 0,

        /// <summary>
        /// Trim from start.
        /// </summary>
        Start = 1,

        /// <summary>
        /// Trim from end.
        /// </summary>
        End = 2
    }
}
