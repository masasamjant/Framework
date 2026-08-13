namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents a list that is initialized lazily.
    /// </summary>
    /// <typeparam name="T">The type of items in the list.</typeparam>
    public sealed class LazyList<T> : LazyCollectionBase<IList<T>, T>, IList<T>
    {
        private readonly Lazy<List<T>> lazyList;

        /// <summary>
        /// Initializes new default instance of the <see cref="LazyList{T}"/> class.
        /// </summary>
        public LazyList()
            : this(10)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="LazyList{T}"/> class with the initial items.
        /// </summary>
        /// <param name="items">The initial items to populate the list.</param>
        public LazyList(IEnumerable<T> items)
            : this(10, items)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="LazyList{T}"/> class with the specified capacity and initial items.
        /// </summary>
        /// <param name="capacity">The initial capacity of the list.</param>
        /// <param name="items">The initial items to populate the list.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="capacity"/> is negative.</exception>
        public LazyList(int capacity, IEnumerable<T> items)
            : this(() => items, capacity)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="LazyList{T}"/> class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The initial capacity of the list.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="capacity"/> is negative.</exception>
        public LazyList(int capacity)
            : this(null, capacity)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="LazyList{T}"/> class with the specified items provider and capacity.
        /// </summary>
        /// <param name="itemsProvider">The function that provides the initial items for the list.</param>
        public LazyList(Func<IEnumerable<T>?>? itemsProvider)
            : this(itemsProvider, 10)
        { }

        /// <summary>
        /// Initializes new instance of the <see cref="LazyList{T}"/> class with the specified items provider and capacity.
        /// </summary>
        /// <param name="itemsProvider">The function that provides the initial items for the list.</param>
        /// <param name="capacity">The initial capacity of the list.</param>
        /// <exception cref="ArgumentOutOfRangeException">If the <paramref name="capacity"/> is negative.</exception>
        public LazyList(Func<IEnumerable<T>?>? itemsProvider, int capacity)
            : base()
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity cannot be negative.");

            lazyList = new Lazy<List<T>>(() => 
            {
                var list = new List<T>(capacity);

                if (itemsProvider != null)
                {
                    var items = itemsProvider();
                    
                    if (items != null && items.Any())
                    {
                        list.AddRange(items);
                    }
                }

                return list;
            }, true);
        }

        /// <summary>
        /// Gets or sets item at specified index.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        /// <returns>The item at the specified index.</returns>
        /// <exception cref="InvalidOperationException">If the list is read-only.</exception>
        public T this[int index]
        {
            get { return Items[index]; }
            set
            {
                CheckReadOnly();
                Items[index] = value;
            }
        }

        /// <summary>
        /// Gets the index of the specified item in the list.
        /// </summary>
        /// <param name="item">The item to locate in the list.</param>
        /// <returns>The index of the specified item, or -1 if not found.</returns>
        public int IndexOf(T item)
        {
            return Items.IndexOf(item);
        }
        
        /// <summary>
        /// Inserts an item at the specified index in the list.
        /// </summary>
        /// <param name="index">The index at which to insert the item.</param>
        /// <param name="item">The item to insert.</param>
        /// <exception cref="InvalidOperationException">If the list is read-only.</exception>
        public void Insert(int index, T item)
        {
            CheckReadOnly();
            Items.Insert(index, item);
        }
        
        /// <summary>
        /// Removes the item at the specified index in the list.
        /// </summary>
        /// <param name="index">The index of the item to remove.</param>
        /// <exception cref="InvalidOperationException">If the list is read-only.</exception>   
        public void RemoveAt(int index)
        {
            CheckReadOnly();
            Items.RemoveAt(index);
        }

        /// <summary>
        /// Gets the internal list of items.
        /// </summary>
        protected override List<T> Items
        {
            get { return lazyList.Value; }
        }
    }
}
