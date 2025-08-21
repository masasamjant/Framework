using System.Linq.Expressions;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents specification of an query.
    /// </summary>
    /// <typeparam name="T">The type of the queried object.</typeparam>
    public sealed class Query<T> : IQuery<T>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Query{T}"/> class with specified criteria.
        /// </summary>
        /// <param name="criteria">The criteria expression.</param>
        public Query(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="Query{T}"/> class that 
        /// use criteria build using specified <see cref="IPredicateBuilder{T}"/>.
        /// </summary>
        /// <param name="predicateBuilder">The <see cref="IPredicateBuilder{T}"/>.</param>
        public Query(IPredicateBuilder<T> predicateBuilder)
        {
            Criteria = predicateBuilder.Build();
        }

        /// <summary>
        /// Gets the expression of query criteria.
        /// </summary>
        public Expression<Func<T, bool>> Criteria { get; }
    }
}
