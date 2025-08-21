using Masasamjant.Modeling.Services;
using Microsoft.EntityFrameworkCore;

namespace Masasamjant.Repositories.EntityFramework
{
    internal static class DbContextHelper
    {
        internal static async Task<T> AddAsync<T>(DbContext context, T model) where T : class
        {
            try
            {
                var entry = await context.AddAsync(model);
                return entry.Entity;
            }
            catch (Exception exception)
            {
                throw new RepositoryException("Add with specified model failed. See inner exception for details.", exception);
            }
        }

        internal static Task<T> DeleteAsync<T>(DbContext context, T model) where T : class
        {
            try
            {
                var set = context.Set<T>();
                var entry = set.Entry(model);
                if (entry.State == EntityState.Detached)
                    set.Attach(model);
                entry = context.Remove(model);
                return Task.FromResult(entry.Entity);
            }
            catch (Exception exception)
            {
                throw new RepositoryException("Delete with specified model failed. See inner exception for details.", exception);
            }
        }

        internal static Task<T> UpdateAsync<T>(DbContext context, T model) where T : class
        {
            try
            {
                var set = context.Set<T>();
                var entry = set.Entry(model);
                if (entry.State == EntityState.Detached)
                    set.Attach(model);
                entry = context.Update(model);
                return Task.FromResult(entry.Entity);
            }
            catch (Exception exception)
            {
                throw new RepositoryException("Update with specified model failed. See inner exception for details.", exception);
            }
        }

        internal static async Task<T?> FindAsync<T>(DbContext context, object key) where T : class
        {
            try
            {
                var array = key as object[];
                var entity = await context.FindAsync<T>(array ?? key);
                return entity;
            }
            catch (Exception exception)
            {
                throw new RepositoryException("The query by key failed. See inner exception for details.", exception);
            }
        }

        internal static async Task SaveChangesAsync(DbContext context)
        {
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException exception)
            {
                throw new RepositoryConcurrencyException("Concurrent update occurred. See inner exception for details.", exception);
            }
            catch (Exception exception)
            {
                if (exception is RepositoryConcurrencyException)
                    throw;
                else
                    throw new InvalidOperationException("Saving work failed. See inner exception for details.", exception);
            }
        }
    }
}
