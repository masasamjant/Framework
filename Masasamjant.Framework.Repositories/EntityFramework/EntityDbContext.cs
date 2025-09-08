using Masasamjant.Repositories.Abstactions;
using Microsoft.EntityFrameworkCore;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents abstract entity context.
    /// </summary>
    public abstract class EntityDbContext : DbContext
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EntityDbContext"/> class.
        /// </summary>
        /// <param name="connectionStringProvider">The <see cref="IConnectionStringProvider"/>.</param>
        public EntityDbContext(IConnectionStringProvider connectionStringProvider)
        {
            ConnectionStringProvider = connectionStringProvider;
        }

        /// <summary>
        /// Gets the <see cref="IConnectionStringProvider"/>.
        /// </summary>
        protected IConnectionStringProvider ConnectionStringProvider { get; }
    }
}
