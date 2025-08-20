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
        public static bool HasVersion(this ISupportVersion model) => model.Version.Length > 0;

        /// <summary>
        /// Try update specified <typeparamref name="TTarget"/> instance from specified <typeparamref name="TSource"/> instance.
        /// </summary>
        /// <typeparam name="TTarget">The type of the update target.</typeparam>
        /// <typeparam name="TSource">The type of the update source.</typeparam>
        /// <param name="target">The update target instance.</param>
        /// <param name="source">The update source instance.</param>
        /// <returns><c>true</c> if updated <paramref name="target"/> from <paramref name="source"/>; <c>false</c> otherwise.</returns>
        public static bool TryUpdate<TTarget, TSource>(this TTarget target, TSource source)
            where TTarget : IUpdateable<TSource>
            where TSource : IModel
        {
            if (ReferenceEquals(target, source))
                return false;
            
            if (target.CanUpdateFrom(source))
            { 
                target.UpdateFrom(source);
                return true;
            }

            return false;
        }
    }
}
