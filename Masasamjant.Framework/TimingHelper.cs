namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to <see cref="Timing"/>.
    /// </summary>
    public static class TimingHelper
    {
        /// <summary>
        /// Check if timing covers <see cref="DateTime.Now"/>.
        /// </summary>
        /// <param name="timing">The timing.</param>
        /// <returns><c>true</c> if timing covers <see cref="DateTime.Now"/>; <c>false</c> otherwise.</returns>
        public static bool IsNow(this Timing timing)
        {
            return timing.Contains(DateTime.Now);
        }

        /// <summary>
        /// Check if timing is in future aka <see cref="Timing.StartDateTime"/> is later than <see cref="DateTime.Now"/>.
        /// </summary>
        /// <param name="timing">The timing.</param>
        /// <returns><c>true</c> if timing starts later than <see cref="DateTime.Now"/>; <c>false</c> otherwise.</returns>
        public static bool IsFuture(this Timing timing) 
        {
            return timing.StartDateTime > DateTime.Now;
        }

        /// <summary>
        /// Check if timing is over aka <see cref="Timing.EndDateTime"/> is earlier than <see cref="DateTime.Now"/>.
        /// </summary>
        /// <param name="timing">The timing.</param>
        /// <returns><c>true</c> if timing ends earlier than <see cref="DateTime.Now"/>; <c>false</c> otherwise.</returns>
        public static bool IsOver(this Timing timing)
        {
            return timing.EndDateTime < DateTime.Now;
        }
    }
}
