using System.Collections;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Collections.Adapters
{
    /// <summary>
    /// Represents adapter of <see cref="IReadOnlyDictionary{TKey, TValue}"/> that implements also <see cref="IDictionary{TKey, TValue}"/>. 
    /// Any attempt to alter the items of dictionary throws <see cref="InvalidOperationException"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    public sealed class ReadOnlyDictionaryAdapter<TKey, TValue> : IDictionary<TKey, TValue>, IReadOnlyDictionary<TKey, TValue>
    {
        private readonly IReadOnlyDictionary<TKey, TValue> source;
        private ReadOnlyCollection<TKey>? keys;
        private ReadOnlyCollection<TValue>? values;

        /// <summary>
        /// Initializes new instance of the <see cref="ReadOnlyDictionaryAdapter{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">The source <see cref="IReadOnlyDictionary{TKey, TValue}"/>.</param>
        /// <exception cref="ArgumentException">If <paramref name="source"/> is <see cref="ReadOnlyDictionaryAdapter{TKey, TValue}"/>.</exception>
        public ReadOnlyDictionaryAdapter(IReadOnlyDictionary<TKey, TValue> source)
        {
            if (source is ReadOnlyDictionaryAdapter<TKey, TValue>)
                throw new ArgumentException($"The source cannot be '{typeof(ReadOnlyDictionaryAdapter<TKey, TValue>)}' instance.", nameof(source));
            this.source = source;
        }

        /// <summary>
        /// Gets the value of the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>A value of the <paramref name="key"/>.</returns>
        /// <exception cref="InvalidOperationException">If attempt to set value.</exception>
        public TValue this[TKey key]
        {
            get { return source[key]; }
            set { throw new InvalidOperationException("The dictionary is read-only."); }
        }

        /// <summary>
        /// Gets the read-only collection of keys.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                return keys ??= source.Keys.ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the read-only collection of values.
        /// </summary>
        public ICollection<TValue> Values
        {
            get
            {
                return values ??= source.Values.ToList().AsReadOnly();
            }
        }

        /// <summary>
        /// Gets the count of items.
        /// </summary>
        public int Count
        {
            get { return source.Count; }
        }

        /// <summary>
        /// Gets if in read-only state. Returns <c>true</c>, always.
        /// </summary>
        public bool IsReadOnly => true;

        /// <summary>
        /// Check if contains specified <see cref="KeyValuePair{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The <see cref="KeyValuePair{TKey, TValue}"/>.</param>
        /// <returns><c>true</c> if contains <paramref name="item"/>; <c>false</c> otherwise.</returns>
        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return source.Contains(item);
        }

        /// <summary>
        /// Check if contains specied key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns><c>true</c> if contains <see cref="KeyValuePair{TKey, TValue}"/> with <paramref name="key"/> as key; <c>false</c> otherwise.</returns>
        public bool ContainsKey(TKey key)
        {
            return source.ContainsKey(key);
        }

        /// <summary>
        /// Gets enumerator to iterate <see cref="KeyValuePair{TKey, TValue}"/> items.
        /// </summary>
        /// <returns>A enumerator of <see cref="KeyValuePair{TKey, TValue}"/>.</returns>
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var keyValue in source)
                yield return keyValue;
        }

        /// <summary>
        /// Tries to get <typeparamref name="TValue"/> value of specified <typeparamref name="TKey"/> key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value, if return true; otherwise <c>default</c>.</param>
        /// <returns><c>true</c> if get value; <c>false</c> otherwise.</returns>
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return source.TryGetValue(key, out value);
        }

        void IDictionary<TKey, TValue>.Add(TKey key, TValue value)
        {
            throw new InvalidOperationException("The dictionary is read-only.");
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Add(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException("The dictionary is read-only.");
        }

        void ICollection<KeyValuePair<TKey, TValue>>.Clear()
        {
            throw new InvalidOperationException("The dictionary is read-only.");
        }

        void ICollection<KeyValuePair<TKey, TValue>>.CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            source.ToArray().CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        bool IDictionary<TKey, TValue>.Remove(TKey key)
        {
            throw new InvalidOperationException("The dictionary is read-only.");
        }

        bool ICollection<KeyValuePair<TKey, TValue>>.Remove(KeyValuePair<TKey, TValue> item)
        {
            throw new InvalidOperationException("The dictionary is read-only.");
        }

        IEnumerable<TKey> IReadOnlyDictionary<TKey, TValue>.Keys => Keys;

        IEnumerable<TValue> IReadOnlyDictionary<TKey, TValue>.Values => Values;
    }
}
