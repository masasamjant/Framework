namespace Masasamjant.Collections
{
    /// <summary>
    /// Provides helper methods to lists.
    /// </summary>
    public static class ListHelper
    {
        /// <summary>
        /// Merge specified source list with specified destination list. This means that items in <paramref name="destination"/> not found
        /// in <paramref name="source"/> are removed from <paramref name="destination"/> and items in <paramref name="source"/>, but no in <paramref name="destination"/> 
        /// are added to <paramref name="destination"/>.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="destination">The destination list.</param>
        /// <param name="source">The source list.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void Merge<T>(this IList<T> destination, IList<T> source)
        {
            if (source.Count == 0 || ReferenceEquals(destination, source))
                return;

            Merge(destination, [source]);
        }

        /// <summary>
        /// Merge specified source lists to specified destination list. This means that items in <paramref name="destination"/> not found
        /// in any of the <paramref name="sources"/> are removed from <paramref name="destination"/> and items in <paramref name="sources"/>, but no in <paramref name="destination"/> 
        /// are added to <paramref name="destination"/>.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="destination">The destination list.</param>
        /// <param name="sources">The source lists.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void Merge<T>(this IList<T> destination, IEnumerable<IList<T>> sources)
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));
    
            RemoveOrphants(destination, sources);

            CollectionHelper.AddMissing(destination, sources);
        }

        /// <summary>
        /// Remove items from specified destination list that do not exist in comparison list.
        /// </summary>
        /// <typeparam name="T">The type of list item.</typeparam>
        /// <param name="destination">The list to remove items.</param>
        /// <param name="compare">The list to check items.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void RemoveOrphants<T>(this IList<T> destination, IList<T> compare)
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));

            if (ReferenceEquals(destination, compare))
                return;

            for (int index = destination.Count - 1; index >= 0; index--)
            {
                if (!compare.Contains(destination[index]))
                    destination.RemoveAt(index);
            }
        }

        /// <summary>
        /// Remove items from specified destination list that do not exist in specified lists.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="destination">The list to remove items.</param>
        /// <param name="lists">The lists to check items.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void RemoveOrphants<T>(this IList<T> destination, IEnumerable<IList<T>> lists)
        {
            CollectionHelper.CheckNotReadOnly(destination, nameof(destination));

            // First remove those items not in any source.
            for (int index = destination.Count - 1; index >= 0; index--)
            {
                bool remove = true;

                foreach (var source in lists)
                {
                    if (ReferenceEquals(destination, source))
                        continue;

                    if (source.Contains(destination[index]))
                    {
                        remove = false;
                        break;
                    }
                }

                if (remove)
                    destination.RemoveAt(index);
            }
        }

        /// <summary>
        /// Split list at specified index. The item at the <paramref name="splitIndex"/> will be in first list and items after <paramref name="splitIndex"/> 
        /// will be in second list. Also if <paramref name="splitIndex"/> is out of range of <paramref name="list"/> or <paramref name="list"/> has single item, then the second 
        /// list will be empty. If <paramref name="list"/> is empty, then returns empty.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="list">The list to split.</param>
        /// <param name="splitIndex">The index to split list.</param>
        /// <returns>A <paramref name="list"/> splitted to to separate lists or empty.</returns>
        public static IEnumerable<IList<T>> SplitByIndex<T>(this IList<T> list, int splitIndex)
        {
            if (list.Count == 0)
                return [];

            if (splitIndex < 0 || splitIndex > list.Count || list.Count == 1)
                return [list, new List<T>()];

            var lists = new List<IList<T>>(2);
            var current = new List<T>();

            for (int index = 0; index < list.Count; index++)
            {
                current.Add(list[index]);

                if (index == splitIndex)
                {
                    lists.Add(current);
                    current = new List<T>();
                }
            }

            if (current.Count > 0 && !lists.Contains(current))
                lists.Add(current);

            return lists.AsReadOnly();
        }

        /// <summary>
        /// Iterate list forward.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="list">The list to iterate.</param>
        /// <returns>A <see cref="Iteration{T}"/>.</returns>
        public static IEnumerable<Iteration<T>> IterateForward<T>(this IList<T> list)
        {
            for (int index = 0; index < list.Count; index++)
                yield return new Iteration<T>(list[index], index);
        }

        /// <summary>
        /// Iterate list backward.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="list">The list to iterate.</param>
        /// <returns>A <see cref="Iteration{T}"/>.</returns>
        public static IEnumerable<Iteration<T>> IterateBackward<T>(this IList<T> list)
        {
            for (int index = list.Count - 1; index >= 0; index--)
                yield return new Iteration<T>(list[index], index);
        }

        /// <summary>
        /// Keep items in list that satisfy specified predicate. This means that items that do not satisfy the predicate are removed from the list.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="keepPredicate">The predicate to keep items.</param>
        /// <exception cref="ArgumentException">If <paramref name="list"/> is in read-only state.</exception>
        public static void Keep<T>(this IList<T> list, Predicate<T> keepPredicate) => Keep(list, new Func<T, bool>(item => keepPredicate(item)));

        /// <summary>
        /// Keep items in list that satisfy specified predicate. This means that items that do not satisfy the predicate are removed from the list.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="keepPredicate">The predicate to keep items.</param>
        /// <exception cref="ArgumentException">If <paramref name="list"/> is in read-only state.</exception>
        public static void Keep<T>(this IList<T> list, Func<T, bool> keepPredicate)
        {
            CollectionHelper.CheckNotReadOnly(list, nameof(list));

            for (int index = list.Count - 1; index >= 0; index--)
            {
                if (!keepPredicate(list[index]))
                    list.RemoveAt(index);
            }
        }

        /// <summary>
        /// Remove items in list that satisfy specified predicate. This means that items that satisfy the predicate are removed from the list.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="removePredicate">The predicate to remove items.</param>
        /// <exception cref="ArgumentException">If <paramref name="list"/> is in read-only state.</exception>
        public static void Remove<T>(this IList<T> list, Predicate<T> removePredicate) => Remove(list, new Func<T, bool>(item => removePredicate(item)));

        /// <summary>
        /// Remove items in list that satisfy specified predicate. This means that items that satisfy the predicate are removed from the list.
        /// </summary>
        /// <typeparam name="T">The type of the list item.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="removePredicate">The predicate to remove items.</param>
        /// <exception cref="ArgumentException">If <paramref name="list"/> is in read-only state.</exception>
        public static void Remove<T>(this IList<T> list, Func<T, bool> removePredicate)
        {
            CollectionHelper.CheckNotReadOnly(list, nameof(list));

            for (int index = list.Count - 1; index >= 0; index--)
            {
                if (removePredicate(list[index]))
                    list.RemoveAt(index);
            }
        }

        /// <summary>
        /// Add possible duplicate item to list.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="list">The list to add item.</param>
        /// <param name="item">The item to add.</param>
        /// <param name="duplicateBehavior">The duplicate behavior, if <paramref name="item"/> happens to be duplicate.</param>
        /// <exception cref="ArgumentException">
        /// If <paramref name="list"/> is in read-only state.
        /// -or-
        /// If value of <paramref name="duplicateBehavior"/> is not defined.
        /// </exception>
        /// <exception cref="DuplicateItemException">If list contains <paramref name="item"/> and <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/>.</exception>
        public static void AddDuplicate<T>(this IList<T> list, T item, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore)
        {
            CollectionHelper.CheckNotReadOnly(list, nameof(list));

            if (!Enum.IsDefined(duplicateBehavior))
                throw new ArgumentException("The value is not defined.", nameof(duplicateBehavior));

            if (list.Contains(item))
            {
                switch (duplicateBehavior)
                {
                    case DuplicateBehavior.Insert:
                        list.Add(item);
                        break;
                    case DuplicateBehavior.Error:
                        throw new DuplicateItemException(item, "The duplicate item in collections.");
                    default:
                        break;
                }
            }
            else
                list.Add(item);
        }
    }
}
