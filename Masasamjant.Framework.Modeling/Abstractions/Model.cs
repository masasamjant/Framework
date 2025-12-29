using Masasamjant.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Masasamjant.Modeling.Abstractions
{
    /// <summary>
    /// Represents model in application.
    /// </summary>
    public abstract class Model : IModel, IAddHandler, IUpdateHandler, IRemoveHandler, IJsonSerializable
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

    /// <summary>
    /// Represents model in application that is identified by <typeparamref name="TIdentifier"/>.
    /// </summary>
    /// <typeparam name="TIdentifier">The type of the identifier.</typeparam>
    public abstract class Model<TIdentifier> : Model, IModel<TIdentifier>
        where TIdentifier : IEquatable<TIdentifier>, new()
    {
        /// <summary>
        /// Initializes new instance of the <see cref="Model{TIdentifier}"/> class.
        /// </summary>
        protected Model()
        { }

        /// <summary>
        /// Gets the unique identifier.
        /// </summary>
        [JsonInclude]
        public TIdentifier Identifier { get; protected set; } = new TIdentifier();

        /// <summary>
        /// Check if object instance is equal to this model.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> has same type and identifier is equal to this identifier; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            if (obj != null && obj.GetType().Equals(GetType()) && obj is Model<TIdentifier> other)
            {
                return Equals(Identifier, other.Identifier);
            }

            return false;
        }

        /// <summary>
        /// Gets hash code.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(GetType(), Identifier);
        }

    }
}
