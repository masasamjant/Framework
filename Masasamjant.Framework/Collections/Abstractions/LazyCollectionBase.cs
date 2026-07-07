using System.Collections;

namespace Masasamjant.Collections.Abstractions
{
    /// <summary>
    /// Represents abstract base class for collections that are initialized lazily.
    /// </summary>
    /// <typeparam name="TCollection">The type of the underlying collection.</typeparam>
    /// <typeparam name="T">The type of items in the collection.</typeparam>
    public abstract class LazyCollectionBase<TCollection, T> : ICollection<T>, ISupportReadOnly 
        where TCollection : ICollection<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LazyCollectionBase{TCollection, T}"/> class.
        /// </summary>
        protected LazyCollectionBase()
        { }

        /// <summary>
        /// Gets the count of items.
        /// </summary>
        public int Count => Items.Count;

        /// <summary>
        /// Gests a value indicating whether the collection is read-only.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Gets the underlying collection.
        /// </summary>
        protected abstract TCollection Items { get; }

        /// <summary>
        /// Add specified item to collection.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="InvalidOperationException">If collection is read-only.</exception>
        public void Add(T item)
        {
            CheckReadOnly();
            Items.Add(item);
        }

        /// <summary>
        /// Remove all items from collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">If collection is read-only.</exception>
        public void Clear()
        {
            CheckReadOnly();
            Items.Clear();
        }

        /// <summary>
        /// Check if collection contains specified item.
        /// </summary>
        /// <param name="item">The item to check for.</param>
        /// <returns><c>true</c> if the item is found; otherwise, <c>false</c>.</returns>
        public bool Contains(T item)
        {
            return Items.Contains(item);
        }

        /// <summary>
        /// Copy items to specified array starting at specified index.
        /// </summary>
        /// <param name="array">The array to copy items to.</param>
        /// <param name="arrayIndex">The starting index in the array.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets an enumerator for the collection.
        /// </summary>
        /// <returns>An enumerator for the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in Items)
                yield return item;
        }
        
        /// <summary>
        /// Remove specified item from collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><c>true</c> if the item was removed; otherwise, <c>false</c>.</returns>
        /// <exception cref="InvalidOperationException">If collection is read-only.</exception>
        public bool Remove(T item)
        {
            CheckReadOnly();
            return Items.Remove(item);
        }

        /// <summary>
        /// Sets the collection to read-only.
        /// </summary>
        public void SetReadOnly()
        {
            IsReadOnly = true;
        }

        /// <summary>
        /// Checks if the collection is read-only and throws an exception if it is.
        /// </summary>
        /// <exception cref="InvalidOperationException">If the collection is read-only.</exception>
        protected void CheckReadOnly()
        {
            if (IsReadOnly)
                throw new InvalidOperationException("Collection is read-only.");
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}
