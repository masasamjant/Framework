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
            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size), size, "The value must be greater than 0.");

            var result = new List<Queue<T>>();

            if (queue.Count == 0)
                return result.AsReadOnly();

            if (queue.Count < size)
            {
                var copy = new Queue<T>();
                while (queue.TryDequeue(out var item))
                    copy.Enqueue(item);
                result.Add(copy);
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
    }
}
