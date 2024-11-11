using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant.Repositories
{
    /// <summary>
    /// Represents repository to save data of objects.
    /// </summary>
    public interface IRepository : IDisposable
    {
        Task<T> AddAsync<T>(T instance, bool saveChanges = false) where T : class;

        Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> criteria) where T : class;

        Task<IReadOnlyCollection<T>> GetAsync<T>() where T : class;

        Task<IReadOnlyCollection<T>> GetAsync<T>(Expression<Func<T, bool>> criteria) where T : class;

        IQueryable<T> Query<T>() where T : class;

        IQueryable<T> Query<T>(Expression<Func<T, bool>> criteria) where T : class;

        Task<T> RemoveAsync<T>(T instance, bool saveChanges = false) where T : class;

        Task<int> SaveChangesAsync();

        Task<T> UpdateAsync<T>(T instance, bool saveChanges = false) where T : class;
    }
}
