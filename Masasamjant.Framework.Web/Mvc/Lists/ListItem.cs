namespace Masasamjant.Web.Mvc.Lists
{
    /// <summary>
    /// Provides methods to create list items.
    /// </summary>
    public static class ListItem
    {
        /// <summary>
        /// Create read-only collection of list items from specified items.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="items">The items to create list items to.</param>
        /// <param name="getOrder">The function to get order for item or <c>null</c> to use default order.</param>
        /// <returns>A read-only collection of list items.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <c>null</c>.</exception>
        public static IReadOnlyCollection<ListItem<T>> Create<T>(IEnumerable<T> items, Func<T, int>? getOrder = null)
        {
            ArgumentNullException.ThrowIfNull(items);

            var array = items.ToArray();
            var list = new List<ListItem<T>>(array.Length);

            if (array.Length == 0)
                return list.AsReadOnly();

            for (int index = 0; index < array.Length; index++)
            {
                var item = array[index];
                bool first = index == 0;
                bool last = index == array.Length - 1;
                bool alternate = index % 2 == 0;
                var order = getOrder != null ? getOrder(item) : index;
                list.Add(new ListItem<T>(item, alternate, order, first, last));
            }

            return list.AsReadOnly();
        }
    }

    /// <summary>
    /// Represents item in list like views.
    /// </summary>
    /// <typeparam name="T">The type of the data item.</typeparam>
    public sealed class ListItem<T>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ListItem{T}"/> class.
        /// </summary>
        /// <param name="item">The data item.</param>
        /// <param name="alternate"><c>true</c> if this is alternate item; <c>false</c> otherwise.</param>
        /// <param name="order">The order of the item.</param>
        /// <param name="first"><c>true</c> if this is the first item in the list; <c>false</c> otherwise.</param>
        /// <param name="last"><c>true</c> if this is the last item in the list; <c>false</c> otherwise.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="item"/> is <c>null</c>.</exception>
        public ListItem(T item, bool alternate, int order, bool first, bool last)
        {
            Item = item ?? throw new ArgumentNullException(nameof(item));
            IsAlternate = alternate;
            Order = order;
            IsFirst = first;
            IsLast = last;  
        }

        /// <summary>
        /// Gets whether or not item is alternate item.
        /// </summary>
        public bool IsAlternate { get; }

        /// <summary>
        /// Gets the data item associated with list item.
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// Gets the order of the item. Smaller value means higher order.
        /// </summary>
        public int Order { get; }

        /// <summary>
        /// Gets whether or not item is the first item in the list.
        /// </summary>
        public bool IsFirst { get; }

        /// <summary>
        /// Gets whether or not item is the last item in the list.
        /// </summary>
        public bool IsLast { get; }
    }
}
