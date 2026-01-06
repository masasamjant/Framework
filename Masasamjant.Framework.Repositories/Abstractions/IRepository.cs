using Masasamjant.ComponentModel;
using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Repositories.Abstractions
{
    /// <summary>
    /// Represents repository of <typeparamref name="TEntity"/> instances.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIdentifier">The type of the entity identifier.</typeparam>
    public interface IRepository<TEntity, TIdentifier> : IWork
        where TEntity : IEntity<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Add specified <typeparamref name="TEntity"/> instance to repository.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A added <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Find <typeparamref name="TEntity"/> entity with specified identifier.
        /// </summary>
        /// <param name="identifier">The entity identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A found <typeparamref name="TEntity"/> or <c>null</c>.</returns>
        Task<TEntity?> FindAsync(TIdentifier identifier, CancellationToken cancellationToken = default);

        /// <summary>
        /// Remove specified <typeparamref name="TEntity"/> instance from repository.
        /// </summary>
        /// <param name="entity">The entity to remove.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A removed <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

        /// <summary>
        /// Mark specified <typeparamref name="TEntity"/> instance as updated in repository.
        /// </summary>
        /// <param name="entity">The updated entity.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A updated <typeparamref name="TEntity"/>.</returns>
        Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    }
}
