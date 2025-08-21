using Masasamjant.ComponentModel;
using Masasamjant.Linq;
using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling.Services.Abstractions
{
    /// <summary>
    /// Represents repository of <typeparamref name="TModel"/> instances.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IRepository<TModel> : IWork where TModel : IModel
    {
        /// <summary>
        /// Add specified <typeparamref name="TModel"/> instance to repository.
        /// </summary>
        /// <param name="model">The model instance to add.</param>
        /// <returns>A added model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        Task<TModel> AddAsync(TModel model);

        /// <summary>
        /// Update specified <typeparamref name="TModel"/> instance in repository.
        /// </summary>
        /// <param name="model">The model instance to update.</param>
        /// <returns>A updated model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        Task<TModel> UpdateAsync(TModel model);

        /// <summary>
        /// Delete specified <typeparamref name="TModel"/> instance from repository.
        /// </summary>
        /// <param name="model">The model instance to delete.</param>
        /// <returns>A deleted model.</returns>
        /// <exception cref="RepositoryException">If operation fails.</exception>
        Task<TModel> DeleteAsync(TModel model);

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository.
        /// </summary>
        /// <returns>A <see cref="IQueryable{TModel}"/>.</returns>
        IQueryable<TModel> Query();

        /// <summary>
        /// Query instance of <typeparamref name="TModel"/> in repository using specified <see cref="IQuery{TModel}"/>.
        /// </summary>
        /// <param name="query">The query specification.</param>
        /// <returns>A <see cref="IQueryable{TModel}"/> of result.</returns>
        IQueryable<TModel> Query(IQuery<TModel> query);

        /// <summary>
        /// Find <typeparamref name="TModel"/> using specified key.
        /// </summary>
        /// <param name="key">The key to identify model.</param>
        /// <returns>A <typeparamref name="TModel"/> with specified key or <c>null</c>, if not exist.</returns>
        Task<TModel?> FindAsync(object key);
    }

    /// <summary>
    /// Represents repository of <typeparamref name="TModel"/> instances.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public interface IRepository<TModel, TIdentifier> : IRepository<TModel>
        where TModel : IModel<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        /// <summary>
        /// Find <typeparamref name="TModel"/> using specified <typeparamref name="TIdentifier"/>.
        /// </summary>
        /// <param name="identifier">The identifier.</param>
        /// <returns>A <typeparamref name="TModel"/> with specified identifier or <c>null</c>, if not exist.</returns>
        Task<TModel?> FindAsync(TIdentifier identifier);
    }
}
