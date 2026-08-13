namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents a collection of items that expire after a specified lifetime. 
    /// Each item is associated with the time it was added to the collection, and the collection provides methods to check for expired items and remove them.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public sealed class ExpiryCollection<T> : Collection<T>
    {
        private readonly List<ExpiryItem<T>> items;

        /// <summary>
        /// Initializes new instance of the <see cref="ExpiryCollection{T}"/> class with the specified lifetime for items.
        /// </summary>
        /// <param name="lifetime">The lifetime of each item in the collection.</param>
        /// <exception cref="ArgumentException">If <paramref name="lifetime"/> is not a positive time span.</exception>
        public ExpiryCollection(TimeSpan lifetime)
        {
            if (lifetime.IsNegative() || lifetime.IsZero())
                throw new ArgumentException("Lifetime must be a positive time span.", nameof(lifetime));

            ItemLifetime = lifetime;
            items = new List<ExpiryItem<T>>();
        }

        /// <summary>
        /// Gets the life time of each item in the collection.
        /// </summary>
        public TimeSpan ItemLifetime { get; }

        /// <summary>
        /// Gets the total number of items, non-expired and expired, in the collection.
        /// </summary>
        public override int Count => base.Count;

        /// <summary>
        /// Add item to the collection. If an unexpired item already exists, an exception is thrown. 
        /// If an expired item exists, it will be replaced with the new item. Use <see cref="Replace(T)"/> method to update an existing item 
        /// regardless of its expiration status.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="ArgumentException">If an unexpired item already exists in the collection.</exception>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public override void Add(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            CheckReadOnly();
            var expiryItem = GetCurrentItem(item);

            if (expiryItem == null)
            {
                expiryItem = new ExpiryItem<T>(item, DateTime.UtcNow);
            }
            else
            {
                if (expiryItem.IsExpired(ItemLifetime))
                {
                    items.Remove(expiryItem);
                    expiryItem = new ExpiryItem<T>(item, DateTime.UtcNow);
                }
                else
                {
                    throw new ArgumentException("An unexpired item already exists in the collection. Use Replace method to update it.", nameof(item));
                }
            }

            items.Add(expiryItem);
        }

        /// <summary>
        /// Replace an existing item in the collection with a new item, regardless of its expiration status. 
        /// If the item does not exist, it will be added to the collection.
        /// </summary>
        /// <param name="item">The item to replace or add.</param>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public void Replace(T item)
        {
            ArgumentNullException.ThrowIfNull(item);

            CheckReadOnly();

            var expiryItem = GetCurrentItem(item);

            if (expiryItem == null)
            {
                expiryItem = new ExpiryItem<T>(item, DateTime.UtcNow);
                items.Add(expiryItem);
            }
            else
            {
                items.Remove(expiryItem);
                expiryItem = new ExpiryItem<T>(item, DateTime.UtcNow);
                items.Add(expiryItem);
            }
        }

        /// <summary>
        /// Check if collection contains specified item. Expired items are not considered as contained in the collection.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><c>true</c> if the item is in the collection and not expired; otherwise, <c>false</c>.</returns>
        public override bool Contains(T item)
        {
            if (item is null)
                return false;

            var expiryItem = GetCurrentItem(item);

            if (expiryItem == null || expiryItem.IsExpired(ItemLifetime))
                return false;

            return true;
        }

        /// <summary>
        /// Check if specified item is expired. If the item does not exist in the collection, it is considered as expired.
        /// </summary>
        /// <param name="item">The item to check for expiration.</param>
        /// <returns><c>true</c> if the item is expired or does not exist in the collection; otherwise, <c>false</c>.</returns>
        public bool IsExpired(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
            var expiryItem = GetCurrentItem(item);
            return expiryItem == null || expiryItem.IsExpired(ItemLifetime);
        }

        /// <summary>
        /// Remove all items from the collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public override void Clear()
        {
            CheckReadOnly();
            items.Clear();
        }

        /// <summary>
        /// Remove specified item from collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><c>true</c> if the item was successfully removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">If collection is in read-only mode.</exception>
        public override bool Remove(T item)
        {
            ArgumentNullException.ThrowIfNull(item);
            CheckReadOnly();
            var expiryItem = GetCurrentItem(item);
            return expiryItem != null && items.Remove(expiryItem);
        }

        /// <summary>
        /// Gets an enumerable collection of expired items. The items returned by this method are not removed from the collection; 
        /// they are simply identified as expired based on their associated timestamps and the defined lifetime.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<T> GetExpired()
        {
            foreach (var expiryItem in items)
            {
                if (expiryItem.IsExpired(ItemLifetime))
                {
                    yield return expiryItem.Item;
                }
            }
        }

        /// <summary>
        /// Gets an enumerable collection of non-expired items. The items returned by this method are those that have not 
        /// yet reached their expiration time based on their associated timestamps and the defined lifetime.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator<T> GetEnumerator()
        {
            foreach (var expiryItem in items)
            {
                if (!expiryItem.IsExpired(ItemLifetime))
                    yield return expiryItem.Item;
            }
        }

        /// <summary>
        /// Remove expired items from the collection. This method iterates through the collection and removes any items that have reached their 
        /// expiration time based on their associated timestamps and the defined lifetime.
        /// </summary>
        public void RemoveExpired()
        {
            CheckReadOnly();

            if (Count == 0)
                return;

            for (int index = items.Count - 1; index >= 0; index--)
            {
                if (items[index].IsExpired(ItemLifetime))
                {
                    items.RemoveAt(index);
                }
            }
        }

        /// <summary>
        /// Copy the non-expired items of the collection to an array, starting at a particular array index.
        /// </summary>
        /// <param name="array">The array to copy items to.</param>
        /// <param name="arrayIndex">The starting index in the array.</param>
        public override void CopyTo(T[] array, int arrayIndex)
        {
            if (Count == 0)
                return;

            var items = this.ToList();
            items.CopyTo(array, arrayIndex);
        }

        private ExpiryItem<T>? GetCurrentItem(T item)
        {
            return items.FirstOrDefault(x => Equals(x.Item, item));
        }
    }
}
