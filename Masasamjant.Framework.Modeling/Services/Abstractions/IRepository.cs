using Masasamjant.Modeling.Abstractions;
using System.Linq.Expressions;

namespace Masasamjant.Modeling.Services.Abstractions
{
    /// <summary>
    /// Represents repository of <typeparamref name="TModel"/> instances.
    /// </summary>
    /// <typeparam name="TModel">The type of the model.</typeparam>
    public interface IRepository<TModel> where TModel : IModel
    {
        Task<TModel> AddAsync(TModel model);

        Task<TModel> UpdateAsync(TModel model);

        Task<TModel> DeleteAsync(TModel model);

        IQueryable<TModel> Query();

        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate);
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
        Task<TModel?> FindAsync(TIdentifier identifier);
    }
}
