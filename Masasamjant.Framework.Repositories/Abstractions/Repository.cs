using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Repositories.Abstractions
{
    /// <summary>
    /// Represents abstract base implementation of <see cref="IRepository{TEntity, TIdentifier}"/>.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIdentifier">The type of the entity identifier.</typeparam>
    public abstract class Repository<TEntity, TIdentifier> : IRepository<TEntity, TIdentifier>
        where TEntity : Entity<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Repository{TEntity, TIdentifier}"/> class.
        /// </summary>
        /// <param name="asyncQueryExecution">The <see cref="IAsyncQueryExecution"/>.</param>
        protected Repository(IAsyncQueryExecution asyncQueryExecution)
        {
            AsyncQueryExecution = asyncQueryExecution;
        }

        /// <summary>
        /// Gets the <see cref="IAsyncQueryExecution"/> associated with repository.
        /// </summary>
        public IAsyncQueryExecution AsyncQueryExecution { get; }

        /// <summary>
        /// Add specified <typeparamref name="TEntity"/> instance to repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A added <typeparamref name="TEntity"/>.</returns>
        public abstract Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find <typeparamref name="TEntity"/> entity with specified identifier.
        /// </summary>
        /// <param name="identifier">The entity identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A found <typeparamref name="TEntity"/> or <c>null</c>.</returns>
        public abstract Task<TEntity?> FindAsync(TIdentifier identifier, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove specified <typeparamref name="TEntity"/> instance from repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A removed <typeparamref name="TEntity"/>.</returns>
        public abstract Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);
        
        /// <summary>
        /// Save changes made to repository items.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing save.</returns>
        public abstract Task SaveAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Mark specified <typeparamref name="TEntity"/> instance as updated in repository.
        /// </summary>
        /// <param name="entity">The updated entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A updated <typeparamref name="TEntity"/>.</returns>
        public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
