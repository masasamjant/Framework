using Masasamjant.Collections.Abstractions;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents a set that is initialized lazily.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class LazySet<T> : LazyCollectionBase<ISet<T>, T>, ISet<T>
    {
        private readonly Lazy<HashSet<T>> lazySet;

        /// <summary>
        /// Initializes a new instance of the <see cref="LazySet{T}"/> class.
        /// </summary>
        /// <param name="comparer">The comparer to use for the set or <c>null</c> to use default one.</param>
        public LazySet(IEqualityComparer<T>? comparer = null)
        {
            lazySet = new Lazy<HashSet<T>>(() => comparer != null ? new HashSet<T>(comparer) : new HashSet<T>(), true);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazySet{T}"/> class.
        /// </summary>
        /// <param name="items">The initial items to populate the set with.</param>
        /// <param name="comparer">The comparer to use for the set or <c>null</c> to use default one.</param>
        public LazySet(IEnumerable<T> items, IEqualityComparer<T>? comparer = null)
            : this(() => items, comparer)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="LazySet{T}"/> class.
        /// </summary>
        /// <param name="itemsProvider">A function that provides the initial items to populate the set with or <c>null</c>.</param>
        /// <param name="comparer">The comparer to use for the set or <c>null</c> to use default one.</param>
        public LazySet(Func<IEnumerable<T>?>? itemsProvider, IEqualityComparer<T>? comparer = null)
        {
            lazySet = new Lazy<HashSet<T>>(() =>
            { 
                var set = comparer != null ? new HashSet<T>(comparer) : new HashSet<T>();

                if (itemsProvider != null)
                {
                    var items = itemsProvider();

                    if (items != null && items.Any())
                    {
                        foreach (var item in items)
                            set.Add(item);
                    }
                }

                return set;
            }, true);
        }

        /// <summary>
        /// Gets the internal set.
        /// </summary>
        protected override ISet<T> Items
        {
            get { return lazySet.Value; }
        }

        /// <summary>
        /// Add specified item to set.
        /// </summary>
        /// <param name="item">The item to add to the set.</param>
        /// <returns><c>true</c> if the item was added to the set; <c>false</c> if the item was already present.</returns>
        /// <exception cref="InvalidOperationException">If the set is read-only.</exception>
        public new bool Add(T item)
        {
            CheckReadOnly();
            return Items.Add(item);
        }

        /// <summary>
        /// Remove all items in <paramref name="other"/> from this set.
        /// </summary>
        /// <param name="other">The items to remove from the set.</param>
        /// <exception cref="InvalidOperationException">If the set is read-only.</exception>
        public void ExceptWith(IEnumerable<T> other)
        {
            CheckReadOnly();
            Items.ExceptWith(other);
        }

        /// <summary>
        /// Modify this set to contains only items in this and <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The items to intersect with the set.</param>
        /// <exception cref="InvalidOperationException">If the set is read-only.</exception>
        public void IntersectWith(IEnumerable<T> other)
        {
            CheckReadOnly();
            Items.IntersectWith(other);
        }

        /// <summary>
        /// Check if this set is proper subset of <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The superset items.</param>
        /// <returns><c>true</c> if this is proper subset; <c>false</c> otherwise.</returns>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            return Items.IsProperSubsetOf(other);
        }

        /// <summary>
        /// Check if this set is proper superset of <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The subset items.</param>
        /// <returns><c>true</c> if this is proper superset; <c>false</c> otherwise.</returns>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            return Items.IsProperSupersetOf(other);
        }

        /// <summary>
        /// Check if this set is subset of <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The superset items.</param>
        /// <returns><c>true</c> if this is subset; <c>false</c> otherwise.</returns>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            return Items.IsSubsetOf(other);
        }

        /// <summary>
        /// Check if this set is superset of <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The subset items.</param>
        /// <returns><c>true</c> if this is superset; <c>false</c> otherwise.</returns>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            return Items.IsSupersetOf(other);
        }

        /// <summary>
        /// Check if this set overlaps with <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The other items.</param>
        /// <returns><c>true</c> if this overlaps; <c>false</c> otherwise.</returns>
        public bool Overlaps(IEnumerable<T> other)
        {
            return Items.Overlaps(other);
        }

        /// <summary>
        /// Check if this set and <paramref name="other"/> contains same items.
        /// </summary>
        /// <param name="other">The other items.</param>
        /// <returns><c>true</c> if this and <paramref name="other"/> contain same items; <c>false</c> otherwise.</returns>
        public bool SetEquals(IEnumerable<T> other)
        {
            return Items.SetEquals(other);
        }

        /// <summary>
        /// Modify this set to contain items that are in this or in <paramref name="other"/>, but not in both.
        /// </summary>
        /// <param name="other">The other items.</param>
        /// <exception cref="InvalidOperationException">If the set is read-only.</exception>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            CheckReadOnly();
            Items.SymmetricExceptWith(other);
        }

        /// <summary>
        /// Modify this set to contain all items that are in this set or in <paramref name="other"/>.
        /// </summary>
        /// <param name="other">The other items.</param>
        /// <exception cref="InvalidOperationException">If the set is read-only.</exception>
        public void UnionWith(IEnumerable<T> other)
        {
            CheckReadOnly();
            Items.UnionWith(other);
        }
    }
}
