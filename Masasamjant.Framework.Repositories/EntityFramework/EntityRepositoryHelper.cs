using Masasamjant.Modeling.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masasamjant.Repositories.EntityFramework
{
    /// <summary>
    /// Provides helper methods related to repositories using Entity Framework.
    /// </summary>
    public static class EntityRepositoryHelper
    {
        /// <summary>
        /// Configure columns of <typeparamref name="TEntity"/> that implements <see cref="ISupportCreated"/> interface.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        /// <param name="identityMaxLength">The maximum length of <see cref="ISupportCreated.CreatedBy"/> or <c>null</c>.</param>
        public static void ConfigureCreated<TEntity>(this EntityTypeBuilder<TEntity> builder, int? identityMaxLength = null)
            where TEntity : class, ISupportCreated
        {
            builder.Property(x => x.CreatedAt);

            if (identityMaxLength.HasValue && identityMaxLength.Value > 0)
                builder.Property(x => x.CreatedBy).HasMaxLength(identityMaxLength.Value);
            else
                builder.Property(x => x.CreatedBy);
        }

        /// <summary>
        /// Configure columns of <typeparamref name="TEntity"/> that implements <see cref="ISupportModified"/> interface.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        /// <param name="identityMaxLength">The maximum length of <see cref="ISupportModified.ModifiedBy"/> or <c>null</c>.</param>
        public static void ConfigureModified<TEntity>(this EntityTypeBuilder<TEntity> builder, int? identityMaxLength = null)
            where TEntity : class, ISupportModified
        {
            builder.Property(x => x.ModifiedAt);

            if (identityMaxLength.HasValue && identityMaxLength.Value > 0)
                builder.Property(x => x.ModifiedBy).HasMaxLength(identityMaxLength.Value);
            else
                builder.Property(x => x.ModifiedBy);
        }

        /// <summary>
        /// Configure columns of <typeparamref name="TEntity"/> that implements <see cref="ISupportDeleted"/> interface.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        /// <param name="identityMaxLength">The maximum length of <see cref="ISupportDeleted.DeletedBy"/> or <c>null</c>.</param>
        public static void ConfigureDeleted<TEntity>(this EntityTypeBuilder<TEntity> builder, int? identityMaxLength = null)
            where TEntity : class, ISupportDeleted
        {
            builder.Property(x => x.DeletedAt);

            if (identityMaxLength.HasValue && identityMaxLength.Value > 0)
                builder.Property(x => x.DeletedBy).HasMaxLength(identityMaxLength.Value);
            else
                builder.Property(x => x.DeletedBy);
        }

        /// <summary>
        /// Configure columns of <typeparamref name="TEntity"/> that implements <see cref="ISupportVersion"/> interface.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        public static void ConfigureVersion<TEntity>(this EntityTypeBuilder<TEntity> builder)
            where TEntity : class, ISupportVersion
        {
            builder.Property(x => x.Version).IsRowVersion();
        }

        /// <summary>
        /// Configure columns of <typeparamref name="TEntity"/> that implements <see cref="IRecord"/> interface.
        /// </summary>
        /// <typeparam name="TEntity">The type of the entity.</typeparam>
        /// <param name="builder">The <see cref="EntityTypeBuilder{TEntity}"/>.</param>
        /// <param name="identityMaxLength">The maximum length of <see cref="ISupportCreated.CreatedBy"/> and <see cref="ISupportModified.ModifiedBy"/> or <c>null</c>.</param>
        public static void ConfigureRecord<TEntity>(this EntityTypeBuilder<TEntity> builder, int? identityMaxLength = null)
            where TEntity : class, IRecord
        {
            ConfigureCreated(builder, identityMaxLength);
            ConfigureModified(builder, identityMaxLength);
        }
    }
}
