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
            ValidateTransfer(source, target);

            if (source.Count == 0)
                return;

            while (source.TryDequeue(out var item))
                target.Enqueue(item);
        }

        /// <summary>
        /// Transfer all items from source <see cref="Queue{T}"/> to a new <see cref="Queue{T}"/>. After transfer the source
        /// <see cref="Queue{T}"/> will be empty.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <param name="target">The target collection.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="target"/> is null.</exception>
        public static void TransferTo<T>(this Queue<T> source, ICollection<T> target)
        {
            ValidateTransfer(source, target);
            
            if (source.Count == 0)
                return;
            
            while (source.TryDequeue(out var item))
                target.Add(item);
        }

        /// <summary>
        /// Transfer all items from source <see cref="Queue{T}"/> to a new <see cref="Stack{T}"/>. After transfer the source 
        /// <see cref="Queue{T}"/> will be empty.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <param name="target">The target stack.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="target"/> is null.</exception>
        public static void TransferTo<T>(this Queue<T> source, Stack<T> target)
        {
            ValidateTransfer(source, target);
            
            if (source.Count == 0)
                return;

            while (source.TryDequeue(out var item))
                target.Push(item);
        }

        /// <summary>
        /// Transfer items that match specified predicate from source <see cref="Queue{T}"/> to target <see cref="Queue{T}"/>. After transfer the source
        /// <see cref="Queue{T}"/> will only contain items that do not match the predicate and the target <see cref="Queue{T}"/> will contain items that match the predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <param name="target">The target queue.</param>
        /// <param name="transferSelector">The predicate to determine which items to transfer.</param>
        /// <exception cref="ArgumentNullException">If any of the arguments are <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If the target queue is the same as the source queue.</exception>
        public static void TransferTo<T>(this Queue<T> source, Queue<T> target, Func<T, bool> transferSelector)
        {
            ValidateTransfer(source, target);
            ArgumentNullException.ThrowIfNull(transferSelector);

            if (ReferenceEquals(source, target))
                throw new ArgumentException("Target queue cannot be the same as source queue.", nameof(target));

            if (source.Count == 0)
                return;

            var keepItems = new Queue<T>(source.Count);
            var transferItems = new Queue<T>(source.Count);

            while (source.TryDequeue(out var item))
            {
                if (transferSelector(item))
                    transferItems.Enqueue(item);
                else
                    keepItems.Enqueue(item);
            }

            if (keepItems.Count > 0)
                keepItems.TransferTo(source);

            if (transferItems.Count > 0)
                transferItems.TransferTo(target);
        }

        /// <summary>
        /// Transfer items that match specified predicate from source <see cref="Queue{T}"/> to target <see cref="ICollection{T}"/>. After transfer the source 
        /// <see cref="Queue{T}"/> will only contain items that do not match the predicate and the target <see cref="ICollection{T}"/> will contain items that match the predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <param name="target">The target collection.</param>
        /// <param name="transferSelector">The predicate to determine which items to transfer.</param>
        /// <exception cref="ArgumentNullException">If any of the arguments are <c>null</c>.</exception>
        public static void TransferTo<T>(this Queue<T> source, ICollection<T> target, Func<T, bool> transferSelector)
        {
            ValidateTransfer(source, target);
            ArgumentNullException.ThrowIfNull(transferSelector);

            if (source.Count == 0)
                return;
            
            var keepItems = new Queue<T>(source.Count);
            var transferItems = new Queue<T>(source.Count);
            
            while (source.TryDequeue(out var item))
            {
                if (transferSelector(item))
                    transferItems.Enqueue(item);
                else
                    keepItems.Enqueue(item);
            }
            
            if (keepItems.Count > 0)
                keepItems.TransferTo(source);
            
            if (transferItems.Count > 0)
                transferItems.TransferTo(target);
        }

        /// <summary>
        /// Transfer items that match specified predicate from source <see cref="Queue{T}"/> to target <see cref="Stack{T}"/>. After transfer the source 
        /// <see cref="Queue{T}"/> will only contain items that do not match the predicate and the target <see cref="Stack{T}"/> will contain items that match the predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <param name="target">The target stack.</param>
        /// <param name="transferSelector">The predicate to determine which items to transfer.</param>
        /// <param name="transferSelector">The predicate to determine which items to transfer.</param>
        /// <exception cref="ArgumentNullException">If any of the arguments are <c>null</c>.</exception>
        public static void TransferTo<T>(this Queue<T> source, Stack<T> target, Func<T, bool> transferSelector)
        {
            ValidateTransfer(source, target);
            ArgumentNullException.ThrowIfNull(transferSelector);

            if (source.Count == 0)
                return;

            var keepItems = new Queue<T>(source.Count);
            var transferItems = new Queue<T>(source.Count);

            while (source.TryDequeue(out var item))
            {
                if (transferSelector(item))
                    transferItems.Enqueue(item);
                else
                    keepItems.Enqueue(item);
            }

            if (keepItems.Count > 0)
                keepItems.TransferTo(source);

            if (transferItems.Count > 0)
                transferItems.TransferTo(target);
        }

        /// <summary>
        /// Creates new <see cref="Queue{T}"/> and transfers all items from the source queue to the new queue.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source queue.</param>
        /// <returns>A new <see cref="Queue{T}"/> containing all items from the source queue.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> is <c>null</c>.</exception>
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
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> is <c>null</c>.</exception>
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
        /// <exception cref="ArgumentNullException">If any of the arguments are <c>null</c>.</exception>
        public static IEnumerable<T> DequeueUntil<T>(this Queue<T> queue, Predicate<T> stopPredicate)
            => DequeueUntil(queue, new Func<T, bool>(x => stopPredicate(x)));

        /// <summary>
        /// Dequeue items from specified <see cref="Queue{T}"/> until first item meets specified stop predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to dequeue items.</param>
        /// <param name="stopPredicate">The stop predicate. If first item match this, then stops.</param>
        /// <returns>A items from <paramref name="queue"/> until first item match <paramref name="stopPredicate"/>.</returns>
        /// <exception cref="ArgumentNullException">If any of the arguments are <c>null</c>.</exception>
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
        /// <exception cref="ArgumentNullException">If any of the arguments are <c>null</c>.</exception>
        public static void EnqueueMatches<T>(this Queue<T> queue, IEnumerable<T> items, Predicate<T> enqueuePredicate)
            => EnqueueMatches(queue, items, new Func<T, bool>(x => enqueuePredicate(x)));

        /// <summary>
        /// Enqueue items that match specified predicate to <see cref="Queue{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The <see cref="Queue{T}"/> to enqueue items.</param>
        /// <param name="items">The all items.</param>
        /// <param name="pushPredicate">The predicate to match to enququed item.</param>
        /// <exception cref="ArgumentNullException">If any of the arguments are <c>null</c>.</exception>
        public static void EnqueueMatches<T>(this Queue<T> queue, IEnumerable<T> items, Func<T, bool> enqueuePredicate)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(enqueuePredicate);

            foreach (var item in items.Where(enqueuePredicate))
                queue.Enqueue(item);
        }

        /// <summary>
        /// Requeue items in specified <see cref="Queue{T}"/> by enqueueing specified items before or after items in queue.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The source <see cref="Queue{T}"/>.</param>
        /// <param name="enqueueBefore">Items to enqueue before the source queue items.</param>
        /// <param name="enqueueAfter">Items to enqueue after the source queue items.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> is <c>null</c>.</exception>
        public static void Requeue<T>(this Queue<T> queue, IEnumerable<T>? enqueueBefore, IEnumerable<T>? enqueueAfter)
        {
            ArgumentNullException.ThrowIfNull(queue);

            if ((enqueueBefore == null || !enqueueBefore.Any()) && (enqueueAfter == null || !enqueueAfter.Any()))
                return;

            var result = new Queue<T>(queue.Count + (enqueueBefore?.Count() ?? 0) + (enqueueAfter?.Count() ?? 0));

            if (enqueueBefore != null)
                result.EnqueueRange(enqueueBefore);

            while (queue.TryDequeue(out var item))
                result.Enqueue(item);

            if (enqueueAfter != null)
                result.EnqueueRange(enqueueAfter);

            result.TransferTo(queue);
        }

        /// <summary>
        /// Requeue items in specified <see cref="Queue{T}"/> by enqueuing specified items to specified positions.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The source <see cref="Queue{T}"/>.</param>
        /// <param name="enqueueItems">The dictionary containing item positions and values to enqueue.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> or <paramref name="enqueueItems"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If any of the positions in <paramref name="enqueueItems"/> is less than 1.</exception>
        public static void Requeue<T>(this Queue<T> queue, IDictionary<int, T> enqueueItems)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(enqueueItems);

            if (enqueueItems.Count == 0)
                return;

            if (queue.Count == 0)
            {
                var current = Create(enqueueItems);
                current.TransferTo(queue);
            }
            else
            {
                RequeueInsert(queue, enqueueItems);
            }
        }

        /// <summary>
        /// Requeue items in specified <see cref="Queue{T}"/> by inserting specified items to specified positions.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The source <see cref="Queue{T}"/>.</param>
        /// <param name="positionProvider">A function that provides the position for each item.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> or <paramref name="positionProvider"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// If any of the positions provided by <paramref name="positionProvider"/> is less than 1.
        /// -or-
        /// If <paramref name="positionProvider"/> provides same position for more than one item.
        /// </exception>
        public static void Requeue<T>(this Queue<T> queue, Func<T, int> positionProvider)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(positionProvider);

            if (queue.Count == 0)
                return;

            // Get items at their new positions.
            var positionItems = GetPositionItems(queue, positionProvider);

            var positions = positionItems.Keys.OrderBy(x => x).ToArray();

            queue.Clear();

            // Enqueue items at their new positions.
            foreach (var position in positions)
            {
                var item = positionItems[position];
                queue.Enqueue(item);
            }
        }

        /// <summary>
        /// Creates new <see cref="Queue{T}"/> from items specified in <see cref="IDictionary{int, T}"/> where 
        /// key is the position of item in the queue and value is the item itself.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="items">The dictionary containing item positions and values.</param>
        /// <returns>A new <see cref="Queue{T}"/> with the items in the specified order.</returns>
        /// <exception cref="ArgumentException">If any key in <paramref name="items"/> is less than 1.</exception>
        public static Queue<T> Create<T>(this IDictionary<int, T> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            var queue = new Queue<T>(items.Count);

            if (items.Count == 0)
                return queue;

            if (items.Keys.Any(k => k < 1))
                throw new ArgumentException("Item positions, a keys in dictionary, must be greater than or equal to 1.", nameof(items));

            foreach (var key in items.Keys.OrderBy(k => k))
                queue.Enqueue(items[key]);

            return queue;
        }

        /// <summary>
        /// Gets the position of item in the queue. 
        /// The position is determined by the order of items in the queue, where the first item has position 1, the second item has position 2, and so on.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The queue to search for the item.</param>
        /// <param name="item">The item to find the position of.</param>
        /// <returns>A position of item in queue; 0 if the item is not found.</returns>
        public static int GetPosition<T>(this Queue<T> queue, T item)
        {
            ArgumentNullException.ThrowIfNull(queue);
            
            var position = 0;
           
            foreach (var current in queue)
            {
                position++;

                if (EqualityComparer<T>.Default.Equals(current, item))
                    return position;
            }

            return 0;
        }

        /// <summary>
        /// Gets the items at their positions in the queue. If <paramref name="items"/> is <c>null</c>, all items in the queue are considered; 
        /// otherwise, only items in <paramref name="items"/> are considered.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The queue to search for the items.</param>
        /// <param name="items">The items to find the positions of.</param>
        /// <returns>A dictionary containing the positions and items.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> is <c>null</c>.</exception>
        public static IReadOnlyDictionary<int, T> GetPositions<T>(this Queue<T> queue, IEnumerable<T>? items = null)
        { 
            ArgumentNullException.ThrowIfNull(queue);

            var result = new Dictionary<int, T>();

            if (queue.Count == 0)
                return result.AsReadOnly();

            if (items == null)
            {
                ReadPositions(queue, result);
            }
            else
            {
                if (!items.Any())
                    return result.AsReadOnly();

                if (items.Count() == 1)
                {
                    var item = items.First();
                    int position = GetPosition(queue, item);
                    
                    if (position > 0)
                        result[position] = item;
                }
                else
                {
                    ReadPositions(queue, items, result);
                }
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Enqueue new item after specified position at the queue.
        /// 0 position means before the first item, 1 means after the first item, and so on.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The queue into which the item will be enqueued.</param>
        /// <param name="position">The position after which the item will be enqueued. 0 means at the beginning of the queue.</param>
        /// <param name="item">The item to be enqueued into the queue.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="position"/> is less than 0 or greater than the number of items in the queue.</exception>
        public static void EnqueueAfter<T>(this Queue<T> queue, int position, T item)
            => EnqueueAfter(queue, position, [item]);

        /// <summary>
        /// Enqueue new items after specified position at the queue. 
        /// 0 position means before the first item, 1 means after the first item and so on.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="queue">The queue into which the items will be enqueued.</param>
        /// <param name="position">The position after which the items will be enqueued. 0 means at the beginning of the queue.</param>
        /// <param name="items">The items to be enqueued into the queue.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="queue"/> or <paramref name="items"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="position"/> is less than 0 or greater than the number of items in the queue.</exception>
        public static void EnqueueAfter<T>(this Queue<T> queue, int position, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(queue);
            ArgumentNullException.ThrowIfNull(items);

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position), position, "Position must be non-negative.");

            if (!items.Any())
                return;

            var count = queue.Count;

            if (position > count)
                throw new ArgumentOutOfRangeException(nameof(position), position, $"Position cannot be greater than the number of items in the queue ({count}).");

            if (count == 0)
            {
                foreach (var item in items)
                    queue.Enqueue(item);
            }
            else
            {

                for (int x = 0; x < position; x++)
                    queue.Enqueue(queue.Dequeue());

                foreach (var item in items)
                    queue.Enqueue(item);

                int remaining = count - position;

                for (int x = 0; x < remaining; x++)
                    queue.Enqueue(queue.Dequeue());
            }
        }

        private static void ValidateTransfer<T>(Queue<T> source, ICollection<T> target)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);
        }

        private static void ValidateTransfer<T>(Queue<T> source, Stack<T> target)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);
        }

        private static void ValidateTransfer<T>(Queue<T> source, Queue<T> target)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(target);

            if (ReferenceEquals(source, target))
                throw new ArgumentException("Target cannot be the same as source queue.", nameof(target));
        }

        private static void RequeueInsert<T>(Queue<T> queue, IDictionary<int, T> enqueueItems)
        {
            if (enqueueItems.Keys.Any(k => k < 1))
                throw new ArgumentException("Item positions, a keys in dictionary, must be greater than or equal to 1.", nameof(enqueueItems));

            var positions = enqueueItems.Keys.OrderBy(k => k).ToList();
            var position = 1;
            var current = new Queue<T>();

            while (queue.TryDequeue(out var item))
            {
                while (enqueueItems.ContainsKey(position))
                {
                    positions.Remove(position);
                    var insertItem = enqueueItems[position];
                    current.Enqueue(insertItem);
                    position++;
                }

                current.Enqueue(item);
                position++;
            }

            // Fill remaining items.
            if (positions.Count > 0)
            {
                foreach (var pos in positions)
                {
                    var insertItem = enqueueItems[pos];
                    current.Enqueue(insertItem);
                }
            }

            current.TransferTo(queue);
        }

        private static void ReadPositions<T>(Queue<T> queue, Dictionary<int, T> result)
        {
            int position = 0;

            foreach (var item in queue)
            {
                position++;
                result[position] = item;
            }
        }

        private static void ReadPositions<T>(Queue<T> queue, IEnumerable<T> items, Dictionary<int, T> result)
        {
            int position = 0;

            foreach (var current in queue)
            {
                position++;
                foreach (var item in items)
                {
                    if (EqualityComparer<T>.Default.Equals(current, item))
                    {
                        result[position] = item;
                        break;
                    }
                }
            }
        }

        private static Dictionary<int, T> GetPositionItems<T>(Queue<T> queue, Func<T, int> positionProvider)
        {
            var positionItems = new Dictionary<int, T>();

            foreach (var item in queue)
            {
                var position = positionProvider(item);

                if (position < 1)
                    throw new InvalidOperationException("Position must be greater than or equal to 1.");

                if (positionItems.ContainsKey(position))
                    throw new InvalidOperationException($"Multiple items cannot have the same position. Position {position} is already assigned to another item.");

                positionItems.Add(position, item);
            }

            return positionItems;
        }
    }
}
