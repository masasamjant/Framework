using Masasamjant.Repositories.Abstractions;

namespace Masasamjant.Repositories
{
    /// <summary>
    /// Represents component that provides asynchronous query execution capabilities
    /// by executing query synchronously.
    /// </summary>
    public sealed class DefaultAsyncQueryExecution : IAsyncQueryExecution
    {
        /// <summary>
        /// Execute query synchronously and returns the first result or default value if no result is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A first result or default, if no results.</returns>
        public Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            return Task.FromResult(query.FirstOrDefault());
        }

        /// <summary>
        /// Execute query synchronously and returns a single result or default value if no result is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A single result or default, it no results.</returns>
        public Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            return Task.FromResult(query.SingleOrDefault());
        }

        /// <summary>
        /// Execute query synchronously and returns the results as a list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of query results.</returns>
        public Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class
        {
            return Task.FromResult(query.ToList());
        }
    }
}
