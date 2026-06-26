using Microsoft.AspNetCore.Mvc.Filters;

namespace Masasamjant.Web.Mvc.Filters
{
    /// <summary>
    /// Provides helper methods to <see cref="FilterContext"/> class.
    /// </summary>
    public static class FilterContextHelper
    {
        /// <summary>
        /// Check if <see cref="FilterContext.Filters"/> contains <typeparamref name="TFilter"/> filter.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="context">The filter context.</param>
        /// <returns><c>true</c> if the filter is present; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="context"/> is <c>null</c>.</exception>
        public static bool ContainsFilter<TFilter>(this FilterContext context) where TFilter : IFilterMetadata
        {
            ArgumentNullException.ThrowIfNull(context);
            return context.Filters.OfType<TFilter>().Any();
        }

        /// <summary>
        /// Gets filters of <typeparamref name="TFilter"/> from <see cref="FilterContext.Filters"/>.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="context">The filter context.</param>
        /// <returns>A read-only collection of filters of type <typeparamref name="TFilter"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="context"/> is <c>null</c>.</exception>   
        public static IReadOnlyCollection<TFilter> GetFilters<TFilter>(this FilterContext context) where TFilter : IFilterMetadata
        {
            ArgumentNullException.ThrowIfNull(context);
            return context.Filters.OfType<TFilter>().ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the first filter of <typeparamref name="TFilter"/> from <see cref="FilterContext.Filters"/>.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="context">The filter context.</param>
        /// <returns>The first filter of type <typeparamref name="TFilter"/>, or <c>null</c> if none is found.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="context"/> is <c>null</c>.</exception>
        public static TFilter? GetFirstFilter<TFilter>(this FilterContext context) where TFilter : IFilterMetadata
            => GetFilters<TFilter>(context).FirstOrDefault();

        /// <summary>
        /// Gets the single filter of <typeparamref name="TFilter"/> from <see cref="FilterContext.Filters"/>.
        /// </summary>
        /// <typeparam name="TFilter">The type of the filter.</typeparam>
        /// <param name="context">The filter context.</param>
        /// <returns>The single filter of type <typeparamref name="TFilter"/>, or <c>null</c> if none is found.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="context"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">If more than one filter of type <typeparamref name="TFilter"/> is found.</exception>
        public static TFilter? GetSingleFilter<TFilter>(this FilterContext context) where TFilter : IFilterMetadata
            => GetFilters<TFilter>(context).SingleOrDefault();
    }
}
