using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Provides helper method to <see cref="IModel"/> interface.
    /// </summary>
    public static class ModelHelper
    {
        /// <summary>
        /// Check if specified <see cref="IModel"/> is valid.
        /// </summary>
        /// <param name="model">The model to validate.</param>
        /// <param name="validationException">The validation exception if returns <c>false</c>.</param>
        /// <returns><c>true</c> if model is valid; <c>false</c> otherwise.</returns>
        public static bool IsValid(this IModel model, out ModelValidationException? validationException)
        {
            ArgumentNullException.ThrowIfNull(model);

            try
            {
                validationException = null;
                model.Validate();
                return true;
            }
            catch (ModelValidationException exception)
            {
                validationException = exception;
                return false;
            }
        }

        /// <summary>
        /// Tries to prepare specified <typeparamref name="TModel"/> if it happens to implement <see cref="ISupportPrepareModel"/> interface.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="model">The model instance.</param>
        /// <returns>A <paramref name="model"/>.</returns>
        public static TModel TryPrepareModel<TModel>(this TModel model) where TModel : IModel
        {
            ArgumentNullException.ThrowIfNull(model);

            if (model is ISupportPrepareModel supportPrepareModel)
                supportPrepareModel.PrepareModel();

            return model;
        }

        /// <summary>
        /// Tries to prepare specified models of <typeparamref name="TModel"/> if it happens to implement <see cref="ISupportPrepareModel"/> interface.
        /// </summary>
        /// <typeparam name="TModel">The type of the model.</typeparam>
        /// <param name="models">The models.</param>
        /// <returns>A models.</returns>
        public static IEnumerable<TModel> TryPrepareModels<TModel>(this IEnumerable<TModel> models) where TModel : IModel
        {
            foreach (var model in models)
            {
                yield return TryPrepareModel(model);
            }
        }

        /// <summary>
        /// Check if specified <see cref="IModel"/> has version data.
        /// </summary>
        /// <param name="model">The <see cref="IModel"/>.</param>
        /// <returns><c>true</c> if <paramref name="model"/> has any version data; <c>false</c> otherwise.</returns>
        public static bool HasVersion(this ISupportVersion model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return model.Version.Length > 0;
        }

        /// <summary>
        /// Check if specified <see cref="ISupportValidityPeriod"/> is valid at specified <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="model">The <see cref="ISupportValidityPeriod"/>.</param>
        /// <param name="dateTime">The <see cref="DateTimeOffset"/> to check validity at.</param>
        /// <returns><c>true</c> if <paramref name="model"/> is valid at <paramref name="dateTime"/>; <c>false</c> otherwise.</returns>
        public static bool IsValidAt(this ISupportValidityPeriod model, DateTimeOffset dateTime)
        {
            ArgumentNullException.ThrowIfNull(model);

            if (model.ValidFrom.HasValue && model.ValidTo.HasValue)
                return model.ValidFrom.Value <= dateTime && dateTime <= model.ValidTo.Value;

            if (model.ValidFrom.HasValue)
                return model.ValidFrom.Value <= dateTime;
            
            if (model.ValidTo.HasValue)
                return dateTime <= model.ValidTo.Value;
            
            return true;
        }

        /// <summary>
        /// Check if two specified <see cref="ISupportVersion"/> models have the same version data.
        /// </summary>
        /// <param name="model">The first <see cref="ISupportVersion"/> model.</param>
        /// <param name="otherModel">The second <see cref="ISupportVersion"/> model.</param>
        /// <returns><c>true</c> if both models have the same version data; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentException">If <paramref name="otherModel"/> is not the same type as <paramref name="model"/>.</exception>
        public static bool HasSameVersion(this ISupportVersion model, ISupportVersion otherModel)
        {
            ArgumentNullException.ThrowIfNull(model);
            ArgumentNullException.ThrowIfNull(otherModel);

            if (!model.GetType().Equals(otherModel.GetType()))
                throw new ArgumentException("The specified other model is not the same type as the model.", nameof(otherModel));

            if (ReferenceEquals(model, otherModel))
                return true;

            return model.Version.SequenceEqual(otherModel.Version);
        }

        /// <summary>
        /// Check if specified <see cref="ISupportDeleted"/> model is marked as deleted.
        /// </summary>
        /// <param name="model">The <see cref="ISupportDeleted"/> model.</param>
        /// <returns><c>true</c> if the model is marked as deleted; <c>false</c> otherwise.</returns>
        public static bool IsDeleted(this ISupportDeleted model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return model.DeletedAt.HasValue;
        }

        /// <summary>
        /// Check if specified <see cref="ISupportDeleted"/> model is marked as deleted and get the deleted information.
        /// </summary>
        /// <param name="model">The <see cref="ISupportDeleted"/> model.</param>
        /// <param name="deletedAt">The date and time when the model was deleted.</param>
        /// <param name="deletedBy">The user who deleted the model or <c>null</c> if not available.</param>
        /// <returns><c>true</c> if the model is marked as deleted; <c>false</c> otherwise.</returns>
        public static bool IsDeleted(this ISupportDeleted model, out DateTimeOffset? deletedAt, out string? deletedBy)
        {
            ArgumentNullException.ThrowIfNull(model);
            deletedAt = model.DeletedAt;
            deletedBy = model.DeletedBy;
            return deletedAt.HasValue;
        }

        /// <summary>
        /// Check if specified <see cref="ISupportModified"/> model is marked as modified.
        /// </summary>
        /// <param name="model">The <see cref="ISupportModified"/> model.</param>
        /// <returns><c>true</c> if the model is marked as modified; <c>false</c> otherwise.</returns>
        public static bool IsModified(this ISupportModified model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return model.ModifiedAt.HasValue;
        }

        /// <summary>
        /// Check if specified <see cref="ISupportModified"/> model is marked as modified and get the modified information.
        /// </summary>
        /// <param name="model">The <see cref="ISupportModified"/> model.</param>
        /// <param name="modifiedAt">The date and time when the model was modified.</param>
        /// <param name="modifiedBy">The user who modified the model or <c>null</c> if not available.</param>
        /// <returns><c>true</c> if the model is marked as modified; <c>false</c> otherwise.</returns>
        public static bool IsModified(this ISupportModified model, out DateTimeOffset? modifiedAt, out string? modifiedBy)
        {
            ArgumentNullException.ThrowIfNull(model);
            modifiedAt = model.ModifiedAt;
            modifiedBy = model.ModifiedBy;
            return modifiedAt.HasValue;
        }

        /// <summary>
        /// Check if specified <see cref="ISupportActivation"/> model is active.
        /// </summary>
        /// <param name="model">The <see cref="ISupportActivation"/> model.</param>
        /// <returns><c>true</c> if the model is active; <c>false</c> otherwise.</returns>
        public static bool IsActive(this ISupportActivation model)
        {
            ArgumentNullException.ThrowIfNull(model);
            return model.ActiveStatus == ActiveStatus.Active;
        }
    }
}
