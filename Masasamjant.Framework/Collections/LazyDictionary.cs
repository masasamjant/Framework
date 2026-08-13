using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents a lazily initialized dictionary.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class LazyDictionary<TKey, TValue> : LazyCollectionBase<IDictionary<TKey, TValue>, KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
        where TKey : notnull
    {
        private readonly Lazy<Dictionary<TKey, TValue>> lazyDictionary;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer to use for the dictionary or <c>null</c> to use the default one.</param>
        public LazyDictionary(IEqualityComparer<TKey>? comparer = null)
        {
            lazyDictionary = new Lazy<Dictionary<TKey, TValue>>(() => comparer != null ? new Dictionary<TKey, TValue>(comparer) : new Dictionary<TKey, TValue>(), true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="items">The initial items to populate the dictionary.</param>
        /// <param name="comparer">The comparer to use for the dictionary or <c>null</c> to use the default one.</param>
        public LazyDictionary(IEnumerable<KeyValuePair<TKey, TValue>> items, IEqualityComparer<TKey>? comparer = null)
            : this(() => items, comparer)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazyDictionary{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="itemsProvider">A delegate that provides the initial items to populate the dictionary or <c>null</c>.</param>
        /// <param name="comparer">The comparer to use for the dictionary or <c>null</c> to use the default one.</param>
        public LazyDictionary(Func<IEnumerable<KeyValuePair<TKey, TValue>>?>? itemsProvider, IEqualityComparer<TKey>? comparer = null)
        {
            lazyDictionary = new Lazy<Dictionary<TKey, TValue>>(() =>
            {
                var dictionary = comparer != null ? new Dictionary<TKey, TValue>(comparer) : new Dictionary<TKey, TValue>();
                if (itemsProvider != null)
                {
                    var items = itemsProvider();
                    if (items != null && items.Any())
                    {
                        foreach (var item in items)
                            dictionary.Add(item.Key, item.Value);
                    }
                }
                return dictionary;
            }, true);
        }

        /// <summary>
        /// Gets the collection of keys.
        /// </summary>
        public ICollection<TKey> Keys => Items.Keys;

        /// <summary>
        /// Gets the collection of values.
        /// </summary>
        public ICollection<TValue> Values => Items.Values;

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        /// <exception cref="InvalidOperationException">If dictionary is read-only.</exception>
        public TValue this[TKey key]
        {
            get => Items[key];
            set
            {
                CheckReadOnly();
                Items[key] = value;
            }
        }

        /// <summary>
        /// Adds an element with the provided key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <exception cref="InvalidOperationException">If dictionary is read-only.</exception>
        public void Add(TKey key, TValue value)
        {
            CheckReadOnly();
            Items.Add(key, value);
        }

        /// <summary>
        /// Check if dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key)
        {
            return Items.ContainsKey(key);
        }

        /// <summary>
        /// Removes the element with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns><c>true</c> if the element is successfully removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">If dictionary is read-only.</exception>
        public bool Remove(TKey key)
        {
            CheckReadOnly();
            return Items.Remove(key);
        }

        /// <summary>
        /// Gets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return Items.TryGetValue(key, out value);
        }

        /// <summary>
        /// Gets the underlying dictionary instance.
        /// </summary>
        protected override Dictionary<TKey, TValue> Items
        {
            get { return lazyDictionary.Value; }
        }
    }
}
