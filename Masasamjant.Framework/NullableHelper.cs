namespace Masasamjant
{
    /// <summary>
    /// Provides helper and extension methods to nullable values.
    /// </summary>
    public static class NullableHelper
    {
        /// <summary>
        /// Gets first nullable <typeparamref name="T"/> with value.
        /// </summary>
        /// <typeparam name="T">The type of the nullable value.</typeparam>
        /// <param name="values">The nullable values.</param>
        /// <param name="defaultValue">The default value to return if none in <paramref name="values"/> has value.</param>
        /// <returns>
        /// A first value from <paramref name="values"/> that has value.
        /// -or-
        /// A <paramref name="defaultValue"/>, if no value or <paramref name="values"/> is empty.
        /// </returns>
        public static T? FirstWithValue<T>(this IEnumerable<T?> values, T? defaultValue = null) where T : struct
        {
            foreach (var value in values)
                if (value.HasValue)
                    return value;

            return defaultValue;
        }

        /// <summary>
        /// Gets last nullable <typeparamref name="T"/> with value.
        /// </summary>
        /// <typeparam name="T">The type of the nullable value.</typeparam>
        /// <param name="values">The nullable values.</param>
        /// <param name="defaultValue">The default value to return if none in <paramref name="values"/> has value.</param>
        /// <returns>
        /// A last value from <paramref name="values"/> that has value.
        /// -or-
        /// A <paramref name="defaultValue"/>,  if no value or <paramref name="values"/> is empty.
        /// </returns>
        public static T? LastWithValue<T>(this IEnumerable<T?> values, T? defaultValue = null) where T : struct
        {
            T? last = null;

            foreach (var value in values)
                if (value.HasValue)
                    last = value;

            return last.HasValue ? last : defaultValue;
        }

        /// <summary>
        /// Gets those items from <paramref name="values"/> that has value.
        /// </summary>
        /// <typeparam name="T">The type of the nullable value.</typeparam>
        /// <param name="values">The nullable values.</param>
        /// <returns>A items from <paramref name="values"/> that has value or empty.</returns>
        public static IEnumerable<T> WithValue<T>(this IEnumerable<T?> values) where T : struct
        {
            foreach (var value in values)
                if (value.HasValue) yield return value.Value;
        }

        /// <summary>
        /// Check if all items in <paramref name="values"/> are without value.
        /// </summary>
        /// <typeparam name="T">The type of the nullable value.</typeparam>
        /// <param name="values">The nullable values.</param>
        /// <returns>
        /// <c>true</c> if all items in <paramref name="values"/> are without value or if <paramref name="values"/> is empty.
        /// -or-
        /// <c>false</c> if any item in <paramref name="values"/> has value.
        /// </returns>
        public static bool NoValue<T>(this IEnumerable<T?> values) where T : struct
        {
            return values.All(value => !value.HasValue);
        }

        /// <summary>
        /// Check if all items in <paramref name="values"/> are without value.
        /// </summary>
        /// <typeparam name="T">The type of the nullable value.</typeparam>
        /// <param name="values">The nullable values.</param>
        /// <returns>
        /// <c>true</c> if all items in <paramref name="values"/> are without value or if <paramref name="values"/> is empty.
        /// -or-
        /// <c>false</c> if any item in <paramref name="values"/> has value.
        /// </returns>
        public static bool NoValue<T>(params T?[] values) where T : struct
            => NoValue(values.AsEnumerable());
    }
}
