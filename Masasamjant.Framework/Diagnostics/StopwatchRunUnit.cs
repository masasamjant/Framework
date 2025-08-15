namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Defines in what unit <see cref="StopwatchRun"/> will record time.
    /// </summary>
    public enum StopwatchRunUnit : int
    {
        /// <summary>
        /// Record time using milliseconds.
        /// </summary>
        Milliseconds = 0,

        /// <summary>
        /// Record time using ticks.
        /// </summary>
        Ticks = 1
    }
}
