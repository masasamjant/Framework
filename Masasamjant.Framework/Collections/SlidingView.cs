using System.Collections;

namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents sliding view over <see cref="IEnumerable{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    public sealed class SlidingView<T> : ISlidingView<T>, IEnumerable<T>
    {
        private readonly IEnumerator<T> enumerator;
        private readonly List<T> items;

        /// <summary>
        /// Initializes new instance of the <see cref="SlidingView{T}"/> class.
        /// </summary>
        /// <param name="source">The source <see cref="IEnumerable{T}"/>.</param>
        /// <param name="size">The view size.</param>
        /// <exception cref="ArgumentException">If <paramref name="source"/> is <see cref="SlidingView{T}"/>.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="size"/> is less than 0.</exception>
        public SlidingView(IEnumerable<T> source, int size)
        {
            if (source is SlidingView<T>)
                throw new ArgumentException("The source cannot be sliding view.", nameof(source));

            if (size < 0)
                throw new ArgumentOutOfRangeException(nameof(size), size, "The size must be greater than or equal to 0.");

            this.enumerator = source.GetEnumerator();
            this.items = new List<T>(Math.Max(1, size));
            this.Size = size;
        }

        /// <summary>
        /// Gets the items in view.
        /// </summary>
        public IEnumerable<T> Items
        {
            get
            {
                foreach (var item in items)
                    yield return item;  
            }
        }

        /// <summary>
        /// Gets the view size.
        /// </summary>
        public int Size { get; }

        /// <summary>
        /// Slide view over next items.
        /// </summary>
        /// <returns><c>true</c> if new items was slide to view; <c>false</c> otherwise.</returns>
        public bool Slide()
        {
            items.Clear();

            while (items.Count < Size && enumerator.MoveNext())
                items.Add(enumerator.Current);

            return items.Count > 0;
        }

        /// <summary>
        /// Reset view to start.
        /// </summary>
        public void Reset()
        {
            enumerator.Reset();
            items.Clear();
        }

        /// <summary>
        /// Gets the <see cref="IEnumerator{T}"/> to get all items.
        /// </summary>
        /// <returns>A <see cref="IEnumerator{T}"/>.</returns>
        public IEnumerator<T> GetEnumerator()
            => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        private class Enumerator : IEnumerator<T> 
        {
            private readonly SlidingView<T> view;
            private T? current;
            private IEnumerator<T>? items;

            public Enumerator(SlidingView<T> view)
            {
                this.view = view;
                this.view.Reset();
            }

            public T Current
            {
                get
                {
                    if (current == null)
                        throw new InvalidOperationException("Enumerator not moved to item.");

                    return current;
                }
            }

            public void Dispose()
            {
                return;
            }

            public bool MoveNext()
            {
                if (items == null)
                {
                    if (view.Slide())
                        items = view.Items.GetEnumerator();
                    else
                        return false;
                }

                return MoveNext(items);
            }

            public void Reset()
            {
                view.Reset();
                items = null;
            }

            private bool MoveNext(IEnumerator<T> enumerator)
            {
                if (enumerator.MoveNext())
                {
                    current = enumerator.Current;
                    return true;
                }
                else
                {
                    if (view.Slide())
                    {
                        items = view.Items.GetEnumerator();
                        return MoveNext(items);
                    }
                }

                return false;
            }

            object IEnumerator.Current
            {
                get { return this.Current ?? throw new InvalidOperationException("Enumerator not moved to item."); }
            }
        }
    }
}
