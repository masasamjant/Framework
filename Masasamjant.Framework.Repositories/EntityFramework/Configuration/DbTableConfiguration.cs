using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masasamjant.Repositories.EntityFramework.Configuration
{
    /// <summary>
    /// Represents abstract <see cref="DbEntityTypeConfiguration{TEntity}"/> to configure <typeparamref name="TEntity"/> to database table.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class DbTableConfiguration<TEntity> : DbEntityTypeConfiguration<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DbTableConfiguration{TEntity}"/> class.
        /// </summary>
        /// <param name="schemaName">The name of database schema.</param>
        /// <param name="tableName">The name of database table.</param>
        /// <exception cref="ArgumentNullException">
        /// If value of <paramref name="schemaName"/> is empty or only whitespace.
        /// -or-
        /// If value of <paramref name="tableName"/> is empty or only whitespace.
        /// </exception>
        protected DbTableConfiguration(string schemaName, string tableName)
            : base(schemaName, tableName)
        { }

        /// <summary>
        /// Configures <typeparamref name="TEntity"/> to database table.
        /// </summary>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        public sealed override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToTable(ObjectName, SchemaName);
            ConfigureTable(builder);
        }

        /// <summary>
        /// Configures database table for <typeparamref name="TEntity"/> entity.
        /// </summary>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        protected abstract void ConfigureTable(EntityTypeBuilder<TEntity> builder);
    }
}
