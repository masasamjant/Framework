using System.Linq.Expressions;

namespace Masasamjant.Modeling.Abstractions.Services
{
    /// <summary>
    /// Represents abstract implementation of <see cref="IProvider{TModel}"/> interface.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public abstract class Provider<TModel> : IProvider<TModel> where TModel : IModel
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Provider{TModel}"/> class.
        /// </summary>
        /// <param name="userIdentityProvider">The <see cref="IUserIdentityProvider"/>.</param>
        protected Provider(IUserIdentityProvider userIdentityProvider)
        {
            UserIdentityProvider = userIdentityProvider;
        }

        /// <summary>
        /// Gets the <see cref="IUserIdentityProvider"/>.
        /// </summary>
        protected IUserIdentityProvider UserIdentityProvider { get; }

        /// <summary>
        /// Finds first instance of <typeparamref name="TModel"/> that match specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A first <typeparamref name="TModel"/> instance that match predicate; <c>null</c> if no matches.</returns>
        public abstract Task<TModel?> FindFirstAsync(Expression<Func<bool, TModel>> predicate);

        /// <summary>
        /// Finds single instance of <typeparamref name="TModel"/> that match specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A <typeparamref name="TModel"/> instance if there is single model that match predicate; <c>null</c> otherwise.</returns>
        public abstract Task<TModel?> FindSingleAsync(Expression<Func<bool, TModel>> predicate);

        /// <summary>
        /// Gets single instance of <typeparamref name="TModel"/> that match specified predicate. 
        /// This expects that there is only single model that match <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A <typeparamref name="TModel"/> instance matching predicate.</returns>
        /// <exception cref="ModelNotExistException">If there is not model or there is more than one model that match predicate.</exception>
        public abstract Task<TModel> GetAsync(Expression<Func<bool, TModel>> predicate);

        /// <summary>
        /// Search all instance of <typeparamref name="TModel"/> that match specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>All instances of <typeparamref name="TModel"/> that match predicate.</returns>
        public abstract Task<IEnumerable<TModel>> SearchAsync(Expression<Func<bool, TModel>> predicate);
    }
}
