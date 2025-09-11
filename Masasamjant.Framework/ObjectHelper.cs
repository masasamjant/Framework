namespace Masasamjant
{
    /// <summary>
    /// Provides general helper methods.
    /// </summary>
    public static class ObjectHelper
    {
        /// <summary>
        /// Swap values of <paramref name="left"/> and <paramref name="right"/>. After the swap <paramref name="left"/> will 
        /// have <paramref name="right"/> value and opposite.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        public static void Swap<T>(ref T left, ref T right)
        {
            T tmp = left;
            left = right;
            right = tmp;
        }

        /// <summary>
        /// Swap values of <paramref name="left"/> and <paramref name="right"/>, if the match specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <param name="match">The predicate to match to perform swap.</param>
        /// <returns><c>true</c> if match and values swapped; <c>false</c> otherwise.</returns>
        public static bool SwapIf<T>(ref T left, ref T right, Func<T, T, bool> match)
        {
            if (match(left, right))
            {
                Swap(ref left, ref right);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check if specified <see cref="Guid"/> value is <see cref="Guid.Empty"/>.
        /// </summary>
        /// <param name="value">The <see cref="Guid"/> value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is <see cref="Guid.Empty"/>; <c>false</c> otherwise.</returns>
        public static bool IsEmpty(this Guid value) => Guid.Empty.Equals(value);
    }
}
