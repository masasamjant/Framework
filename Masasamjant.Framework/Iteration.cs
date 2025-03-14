namespace Masasamjant
{
    /// <summary>
    /// Represents iteration of item at indexed collection.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public sealed class Iteration<T> : IEquatable<Iteration<T>>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Iteration{T}"/> class.
        /// </summary>
        /// <param name="item">The iteration item.</param>
        /// <param name="index">The iteration index.</param>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="index"/> is less than 0.</exception>
        public Iteration(T item, int index)
        {
            if (index < 0)
                throw new ArgumentOutOfRangeException(nameof(index), index, "The index must be greater than or equal to 0.");

            Item = item;
            Index = index;
        }

        /// <summary>
        /// Gets the iteration item.
        /// </summary>
        public T Item { get; }

        /// <summary>
        /// Gets the iteration index.
        /// </summary>
        public int Index { get; }

        /// <summary>
        /// Check if other <see cref="Iteration{T}"/> is equal to this instance.
        /// </summary>
        /// <param name="other">The other <see cref="Iteration{T}"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this instance; <c>false</c> otherwise.</returns>
        public bool Equals(Iteration<T>? other)
        {
            return other != null &&
                Index == other.Index &&
                Equals(Item, other.Item);
        }

        /// <summary>
        /// Check if object instance is <see cref="Iteration{T}"/> and equal to this instance.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="Iteration{T}"/> and equal to this instance; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as Iteration<T>);
        }

        /// <summary>
        /// Gets hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(Item, Index);
        }

        /// <summary>
        /// Gets string presentation.
        /// </summary>
        /// <returns>A string presentation.</returns>
        public override string ToString()
        {
            return $"[{Index}] {Item}";
        }
    }
}
