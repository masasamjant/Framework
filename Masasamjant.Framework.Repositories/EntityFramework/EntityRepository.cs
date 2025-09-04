using Masasamjant.Linq;
using Masasamjant.Modeling.Abstractions;
using Masasamjant.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents repository of <typeparamref name="TModel"/> that use Entity Framework.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public class EntityRepository<TModel> : Repository<TModel>, IDisposable, ISupportIsDisposed where TModel : class, IModel
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes new instance of the <see cref="EntityRepository{TModel}"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/>.</param>
        /// <param name="userIdentityProvider">The <see cref="IUserIdentityProvider"/>.</param>
        public EntityRepository(DbContext context, IUserIdentityProvider userIdentityProvider)
            : base(userIdentityProvider)
        {
            this.context = context;
        }

        /// <summary>
        /// Finalizes current instance.
        /// </summary>
        ~EntityRepository()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the associated database context.
        /// </summary>
        protected DbContext DbContext 
        {
            get 
            {
                this.CheckIsDisposed();
                return context;
            } 
        }

        /// <summary>
        /// Gets whether or not instance is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Add specified <typeparamref name="TModel"/> instance to repository.
        /// </summary>
        /// <param name="model">The model instance to add.</param>
        /// <returns>A added model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public override async Task<TModel> AddAsync(TModel model)
        {
            return await DbContextHelper.AddAsync(DbContext, model);
        }

        /// <summary>
        /// Delete specified <typeparamref name="TModel"/> instance from repository.
        /// </summary>
        /// <param name="model">The model instance to delete.</param>
        /// <returns>A deleted model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public override Task<TModel> DeleteAsync(TModel model)
        {
            return DbContextHelper.DeleteAsync(DbContext, model);
        }

        /// <summary>
        /// Update specified <typeparamref name="TModel"/> instance in repository.
        /// </summary>
        /// <param name="model">The model instance to update.</param>
        /// <returns>A updated model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public override Task<TModel> UpdateAsync(TModel model)
        {
            return DbContextHelper.UpdateAsync(DbContext, model);
        }

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository.
        /// </summary>
        /// <returns>A <see cref="IQueryable{TModel}"/>.</returns>
        public override IQueryable<TModel> Query()
        {
            return DbContext.Set<TModel>();
        }

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository using specified <see cref="IQuery{TModel}"/>.
        /// </summary>
        /// <param name="query">The query specification.</param>
        /// <returns>A <see cref="IQueryable{TModel}"/> of result.</returns>
        public override IQueryable<TModel> Query(IQuery<TModel> query)
        {
            return DbContext.Set<TModel>().Where(query.Criteria);
        }

        /// <summary>
        /// Find <typeparamref name="TModel"/> using specified key.
        /// </summary>
        /// <param name="key">The key to identify model.</param>
        /// <returns>A <typeparamref name="TModel"/> with specified key or <c>null</c>, if not exist.</returns>
        public override async Task<TModel?> FindAsync(object key)
        {
            return await DbContextHelper.FindAsync<TModel>(DbContext, key);
        }

        /// <summary>
        /// Save the work done in repository.
        /// </summary>
        /// <returns>A task.</returns>
        /// <exception cref="InvalidOperationException">If saving work fails.</exception>
        public override async Task SaveAsync()
        {
            await DbContextHelper.SaveChangesAsync(DbContext);
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            if (disposing)
                context.Dispose();
        }
    }

    /// <summary>
    /// Represents repository of <typeparamref name="TModel"/> identified by <typeparamref name="TIdentifier"/> that use Entity Framework.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public class EntityRepository<TModel, TIdentifier> : Repository<TModel, TIdentifier>, IDisposable, ISupportIsDisposed
        where TModel : class, IModel<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        private readonly DbContext context;

        /// <summary>
        /// Initializes new instance of the <see cref="EntityRepository{TModel, TIdentifier}"/> class.
        /// </summary>
        /// <param name="context">The <see cref="DbContext"/>.</param>
        /// <param name="userIdentityProvider">The <see cref="IUserIdentityProvider"/>.</param>
        public EntityRepository(DbContext context, IUserIdentityProvider userIdentityProvider)
            : base(userIdentityProvider)
        {
            this.context = context;
        }

        /// <summary>
        /// Finalizes current instance.
        /// </summary>
        ~EntityRepository()
        {
            Dispose(false);
        }

        /// <summary>
        /// Gets the associated database context.
        /// </summary>
        protected DbContext DbContext
        {
            get
            {
                this.CheckIsDisposed();
                return context;
            }
        }

        /// <summary>
        /// Gets whether or not instance is disposed.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// Add specified <typeparamref name="TModel"/> instance to repository.
        /// </summary>
        /// <param name="model">The model instance to add.</param>
        /// <returns>A added model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public override async Task<TModel> AddAsync(TModel model)
        {
            return await DbContextHelper.AddAsync(DbContext, model);
        }

        /// <summary>
        /// Delete specified <typeparamref name="TModel"/> instance from repository.
        /// </summary>
        /// <param name="model">The model instance to delete.</param>
        /// <returns>A deleted model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public override Task<TModel> DeleteAsync(TModel model)
        {
            return DbContextHelper.DeleteAsync(DbContext, model);
        }

        /// <summary>
        /// Update specified <typeparamref name="TModel"/> instance in repository.
        /// </summary>
        /// <param name="model">The model instance to update.</param>
        /// <returns>A updated model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public override Task<TModel> UpdateAsync(TModel model)
        {
            return DbContextHelper.UpdateAsync(DbContext, model);
        }

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository.
        /// </summary>
        /// <returns>A <see cref="IQueryable{TModel}"/>.</returns>
        public override IQueryable<TModel> Query()
        {
            return DbContext.Set<TModel>();
        }

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository using specified <see cref="IQuery{TModel}"/>.
        /// </summary>
        /// <param name="query">The query specification.</param>
        /// <returns>A <see cref="IQueryable{TModel}"/> of result.</returns>
        public override IQueryable<TModel> Query(IQuery<TModel> query)
        {
            return DbContext.Set<TModel>().Where(query.Criteria);
        }

        /// <summary>
        /// Find <typeparamref name="TModel"/> using specified key.
        /// </summary>
        /// <param name="key">The key to identify model.</param>
        /// <returns>A <typeparamref name="TModel"/> with specified key or <c>null</c>, if not exist.</returns>
        public override async Task<TModel?> FindAsync(object key)
        {
            return await DbContextHelper.FindAsync<TModel>(DbContext, key);
        }

        /// <summary>
        /// Find <typeparamref name="TModel"/> using specified <typeparamref name="TIdentifier"/>.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>A <typeparamref name="TModel"/> with specified identifier or <c>null</c>, if not exist.</returns>
        public override async Task<TModel?> FindAsync(TIdentifier identifier)
        {
            return await DbContextHelper.FindAsync<TModel>(DbContext, identifier);
        }

        /// <summary>
        /// Save the work done in repository.
        /// </summary>
        /// <returns>A task.</returns>
        /// <exception cref="InvalidOperationException">If saving work fails.</exception>
        public override async Task SaveAsync()
        {
            await DbContextHelper.SaveChangesAsync(DbContext);
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Disposes current instance.
        /// </summary>
        /// <param name="disposing"><c>true</c> if disposing; <c>false</c> otherwise.</param>
        protected virtual void Dispose(bool disposing)
        {
            if (IsDisposed)
                return;

            IsDisposed = true;

            if (disposing)
                context.Dispose();
        }
    }
}
