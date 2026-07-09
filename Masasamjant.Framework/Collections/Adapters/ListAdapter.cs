namespace Masasamjant.Collections.Adapters
{
    /// <summary>
    /// Represents base class for adapter of <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the items in collection.</typeparam>
    public class ListAdapter<T> : CollectionAdapter<T>, IList<T>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ListAdapter{T}"/> class.
        /// </summary>
        /// <param name="source">The source list.</param>
        protected ListAdapter(IList<T> source)
            : base(source)
        { }

        /// <summary>
        /// Gets the adapted list.
        /// </summary>
        protected IList<T> SourceList => (IList<T>)Source;

        /// <summary>
        /// Gets or sets the item at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the item to get or set.</param>
        /// <returns>The item at the specified index.</returns>
        public virtual T this[int index]
        {
            get => SourceList[index];
            set => SourceList[index] = value;
        }

        /// <summary>
        /// Gets the index of the specified item in the list.
        /// </summary>
        /// <param name="item">The item to locate in the list.</param>
        /// <returns>The index of the item if found; otherwise, -1.</returns>
        public virtual int IndexOf(T item)
        {
            return SourceList.IndexOf(item);
        }

        /// <summary>
        /// Inserts an item at the specified index in the list.
        /// </summary>
        /// <param name="index">The zero-based index at which the item should be inserted.</param>
        /// <param name="item">The item to insert.</param>
        public virtual void Insert(int index, T item)
        {
            SourceList.Insert(index, item);
        }

        /// <summary>
        /// Removes the item at the specified index from the list.
        /// </summary>
        /// <param name="index">The zero-based index of the item to remove.</param>
        public virtual void RemoveAt(int index)
        {
            SourceList.RemoveAt(index);
        }
    }
}
