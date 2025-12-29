using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents abstract model.
    /// </summary>
    public abstract class Model : IModel, IAddHandler, IUpdateHandler, IRemoveHandler
    {
        /// <summary>
        /// Validates model state.
        /// </summary>
        /// <exception cref="ModelValidationException">If model is not valid.</exception>
        public virtual void Validate()
        {
            try
            {
                var validationContext = new ValidationContext(this);
                Validator.ValidateObject(this, validationContext, true);
            }
            catch (ValidationException exception)
            {
                throw HandleValidationException(exception);
            }
        }

        /// <summary>
        /// Gets the current <see cref="DateTime"/> using <see cref="DateTimeConfigurationProvider.Current"/> provider.
        /// </summary>
        /// <returns>A current <see cref="DateTime"/>.</returns>
        protected static DateTime GetDateTimeNow()
        {
            return DateTimeConfigurationProvider.Current.Configuration.GetDateTimeNow();
        }

        /// <summary>
        /// Gets the current <see cref="DateTimeOffset"/> using <see cref="DateTimeConfigurationProvider.Current"/> provider.
        /// </summary>
        /// <returns>A current <see cref="DateTimeOffset"/>.</returns>
        protected static DateTimeOffset GetDateTimeOffsetNow()
        {
            return DateTimeConfigurationProvider.Current.Configuration.GetDateTimeOffsetNow();
        }

        private ModelValidationException HandleValidationException(ValidationException validationException)
        {
            var errors = new List<ModelError>();

            foreach (var memberName in validationException.ValidationResult.MemberNames)
            {
                var errorMessage = validationException.ValidationAttribute?.FormatErrorMessage(memberName) ?? validationException.ValidationResult.ErrorMessage ?? "The property is not valid.";
                errors.Add(new ModelError(memberName, errorMessage));
            }

            return new ModelValidationException(this, errors);
        }

        /// <summary>
        /// Invoked when model instance is added to non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity to identify who is performing addition.</param>
        protected virtual void OnAdd(IIdentity? identity)
        {
            return;
        }

        /// <summary>
        /// Invoked when model instance is updated in non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity to identify who is performing update.</param>
        protected virtual void OnUpdate(IIdentity? identity)
        {
            return;
        }

        /// <summary>
        /// Invoked when model instance is removed from non-volatile memory like database or file. This usually means physical delete
        /// where same model does not exist in non-volatile memory after remove.
        /// </summary>
        /// <param name="identity">The identity to identify who is performing remove.</param>
        protected virtual void OnRemove(IIdentity? identity)
        {
            return;
        }

        void IAddHandler.OnAdd(IIdentity? identity)
        {
            this.OnAdd(identity);
        }

        void IUpdateHandler.OnUpdate(IIdentity? identity)
        {
            this.OnUpdate(identity);
        }

        void IRemoveHandler.OnRemove(IIdentity? identity)
        {
            this.OnRemove(identity);
        }
    }
}
