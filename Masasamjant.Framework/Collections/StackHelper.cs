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
            var result = new List<T>(stack.Count);

            while (stack.TryPop(out var item))
                result.Add(item);

            return result.AsReadOnly();
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
    }
}
