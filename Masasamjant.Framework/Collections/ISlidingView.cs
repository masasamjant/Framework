namespace Masasamjant.Collections
{
    /// <summary>
    /// Represents sliding view.
    /// </summary>
    /// <typeparam name="T">The type of item.</typeparam>
    public interface ISlidingView<T>
    {
        /// <summary>
        /// Gets the items in view.
        /// </summary>
        IEnumerable<T> Items { get; }

        /// <summary>
        /// Gets the view size.
        /// </summary>
        int Size { get; }

        /// <summary>
        /// Slide view over next items.
        /// </summary>
        /// <returns><c>true</c> if new items was slide to view; <c>false</c> otherwise.</returns>
        bool Slide();
    }
}
