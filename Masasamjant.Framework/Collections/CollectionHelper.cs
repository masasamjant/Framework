using System.ComponentModel;
using System.Linq;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Provides helper methods to work with collections.
    /// </summary>
    public static class CollectionHelper
    {
        /// <summary>
        /// Combines the specified collections into a new collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="first">The 1st collection.</param>
        /// <param name="second">The 2nd collection.</param>
        /// <param name="duplicateBehavior">The behavior of handling duplicate items.</param>
        /// <param name="maxItemCount">The how many items <paramref name="collections"/> should have at max. Default value is <see cref="int.MaxValue"/>.</param>
        /// <returns>A new collection that contains items from <paramref name="collections"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If value of <paramref name="duplicateBehavior"/> is not defined.
        /// -or-
        /// If <paramref name="dictionaries"/> has more than <paramref name="maxItemCount"/> items in total.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="maxItemCount"/> is not greater than 0.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Insert"/>.</exception>
        /// <exception cref="DuplicateKeyException{TKey}">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/> and duplicate key is found.</exception>
        public static ICollection<T> Combine<T>(ICollection<T> first, ICollection<T> second, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore, int maxItemCount = int.MaxValue)
        {
            return Combine([first, second], duplicateBehavior, maxItemCount);
        }

        /// <summary>
        /// Combines the specified collections into a new collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="first">The 1st collection.</param>
        /// <param name="second">The 2nd collection.</param>
        /// <param name="third">The 3rd collection.</param>
        /// <param name="duplicateBehavior">The behavior of handling duplicate items.</param>
        /// <param name="maxItemCount">The how many items <paramref name="collections"/> should have at max. Default value is <see cref="int.MaxValue"/>.</param>
        /// <returns>A new collection that contains items from <paramref name="collections"/>.</returns>
        /// <exception cref="ArgumentException">
        /// If value of <paramref name="duplicateBehavior"/> is not defined.
        /// -or-
        /// If <paramref name="dictionaries"/> has more than <paramref name="maxItemCount"/> items in total.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="maxItemCount"/> is not greater than 0.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Insert"/>.</exception>
        /// <exception cref="DuplicateKeyException{TKey}">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/> and duplicate key is found.</exception>
        public static ICollection<T> Combine<T>(ICollection<T> first, ICollection<T> second, ICollection<T> third, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore, int maxItemCount = int.MaxValue)
        {
            return Combine([first, second, third], duplicateBehavior, maxItemCount);
        }

        /// <summary>
        /// Combines the specified collections into a new collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collections">The collections to combine.</param>
        /// <param name="duplicateBehavior">The behavior of handling duplicate items.</param>
        /// <param name="maxItemCount">The how many items <paramref name="collections"/> should have at max. Default value is <see cref="int.MaxValue"/>.</param>
        /// <returns>A new collection that contains items from <paramref name="collections"/>.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="duplicateBehavior"/> is not defined.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="maxItemCount"/> is not greater than 0.</exception>
        /// <exception cref="InvalidOperationException">If <paramref name="collections"/> has more than <paramref name="maxItemCount"/> items in total.</exception>
        /// <exception cref="NotSupportedException">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Insert"/>.</exception>
        /// <exception cref="DuplicateKeyException{TKey}">If <paramref name="duplicateBehavior"/> is <see cref="DuplicateBehavior.Error"/> and duplicate key is found.</exception>
        public static ICollection<T> Combine<T>(IEnumerable<ICollection<T>> collections, DuplicateBehavior duplicateBehavior = DuplicateBehavior.Ignore, int maxItemCount = int.MaxValue)
        {
            if (!Enum.IsDefined(duplicateBehavior))
                throw new ArgumentException("The value is not defined.", nameof(duplicateBehavior));

            if (maxItemCount < 1)
                throw new ArgumentOutOfRangeException(nameof(maxItemCount), maxItemCount, "The value must be greater than 0.");

            var result = new List<T>();

            foreach (var collection in collections)
            {
                CombineCollection(result, collection, duplicateBehavior, maxItemCount);
            }

            return result;
        }

        private static void CombineCollection<T>(List<T> list, ICollection<T> collection, DuplicateBehavior duplicateBehavior, int maxItemCount)
        {
            foreach (var item in collection)
            {
                if (list.Count == maxItemCount)
                    throw new InvalidOperationException($"The collections cannot have more than {maxItemCount} items in total.");

                list.AddDuplicate(item, duplicateBehavior);
            }
        }

        /// <summary>
        /// Merges the specified collections into the destination collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="destination">The destination collection.</param>
        /// <param name="source">The source collection.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void Merge<T>(this ICollection<T> destination, ICollection<T> source) => Merge(destination, [source]);

        /// <summary>
        /// Merges the specified collections into the destination collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="destination">The destination collection.</param>
        /// <param name="collections">The collections to merge.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void Merge<T>(this ICollection<T> destination, IEnumerable<ICollection<T>> collections)
        {
            CheckNotReadOnly(destination, nameof(destination));
            RemoveOrphants(destination, collections);
            AddMissing(destination, collections);
        }

        /// <summary>
        /// Remove items from specified destination collection that do not exist in comparison collection.
        /// </summary>
        /// <typeparam name="T">The type of collection item.</typeparam>
        /// <param name="destination">The collection to remove items.</param>
        /// <param name="compare">The collection to check items.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void RemoveOrphants<T>(this ICollection<T> destination, ICollection<T> compare)
        {
            CheckNotReadOnly(destination, nameof(destination));

            if (ReferenceEquals(destination, compare))
                return;

            foreach (var item in CopyEnumerable.CreateCopyEnumerable(destination))
            {
                if (!compare.Contains(item))
                    destination.Remove(item);
            }
        }

        /// <summary>
        /// Remove items from specified destination collection that do not exist in specified collections.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="destination">The collection to remove items.</param>
        /// <param name="collections">The collections to check items.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void RemoveOrphants<T>(this ICollection<T> destination, IEnumerable<ICollection<T>> collections)
        {
            CheckNotReadOnly(destination, nameof(destination));

            foreach (var item in CopyEnumerable.CreateCopyEnumerable(destination))
            {
                bool remove = true;

                foreach (var collection in collections)
                {
                    if (ReferenceEquals(destination, collection))
                        continue;

                    if (collection.Contains(item))
                    {
                        remove = false;
                        break;
                    }
                }

                if (remove)
                    destination.Remove(item);
            }
        }

        /// <summary>
        /// Adds the specified items to the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collection">The collection to add items.</param>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            CheckNotReadOnly(collection, nameof(collection));

            if (collection is List<T> list)
                list.AddRange(items);
            else
            {
                foreach (var item in items)
                    collection.Add(item);
            }
        }

        /// <summary>
        /// Adds the specified items to the collection if the item is not already in the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collection">The collection to add items.</param>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void AddMissing<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            CheckNotReadOnly(collection, nameof(collection));

            foreach (var item in items)
            {
                if (!collection.Contains(item))
                    collection.Add(item);
            }
        }

        /// <summary>
        /// Adds the missing items from specified collections to destination collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="destination">The destination collection to add items.</param>
        /// <param name="collections">The collections to get missing items.</param>
        /// <exception cref="ArgumentException">If <paramref name="destination"/> is in read-only state.</exception>
        public static void AddMissing<T>(ICollection<T> destination, IEnumerable<ICollection<T>> collections)
        {
            CheckNotReadOnly(destination, nameof(destination));

            foreach (var collection in collections)
            {
                if (ReferenceEquals(destination, collection))
                    continue;

                foreach (var item in collection)
                {
                    if (!destination.Contains(item))
                        destination.Add(item);
                }
            }
        }

        /// <summary>
        /// Fills the collection of <typeparamref name="T1"/> with the specified items of <typeparamref name="T2"/>.
        /// </summary>
        /// <typeparam name="T1">The type of the collection item.</typeparam>
        /// <typeparam name="T2">The type of the items inherited from <typeparamref name="T1"/>.</typeparam>
        /// <param name="collection">The collection to add items.</param>
        /// <param name="items">The items to add.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void Fill<T1, T2>(this ICollection<T1> collection, IEnumerable<T2> items) where T2 : T1
        {
            CheckNotReadOnly(collection, nameof(collection));

            foreach (var item in items)
                collection.Add(item);
        }

        /// <summary>
        /// Removes the specified items from the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collection">The collection to remove items.</param>
        /// <param name="items">The items to remove.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void RemoveRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            CheckNotReadOnly(collection, nameof(collection));

            foreach (var item in items)
                collection.Remove(item);
        }

        /// <summary>
        /// Determines whether the collection contains all the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="items">The items to check.</param>
        /// <returns><c>true</c> if <paramref name="collection"/> contains all items in <paramref name="items"/>; <c>false</c> otherwise.</returns>
        public static bool ContainsRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            if (ReferenceEquals(collection, items))
                return true;

            if (collection.Count == 0)
                return items.Count() == 0;

            foreach (var item in items)
            {
                if (!collection.Contains(item))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Splits the collection into multiple collections by the specified count.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collection">The collection to split.</param>
        /// <param name="splitCount">The split count of how many items each collection should contain.</param>
        /// <returns>A split collections.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="splitCount"/> is less than 0.</exception>
        public static IEnumerable<ICollection<T>> SplitByCount<T>(this ICollection<T> collection, int splitCount)
        {
            if (splitCount < 0)
                throw new ArgumentOutOfRangeException(nameof(splitCount), splitCount, "The value must be greater than or equal to 0.");

            if (splitCount == 0 || collection.Count <= splitCount)
                return [collection];

            var result = new List<ICollection<T>>();
            var current = new List<T>();

            foreach (var item in collection)
            {
                current.Add(item);

                if (current.Count == splitCount)
                {
                    result.Add(current);
                    current = new List<T>();
                }
            }

            if (current.Count > 0 && !result.Contains(current))
                result.Add(current);

            return result;
        }

        /// <summary>
        /// Keep items in collection that satisfy specified predicate. This means that items that do not satisfy the predicate are removed from the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="keepPredicate">The predicate to keep items.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void Keep<T>(this ICollection<T> collection, Predicate<T> keepPredicate) => Keep(collection, new Func<T, bool>(item => keepPredicate(item)));

        /// <summary>
        /// Keep items in collection that satisfy specified predicate. This means that items that do not satisfy the predicate are removed from the collection.
        /// </summary>
        /// <typeparam name="T">The type of the collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="keepPredicate">The predicate to keep items.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void Keep<T>(this ICollection<T> collection, Func<T, bool> keepPredicate)
        {
            CheckNotReadOnly(collection, nameof(collection));

            foreach (var item in CopyEnumerable.CreateCopyEnumerable(collection))
            {
                if (!keepPredicate(item))
                    collection.Remove(item);
            }
        }

        /// <summary>
        /// Remove items in collection that satisfy specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="removePredicate">The predicate to remove items.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void Remove<T>(this ICollection<T> collection, Predicate<T> removePredicate) => Remove(collection, new Func<T, bool>(item => removePredicate(item)));

        /// <summary>
        /// Remove items in collection that satisfy specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of collection item.</typeparam>
        /// <param name="collection">The collection.</param>
        /// <param name="removePredicate">The predicate to remove items.</param>
        /// <exception cref="ArgumentException">If <paramref name="collection"/> is in read-only state.</exception>
        public static void Remove<T>(this ICollection<T> collection, Func<T, bool> removePredicate)
        {
            CheckNotReadOnly(collection, nameof(collection));

            foreach (var item in CopyEnumerable.CreateCopyEnumerable(collection))
            {
                if (removePredicate(item))
                    collection.Remove(item);
            }
        }

        internal static void CheckNotReadOnly<T>(ICollection<T> collection, string paramName)
        {
            if (collection.IsReadOnly)
                throw new ArgumentException("The collection is in read-only state.", paramName);
        }
    }
}
