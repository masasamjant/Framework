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

        /// <summary>
        /// Gets chain of items of <typeparamref name="T"/> starting from <paramref name="item"/> and following the chain by <paramref name="next"/> function 
        /// until <paramref name="getNext"/> returns <c>false</c>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The start item.</param>
        /// <param name="next">The function to get the next item in the chain.</param>
        /// <param name="getNext">The function to determine if the next item should be included in the chain.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> representing the chain of items.</returns>
        public static IEnumerable<T> GetChain<T>(this T item, Func<T, T> next, Func<T, bool> getNext)
        {
            ArgumentNullException.ThrowIfNull(item);
            ArgumentNullException.ThrowIfNull(next);
            ArgumentNullException.ThrowIfNull(getNext);

            for (var current = item; getNext(current); current = next(current))
                yield return current;
        }

        /// <summary>
        /// Gets the chain of items of <typeparamref name="T"/> starting from <paramref name="item"/> and following the chain by <paramref name="next"/> function 
        /// until the next item is <c>null</c>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="item">The start item.</param>
        /// <param name="next">The function to get the next item in the chain.</param>
        /// <returns>An <see cref="IEnumerable{T}"/> representing the chain of items.</returns>
        public static IEnumerable<T> GetChain<T>(this T item, Func<T, T> next)
            => GetChain<T>(item, next, x => x is not null);
    }
}
