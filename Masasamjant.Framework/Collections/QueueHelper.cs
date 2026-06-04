namespace Masasamjant.Collections
{
    /// <summary>
    /// Provides helper and extension methods to <see cref="Queue{T}"/>.
    /// </summary>
    public static class QueueHelper
    {
        /// <summary>
        /// Execute specified <see cref="Action{T}"/> action to each dequeued item of specified <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to get item.</param>
        /// <param name="action">The <see cref="Action{T}"/> to execute for item.</param>
        public static void ForEachDequeue<T>(this Queue<T> queue, Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(action);

            while (queue.TryDequeue(out var item))
                action(item);
        }

        /// <summary>
        /// Enqueue range of items to specified <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to add items.</param>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of items to add.</param>
        public static void EnqueueRange<T>(this Queue<T> queue, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(items);

            if (ReferenceEquals(queue, items))
                return;

            foreach (var item in items)
                queue.Enqueue(item);
        }

        /// <summary>
        /// Dequeue range of items from specified <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to dequeue items.</param>
        /// <param name="count">The max count of items to dequeue.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of dequeued items.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="count"/> is less than 0.</exception>
        public static IEnumerable<T> DequeueRange<T>(this Queue<T> queue, int count)
        {
            ArgumentNullException.ThrowIfNull(queue);

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "The value must be greater than or equal to 0.");

            var result = new List<T>(count);

            if (count == 0 || queue.Count == 0)
                return result.AsReadOnly();

            while (result.Count < count && queue.TryDequeue(out var item))
                result.Add(item);

            return result.AsReadOnly();
        }

        /// <summary>
        /// Dequeue range of items from specified <see cref="Queue{T}"/>. This dequeues all items 
        /// from specified <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to dequeue items.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of dequeued items.</returns>
        public static IEnumerable<T> DequeueRange<T>(this Queue<T> queue)
        {
            ArgumentNullException.ThrowIfNull(queue);

            var result = new List<T>(queue.Count);

            while (queue.TryDequeue(out var item))
                result.Add(item);

            return result.AsReadOnly();
        }

        /// <summary>
        /// Split single <see cref="Queue{T}"/> to several <see cref="Queue{T}"/>s. After split the original 
        /// <see cref="Queue{T}"/> is empty.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to split.</param>
        /// <param name="size">The target size of each queue.</param>
        /// <returns>A split queues.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="size"/> is less than 1.</exception>
        public static IEnumerable<Queue<T>> Split<T>(this Queue<T> queue, int size)
        {
            ArgumentNullException.ThrowIfNull(queue);

            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size), size, "The value must be greater than 0.");

            var result = new List<Queue<T>>();

            if (queue.Count == 0)
                return result.AsReadOnly();

            if (queue.Count < size)
            {
                result.Add(Transfer(queue));
            }
            else
            {
                var current = new Queue<T>();

                while (queue.Count > 0)
                {
                    current.Enqueue(queue.Dequeue());

                    if (current.Count == size)
                    {
                        result.Add(current);
                        current = new Queue<T>();
                    }
                }

                if (current.Count > 0 && !result.Contains(current))
                    result.Add(current);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Transfer all items from source <see cref="Queue{T}"/> to target <see cref="Queue{T}"/>. After transfer the source
        /// <see cref="Queue{T}"/> will be empty.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <param name="target">The target queue.</param>
        public static void TransferTo<T>(this Queue<T> source, Queue<T> target)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);

            if (ReferenceEquals(source, target))
                return;

            while (source.TryDequeue(out var item))
                target.Enqueue(item);
        }

        /// <summary>
        /// Creates new <see cref="Queue{T}"/> and transfers all items from the source queue to the new queue.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <returns>A new <see cref="Queue{T}"/> containing all items from the source queue.</returns>
        public static Queue<T> Transfer<T>(this Queue<T> source)
        {
            ArgumentNullException.ThrowIfNull(source);
            var target = new Queue<T>();
            source.TransferTo(target);
            return target;
        }

        /// <summary>
        /// Dequeue items from specified <see cref="Queue{T}"/> until first item is specified stop item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to dequeue items.</param>
        /// <param name="stopItem">The first item to stop dequeue.</param>
        /// <returns>A items from <paramref name="queue"/> until <paramref name="stopItem"/> is first item.</returns>
        public static IEnumerable<T> DequeueUntil<T>(this Queue<T> queue, T stopItem)
        {
            ArgumentNullException.ThrowIfNull(queue);
            Predicate<T> stopPredicate = item => Equals(item, stopItem);
            return DequeueUntil(queue, stopPredicate);
        }

        /// <summary>
        /// Dequeue items from specified <see cref="Queue{T}"/> until first item meets specified stop predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to dequeue items.</param>
        /// <param name="stopPredicate">The stop predicate. If first item match this, then stops.</param>
        /// <returns>A items from <paramref name="queue"/> until first item match <paramref name="stopPredicate"/>.</returns>
        public static IEnumerable<T> DequeueUntil<T>(this Queue<T> queue, Predicate<T> stopPredicate)
            => DequeueUntil(queue, new Func<T, bool>(x => stopPredicate(x)));

        /// <summary>
        /// Dequeue items from specified <see cref="Queue{T}"/> until first item meets specified stop predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to dequeue items.</param>
        /// <param name="stopPredicate">The stop predicate. If first item match this, then stops.</param>
        /// <returns>A items from <paramref name="queue"/> until first item match <paramref name="stopPredicate"/>.</returns>
        public static IEnumerable<T> DequeueUntil<T>(this Queue<T> queue, Func<T, bool> stopPredicate)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(stopPredicate);

            var result = new List<T>();

            while (queue.TryPeek(out var item))
            {
                if (stopPredicate(item))
                    break;

                item = queue.Dequeue();
                result.Add(item);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Enqueue items that match specified predicate to <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to enqueue items.</param>
        /// <param name="items">The all items.</param>
        /// <param name="pushPredicate">The predicate to match to enququed item.</param>
        public static void EnqueueMatches<T>(this Queue<T> queue, IEnumerable<T> items, Predicate<T> enqueuePredicate)
            => EnqueueMatches(queue, items, new Func<T, bool>(x => enqueuePredicate(x)));

        /// <summary>
        /// Enqueue items that match specified predicate to <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to enqueue items.</param>
        /// <param name="items">The all items.</param>
        /// <param name="pushPredicate">The predicate to match to enququed item.</param>
        public static void EnqueueMatches<T>(this Queue<T> queue, IEnumerable<T> items, Func<T, bool> enqueuePredicate)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(enqueuePredicate);

            foreach (var item in items.Where(enqueuePredicate))
                queue.Enqueue(item);
        }
    }
}
