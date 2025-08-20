using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant.Modeling.Abstractions.Services
{
    public interface IRepository<TModel> where TModel : IModel
    {
        Task<TModel> AddAsync(TModel model);

        Task<TModel> UpdateAsync(TModel model);

        Task<TModel> DeleteAsync(TModel model);

        IQueryable<TModel> Query();

        IQueryable<TModel> Query(Expression<Func<TModel, bool>> predicate);
    }

    public interface IRepository<TModel, TIdentifier> : IRepository<TModel>
        where TModel : IModel<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>
    {
        Task<TModel?> FindAsync(TIdentifier identifier);
    }
}
