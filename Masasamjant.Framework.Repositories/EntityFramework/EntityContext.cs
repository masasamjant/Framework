using Masasamjant.Modeling.Abstractions;
using Masasamjant.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Principal;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents abstract repository context using Entity Framework.
    /// </summary>
    public abstract class EntityContext : DbContext
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EntityRepositoryContext"/> class.
        /// </summary>
        /// <param name="connectionStringProvider">The connection string provider.</param>
        /// <param name="currentIdentityProvider">The current identity provider.</param>
        protected EntityContext(IConnectionStringProvider connectionStringProvider, ICurrentIdentityProvider currentIdentityProvider)
        {
            ConnectionStringProvider = connectionStringProvider;
            CurrentIdentityProvider = currentIdentityProvider;
        }

        /// <summary>
        /// Gets the connection string provider.
        /// </summary>
        protected IConnectionStringProvider ConnectionStringProvider { get; }

        /// <summary>
        /// Gets the current identity provider. 
        /// </summary>
        protected ICurrentIdentityProvider CurrentIdentityProvider { get; }

        /// <summary>
        /// Gets the async query execution.
        /// </summary>
        public IAsyncQueryExecution AsyncQueryExecution { get; } = new EntityAsyncQueryExecution();

        /// <summary>
        /// Add specified <typeparamref name="TEntity"/> instance to repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The instance to add.</param>
        /// <returns>A added <typeparamref name="TEntity"/> instance.</returns>
        /// <exception cref="RepositoryException">If adding <paramref name="entity"/> fails.</exception>
        public async Task<TEntity> AddEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                var entry = await Set<TEntity>().AddAsync(entity);
                return entry.Entity;
            }
            catch (Exception exception)
            {
                throw new RepositoryException("Adding specified entity failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Query <typeparamref name="TEntity"/> entities.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entities to query.</typeparam>
        /// <returns>A query of <typeparamref name="TEntity"/>.</returns>
        public IQueryable<TEntity> Query<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        /// <summary>
        /// Remove specified <typeparamref name="TEntity"/> instance from repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The instance to remove.</param>
        /// <returns>A removed <typeparamref name="TEntity"/> instance.</returns>
        /// <exception cref="RepositoryException">If removing <paramref name="entity"/> fails.</exception>
        public Task<TEntity> RemoveEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                var entry = Set<TEntity>().Remove(entity);
                return Task.FromResult(entry.Entity);
            }
            catch (Exception exception)
            {
                throw new RepositoryException("Delete with specified entity failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Update specified <typeparamref name="TEntity"/> instance in repository.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="entity">The instance to update.</param>
        /// <returns>A updated <typeparamref name="TEntity"/> instance.</returns>
        /// <exception cref="RepositoryException">If updating <paramref name="entity"/> fails.</exception>
        public Task<TEntity> UpdateEntityAsync<TEntity>(TEntity entity) where TEntity : class
        {
            try
            {
                var entry = Entry(entity);
                if (entry.State == EntityState.Detached)
                    Set<TEntity>().Attach(entity);
                entry = Set<TEntity>().Update(entity);
                return Task.FromResult(entry.Entity);
            }
            catch (Exception exception)
            {
                throw new RepositoryException("Updating with specified entity failed. See inner exception for details.", exception);
            }
        }

        /// <summary>
        /// Save changes made to repository items.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A task representing save.</returns>
        /// <exception cref="RepositoryConcurrencyException">If concurrent update occurs.</exception>
        /// <exception cref="RepositoryException">If saving fails.</exception>
        public async Task SaveAsync(CancellationToken cancellationToken = default)
        {
            try
            {
                CheckChangeHandlers();
                await SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new RepositoryConcurrencyException("Concurrent update occurred. See inner exception for details.", exception);
            }
            catch (Exception exception)
            {
                if (exception is RepositoryConcurrencyException)
                    throw;
                else
                    throw new RepositoryException("Saving work failed. See inner exception for details.", exception);
            }
        }

        private void CheckChangeHandlers()
        {
            bool autoDetectChangesEnabled = ChangeTracker.AutoDetectChangesEnabled;

            try
            {
                ChangeTracker.AutoDetectChangesEnabled = false;

                var entries = GetEntries();

                if (entries.Count == 0)
                    return;

                var identity = CurrentIdentityProvider.GetCurrentIdentity();

                CheckHandlers(entries, identity);
            }
            finally
            { 
                ChangeTracker.AutoDetectChangesEnabled = autoDetectChangesEnabled;
            }
        }

        private List<EntityEntry> GetEntries()
        {
            return ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified 
                    || e.State == EntityState.Added 
                    || e.State == EntityState.Deleted)
                .ToList();
        }

        private static void CheckHandlers(IEnumerable<EntityEntry> entries, IIdentity? identity)
        {
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is IAddHandler addHandler)
                            addHandler.OnAdd(identity);
                        break;
                    case EntityState.Modified:
                        if (entry.Entity is IUpdateHandler updateHandler)
                            updateHandler.OnUpdate(identity);
                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is IRemoveHandler removeHandler)
                            removeHandler.OnRemove(identity);
                        break;
                }
            }
        }
    }
}