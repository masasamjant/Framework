using Masasamjant.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents component that provides asynchronous query execution capabilities using Entity Framework.
    /// </summary>
    public class EntityAsyncQueryExecution : IAsyncQueryExecution
    {
        /// <summary>
        /// Execute query asynchronously and returns the first result or default value if no result is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A first result or default, if no results.</returns>
        public async Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            var result = await query.FirstOrDefaultAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// Execute query asynchronously and returns a single result or default value if no result is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A single result or default, it no results.</returns>
        public async Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            var result = await query.SingleOrDefaultAsync(cancellationToken);
            return result;
        }

        /// <summary>
        /// Execute query asynchronously and returns the results as a list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of query results.</returns>
        public async Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            var result = await query.ToListAsync(cancellationToken);
            return result;
        }
    }
}
