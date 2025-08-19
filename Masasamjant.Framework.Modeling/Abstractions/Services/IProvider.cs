using System.Linq.Expressions;

namespace Masasamjant.Modeling.Abstractions.Services
{
    /// <summary>
    /// Represents service that provides <typeparamref name="TModel"/> instances 
    /// from non-volatile memory like database or file.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public interface IProvider<TModel> where TModel : IModel
    {
        /// <summary>
        /// Gets single instance of <typeparamref name="TModel"/> that match specified predicate. 
        /// This expects that there is only single model that match <paramref name="predicate"/>.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A <typeparamref name="TModel"/> instance matching predicate.</returns>
        /// <exception cref="ModelNotExistException">If there is not model or there is more than one model that match predicate.</exception>
        Task<TModel> GetAsync(Expression<Func<bool, TModel>> predicate);

        /// <summary>
        /// Finds single instance of <typeparamref name="TModel"/> that match specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A <typeparamref name="TModel"/> instance if there is single model that match predicate; <c>null</c> otherwise.</returns>
        Task<TModel?> FindSingleAsync(Expression<Func<bool, TModel>> predicate);

        /// <summary>
        /// Finds first instance of <typeparamref name="TModel"/> that match specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>A first <typeparamref name="TModel"/> instance that match predicate; <c>null</c> if no matches.</returns>
        Task<TModel?> FindFirstAsync(Expression<Func<bool, TModel>> predicate);

        /// <summary>
        /// Search all instance of <typeparamref name="TModel"/> that match specified predicate.
        /// </summary>
        /// <param name="predicate">The predicate.</param>
        /// <returns>All instances of <typeparamref name="TModel"/> that match predicate.</returns>
        Task<IEnumerable<TModel>> SearchAsync(Expression<Func<bool, TModel>> predicate);
    }
}
