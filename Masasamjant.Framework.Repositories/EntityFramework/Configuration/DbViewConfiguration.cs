using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masasamjant.Repositories.EntityFramework.Configuration
{
    /// <summary>
    /// Represents abstract <see cref="DbEntityTypeConfiguration{TEntity}"/> to configure <typeparamref name="TEntity"/> to database view.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class DbViewConfiguration<TEntity> : DbEntityTypeConfiguration<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DbViewConfiguration{TEntity}"/> class.
        /// </summary>
        /// <param name="schemaName">The name of database schema.</param>
        /// <param name="viewName">The name of database table.</param>
        /// <exception cref="ArgumentNullException">
        /// If value of <paramref name="schemaName"/> is empty or only whitespace.
        /// -or-
        /// If value of <paramref name="viewName"/> is empty or only whitespace.
        /// </exception>
        protected DbViewConfiguration(string schemaName, string viewName)
            : base(schemaName, viewName)
        { }

        /// <summary>
        /// Configures <typeparamref name="TEntity"/> to database view.
        /// </summary>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        public sealed override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.ToView(ObjectName, SchemaName);
            ConfigureView(builder);
        }

        /// <summary>
        /// Configures database view for <typeparamref name="TEntity"/> entity.
        /// </summary>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        protected abstract void ConfigureView(EntityTypeBuilder<TEntity> builder);
    }
}
