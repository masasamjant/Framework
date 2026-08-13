using Masasamjant.Resources.Strings;
using System.ComponentModel.DataAnnotations;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to validate that value is required if other property also has value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class RequiredWithAttribute : PropertyValidationAttribute
    {
        /// <summary>
        /// Initializes new instance of the <see cref="RequiredWithAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">The name of the other property.</param>
        /// <param name="otherPropertyDisplayName">The display name of the other property.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="otherPropertyName"/> is null, empty, or only white-space.</exception>
        public RequiredWithAttribute(string otherPropertyName, string? otherPropertyDisplayName = null)
            : base(otherPropertyName, otherPropertyDisplayName)
        { }

        /// <summary>
        /// Check if <paramref name="value"/> is valid.
        /// </summary>
        /// <param name="value">The value to validate.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation result.</returns>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherValue = GetOtherPropertyValue(validationContext);

            if (otherValue == null || (otherValue != null && value != null))
                return ValidationResult.Success;

            var message = GetValidationMessage(validationContext);
            return new ValidationResult(message);
        }

        /// <summary>
        /// Gets validation message when validation fails. 
        /// Can be overridden in derived classes to provide custom messages.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The validation message.</returns>
        protected virtual string GetValidationMessage(ValidationContext validationContext)
        {
            return string.Format(ValidationResource.RequiredWithMessage, validationContext.DisplayName, OtherPropertyDisplayName);
        }
    }
}
