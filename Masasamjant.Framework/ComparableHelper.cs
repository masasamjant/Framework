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

            if (x == null && y == null)
            {
                result = 0;
                return true;
            }

            var genericComparable = GetGenericComparable(x, y, out var z);

            if (genericComparable != null)
            {
                result = genericComparable.CompareTo(z);
                return true;
            }

            var comparable = GetComparable(x, y, out object? value);

            if (comparable != null)
            {
                result = comparable.CompareTo(value);
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

        /// <summary>
        /// Tries to compare object instances, if implements <see cref="IComparable"/>.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <param name="result">The comparison result if returns <c>true</c>; <c>null</c> otherwise.</param>
        /// <returns><c>true</c> if comparison succceeded; <c>false</c> otherwise.</returns>
        public static bool TryCompare(object? x, object? y, out int? result)
        {
            result = null;

            if (x == null && y == null)
            {
                result = 0;
                return true;
            }

            var comparable = GetComparable(x, y, out var value);

            if (comparable != null)
            {
                result = comparable.CompareTo(value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Tries to compare object instances, if implements <see cref="IComparable"/>.
        /// </summary>
        /// <param name="x">The first value.</param>
        /// <param name="y">The second value.</param>
        /// <returns>A result of comparison or <c>null</c>, if could not compare values.</returns>
        public static int? TryCompare(object? x, object? y) => TryCompare(x, y, out var result) ? result : null;

        private static Comparable? GetComparable(object? x, object? y, out object? value)
        {
            if (x is IComparable cx)
            {
                value = y;
                return new Comparable(cx);
            }

            if (y is IComparable cy)
            {
                value = x;
                return new Comparable(cy);
            }

            value = null;
            return null;
        }

        private static Comparable<T>? GetGenericComparable<T>(T? x, T? y, out T? value)
        {
            if (x is IComparable<T> cx)
            {
                value = y;
                return new Comparable<T>(cx);
            }

            if (y is IComparable<T> cy)
            {
                value = x;
                return new Comparable<T>(cy);
            }

            value = default;
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

        private sealed class Comparable<T> : IComparable<T>
        {
            private readonly IComparable<T> comparable;

            public Comparable(IComparable<T> comparable)
            {
                this.comparable = comparable;
            }

            public int CompareTo(T? other)
            {
                return comparable.CompareTo(other);
            }
        }
    }
}
