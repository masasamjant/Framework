using System.Data;
using System.Diagnostics.CodeAnalysis;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents a collection of key-value pairs that expire after a specified lifetime. 
    /// Each key-value pair is associated with the time it was added to the collection, and the collection provides methods to check for expired items and remove them.
    /// </summary>
    /// <typeparam name="TKey">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="TValue">The type of values in the dictionary.</typeparam>
    public sealed class ExpiryDictionary<TKey, TValue> : Collection<KeyValuePair<TKey, TValue>>, IDictionary<TKey, TValue>
        where TKey : notnull
    {
        private readonly Dictionary<TKey, ExpiryItem<TValue>> items;

        /// <summary>
        /// Initializes new instance of the <see cref="ExpiryDictionary{TKey, TValue}"/> class with the specified lifetime for items.
        /// </summary>
        /// <param name="lifetime">The lifetime of each item in the dictionary.</param>
        /// <exception cref="ArgumentException">If <paramref name="lifetime"/> is not a positive time span.</exception>
        public ExpiryDictionary(TimeSpan lifetime)
        {
            if (lifetime.IsNegative() || lifetime.IsZero())
                throw new ArgumentException("Lifetime must be a positive time span.", nameof(lifetime));
            
            ItemLifetime = lifetime;
            items = new Dictionary<TKey, ExpiryItem<TValue>>();
        }

        /// <summary>
        /// Gets the life time of each item in the collection.
        /// </summary>
        public TimeSpan ItemLifetime { get; }

        /// <summary>
        /// Gets a collection containing the keys in the dictionary that are not expired. 
        /// Keys of expired items are not included in the collection.
        /// </summary>
        public ICollection<TKey> Keys
        {
            get
            {
                var keys = items.Where(x => !x.Value.IsExpired(ItemLifetime)).Select(x => x.Key).ToList();
                return keys.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets a collection containing the values in the dictionary that are not expired.
        /// Value of expired items are not included in the collection.
        /// </summary>
        public ICollection<TValue> Values
        {
            get 
            {
                var values = items.Values.Where(x => !x.IsExpired(ItemLifetime)).Select(x => x.Item).ToList();
                return values.AsReadOnly();
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key. 
        /// If the key does not exist or the item has expired, a <see cref="KeyNotFoundException"/> is thrown when getting the value.
        /// </summary>
        /// <remarks>Setting value is same as invoking <see cref="Replace(KeyValuePair{TKey, TValue})"/> method.</remarks>
        /// <param name="key">The key of the item to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        /// <exception cref="KeyNotFoundException">If item with key does not exist or has expired.</exception>
        public TValue this[TKey key]
        {
            get
            {
                var item = GetCurrentItem(key);
                
                if (item == null || item.IsExpired(ItemLifetime))
                    throw new KeyNotFoundException("The item with the specified key does not exist or has expired.");

                return item.Item;
            }
            set
            {
                Replace(new KeyValuePair<TKey, TValue>(key, value));
            }
        }

        /// <summary>
        /// Add new key-value pair to the dictionary. 
        /// If an unexpired item with the same key already exists, an exception is thrown.
        /// </summary>
        /// <param name="key">The key of the item to add.</param>
        /// <param name="value">The value of the item to add.</param>
        /// <exception cref="ArgumentException">If an unexpired item with the same key already exists in the collection.</exception>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public void Add(TKey key, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Add new key-value pair to the dictionary.
        /// If an unexpired item with the same key already exists, an exception is thrown.
        /// </summary>
        /// <param name="item">The key-value pair to add.</param>
        /// <exception cref="ArgumentException">If an unexpired item with the same key already exists in the collection.</exception>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public override void Add(KeyValuePair<TKey, TValue> item)
        {
            CheckReadOnly();

            var expiryItem = GetCurrentItem(item.Key);

            if (expiryItem == null)
            {
                items[item.Key] = new ExpiryItem<TValue>(item.Value, DateTime.UtcNow);
            }
            else
            {
                if (expiryItem.IsExpired(ItemLifetime))
                {
                    items[item.Key] = new ExpiryItem<TValue>(item.Value, DateTime.UtcNow);
                }
                else
                {
                    throw new ArgumentException("An unexpired item with the same key already exists in the collection. Use Replace method to update it.", nameof(item));
                }
            }
        }

        /// <summary>
        /// Replaces the value of an existing key-value pair in the dictionary regardless of its expiration status.
        /// </summary>
        /// <param name="key">The key of the item to replace.</param>
        /// <param name="value">The value of the item to replace.</param>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public void Replace(TKey key, TValue value)
        {
            Replace(new KeyValuePair<TKey, TValue>(key, value));
        }

        /// <summary>
        /// Replaces the value of an existing key-value pair in the dictionary regardless of its expiration status.
        /// </summary>
        /// <param name="item">The key-value pair to replace.</param>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public void Replace(KeyValuePair<TKey, TValue> item)
        {
            CheckReadOnly();

            var expiryItem = GetCurrentItem(item.Key);

            if (expiryItem == null)
            {
                items[item.Key] = new ExpiryItem<TValue>(item.Value, DateTime.UtcNow);
            }
            else
            {
                items[item.Key] = new ExpiryItem<TValue>(item.Value, DateTime.UtcNow);
            }
        }

        /// <summary>
        /// Determines whether the dictionary contains an unexpired item with the specified key.
        /// </summary>
        /// <param name="key">The key to check.</param>
        /// <returns><c>true</c> if the dictionary contains an unexpired item with the specified key; otherwise, <c>false</c>.</returns>
        public bool ContainsKey(TKey key)
        {
            var item = GetCurrentItem(key);
            return item != null && !item.IsExpired(ItemLifetime);
        }

        /// <summary>
        /// Removes all items from the dictionary.
        /// </summary>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public override void Clear()
        {
            CheckReadOnly();
            items.Clear();
        }

        /// <summary>
        /// Check if the dictionary contains an unexpired key-value pair that matches the specified key and value.
        /// </summary>
        /// <param name="item">The key-value pair to check.</param>
        /// <returns><c>true</c> if the dictionary contains an unexpired key-value pair that matches the specified key and value; otherwise, <c>false</c>.</returns>
        public override bool Contains(KeyValuePair<TKey, TValue> item)
        {
            var expiryItem = GetCurrentItem(item.Key);
            
            return expiryItem != null && !expiryItem.IsExpired(ItemLifetime) 
                && EqualityComparer<TValue>.Default.Equals(expiryItem.Item, item.Value);
        }

        /// <summary>
        /// Determines whether the item with the specified key is expired.
        /// </summary>
        /// <param name="key">The key of the item to check.</param>
        /// <returns><c>true</c> if the item is expired; otherwise, <c>false</c>.</returns>
        public bool IsExpired(TKey key)
        {
            var item = GetCurrentItem(key);
            return item == null || item.IsExpired(ItemLifetime);
        }

        /// <summary>
        /// Determines whether the specified key-value pair is expired.
        /// </summary>
        /// <param name="item">The key-value pair to check.</param>
        /// <returns><c>true</c> if the specified key-value pair is expired; otherwise, <c>false</c>.</returns>
        public bool IsExpired(KeyValuePair<TKey, TValue> item)
        {
            var expiryItem = GetCurrentItem(item.Key);
            return expiryItem == null || expiryItem.IsExpired(ItemLifetime);
        }

        /// <summary>
        /// Remove the specified key-value pair from the dictionary.
        /// </summary>
        /// <param name="item">The key-value pair to remove.</param>
        /// <returns><c>true</c> if the item was successfully removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public override bool Remove(KeyValuePair<TKey, TValue> item)
        {
            CheckReadOnly();
            var expiryItem = GetCurrentItem(item.Key);
            return expiryItem != null && items.Remove(item.Key);
        }

        /// <summary>
        /// Remove the item with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the item to remove.</param>
        /// <returns><c>true</c> if the item was successfully removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public bool Remove(TKey key)
        {
            CheckReadOnly();
            return items.Remove(key);
        }

        /// <summary>
        /// Remove expired items from the dictionary. This method iterates through the dictionary and removes all key-value pairs 
        /// that have expired based on their associated timestamps and the defined item lifetime.
        /// </summary>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public void RemoveExpired()
        {
            CheckReadOnly();

            var expiredKeys = items.Where(kv => kv.Value.IsExpired(ItemLifetime)).Select(kv => kv.Key).ToList();

            foreach (var key in expiredKeys)
                items.Remove(key);
        }

        /// <summary>
        /// Tries to get the value associated with the specified key. If the key exists and the item is not expired,
        /// the value is returned; otherwise, the default value is returned.
        /// </summary>
        /// <param name="key">The key of the value to get.</param>
        /// <param name="value">When this method returns, contains the value associated with the specified key, if the key is found and the item is not expired; otherwise, the default value for the type of the value parameter.</param>
        /// <returns><c>true</c> if the key was found and the item is not expired; otherwise, <c>false</c>.</returns>
        public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
        {
            var item = GetCurrentItem(key);
            
            if (item != null && !item.IsExpired(ItemLifetime))
            {
                value = item.Item;
                return true;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Gets an enumerable collection of key-value pairs that have expired. This method iterates through the dictionary and yields all key-value pairs
        /// that have expired based on their associated timestamps and the defined item lifetime.
        /// </summary>
        /// <returns>An enumerable collection of key-value pairs that have expired.</returns>
        public IEnumerable<KeyValuePair<TKey, TValue>> GetExpired()
        {
            foreach (var kv in items)
            {
                if (kv.Value.IsExpired(ItemLifetime))
                    yield return new KeyValuePair<TKey, TValue>(kv.Key, kv.Value.Item);
            }
        }

        /// <summary>
        /// Gets an enumerable collection of key-value pairs that are not expired. This method iterates through the dictionary and yields all key-value pairs 
        /// that are not expired based on their associated timestamps and the defined item lifetime.
        /// </summary>
        /// <returns>An enumerable collection of key-value pairs that are not expired.</returns>
        public override IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (var kv in items)
            {
                if (!kv.Value.IsExpired(ItemLifetime))
                    yield return new KeyValuePair<TKey, TValue>(kv.Key, kv.Value.Item);
            }
        }

        private ExpiryItem<TValue>? GetCurrentItem(TKey key)
        {
            return items.TryGetValue(key, out var item) ? item : null;
        }
    }
}
