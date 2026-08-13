namespace Masasamjant.Collections
{
    /// <summary>
    /// Provides helper and extension methods to <see cref="Stack{T}"/>.
    /// </summary>
    public static class StackHelper
    {
        /// <summary>
        /// Execute specified <see cref="Action{T}"/> to each item popped from specified <see cref="Stack{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to get item.</param>
        /// <param name="action">The <see cref="Action{T}"/> to execute with item.</param>
        public static void ForEachPop<T>(this Stack<T> stack, Action<T> action)
        {
            ArgumentNullException.ThrowIfNull(stack);
            ArgumentNullException.ThrowIfNull(action);

            while (stack.TryPop(out var item))
                action(item);
        }

        /// <summary>
        /// Push range of items to specified <see cref="IStack{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="IStack{T}"/> to push item.</param>
        /// <param name="items">The <see cref="IEnumerable{T}"/> of items.</param>
        public static void PushRange<T>(this Stack<T> stack, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(stack);
            ArgumentNullException.ThrowIfNull(items);

            if (ReferenceEquals(stack, items))
                return;

            foreach (var item in items)
                stack.Push(item);
        }

        /// <summary>
        /// Pop range of items from specified <see cref="Stack{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to get items.</param>
        /// <param name="count">The max count of items to get.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of items.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="count"/> is less than 0.</exception>
        public static IEnumerable<T> PopRange<T>(this Stack<T> stack, int count)
        {
            ArgumentNullException.ThrowIfNull(stack);

            if (count < 0)
                throw new ArgumentOutOfRangeException(nameof(count), count, "The value must be greater than or equal to 0.");

            var result = new List<T>(count);

            if (count == 0 || stack.Count == 0)
                return result.AsReadOnly();

            while (result.Count < count && stack.TryPop(out var item))
                result.Add(item);

            return result.AsReadOnly();
        }

        /// <summary>
        /// Pop all items from specified <see cref="Stack{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to get items.</param>
        /// <returns>A <see cref="IEnumerable{T}"/> of items.</returns>
        public static IEnumerable<T> PopRange<T>(this Stack<T> stack)
        {
            ArgumentNullException.ThrowIfNull(stack);

            var result = new List<T>(stack.Count);

            while (stack.TryPop(out var item))
                result.Add(item);

            return result.AsReadOnly();
        }

        /// <summary>
        /// Pop items from specified <see cref="Stack{T}"/> until top item is specified stop item.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to pop items.</param>
        /// <param name="stopItem">The top item to stop pop.</param>
        /// <returns>A items from <paramref name="stack"/> until <paramref name="stopItem"/> is top item.</returns>
        public static IEnumerable<T> PopUntil<T>(this Stack<T> stack, T stopItem)
        {
            ArgumentNullException.ThrowIfNull(stack);
            Predicate<T> stopPredicate = (item) => Equals(stopItem, item);
            return PopUntil(stack, stopPredicate);
        }

        /// <summary>
        /// Pop items from specified <see cref="Stack{T}"/> until top item meets specified stop predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to pop items.</param>
        /// <param name="stopPredicate">The stop predicate. If top item match this, then stops.</param>
        /// <returns>A items from <paramref name="stack"/> until top item match <paramref name="stopPredicate"/>.</returns>
        public static IEnumerable<T> PopUntil<T>(this Stack<T> stack, Predicate<T> stopPredicate)
            => PopUntil(stack, new Func<T, bool>(x => stopPredicate(x)));

        /// <summary>
        /// Pop items from specified <see cref="Stack{T}"/> until top item meets specified stop predicate.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to pop items.</param>
        /// <param name="stopPredicate">The stop predicate. If top item match this, then stops.</param>
        /// <returns>A items from <paramref name="stack"/> until top item match <paramref name="stopPredicate"/>.</returns>
        public static IEnumerable<T> PopUntil<T>(this Stack<T> stack, Func<T, bool> stopPredicate)
        {
            ArgumentNullException.ThrowIfNull(stack);
            ArgumentNullException.ThrowIfNull(stopPredicate);

            var result = new List<T>(stack.Count);

            while (stack.TryPeek(out var top))
            {
                if (stopPredicate(top))
                    break;

                top = stack.Pop();
                result.Add(top);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Push items that match specified predicate to <see cref="Stack{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to push items.</param>
        /// <param name="items">The all items.</param>
        /// <param name="pushPredicate">The predicate to match to pushed item.</param>
        public static void PushMatches<T>(this Stack<T> stack, IEnumerable<T> items, Predicate<T> pushPredicate)
            => PushMatches(stack, items, new Func<T, bool>(x => pushPredicate(x)));

        /// <summary>
        /// Push items that match specified predicate to <see cref="Stack{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to push items.</param>
        /// <param name="items">The all items.</param>
        /// <param name="pushPredicate">The predicate to match to pushed item.</param>
        public static void PushMatches<T>(this Stack<T> stack, IEnumerable<T> items, Func<T, bool> pushPredicate)
        {
            ArgumentNullException.ThrowIfNull(stack);
            ArgumentNullException.ThrowIfNull(items);
            ArgumentNullException.ThrowIfNull(pushPredicate);

            foreach (var item in items.Where(pushPredicate))
                stack.Push(item);
        }

        /// <summary>
        /// Split specified <see cref="Stack{T}"/> to several stacks. After split the original 
        /// <see cref="Stack{T}"/> is empty.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The <see cref="Stack{T}"/> to split.</param>
        /// <param name="size">The target size of each stack.</param>
        /// <returns>A slit stacks.</returns>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="size"/> is less than 1.</exception>
        public static IEnumerable<Stack<T>> Split<T>(this Stack<T> stack, int size)
        {
            ArgumentNullException.ThrowIfNull(stack);

            if (size < 1)
                throw new ArgumentOutOfRangeException(nameof(size), size, "The value must be greater than 0.");

            var result = new List<Stack<T>>();

            if (stack.Count == 0)
                return result.AsReadOnly();

            if (stack.Count < size)
            {
                var list = new List<T>(stack.Count);
                while (stack.TryPop(out var item))
                    list.Add(item);
                list.Reverse();
                var copy = new Stack<T>(stack.Count);
                copy.PushRange(list);
                result.Add(copy);
            }
            else
            {
                var current = new Stack<T>();

                while (stack.Count > 0)
                {
                    current.Push(stack.Pop());

                    if (current.Count == size)
                    {
                        result.Add(current);
                        current = new Stack<T>();
                    }
                }

                if (current.Count > 0 && !result.Contains(current))
                    result.Add(current);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Clone specified <see cref="Stack{T}"/>. 
        /// After clone the original <see cref="Stack{T}"/> is unchanged.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The stack to clone.</param>
        /// <returns>A new stack that is a clone of the original stack.</returns>
        public static Stack<T> Clone<T>(this Stack<T> stack)
        {
            ArgumentNullException.ThrowIfNull(stack);

            var result = new Stack<T>(stack.Count);
            var items = new List<T>(stack.Count);

            while (stack.TryPop(out var item))
                items.Add(item);

            items.Reverse();

            result.PushRange(items);
            stack.PushRange(items);

            return result;
        }

        /// <summary>
        /// Transfer items from source stack to destination stack. After transfer the source stack is empty 
        /// and the destination stack contains all items of source stack on top of its original items.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source stack.</param>
        /// <param name="destination">The destination stack.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="destination"/> is <c>null</c>.</exception>
        public static void TransferTo<T>(this Stack<T> source, Stack<T> destination)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            if (source.Count == 0)
                return;

            var items = new List<T>(source.Count);

            while (source.TryPop(out var item))
                items.Add(item);

            items.Reverse();

            destination.PushRange(items);
        }

        /// <summary>
        /// Transfer items that match specified predicate from source stack to destination stack. After transfer the source stack contains only 
        /// items that did not match selector. The destination stack contains all items of source stack that match selector on top of its original items.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source stack.</param>
        /// <param name="destination">The destination stack.</param>
        /// <param name="transferSelector">The predicate to determine which items to transfer.</param>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public static void TransferTo<T>(this Stack<T> source, Stack<T> destination, Func<T, bool> transferSelector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentNullException.ThrowIfNull(transferSelector);

            if (source.Count == 0)
                return;

            var keepItems = new List<T>(source.Count);
            var transferItems = new List<T>(source.Count);

            while (source.TryPop(out var item))
            {
                if (transferSelector(item))
                    transferItems.Add(item);
                else
                    keepItems.Add(item);
            }

            if (keepItems.Count > 0)
            {
                keepItems.Reverse();
                transferItems.Reverse();
            }

            if (transferItems.Count > 0)
            {
                source.PushRange(keepItems);
                destination.PushRange(transferItems);
            }
        }

        /// <summary>
        /// Transfer items from source stack to destination collection. After transfer the source stack is empty. 
        /// Items are added to collection in order of pop from stack.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source stack.</param>
        /// <param name="destination">The destination collection.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="source"/> or <paramref name="destination"/> is <c>null</c>.</exception>
        public static void TransferTo<T>(this Stack<T> source, ICollection<T> destination)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            if (source.Count == 0)
                return;

            while (source.TryPop(out var item))
                destination.Add(item);
        }

        /// <summary>
        /// Transfer items that match specified predicate from source stack to destination collection. After transfer the source stack contains only 
        /// items that did not match selector. The destination collection contains all items of source stack that match selector.
        /// Items are added to collection in order of pop from stack.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source stack.</param>
        /// <param name="destination">The destination collection.</param>
        /// <param name="transferSelector">The predicate to determine which items to transfer.</param>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public static void TransferTo<T>(this Stack<T> source, ICollection<T> destination, Func<T, bool> transferSelector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentNullException.ThrowIfNull(transferSelector);

            if (source.Count == 0)
                return;

            var keepItems = new List<T>(source.Count);

            while (source.TryPop(out var item))
            {
                if (transferSelector(item))
                    destination.Add(item);
                else
                    keepItems.Add(item);
            }

            if (keepItems.Count > 0)
            {
                keepItems.Reverse();
                source.PushRange(keepItems);
            }
        }

        /// <summary>
        /// Transfer items from source stack to destination queue. After transfer the source stack is empty 
        /// and the destination queue contains all items of source stack at the end of its original items. 
        /// Items are enqueued to queue in order of pop from stack.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source stack.</param>
        /// <param name="destination">The destination queue.</param>
        public static void TransferTo<T>(this Stack<T> source, Queue<T> destination)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);

            if (source.Count == 0)
                return;

            while (source.TryPop(out var item))
                destination.Enqueue(item);
        }

        /// <summary>
        /// Transfer items that match specified predicate from source stack to destination queue. After transfer the source stack contains only
        /// items that did not match selector. The destination queue contains all items of source stack that match selector at the end of its original items.
        /// Items are enqueued to queue in order of pop from stack.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source stack.</param>
        /// <param name="destination">The destination queue.</param>
        /// <param name="transferSelector">The predicate to determine which items to transfer.</param>
        /// <exception cref="ArgumentNullException">If any of the parameters is <c>null</c>.</exception>
        public static void TransferTo<T>(this Stack<T> source, Queue<T> destination, Func<T, bool> transferSelector)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(destination);
            ArgumentNullException.ThrowIfNull(transferSelector);

            if (source.Count == 0)
                return;

            var keepItems = new List<T>(source.Count);

            while (source.TryPop(out var item))
            {
                if (transferSelector(item))
                    destination.Enqueue(item);
                else
                    keepItems.Add(item);
            }
            if (keepItems.Count > 0)
            {
                keepItems.Reverse();
                source.PushRange(keepItems);
            }
        }

        /// <summary>
        /// Restack specified <see cref="Stack{T}"/> with items to push before and after. 
        /// After restack the original <see cref="Stack{T}"/> is empty.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The current stack.</param>
        /// <param name="pushBefore">The items to push before the current stack items.</param>
        /// <param name="pushAfter">The items to push after the current stack items.</param>
        /// <returns>A new stack with the specified items pushed before and after the current stack.</returns>
        public static void Restack<T>(this Stack<T> stack, IEnumerable<T>? pushBefore = null, IEnumerable<T>? pushAfter = null)
        {
            ArgumentNullException.ThrowIfNull(stack);

            if ((pushBefore == null || !pushBefore.Any()) && (pushAfter == null || !pushAfter.Any()))
                return;

            var queue = new Queue<T>(stack.Count + (pushBefore?.Count() ?? 0) + (pushAfter?.Count() ?? 0));

            if (pushBefore != null)
                queue.EnqueueRange(pushBefore);

            var items = new List<T>(stack.Count);

            while (stack.TryPop(out var item))
                items.Add(item);

            items.Reverse();
            queue.EnqueueRange(items);

            if (pushAfter != null)
                queue.EnqueueRange(pushAfter);

            while (queue.TryDequeue(out var item))
                stack.Push(item);
        }

        /// <summary>
        /// Restack specified <see cref="Stack{T}"/> with items to push at specified positions.
        /// </summary>
        /// <typeparam name="T">The type of the items in the stack.</typeparam>
        /// <param name="stack">The stack to restack.</param>
        /// <param name="pushItems">A dictionary containing the items to push and their positions.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="stack"/> or <paramref name="pushItems"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If any key in the <paramref name="pushItems"/> dictionary is less than 1.</exception>
        public static void Restack<T>(this Stack<T> stack, IDictionary<int, T> pushItems)
        {
            ArgumentNullException.ThrowIfNull(stack);
            ArgumentNullException.ThrowIfNull(pushItems);

            if (pushItems.Count == 0)
                return;

            if (stack.Count == 0)
            {
                var current = Create(pushItems);
                current.TransferTo(stack);
            }
            else 
            {
                PushInsert(stack, pushItems);
            }
        }

        /// <summary>
        /// Restack specified <see cref="Stack{T}"/> with current items palaced to new positions provided by <paramref name="positionProvider"/>.
        /// </summary>
        /// <typeparam name="T">The type of the items in the stack.</typeparam>
        /// <param name="stack">The stack to restack.</param>
        /// <param name="positionProvider">A function that provides the new position for each item in the stack.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="stack"/> or <paramref name="positionProvider"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// If the position provided by <paramref name="positionProvider"/> is less than 1.
        /// -or-
        /// If <paramref name="positionProvider"/> returns duplicate positions for different items in the stack.
        /// </exception>
        public static void Restack<T>(this Stack<T> stack, Func<T, int> positionProvider)
        {
            ArgumentNullException.ThrowIfNull(stack);
            ArgumentNullException.ThrowIfNull(positionProvider);

            if (stack.Count == 0)
                return;

            var positionItems = GetPositionItems(stack, positionProvider);
            var positions = positionItems.Keys.OrderByDescending(k => k).ToArray();

            stack.Clear();

            foreach (var position in positions)
            {
                var item = positionItems[position];
                stack.Push(item);
            }
        }

        /// <summary>
        /// Create a stack with specified items. Items and their positions are specified in <paramref name="pushItems"/>.
        /// The key of <paramref name="pushItems"/> is the position of the item in the stack, where smallest key is the top of the stack.
        /// </summary>
        /// <typeparam name="T">The type of the items in the stack.</typeparam>
        /// <param name="items">The items to create the stack with.</param>
        /// <returns>A stack containing the specified items.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="items"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If any key in the <paramref name="items"/> dictionary is less than 1.</exception>
        public static Stack<T> Create<T>(IDictionary<int, T> items)
        {
            ArgumentNullException.ThrowIfNull(items);

            var stack = new Stack<T>(items.Count);

            if (items.Count == 0)
                return stack;

            if (items.Keys.Any(k => k < 1))
                throw new ArgumentException("Item positions, a key in dictionary, must be greater than or equal to 1.", nameof(items));

            foreach (var key in items.Keys.OrderByDescending(k => k))
                stack.Push(items[key]);

            return stack;
        }

        /// <summary>
        /// Gets the items at their positions in the stack without modifying the stack. The items are returned in order of pop from stack. 
        /// If <paramref name="items"/> is <c>null</c>, all items in the stack are returned; otherwise, only positions of specified items are returned.
        /// </summary>
        /// <typeparam name="T">The type of the items in the stack.</typeparam>
        /// <param name="stack">The stack to get positions from.</param>
        /// <param name="items">The items to get positions for.</param>
        /// <returns>A read-only dictionary mapping positions to items.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="stack"/> is <c>null</c>.</exception>
        public static IReadOnlyDictionary<int, T> GetPositions<T>(this Stack<T> stack, IEnumerable<T>? items = null)
        {
            ArgumentNullException.ThrowIfNull(stack);

            var result = new Dictionary<int, T>();

            if (stack.Count == 0)
                return result.AsReadOnly();

            if (items == null)
            {
                ReadPositions(stack, result);
            }
            else
            {
                if (!items.Any())
                    return result.AsReadOnly();

                if (items.Count() == 1)
                {
                    var item = items.First();
                    int position = GetPosition(stack, item);
                    if (position > 0)
                        result[position] = item;
                }
                else
                {
                    ReadPositions(stack, items, result);
                }
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Gets the position of item in the stack. 
        /// The position is determined by the order of pop from stack. The top item has position 1, the second item has position 2, and so on.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The stack to search.</param>
        /// <param name="item">The item to find.</param>
        /// <returns>The position of the item in the stack, or 0 if not found.</returns>
        public static int GetPosition<T>(this Stack<T> stack, T item)
        {
            ArgumentNullException.ThrowIfNull(stack);

            var position = 0;

            foreach (var current in stack)
            {
                position++;

                if (EqualityComparer<T>.Default.Equals(current, item))
                    return position;
            }

            return 0;
        }

        /// <summary>
        /// Push new item after specified position in the stack.
        /// 0 position means push to the top of the stack, 1 means push after the top item, and so on.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The stack into which the item will be pushed.</param>
        /// <param name="position">The position after which the item will be pushed. 0 means at the top of the stack.</param>
        /// <param name="item">The item to be pushed into the stack.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="position"/> is less than 0 or greater than the number of items in the stack.</exception>
        public static void PushAfter<T>(this Stack<T> stack, int position, T item)
            => PushAfter(stack, position, [item]);

        /// <summary>
        /// Push new items after specified position in the stack.
        /// 0 position means push as top item, 1 means after the top item, and so on.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="stack">The stack into which the items will be pushed.</param>
        /// <param name="position">The position after which the items will be pushed. 0 means at the top of the stack.</param>
        /// <param name="items">The items to be pushed into the stack.</param>
        /// <exception cref="ArgumentOutOfRangeException">If <paramref name="position"/> is less than 0 or greater than the number of items in the stack.</exception>
        public static void PushAfter<T>(this Stack<T> stack, int position, IEnumerable<T> items)
        {
            ArgumentNullException.ThrowIfNull(stack);
            ArgumentNullException.ThrowIfNull(items);

            if (position < 0)
                throw new ArgumentOutOfRangeException(nameof(position), position, "Position must be non-negative.");

            if (!items.Any())
                return;

            var count = stack.Count;

            if (position > count)
                throw new ArgumentOutOfRangeException(nameof(position), position, $"Position must be less than or equal to the stack count ({count}).");

            if (count == 0)
            {
                foreach (var item in items)
                    stack.Push(item);
            }
            else
            {
                var list = new List<T>();

                while (stack.TryPop(out var item))
                    list.Add(item);

                list.Reverse();

                int pos = 0;

                foreach (var item in list)
                {
                    stack.Push(item);
                    
                    pos++;

                    if (pos == position)
                    {
                        foreach (var newItem in items)
                            stack.Push(newItem);
                    }
                }
            }
        }

        private static void ReadPositions<T>(Stack<T> stack, Dictionary<int, T> result)
        {
            int position = 0;

            foreach (var item in stack)
            {
                position++;
                result[position] = item;
            }
        }

        private static void ReadPositions<T>(Stack<T> stack, IEnumerable<T> items, Dictionary<int, T> result)
        {
            int position = 0;

            foreach (var current in stack)
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

        private static void PushInsert<T>(Stack<T> stack, IDictionary<int, T> pushItems)
        {
            if (pushItems.Keys.Any(k => k < 1))
                throw new ArgumentException("Item positions, a keys in dictionary, must be greater than or equal to 1.", nameof(pushItems));

            var positions = pushItems.Keys.OrderByDescending(k => k).ToList();
            var position = 0;
            var list = new List<T>(stack.Count + pushItems.Count);

            while (stack.TryPop(out var item))
            {
                position++;

                while (pushItems.ContainsKey(position))
                {
                    positions.Remove(position);
                    var pushItem = pushItems[position];
                    list.Add(pushItem);
                    position++;
                }

                list.Add(item);
            }

            // Fill remaining items.
            if (positions.Count > 0)
            {
                foreach (var pos in positions)
                {
                    var pushItem = pushItems[pos];
                    list.Add(pushItem);
                }
            }

            // Transfer items back to stack.
            list.Reverse();
            stack.PushRange(list);
        }

        private static Dictionary<int, T> GetPositionItems<T>(Stack<T> stack, Func<T, int> positionProvider)
        {
            var positionItems = new Dictionary<int, T>();

            foreach (var item in stack)
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
