using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Factory class to create instance of <see cref="DictionaryEqualityComparer{TDictionary, TKey, TValue}"/> to default <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    public static class DictionaryEqualityComparer
    {
        /// <summary>
        /// Creates new instance of <see cref="DictionaryEqualityComparer{TDictionary, TKey, TValue}"/> for <see cref="Dictionary{TKey, TValue}"/> dictionary.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <returns>A <see cref="DictionaryEqualityComparer{TDictionary, TKey, TValue}"/> to <see cref="Dictionary{TKey, TValue}"/>.</returns>
        public static DictionaryEqualityComparer<Dictionary<TKey, TValue>, TKey, TValue> CreateDefaultComparer<TKey, TValue>() where TKey : notnull
            => new DictionaryEqualityComparer<Dictionary<TKey, TValue>, TKey, TValue>();
    }

    /// <summary>
    /// Represents equality comparer of <typeparamref name="TDictionary"/> dictionaries.
    /// </summary>
    /// <typeparam name="TDictionary">The type of the dictionary implementing <see cref="IDictionary{TKey, TValue}"/>.</typeparam>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class DictionaryEqualityComparer<TDictionary, TKey, TValue> : IEqualityComparer<TDictionary>
        where TDictionary : IDictionary<TKey, TValue>
    {
        /// <summary>
        /// Check if <typeparamref name="TDictionary"/> instances are equal.
        /// </summary>
        /// <param name="x">The first <typeparamref name="TDictionary"/> instance.</param>
        /// <param name="y">The second <typeparamref name="TDictionary"/> instance.</param>
        /// <returns><c>true</c> if <paramref name="x"/> and <paramref name="y"/> are equal; <c>false</c> otherwise.</returns>
        /// <remarks><paramref name="x"/> and <paramref name="y"/> are considered also equal, if both are empty.</remarks>
        public bool Equals(TDictionary? x, TDictionary? y)
        {
            if (x == null)
                return y == null;
            else if (y == null)
                return x == null;
            else
            {
                if (ReferenceEquals(x, y))
                    return true;
                else if (x.Count != y.Count)
                    return false;
                else if (x.Count == 0)
                    return true;
                else
                    return DictionaryHelper.AreEqual(x, y);
            }
        }

        /// <summary>
        /// Computes hash code for specified <typeparamref name="TDictionary"/> instance.
        /// </summary>
        /// <param name="obj">The <typeparamref name="TDictionary"/> instance.</param>
        /// <returns>A hash code.</returns>
        public int GetHashCode([DisallowNull] TDictionary obj)
        {
            int code = 0;

            if (obj.Count == 0)
                return code;

            foreach (var keyValue in obj)
                code ^= HashCode.Combine(keyValue.Key, keyValue.Value);

            return code;
        }
    }
}
