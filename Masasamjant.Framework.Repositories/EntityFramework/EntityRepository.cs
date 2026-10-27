using Masasamjant.Repositories.Abstractions;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents abstract <see cref="Repository{TEntity, TIdentifier}"/> that use Entity Framework for data access.
    /// </summary>
    /// <typeparam name="TContext">The type of the entity context.</typeparam>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    /// <typeparam name="TIdentifier">The type of the entity identifier.</typeparam>
    public abstract class EntityRepository<TContext, TEntity, TIdentifier> : Repository<TEntity, TIdentifier>
        where TContext : EntityContext
        where TEntity : class
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EntityRepository{TContext,TEntity, TIdentifier}"/> class.
        /// </summary>
        /// <param name="context">The entity context.</param>
        protected EntityRepository(TContext context)
            : base(context.AsyncQueryExecution)
        {
            Context = context;
        }

        /// <summary>
        /// Gets the entity context associated with repository.
        /// </summary>
        protected TContext Context { get; }

        /// <summary>
        /// Find <typeparamref name="TEntity"/> entity with specified identifier.
        /// </summary>
        /// <param name="identifier">The entity identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A found <typeparamref name="TEntity"/> or <c>null</c>.</returns>
        public override async Task<TEntity?> FindAsync(TIdentifier identifier, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
                throw new OperationCanceledException(cancellationToken);

            return await Context.FindAsync<TEntity>(identifier);
        }

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
