using System.Diagnostics.CodeAnalysis;

namespace Masasamjant
{
    /// <summary>
    /// Provides helper methods to comparison using <see cref="IComparable{T}"/> and <see cref="IComparable"/> interfaces.
    /// </summary>
    public static class ComparableHelper
    {
        /// <summary>
        /// Gets minimum value between specified values.
        /// </summary>
        /// <typeparam name="T">The type of compared values.</typeparam>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>A minimum value between <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static T Min<T>(T left, T right) where T : IComparable<T>
        {
            return left.CompareTo(right) <= 0 ? left : right;
        }

        /// <summary>
        /// Gets maximum value between specified values.
        /// </summary>
        /// <typeparam name="T">The type of compared values.</typeparam>
        /// <param name="left">The left value.</param>
        /// <param name="right">The right value.</param>
        /// <returns>A maximum value between <paramref name="left"/> and <paramref name="right"/>.</returns>
        public static T Max<T>(T left, T right) where T : IComparable<T>
        {
            return left.CompareTo(right) >= 0 ? left : right;
        }

        /// <summary>
        /// Determines if value of <typeparamref name="T"/> is greater than comparison value.
        /// </summary>
        /// <typeparam name="T">The compared type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is greater than <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsGreaterThan<T>(this T value, T compare) where T : IComparable<T>
        {
            return value.CompareTo(compare) > 0;
        }

        /// <summary>
        /// Determines if value of <see cref="IComparable"/> is greater than comparison value.
        /// </summary>
        /// <param name="comparable">The comparable value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if value of <paramref name="comparable"/> is greater than <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsGreaterThan(this IComparable comparable, object? compare)
        {
            return comparable.CompareTo(compare) > 0;
        }

        /// <summary>
        /// Determines if value of <typeparamref name="T"/> is greater than or equal to comparison value.
        /// </summary>
        /// <typeparam name="T">The compared type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is greater than or equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsGreaterThanOrEqual<T>(this T value, T compare) where T : IComparable<T>
        {
            return value.CompareTo(compare) >= 0;
        }

        /// <summary>
        /// Determines if value of <see cref="IComparable"/> is greater than or equal to comparison value.
        /// </summary>
        /// <param name="comparable">The comparable value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if value of <paramref name="comparable"/> is greater than or equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsGreaterThanOrEqual(this IComparable comparable, object? compare)
        {
            return comparable.CompareTo(compare) >= 0;
        }

        /// <summary>
        /// Determines if value of <typeparamref name="T"/> is less than comparison value.
        /// </summary>
        /// <typeparam name="T">The compared type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is less than <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsLessThan<T>(this T value, T compare) where T : IComparable<T>
        {
            return value.CompareTo(compare) < 0;
        }

        /// <summary>
        /// Determines if value of <see cref="IComparable"/> is less than comparison value.
        /// </summary>
        /// <param name="comparable">The comparable value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if value of <paramref name="comparable"/> is less than <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsLessThan(this IComparable comparable, object? compare)
        {
            return comparable.CompareTo(compare) < 0;
        }

        /// <summary>
        /// Determines if value of <typeparamref name="T"/> is less than or equal to comparison value.
        /// </summary>
        /// <typeparam name="T">The compared type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is less than or equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsLessThanOrEqual<T>(this T value, T compare) where T : IComparable<T>
        {
            return value.CompareTo(compare) <= 0;
        }

        /// <summary>
        /// Determines if value of <see cref="IComparable"/> is less than or equal to comparison value.
        /// </summary>
        /// <param name="comparable">The comparable value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if value of <paramref name="comparable"/> is less than or equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsLessThanOrEqual(this IComparable comparable, object? compare)
        {
            return comparable.CompareTo(compare) <= 0;
        }

        /// <summary>
        /// Determines if value of <typeparamref name="T"/> is equal to comparison value.
        /// </summary>
        /// <typeparam name="T">The compared type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsEqual<T>(this T value, T compare) where T : IComparable<T>
        {
            return value.CompareTo(compare) == 0;
        }

        /// <summary>
        /// Determines if value of <see cref="IComparable"/> is equal to comparison value.
        /// </summary>
        /// <param name="comparable">The comparable value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if value of <paramref name="comparable"/> is equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsEqual(this IComparable comparable, object? compare)
        {
            return comparable.CompareTo(compare) == 0;
        }

        /// <summary>
        /// Determines if value of <typeparamref name="T"/> is not equal to comparison value.
        /// </summary>
        /// <typeparam name="T">The compared type.</typeparam>
        /// <param name="value">The value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if <paramref name="value"/> is not equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsNotEqual<T>(this T value, T compare) where T : IComparable<T>
        {
            return value.CompareTo(compare) != 0;
        }

        /// <summary>
        /// Determines if value of <see cref="IComparable"/> is not equal to comparison value.
        /// </summary>
        /// <param name="comparable">The comparable value.</param>
        /// <param name="compare">The comparison value.</param>
        /// <returns><c>true</c> if value of <paramref name="comparable"/> is not equal to <paramref name="compare"/>; <c>false</c> otherwise.</returns>
        public static bool IsNotEqual(this IComparable comparable, object? compare)
        {
            return comparable.CompareTo(compare) != 0;
        }

        /// <summary>
        /// Tries to compare values of <typeparamref name="T"/>, if implements <see cref="IComparable{T}"/> or <see cref="IComparable"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <param name="result">The comparison result if returns <c>true</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if comparison succceeded; <c>false</c> otherwise.</returns>
        public static bool TryCompare<T>(T? x, T? y, [NotNullWhen(true)] out int? result)
        {
            result = null;

            if (x != null && y != null)            {

                if (!x.GetType().Equals(y.GetType()))
                    return false;

                var comparable = GetComparable(x, y, out object? value);

                if (comparable != null)
                {
                    result = comparable.CompareTo(value);
                    return true;
                }
            }
            else if (x == null && y == null)
            {
                result = 0;
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to compare values of <typeparamref name="T"/>, if implements <see cref="IComparable{T}"/> or <see cref="IComparable"/>.
        /// </summary>
        /// <typeparam name="T">The type of the value.</typeparam>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>A result of comparison or <c>null</c>, if could not compare values.</returns>
        public static int? TryCompare<T>(T? x, T? y) => TryCompare(x, y, out var result) ? result : null;

        private static Comparable? GetComparable(object? x, object? y, out object? value)
        {
            if (x is IComparable cx)
            {
                value = y;
                return new Comparable(cx);
            }

            value = null;
            return null;
        }

        private sealed class Comparable : IComparable
        {
            private readonly IComparable comparable;

            public Comparable(IComparable comparable)
            {
                this.comparable = comparable;
            }

            public int CompareTo(object? obj)
            {
                return comparable.CompareTo(obj);
            }
        }
    }
}
