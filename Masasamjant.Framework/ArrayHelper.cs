namespace Masasamjant
{
    /// <summary>
    /// Provides helper and extension methods to arrays.
    /// </summary>
    public static class ArrayHelper
    {
        /// <summary>
        /// Iterate specified array forward from start to end.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="array">The array to iterate.</param>
        /// <returns>A items of <paramref name="array"/> from start to end as <see cref="Iteration{T}"/>.</returns>
        public static IEnumerable<Iteration<T>> IterateForward<T>(this T[] array)
        {
            for (int index = 0; index < array.Length; index++)
                yield return new Iteration<T>(array[index], index);
        }

        /// <summary>
        /// Iterate specified array backward from end to start.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="array">The array to iterate.</param>
        /// <returns>A items of <paramref name="array"/> from end to start as <see cref="Iteration{T}"/>.</returns>
        public static IEnumerable<Iteration<T>> IterateBackward<T>(this T[] array)
        {
            for (int index = array.Length - 1; index >= 0; index--)
                yield return new Iteration<T>(array[index], index);
        }

        /// <summary>
        /// Combine two arrays into single one.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="array1">The first array.</param>
        /// <param name="array2">The second array.</param>
        /// <returns>A combined array.</returns>
        public static T[] Combine<T>(T[]? array1, T[]? array2)
        {
            if (array1 != null && array2 != null)
            {
                int length = array1.Length + array2.Length;
                var combined = new T[length];
                if (length == 0)
                    return combined;
                array1.CopyTo(combined, 0);
                array2.CopyTo(combined, array1.Length);
                return combined;
            }
            else if (array1 != null)
                return (T[])array1.Clone();
            else if (array2 != null)
                return (T[])array2.Clone();
            else
                return [];
        }

        /// <summary>
        /// Combine three arrays into single one.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="array1">The first array.</param>
        /// <param name="array2">The second array.</param>
        /// <param name="array3">The third array.</param>
        /// <returns>A combined array.</returns>
        public static T[] Combine<T>(T[]? array1, T[]? array2, T[]? array3)
            => Combine(Combine(array1, array2), array3);

        /// <summary>
        /// Combine four arrays into single one.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="array1">The first array.</param>
        /// <param name="array2">The second array.</param>
        /// <param name="array3">The third array.</param>
        /// <param name="array4">The fourth array.</param>
        /// <returns>A combined array.</returns>
        public static T[] Combine<T>(T[]? array1, T[]? array2, T[]? array3, T[]? array4)
            => Combine(Combine(array1, array2, array3), array4);
    }
}
