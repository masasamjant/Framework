using Masasamjant.Modeling.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant.Modeling.Services.Abstractions
{
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

        public abstract Task<TModel> AddAsync(TModel model);
        public abstract Task<TModel> DeleteAsync(TModel model);
        public abstract Task<TModel> UpdateAsync(TModel model);

        public abstract IQueryable<TModel> Query();
        public abstract IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate);

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

        public abstract Task<TModel?> FindAsync(TIdentifier identifier);
    }
}
