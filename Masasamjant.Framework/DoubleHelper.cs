namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to work with <see cref="double"/> values.
    /// </summary>
    public static class DoubleHelper
    {
        /// <summary>
        /// Value of zero.
        /// </summary>
        public const double Zero = default;

        /// <summary>
        /// Replace not-a-number with specified value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="replacement">The replacement value.</param>
        /// <returns>A <paramref name="value"/> if not <see cref="double.NaN"/>; otherwise <paramref name="replacement"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="replacement"/> is <see cref="double.NaN"/>.</exception>
        public static double ReplaceNaN(this double value, double replacement)
        {
            if (double.IsNaN(replacement))
                throw new ArgumentException("Replacement value can not be double.NaN.", nameof(replacement));

            return double.IsNaN(value) ? replacement : value;
        }

        /// <summary>
        /// Replace positive or negative infinity with specified value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        /// <param name="replacement">The replacement value.</param>
        /// <returns>A <paramref name="value"/> if not positive or negative infinity; otherwise <paramref name="replacement"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="replacement"/> is positive or negative infinity.</exception>
        public static double ReplaceInfinity(this double value, double replacement)
        {
            if (double.IsInfinity(replacement))
                throw new ArgumentException("Replacement value cannot be positive or negative infinity.", nameof(replacement));

            return double.IsInfinity(value) ? replacement : value;
        }

        /// <summary>
        /// Determines whether two double values are equal up to a specified number of decimal places.
        /// </summary>
        /// <param name="first">The first value.</param>
        /// <param name="second">The second value.</param>
        /// <param name="digits">The number of decimal places to consider.</param>
        /// <returns><c>true</c> if the values are equal up to the specified number of decimal places; otherwise, <c>false</c>.</returns>
        public static bool AreEqual(double first, double second, int digits)
        {
            return Math.Round(first, digits) == Math.Round(second, digits);
        }
    }
}
