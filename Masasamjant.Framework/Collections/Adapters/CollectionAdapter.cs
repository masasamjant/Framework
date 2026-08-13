using System.Collections;

namespace Masasamjant.Collections.Adapters
{
    /// <summary>
    /// Represents base class for adapter of <see cref="ICollection{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in collection.</typeparam>
    public class CollectionAdapter<T> : ICollection<T>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="CollectionAdapter{T}"/> class
        /// </summary>
        /// <param name="source">The source <see cref="ICollection{T}"/>.</param>
        /// <exception cref="ArgumentException">If <paramref name="source"/> is <see cref="CollectionAdapter{T}"/>.</exception>
        protected CollectionAdapter(ICollection<T> source)
        {
            if (source is CollectionAdapter<T>)
                throw new ArgumentException("The collection adapter should not be adapted.", nameof(source));
            Source = source;
        }

        /// <summary>
        /// Gets count of items.
        /// </summary>
        public virtual int Count => Source.Count;

        /// <summary>
        /// Gets if is read-only.
        /// </summary>
        public virtual bool IsReadOnly => Source.IsReadOnly;

        /// <summary>
        /// Gets adapted collection.
        /// </summary>
        protected ICollection<T> Source { get; }

        /// <summary>
        /// Adds an item to the collection.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public virtual void Add(T item)
        {
            Source.Add(item);
        }
        
        /// <summary>
        /// Clears the collection.
        /// </summary>
        public virtual void Clear()
        {
            Source.Clear();
        }

        /// <summary>
        /// Determines whether the collection contains a specific item.
        /// </summary>
        /// <param name="item">The item to locate in the collection.</param>
        /// <returns><c>true</c> if the item is found; otherwise, <c>false</c>.</returns>
        public virtual bool Contains(T item)
        {
            return Source.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the collection to an array, starting at a particular index.
        /// </summary>
        /// <param name="array">The one-dimensional array that is the destination of the elements copied from the collection.</param>
        /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            Source.CopyTo(array, arrayIndex);
        }

        /// <summary>
        /// Gets an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>An enumerator that can be used to iterate through the collection.</returns>
        public virtual IEnumerator<T> GetEnumerator()
        {
            return Source.GetEnumerator();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the collection.
        /// </summary>
        /// <param name="item">The item to remove from the collection.</param>
        /// <returns><c>true</c> if the item was successfully removed; otherwise, <c>false</c>.</returns>
        public virtual bool Remove(T item)
        {
            return Source.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)Source).GetEnumerator();
        }
    }
}
