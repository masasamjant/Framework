using System.ComponentModel.DataAnnotations;

namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents abstract <see cref="IModel"/> implementation.
    /// </summary>
    public abstract class Model : IModel, IAddHandler, IUpdateHandler, IRemoveHandler
    {
        /// <summary>
        /// Gets the version bytes or empty array.
        /// </summary>
        public byte[] Version { get; protected set; } = [];

        /// <summary>
        /// Gets <see cref="Version"/> as upper-case base-64 string.
        /// </summary>
        /// <returns>A <see cref="Version"/> as upper-case base-64 string or empty string.</returns>
        public string GetVersionString()
        {
            byte[] version = Version;
            if (version.Length == 0)
                return string.Empty;
            return Convert.ToBase64String(version).ToUpperInvariant();
        }

        /// <summary>
        /// Gets the <see cref="ModelIdentity"/> of the model or <c>null</c>, if 
        /// model is new and available only on current context.
        /// </summary>
        /// <returns>A <see cref="ModelIdentity"/> or <c>null</c>.</returns>
        public ModelIdentity? GetIdentity()
        {
            var keys = GetKeyProperties();

            if (keys.Length == 0)
                return null;

            return new ModelIdentity(this, keys);
        }

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
        /// Invoked when model instance is added to non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing addition.</param>
        protected virtual void OnAdd(string? identity)
        {
            return;
        }

        /// <summary>
        /// Invoked when model instance is removed from non-volatile memory like database or file. This usually means physical delete
        /// where same model does not exist in non-volatile memory after remove.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing remove.</param>
        protected virtual void OnRemove(string? identity) 
        {
            return;
        }

        /// <summary>
        /// Invoked when model instance is updated in non-volatile memory like database or file.
        /// </summary>
        /// <param name="identity">The identity, like user name or identifier, to identify user who is performing update.</param>
        protected virtual void OnUpdate(string? identity)
        {
            return;
        }

        /// <summary>
        /// Gets values of key properties or empty array if model is new one and key(s) not assined.
        /// </summary>
        /// <returns>A values of key properties.</returns>
        protected abstract object[] GetKeyProperties();

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

        void IAddHandler.OnAdd(string? identity)
        {
            this.OnAdd(identity);
        }

        void IRemoveHandler.OnRemove(string? identity)
        {
            this.OnRemove(identity);
        }

        void IUpdateHandler.OnUpdate(string? identity)
        {
            this.OnUpdate(identity);
        }
    }
}
