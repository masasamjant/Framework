using Masasamjant.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Represents abstract entity context that use SQL Server as data source.
    /// </summary>
    public abstract class SqlServerDbContext : EntityDbContext
    {
        /// <summary>
        /// Initializes new instance of the <see cref="SqlServerDbContext"/> class.
        /// </summary>
        /// <param name="connectionStringProvider">The <see cref="IConnectionStringProvider"/>.</param>
        protected SqlServerDbContext(IConnectionStringProvider connectionStringProvider)
            : base(connectionStringProvider) 
        { }

        /// <summary>
        /// Gets global timeout in seconds for executed commands or <c>null</c> to use default timeout.
        /// </summary>
        /// <returns>A command timeout in seconds.</returns>
        protected virtual int? GetCommandTimeoutSeconds() => null;

        /// <summary>
        /// Configures context using <see cref="DbContextOptionsBuilder"/>.
        /// </summary>
        /// <param name="optionsBuilder">The <see cref="DbContextOptionsBuilder"/>.</param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var connectionString = ConnectionStringProvider.GetConnectionString();
            var commandTimeout = GetCommandTimeoutSeconds();

            if (commandTimeout.HasValue && commandTimeout.Value > 0)
                optionsBuilder.UseSqlServer(connectionString, op => op.CommandTimeout(commandTimeout.Value));
            else
                optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
