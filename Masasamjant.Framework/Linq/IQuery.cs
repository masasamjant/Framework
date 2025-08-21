using System.Linq.Expressions;

namespace Masasamjant.Linq
{
    /// <summary>
    /// Represents specification of an query.
    /// </summary>
    /// <typeparam name="T">The type of the queried object.</typeparam>
    public interface IQuery<T>
    {
        /// <summary>
        /// Gets the expression of query criteria.
        /// </summary>
        Expression<Func<T, bool>> Criteria { get; }
    }
}
