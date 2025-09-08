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

        /// <summary>
        /// Validate mandatory string value.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="paramName">The name of parameter.</param>
        /// <param name="allowEmpty"><c>true</c> if <paramref name="value"/> can be empty string; <c>false</c> otherwise.</param>
        /// <param name="allowWhiteSpace"><c>true</c> if <paramref name="value"/> can contain only whitespace; <c>false</c> otherwise.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="checkNoLeadingWhiteSpace"><c>true</c> to check that <paramref name="value"/> does not have leading whitespace; <c>false</c> otherwise.</param>
        /// <param name="checkNoTrailingWhiteSpace"><c>true</c> to check that <paramref name="value"/> does not have trailing whitespace; <c>false</c> otherwise.</param>
        /// <returns>A valid value.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="value"/> is not valid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="value"/> is shorter than <paramref name="minLength"/> or longer than <paramref name="maxLength"/>.</exception>
        protected static string ValidateMandatoryString(string? value, string paramName, bool allowEmpty = false, bool allowWhiteSpace = false, int? maxLength = null, int? minLength = null, bool checkNoLeadingWhiteSpace = false, bool checkNoTrailingWhiteSpace = false)
        {
            if (value == null)
                throw new ArgumentException("The value cannot be null.", paramName);

            if (!allowEmpty && !allowWhiteSpace && string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("The value cannot be null, empty or only white-space.", paramName);

            if (!allowEmpty && allowWhiteSpace && string.IsNullOrEmpty(value))
                throw new ArgumentException("The value cannot be null or empty.", paramName);

            if (allowEmpty && !allowWhiteSpace && value.Length > 0 && value.Trim().Length == 0)
                throw new ArgumentException("The value contains only white-space.", paramName);

            if (allowWhiteSpace && value.Length > 0 && value.Trim().Length == 0)
                return value;

            if (checkNoLeadingWhiteSpace && value.Length != value.TrimStart().Length)
                throw new ArgumentException("The value cannot start with whitespace.", paramName);

            if (checkNoTrailingWhiteSpace && value.Length != value.TrimEnd().Length)
                throw new ArgumentException("The value cannot end with whitespace.", paramName);

            if (minLength.HasValue && minLength.Value >= 0 && value.Length < minLength.Value)
                throw new ArgumentOutOfRangeException(paramName, value.Length, $"The value cannot be shorter than '{minLength.Value}' characters.");

            if (maxLength.HasValue && maxLength.Value >= 0 && value.Length > maxLength.Value)
                throw new ArgumentOutOfRangeException(paramName, value.Length, $"The value cannot be longer than '{maxLength.Value}' characters.");

            return value;
        }

        /// <summary>
        /// Validate optional string value.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="value">The value.</param>
        /// <param name="paramName">The name of parameter.</param>
        /// <param name="allowWhiteSpace"><c>true</c> if <paramref name="value"/> can contain only whitespace; <c>false</c> otherwise.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="emptyAsNull"><c>true</c> to return <c>null</c> if <paramref name="value"/> is empty; <c>false</c> to return empty.</param>
        /// <param name="checkNoLeadingWhiteSpace"><c>true</c> to check that <paramref name="value"/> does not have leading whitespace; <c>false</c> otherwise.</param>
        /// <param name="checkNoTrailingWhiteSpace"><c>true</c> to check that <paramref name="value"/> does not have trailing whitespace; <c>false</c> otherwise.</param>
        /// <returns>A valid value, empty string or <c>null</c>.</returns>
        /// <exception cref="ArgumentException">If value of <paramref name="value"/> is not valid.</exception>
        /// <exception cref="ArgumentOutOfRangeException">If value of <paramref name="value"/> is shorter than <paramref name="minLength"/> or longer than <paramref name="maxLength"/>.</exception>
        public static string? ValidateOptionalString(string? value, string paramName, bool allowWhiteSpace = false, int? maxLength = null, int? minLength = null, bool emptyAsNull = true, bool checkNoLeadingWhiteSpace = false, bool checkNoTrailingWhiteSpace = false)
        {
            if (value == null || (emptyAsNull && value.Length == 0))
                return null;

            if (value.Length == 0 || (allowWhiteSpace && value.Trim().Length == 0))
                return value;

            return ValidateMandatoryString(value, paramName, true, allowWhiteSpace, maxLength, minLength, checkNoLeadingWhiteSpace, checkNoTrailingWhiteSpace);
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
        where TIdentifier : IEquatable<TIdentifier>
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
        public TIdentifier Identifier { get; protected set; } = default!;

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
