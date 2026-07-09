using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Collections.Adapters
{
    /// <summary>
    /// Represents base class for adapter of <see cref="IDictionary{TKey, TValue}"/>.
    /// </summary>
    /// <typeparam name="TKey">The type of the keys.</typeparam>
    /// <typeparam name="TValue">The type of the values.</typeparam>
    public class DictionaryAdapter<TKey, TValue> : CollectionAdapter<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue> where TKey : notnull
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DictionaryAdapter{TKey, TValue}"/> class.
        /// </summary>
        /// <param name="source">The source dictionary.</param>
        protected DictionaryAdapter(IDictionary<TKey, TValue> source)
            : base(source)
        { }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual TValue this[TKey key] 
        { 
            get => Source[key]; 
            set => Source[key] = value; 
        }

        /// <summary>
        /// Gets the collection of keys in the dictionary.
        /// </summary>
        public virtual ICollection<TKey> Keys => Source.Keys;

        /// <summary>
        /// Gets the collection of values in the dictionary.
        /// </summary>
        public virtual ICollection<TValue> Values => Source.Values;

        /// <summary>
        /// Gets the adapted dictionary.
        /// </summary>
        protected new IDictionary<TKey, TValue> Source => (IDictionary<TKey, TValue>)base.Source;

        /// <summary>
        /// Adds an item with the provided key and value to the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to add.</param>
        /// <param name="value">The value of the item to add.</param>
        public virtual void Add(TKey key, TValue value)
        {
            Source.Add(key, value);
        }

        /// <summary>
        /// Determines whether the dictionary contains an item with the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <returns><c>true</c> if the dictionary contains an item with the key; otherwise, <c>false</c>.</returns>
        public virtual bool ContainsKey(TKey key)
        {
            return Source.ContainsKey(key);
        }

        /// <summary>
        /// Removes the item with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to remove.</param>
        /// <returns><c>true</c> if the item was successfully removed; otherwise, <c>false</c>.</returns>
        public virtual bool Remove(TKey key)
        {
            return Source.Remove(key);
        }

        /// <summary>
        /// Tries to get the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found; otherwise, the default value for the type of the value parameter.</param>
        /// <returns><c>true</c> if the dictionary contains an item with the specified key; otherwise, <c>false</c>.</returns>
        public virtual bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            return Source.TryGetValue(key, out value);
        }
    }
}
