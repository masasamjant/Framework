namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model that can be update from <typeparamref name="TSource"/> model.
    /// </summary>
    /// <typeparam name="TSource">The type of the source model.</typeparam>
    public interface IUpdateable<TSource> : IModel 
        where TSource : IModel
    {
        /// <summary>
        /// Check if this instance can be update from specified <typeparamref name="TSource"/> instance.
        /// </summary>
        /// <param name="source">The source instance.</param>
        /// <returns><c>true</c> if can update this instance from <paramref name="source"/>; <c>false</c> otherwise.</returns>
        bool CanUpdateFrom(TSource source);

        /// <summary>
        /// Update this instance from specified <typeparamref name="TSource"/> instance. Before update the 
        /// <see cref="CanUpdateFrom(TSource)"/> should be used to check that update is possible.
        /// </summary>
        /// <param name="source">The source instance.</param>
        /// <exception cref="ArgumentException">If cannot update from <paramref name="source"/>.</exception>
        void UpdateFrom(TSource source);
    }
}
