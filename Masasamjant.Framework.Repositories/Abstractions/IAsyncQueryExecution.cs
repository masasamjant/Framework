namespace Masasamjant.Repositories.Abstractions
{
    /// <summary>
    /// Represents component that provides asynchronous query execution capabilities.
    /// </summary>
    public interface IAsyncQueryExecution
    {
        /// <summary>
        /// Execute query asynchronously and returns the results as a list.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of query results.</returns>
        Task<List<TEntity>> ToListAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// Execute query asynchronously and returns the first result or default value if no result is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A first result or default, if no results.</returns>
        Task<TEntity?> FirstOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class;

        /// <summary>
        /// Execute query asynchronously and returns a single result or default value if no result is found.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="query">The query to execute.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A single result or default, it no results.</returns>
        Task<TEntity?> SingleOrDefaultAsync<TEntity>(IQueryable<TEntity> query, CancellationToken cancellationToken = default) where TEntity : class;
    }
}
