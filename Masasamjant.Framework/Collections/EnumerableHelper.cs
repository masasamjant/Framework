using Masasamjant.Linq;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Provides helper methods to <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class EnumerableHelper
    {
        /// <summary>
        /// Check if specified <see cref="IEnumerable{T}"/> is <c>null</c> or empty.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> or <c>null</c>.</param>
        /// <returns><c>true</c> if <paramref name="source"/> is <c>null</c> or empty; <c>false</c> otherwise.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T>? source)
        {
            if (source != null)
                return !source.Any();

            return true;
        }

        /// <summary>
        /// Execute specified <see cref="Action{T}"/> for each item in specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <param name="action">The <see cref="Action{T}"/> to execute for each item.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

        /// <summary>
        /// Execute specified <see cref="Action{T}"/> for each item in specified <see cref="IEnumerable{T}"/> that matches the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <param name="match">The predicate to match.</param>
        /// <param name="action">The <see cref="Action{T}"/> to execute for each match item.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Predicate<T> match, Action<T> action)
        {
            foreach (var item in source)
            {
                if (match(item))
                    action(item);
            }
        }

        /// <summary>
        /// Execute specified <see cref="Action{T}"/> for each item in specified <see cref="IEnumerable{T}"/> that matches the specified predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <param name="match">The predicate to match.</param>
        /// <param name="action">The <see cref="Action{T}"/> to execute for each match item.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Func<T, bool> match, Action<T> action)
        {
            foreach (var item in source)
            {
                if (match(item))
                    action(item);
            }
        }

        /// <summary>
        /// Check if specified <see cref="IEnumerable{T}"/> contains any of the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to check.</param>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of items to check.</param>
        /// <returns><c>true</c> if <paramref name="source"/> contains any items from <paramref name="items"/>; <c>false</c> otherwise.</returns>
        public static bool ContainsAny<T>(this IEnumerable<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (source.Contains(item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Check if specified <see cref="IEnumerable{T}"/> contains all of the specified items.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to check.</param>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of items to check.</param>
        /// <returns><c>true</c> if <paramref name="source"/> contains all items from <paramref name="items"/>; <c>false</c> otherwise.</returns>
        public static bool ContainsAll<T>(this IEnumerable<T> source, IEnumerable<T> items)
        {
            foreach (var item in items)
            {
                if (!source.Contains(item))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Create a batches from specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="size">The batch size.</param>
        /// <returns>A batches.</returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(this IEnumerable<T> source, int size)
        {
            if (size <= 0)
                yield break;

            T[]? array = null;
            int count = 0;

            foreach (var item in source)
            {
                if (array == null)
                    array = new T[size];
                
                array[count++] = item;

                if (count == size)
                {
                    yield return array;
                    array = null;
                    count = 0;
                }
            }

            if (array != null && count > 0)
                yield return array.Take(count);
        }

        /// <summary>
        /// Sort specified <see cref="IEnumerable{T}"/> using specified sort descriptors.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/> to sort.</param>
        /// <param name="descriptors">The sort descriptors.</param>
        /// <returns>A sorted <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> source, IEnumerable<SortDescriptor> descriptors)
            => QueryableHelper.Sort(source.AsQueryable(), descriptors);

        /// <summary>
        /// Get <see cref="CopyEnumerable{T}"/> for specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <returns>A <see cref="CopyEnumerator{T}"/>.</returns>
        public static CopyEnumerable<T> GetCopyEnumerator<T>(this IEnumerable<T> source)
            => new CopyEnumerable<T>(source);

        /// <summary>
        /// Create a <see cref="Queue{T}"/> from specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <returns>A <see cref="Queue{T}"/>.</returns>
        public static Queue<T> ToQueue<T>(this IEnumerable<T> source) => new Queue<T>(source);

        /// <summary>
        /// Create a <see cref="Stack{T}"/> from specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <returns>A <see cref="Stack{T}"/>.</returns>
        public static Stack<T> ToStack<T>(this IEnumerable<T> source) => new Stack<T>(source);

        /// <summary>
        /// Execute paging to specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The base <see cref="IEnumerable{T}"/>.</param>
        /// <param name="page">The <see cref="PageInfo"/>.</param>
        /// <param name="calculateTotalCount"><c>true</c> if calculates total item count in <paramref name="source"/>; <c>false</c> otherwise.</param>
        /// <returns>A paging <see cref="IEnumerable{T}"/>.</returns>
        public static IEnumerable<T> Page<T>(this IEnumerable<T> source, PageInfo page, bool calculateTotalCount = false)
            => QueryableHelper.Page(source.AsQueryable(), page, calculateTotalCount);

        /// <summary>
        /// Get unique items from specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="equalityComparer">The <see cref="IEqualityComparer{T}"/> or <c>null</c> to use default one.</param>
        /// <returns>A unique items from <paramref name="source"/>.</returns>
        public static IEnumerable<T> Unique<T>(this IEnumerable<T> source, IEqualityComparer<T>? equalityComparer = null)
        {
            equalityComparer ??= EqualityComparer<T>.Default;

            var set = new HashSet<T>(equalityComparer);

            foreach (var item in source)
                set.Add(item);

            return set;
        }
    }
}
