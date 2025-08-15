namespace Masasamjant.Diagnostics
{
    /// <summary>
    /// Represents record created by <see cref="StopwatchRun"/>.
    /// </summary>
    public sealed class StopwatchRunRecord
    {
        internal StopwatchRunRecord(StopwatchRunBehavior behavior, StopwatchRunUnit unit, string message, long elapsedTime)
        {
            Behavior = behavior;
            Unit = unit;
            Message = message;
            ElapsedTime = elapsedTime;
        }

        /// <summary>
        /// Gets the <see cref="StopwatchRunBehavior"/>.
        /// </summary>
        public StopwatchRunBehavior Behavior { get; }

        /// <summary>
        /// Gets the <see cref="StopwatchRunUnit"/>.
        /// </summary>
        public StopwatchRunUnit Unit { get; }

        /// <summary>
        /// Gets the record message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the elapsed time.
        /// </summary>
        public long ElapsedTime { get; }
    }
}
