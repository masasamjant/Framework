using System.Linq.Expressions;
using System.Reflection;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Provides helper and extension methods to <see cref="IQueryable{T}"/> and <see cref="IOrderedQueryable{T}"/> interfaces.
    /// </summary>
    public static class QueryableHelper
    {
        /// <summary>
        /// Execute paging query to specified <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the queried instance.</typeparam>
        /// <param name="baseQuery">The base <see cref="IQueryable{T}"/>.</param>
        /// <param name="page">The <see cref="PageInfo"/>.</param>
        /// <param name="calculateTotalCount"><c>true</c> if calculates total item count in <paramref name="baseQuery"/>; <c>false</c> otherwise.</param>
        /// <returns>A paging <see cref="IQueryable{T}"/>.</returns>
        public static IQueryable<T> Page<T>(this IQueryable<T> baseQuery, PageInfo page, bool calculateTotalCount = false)
        {
            var query = baseQuery;

            if (calculateTotalCount)
                page.TotalCount = query.Count();

            query = query.Skip(page.Index * page.Size).Take(page.Size);

            return query;
        }

        /// <summary>
        /// Execute sorting query to specified <see cref="IQueryable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The type of the queried instance.</typeparam>
        /// <param name="baseQuery">The base <see cref="IQueryable{T}"/>.</param>
        /// <param name="descriptors">The sort descriptors.</param>
        /// <returns>A sorting <see cref="IQueryable{T}"/>.</returns>
        /// <exception cref="ArgumentException">If any descriptor at <paramref name="descriptors"/> contains empty or only white-space property name.</exception>
        /// <exception cref="InvalidOperationException">If <see cref="SortDescriptor"/> has name of public instance property that does not exist at <typeparamref name="T"/>.</exception>
        public static IQueryable<T> Sort<T>(this IQueryable<T> baseQuery, IEnumerable<SortDescriptor> descriptors)
        {
            var first = true;
            var query = baseQuery;

            foreach (var descriptor in descriptors)
            {
                if (string.IsNullOrWhiteSpace(descriptor.PropertyName))
                    throw new ArgumentException("The descriptor has empty or only white-space property name.", nameof(descriptors));

                if (descriptor.SortOrder == SortOrder.None)
                    continue;

                if (descriptor.SortOrder == SortOrder.Ascending)
                    query = CreateAscendingOrderQuery(query, first, descriptor);
                else
                    query = CreateDescendingOrderQuery(query, first, descriptor);

                first = false;
            }

            return query;
        }

        private static IQueryable<T> CreateAscendingOrderQuery<T>(IQueryable<T> query, bool first, SortDescriptor descriptor)
            => first ? OrderBy(query, descriptor.PropertyName) : ThenBy((IOrderedQueryable<T>)query, descriptor.PropertyName);

        private static IQueryable<T> CreateDescendingOrderQuery<T>(IQueryable<T> query, bool first, SortDescriptor descriptor)
            => first ? OrderByDescending(query, descriptor.PropertyName) : ThenByDescending((IOrderedQueryable<T>)query, descriptor.PropertyName);

        /// <summary>
        /// Execute ordering specified <see cref="IQueryable{T}"/> in ascending order using property specified by name.
        /// </summary>
        /// <typeparam name="T">The type of the queried instance.</typeparam>
        /// <param name="baseQuery">The base <see cref="IQueryable{T}"/>.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A <see cref="IOrderedQueryable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="propertyName"/> is empty or only white-space.</exception>
        /// <exception cref="InvalidOperationException">If <typeparamref name="T"/> does not have public instance property with name specified by <paramref name="propertyName"/>.</exception>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> baseQuery, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "The value cannot be empty or only white-space characters.");

            return OrderByMethod(baseQuery, SortMethod.OrderBy, propertyName);
        }

        /// <summary>
        /// Execute subsequent ordering specified <see cref="IOrderedQueryable{T}"/> in ascending order using property specified by name.
        /// </summary>
        /// <typeparam name="T">The type of the queried instance.</typeparam>
        /// <param name="baseQuery">The base <see cref="IOrderedQueryable{T}"/>.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A <see cref="IOrderedQueryable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="propertyName"/> is empty or only white-space.</exception>
        /// <exception cref="InvalidOperationException">If <typeparamref name="T"/> does not have public instance property with name specified by <paramref name="propertyName"/>.</exception>
        public static IOrderedQueryable<T> ThenBy<T>(this IOrderedQueryable<T> baseQuery, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "The value cannot be empty or only white-space characters.");

            return OrderByMethod(baseQuery, SortMethod.ThenBy, propertyName);
        }

        /// <summary>
        /// Execute ordering specified <see cref="IQueryable{T}"/> in descending order using property specified by name.
        /// </summary>
        /// <typeparam name="T">The type of the queried instance.</typeparam>
        /// <param name="baseQuery">The base <see cref="IQueryable{T}"/>.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A <see cref="IOrderedQueryable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="propertyName"/> is empty or only white-space.</exception>
        /// <exception cref="InvalidOperationException">If <typeparamref name="T"/> does not have public instance property with name specified by <paramref name="propertyName"/>.</exception>
        public static IOrderedQueryable<T> OrderByDescending<T>(this IQueryable<T> baseQuery, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "The value cannot be empty or only white-space characters.");

            return OrderByMethod(baseQuery, SortMethod.OrderByDescending, propertyName);
        }

        /// <summary>
        /// Execute subsequent ordering specified <see cref="IOrderedQueryable{T}"/> in descending order using property specified by name.
        /// </summary>
        /// <typeparam name="T">The type of the queried instance.</typeparam>
        /// <param name="baseQuery">The base <see cref="IOrderedQueryable{T}"/>.</param>
        /// <param name="propertyName">The property name.</param>
        /// <returns>A <see cref="IOrderedQueryable{T}"/>.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="propertyName"/> is empty or only white-space.</exception>
        /// <exception cref="InvalidOperationException">If <typeparamref name="T"/> does not have public instance property with name specified by <paramref name="propertyName"/>.</exception>
        public static IOrderedQueryable<T> ThenByDescending<T>(this IOrderedQueryable<T> baseQuery, string propertyName)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
                throw new ArgumentNullException(nameof(propertyName), "The value cannot be empty or only white-space characters.");

            return OrderByMethod(baseQuery, SortMethod.ThenByDescending, propertyName);
        }

        private enum SortMethod : int
        {
            OrderBy = 0,
            ThenBy = 1,
            OrderByDescending = 2,
            ThenByDescending = 3
        }

        private static IOrderedQueryable<T> OrderByMethod<T>(IQueryable<T> baseQuery, SortMethod method, string propertyName)
        {
            var property = typeof(T).GetProperty(propertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);

            if (property == null)
                throw new InvalidOperationException($"{typeof(T).FullName} does not have public instance property of {propertyName}.");

            IOrderedQueryable<T> query;
            var parameterExpression = Expression.Parameter(typeof(T), string.Empty);
            var propertyExpression = Expression.Property(parameterExpression, property);
            var lambdaExpression = Expression.Lambda<Func<T, object?>>(Expression.Convert(propertyExpression, typeof(object)), parameterExpression);

            switch (method)
            {
                case SortMethod.OrderBy:
                    query = baseQuery.OrderBy(lambdaExpression);
                    break;
                case SortMethod.ThenBy:
                    query = ((IOrderedQueryable<T>)baseQuery).ThenBy(lambdaExpression);
                    break;
                case SortMethod.OrderByDescending:
                    query = baseQuery.OrderByDescending(lambdaExpression);
                    break;
                case SortMethod.ThenByDescending:
                    query = ((IOrderedQueryable<T>)baseQuery).ThenByDescending(lambdaExpression);
                    break;
                default:
                    throw new NotSupportedException($"Sort method '{method}' is not supported.");
            }

            return query;
        }
    }
}
