namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Defines supported reset behaviors of <see cref="StopwatchRun"/>.
    /// </summary>
    public enum StopwatchRunBehavior : int
    {
        /// <summary>
        /// Internal stopwatch is not reset between records.
        /// </summary>
        CalculateTotalTime = 0,

        /// <summary>
        /// Internal stopwatch is reset after each record.
        /// </summary>
        CalculateWriteTime = 1
    }
}
