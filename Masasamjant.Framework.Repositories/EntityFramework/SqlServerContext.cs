using Masasamjant.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents abstract repository context using Entity Framework with SQL Server.
    /// </summary>
    public abstract class SqlServerContext : EntityContext
    {
        /// <summary>
        /// Initializes new instance of the <see cref="SqlServerContext"/> class.
        /// </summary>
        /// <param name="connectionStringProvider">The connection string provider.</param>
        protected SqlServerContext(IConnectionStringProvider connectionStringProvider, ICurrentIdentityProvider currentIdentityProvider)
            : base(connectionStringProvider, currentIdentityProvider)
        { }

        /// <summary>
        /// Configures context options.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/>.</param>
        protected override sealed void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var connectionString = ConnectionStringProvider.GetConnectionString();
            optionsBuilder.UseSqlServer(connectionString, OnConfiguringSqlServer);
        }

        /// <summary>
        /// Invoked so that derived classes can further configure the SQL Server specific options.
        /// </summary>
        /// <param name="options">The <see cref="SqlServerDbContextOptionsBuilder"/>.</param>
        protected virtual void OnConfiguringSqlServer(SqlServerDbContextOptionsBuilder options)
        {
            return;
        }
    }
}
