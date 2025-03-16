using System.Collections;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents <see cref="IEnumerator{T}"/> that creates copy from specified <see cref="IEnumerable{T}"/> for iteration. 
    /// The enumerator is snapshot from source when initialized or resetted. While iterating this, it is possible to alter source.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public sealed class CopyEnumerator<T> : IEnumerator<T>
    {
        private IEnumerator<T> enumerator;
        private readonly List<T> list;
        private readonly IEnumerable<T> source;

        /// <summary>
        /// Initializes new instance of the <see cref="CopyEnumerator{T}"/> class.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <exception cref="ArgumentException">If <paramref name="source"/> is <see cref="CopyEnumerable{T}"/>.</exception>
        public CopyEnumerator(IEnumerable<T> source)
        {
            if (source is CopyEnumerable<T>)
                throw new ArgumentException("The source cannot be copy enumerable.", nameof(source));

            this.source = source;
            this.list = source.ToList();
            enumerator = list.GetEnumerator();
        }

        /// <summary>
        /// Gets the element in the collection at the current position.
        /// </summary>
        public T Current
        {
            get { return enumerator.Current; }
        }

        /// <summary>
        /// Disposes current enumerator.
        /// </summary>
        public void Dispose()
        {
            enumerator.Dispose();
        }

        /// <summary>
        /// Advances enumerator to next element.
        /// </summary>
        /// <returns><c>true</c> if moved to next element; <c>false</c> otherwise.</returns>
        public bool MoveNext()
        {
            return enumerator.MoveNext();
        }

        /// <summary>
        /// Resets the enumerator by reading source enumerable again.
        /// </summary>
        public void Reset()
        {
            list.Clear();
            list.AddRange(source);
            enumerator = list.GetEnumerator();
        }

        object? IEnumerator.Current
        {
            get { return this.Current; }
        }
    }
}
