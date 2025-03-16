using System.Collections;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Provides helper methods to create <see cref="CopyEnumerable{T}"/>.
    /// </summary>
    public static class CopyEnumerable
    {
        /// <summary>
        /// Creates new <see cref="CopyEnumerable{T}"/> from specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the item.</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <returns>A <see cref="CopyEnumerable{T}"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="source"/> is <see cref="CopyEnumerable{T}"/>.</exception>
        public static CopyEnumerable<T> CreateCopyEnumerable<T>(this IEnumerable<T> source)
        {
            return new CopyEnumerable<T>(source);
        }
    }

    /// <summary>
    /// Represents <see cref="IEnumerable{T}"/> that creates <see cref="CopyEnumerator{T}"/> from specified source <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the item.</typeparam>
    public sealed class CopyEnumerable<T> : IEnumerable<T>
    {
        private readonly IEnumerable<T> source;

        /// <summary>
        /// Initializes new instance of the <see cref="CopyEnumerable{T}"/> class.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <exception cref="ArgumentException">If <paramref name="source"/> is <see cref="CopyEnumerable{T}"/>.</exception>
        public CopyEnumerable(IEnumerable<T> source)
        {
            if (source is CopyEnumerable<T>)
                throw new ArgumentException("The source cannot be copy enumerable.", nameof(source));

            this.source = source;
        }

        /// <summary>
        /// Gets the <see cref="CopyEnumerator{T}"/> to iterate source.
        /// </summary>
        /// <returns>A <see cref="CopyEnumerator{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            return new CopyEnumerator<T>(source);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
