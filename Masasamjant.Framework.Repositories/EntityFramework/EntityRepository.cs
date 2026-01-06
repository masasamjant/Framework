using Masasamjant.Modeling.Abstractions;
using Masasamjant.Repositories.Abstractions;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents abstract <see cref="Repository{TEntity, TIdentifier}"/> that use Entity Framework for data access.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIdentifier">The type of the entity identifier.</typeparam>
    public abstract class EntityRepository<TEntity, TIdentifier> : Repository<TEntity, TIdentifier>
        where TEntity : Entity<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EntityRepository{TEntity, TIdentifier}"/> class.
        /// </summary>
        /// <param name="context">The entity context.</param>
        protected EntityRepository(EntityContext context)
            : base(new EntityAsyncQueryExecution())
        {
            Context = context;
        }

        /// <summary>
        /// Gets the entity context associated with repository.
        /// </summary>
        protected EntityContext Context { get; }

        /// <summary>
        /// Save changes made to repository items.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing save.</returns>
        public override Task SaveAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveAsync(cancellationToken);
        }
    }
}
