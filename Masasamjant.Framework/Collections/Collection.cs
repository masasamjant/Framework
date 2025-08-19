using Masasamjant.Collections.Abstractions;
using System.Collections;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Repreesents a collection of items that implements <see cref="ISupportReadOnly"/> interface.
    /// </summary>
    /// <typeparam name="T">The type of the collection item.</typeparam>
    public class Collection<T> : ICollection<T>, ICollection, ISupportReadOnly
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Collection{T}"/> class.
        /// </summary>
        public Collection()
            : this(new List<T>())
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collection{T}"/> class using specified items.
        /// </summary>
        /// <param name="items">The initial items.</param>
        public Collection(IEnumerable<T> items)
            : this()
        {
            Items.AddRange(items);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Collection{T}"/> class using specified 
        /// <see cref="ICollection{T}"/> as items collection.
        /// </summary>
        /// <param name="items">The <see cref="ICollection{T}"/> to wrap.</param>
        /// <exception cref="ArgumentException">If <paramref name="items"/> is in read-only state.</exception>
        protected Collection(ICollection<T> items)
        {
            if (items.IsReadOnly)
                throw new ArgumentException("The items collection is read-only.", nameof(items));
            
            Items = items;
        }

        /// <summary>
        /// Gets whether or not collection is in read-only state.
        /// </summary>
        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Gets the items.
        /// </summary>
        public int Count => Items.Count;

        /// <summary>
        /// Gets the <see cref="ICollection{T}"/> to access items.
        /// </summary>
        protected ICollection<T> Items { get; }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        public virtual void Add(T item)
        {
            CheckReadOnly();
            Items.Add(item);
        }

        /// <summary>
        /// Clears the collection.
        /// </summary>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        public void Clear()
        {
            CheckReadOnly();
            Items.Clear();
        }

        /// <summary>
        /// Determines whether the collection contains a specific value.
        /// </summary>
        /// <param name="item">The item to check.</param>
        /// <returns><c>true</c> if contains <paramref name="item"/>; <c>false</c> otherwise.</returns>
        public virtual bool Contains(T item)
        {
            return Items.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to an array, starting at a particular index.
        /// </summary>
        /// <param name="array">The array to copy items.</param>
        /// <param name="arrayIndex">The starting index.</param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            Items.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the collection.
        /// </summary>
        /// <param name="item">The item to remove.</param>
        /// <returns><c>true</c> if was in collection and removed; <c>false</c> otherwise.</returns>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        public virtual bool Remove(T item)
        {
            CheckReadOnly();
            return Items.Remove(item);
        }

        /// <summary>
        /// Sets collection to read-only state.
        /// </summary>
        public void SetReadOnly()
        {
            IsReadOnly = true;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>A enumerator that iterates through the collection.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in Items)
                yield return item;  
        }

        /// <summary>
        /// Check if collection is read-only and if to then throws <see cref="InvalidOperationException"/>.
        /// </summary>
        /// <exception cref="InvalidOperationException">If <see cref="IsReadOnly"/> is <c>true</c>.</exception>
        protected void CheckReadOnly()
        {
            if (IsReadOnly)
                throw new InvalidOperationException("The collection is read-only.");
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            Items.ToArray().CopyTo(array, index);
        }

        bool ICollection.IsSynchronized => false;

        object ICollection.SyncRoot => throw new NotImplementedException();
    }
}
