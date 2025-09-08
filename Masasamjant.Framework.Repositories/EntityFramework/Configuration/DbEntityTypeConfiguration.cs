using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masasamjant.Repositories.EntityFramework.Configuration
{
    /// <summary>
    /// Represents abstract configuration of <typeparamref name="TEntity"/> to database object.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public abstract class DbEntityTypeConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : class
    {
        /// <summary>
        /// Initializes new instance of the <see cref="DbEntityTypeConfiguration{TEntity}"/> class.
        /// </summary>
        /// <param name="schemaName">The name of database schema.</param>
        /// <param name="objectName">The name of database object.</param>
        /// <exception cref="ArgumentNullException">
        /// If value of <paramref name="schemaName"/> is empty or only whitespace.
        /// -or-
        /// If value of <paramref name="objectName"/> is empty or only whitespace.
        /// </exception>
        protected DbEntityTypeConfiguration(string schemaName, string objectName)
        {
            if (string.IsNullOrWhiteSpace(schemaName))
                throw new ArgumentNullException(nameof(schemaName), "The schema name cannot be empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(objectName)) 
                throw new ArgumentNullException(nameof(objectName), "The object name cannot be empty or only whitespace.");

            SchemaName = schemaName;
            ObjectName = objectName;
        }

        /// <summary>
        /// Gets the name of schema.
        /// </summary>
        protected string SchemaName { get; }

        /// <summary>
        /// Gets the name of database object.
        /// </summary>
        protected string ObjectName { get; }

        /// <summary>
        /// Configures <typeparamref name="TEntity"/> to database object.
        /// </summary>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        public abstract void Configure(EntityTypeBuilder<TEntity> builder);
    }
}
