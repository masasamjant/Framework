namespace Masasamjant.Modeling.Abstractions.Services
{
    /// <summary>
    /// Represents abstact implementation of <see cref="IManager{TModel}"/> interface.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class Manager<TModel> : IManager<TModel> where TModel : IModel
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Manager{TModel}"/> class.
        /// </summary>
        /// <param name="userIdentityProvider">The <see cref="IUserIdentityProvider"/>.</param>
        protected Manager(IUserIdentityProvider userIdentityProvider)
        {
            UserIdentityProvider = userIdentityProvider;
        }

        /// <summary>
        /// Gets the <see cref="IUserIdentityProvider"/>.
        /// </summary>
        protected IUserIdentityProvider UserIdentityProvider { get; }

        /// <summary>
        /// Adds new <typeparamref name="TModel"/> to non-volatile memory.
        /// </summary>
        /// <param name="model">The model to add.</param>
        /// <returns>A <typeparamref name="TModel"/> after it has been added.</returns>
        public abstract Task<TModel> AddAsync(TModel model);

        /// <summary>
        /// Removes aka deleted specified <typeparamref name="TModel"/> from non-volatile memory.
        /// </summary>
        /// <param name="model">The model to delete.</param>
        /// <returns>A <typeparamref name="TModel"/> after it has been removed.</returns>
        public abstract Task<TModel> RemoveAsync(TModel model);

        /// <summary>
        /// Updates specified <typeparamref name="TModel"/> in non-volatile memory.
        /// </summary>
        /// <param name="model">The model to update.</param>
        /// <returns>A <typeparamref name="TModel"/> after it has been updated.</returns>
        public abstract Task<TModel> UpdateAsync(TModel model);

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
}
