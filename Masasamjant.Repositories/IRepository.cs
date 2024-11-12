using System.Linq.Expressions;

namespace Masasamjant.Repositories
{
    /// <summary>
    /// Represents repository to save data of objects.
    /// </summary>
    public interface IRepository : IDisposable
    {
        /// <summary>
        /// Add specified <typeparamref name="T"/> instance to repository.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="instance">The <typeparamref name="T"/> instance to add.</param>
        /// <param name="saveChanges"><c>true</c> to save changes immediately; <c>false</c> if <see cref="SaveChangesAsync"/> is invoked afterwards.</param>
        /// <returns>A added <typeparamref name="T"/> instance.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        Task<T> AddAsync<T>(T instance, bool saveChanges = false) where T : class;

        /// <summary>
        /// Check if repository contains any instance of <typeparamref name="T"/> that match specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="criteria">The criteria expression to match.</param>
        /// <returns><c>true</c> if repository contains any instance of <typeparamref name="T"/> that match <paramref name="criteria"/>; <c>false</c> otherwise.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        Task<bool> ExistsAsync<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Gets all instance of <typeparamref name="T"/> in repository. This method can be used in case the amount of instance of <typeparamref name="T"/> is limited 
        /// and it is known that all those are needed.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <returns>A collection of <typeparamref name="T"/> instances in repository.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        Task<IReadOnlyCollection<T>> GetAsync<T>() where T : class;

        /// <summary>
        /// Gets all instance of <typeparamref name="T"/> in repository that match specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="criteria">The criteria expression to match.</param>
        /// <returns>A collection of <typeparamref name="T"/> instances in repository that match <paramref name="criteria"/>.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        Task<IReadOnlyCollection<T>> GetAsync<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Query instances of <typeparamref name="T"/> in repository.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <returns>A query of <typeparamref name="T"/> instance in repository.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        IQueryable<T> Query<T>() where T : class;

        /// <summary>
        /// Query instances of <typeparamref name="T"/> in repository that match specified criteria.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="criteria">The criteria expression to match.</param>
        /// <returns>A query of <typeparamref name="T"/> instance in repository that match <paramref name="criteria"/>.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        IQueryable<T> Query<T>(Expression<Func<T, bool>> criteria) where T : class;

        /// <summary>
        /// Remove, delete permanently, instance of <typeparamref name="T"/> from repository.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="instance">The instance to remove.</param>
        /// <param name="saveChanges"><c>true</c> to save changes immediately; <c>false</c> if <see cref="SaveChangesAsync"/> is invoked afterwards.</param>
        /// <returns>A removed instance.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        /// <exception cref="ConcurrentUpdateException">If concurrent update error occurs.</exception>
        Task<T> RemoveAsync<T>(T instance, bool saveChanges = false) where T : class;

        /// <summary>
        /// Save changes made to instance stored in repository.
        /// </summary>
        /// <returns>A count of changed instances.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        /// <exception cref="ConcurrentUpdateException">If concurrent update error occurs.</exception>
        Task<int> SaveChangesAsync();

        /// <summary>
        /// Update specified instance of <typeparamref name="T"/> in repository.
        /// </summary>
        /// <typeparam name="T">The type of the object.</typeparam>
        /// <param name="instance">The instance to update.</param>
        /// <param name="saveChanges"><c>true</c> to save changes immediately; <c>false</c> if <see cref="SaveChangesAsync"/> is invoked afterwards.</param>
        /// <returns>A update instance.</returns>
        /// <exception cref="RepositoryException">If exception occurs at operation.</exception>
        /// <exception cref="ConcurrentUpdateException">If concurrent update error occurs.</exception>
        Task<T> UpdateAsync<T>(T instance, bool saveChanges = false) where T : class;
    }
}
