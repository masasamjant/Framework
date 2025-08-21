using Masasamjant.Linq;
using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling.Services.Abstractions
{
    /// <summary>
    /// Represents abstract <see cref="IRepository{TModel}"/>.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class Repository<TModel> : IRepository<TModel> where TModel : IModel
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Repository{TModel}"/> class.
        /// </summary>
        /// <param name="userIdentityProvider">The <see cref="IUserIdentityProvider"/>.</param>
        protected Repository(IUserIdentityProvider userIdentityProvider)
        {
            UserIdentityProvider = userIdentityProvider;
        }

        /// <summary>
        /// Gets the <see cref="IUserIdentityProvider"/>.
        /// </summary>
        protected IUserIdentityProvider UserIdentityProvider { get; }

        /// <summary>
        /// Add specified <typeparamref name="TModel"/> instance to repository.
        /// </summary>
        /// <param name="model">The model instance to add.</param>
        /// <returns>A added model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public abstract Task<TModel> AddAsync(TModel model);

        /// <summary>
        /// Delete specified <typeparamref name="TModel"/> instance from repository.
        /// </summary>
        /// <param name="model">The model instance to delete.</param>
        /// <returns>A deleted model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public abstract Task<TModel> DeleteAsync(TModel model);

        /// <summary>
        /// Update specified <typeparamref name="TModel"/> instance in repository.
        /// </summary>
        /// <param name="model">The model instance to update.</param>
        /// <returns>A updated model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        public abstract Task<TModel> UpdateAsync(TModel model);

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository.
        /// </summary>
        /// <returns>A <see cref="IQueryable{TModel}"/>.</returns>
        public abstract IQueryable<TModel> Query();

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository using specified <see cref="IQuery{TModel}"/>.
        /// </summary>
        /// <param name="query">The query specification.</param>
        /// <returns>A <see cref="IQueryable{TModel}"/> of result.</returns>
        public abstract IQueryable<TModel> Query(IQuery<TModel> query);

        /// <summary>
        /// Find <typeparamref name="TModel"/> using specified key.
        /// </summary>
        /// <param name="key">The key to identify model.</param>
        /// <returns>A <typeparamref name="TModel"/> with specified key or <c>null</c>, if not exist.</returns>
        public abstract Task<TModel?> FindAsync(object key);

        /// <summary>
        /// Save the work done in repository.
        /// </summary>
        /// <returns>A task.</returns>
        /// <exception cref="InvalidOperationException">If saving work fails.</exception>
        public abstract Task SaveAsync();

        /// <summary>
        /// Can be invoked when <typeparamref name="TModel"/> is added to invoke <see cref="IAddHandler.OnAdd(string?)"/> 
        /// of the model with identity of current user.
        /// </summary>
        /// <param name="model">The added model.</param>
        protected virtual void OnAdd(TModel model)
        {
            var identity = GetUserIdentityString();
            model.OnAdd(identity);
        }

        /// <summary>
        /// Can be invoked when <typeparamref name="TModel"/> is updated to invoke <see cref="IUpdateHandler.OnUpdate(string?)"/> 
        /// of the model with identity of current user.
        /// </summary>
        /// <param name="model">The updated model.</param>
        protected virtual void OnUpdate(TModel model)
        {
            var identity = GetUserIdentityString();
            model.OnUpdate(identity);
        }

        /// <summary>
        /// Can be invoked when <typeparamref name="TModel"/> is removed to invoke <see cref="IRemoveHandler.OnRemove(string?)"/> 
        /// of the model with identity of current user.
        /// </summary>
        /// <param name="model">The removed model.</param>
        protected virtual void OnRemove(TModel model)
        {
            var identity = GetUserIdentityString();
            model.OnRemove(identity);
        }

        /// <summary>
        /// Gets the identity of current user.
        /// </summary>
        /// <returns>A ídentity of current user or <c>null</c>, if current user is anonymous.</returns>
        protected virtual string? GetUserIdentityString()
        {
            var userIdentity = UserIdentityProvider.GetUserIdentity();
            return userIdentity.IsAnonymous() ? null : userIdentity.Identity;
        }    
    }

    /// <summary>
    /// Represents abstract <see cref="IRepository{TModel, TIdentifier}"/>.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class Repository<TModel, TIdentifier> : Repository<TModel>, IRepository<TModel, TIdentifier>
        where TModel : IModel<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Repository{TModel, TIdentifier}"/> class.
        /// </summary>
        /// <param name="userIdentityProvider">The <see cref="IUserIdentityProvider"/>.</param>
        protected Repository(IUserIdentityProvider userIdentityProvider)
            : base(userIdentityProvider)
        { }

        /// <summary>
        /// Find <typeparamref name="TModel"/> using specified <typeparamref name="TIdentifier"/>.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>A <typeparamref name="TModel"/> with specified identifier or <c>null</c>, if not exist.</returns>
        public abstract Task<TModel?> FindAsync(TIdentifier identifier);
    }
}
