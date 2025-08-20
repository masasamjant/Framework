using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    /// <summary>
    /// Provides helper methods to validate model or model parameters.
    /// </summary>
    public static class ModelValidationHelper
    {
        private const string ParameterMessageFormat = "The parameter '{0}' contains invalid value.";

        /// <summary>
        /// Validate mandatory string value.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="value">The value.</param>
        /// <param name="paramName">The name of parameter.</param>
        /// <param name="allowEmpty"><c>true</c> if <paramref name="value"/> can be empty string; <c>false</c> otherwise.</param>
        /// <param name="allowWhiteSpace"><c>true</c> if <paramref name="value"/> can contain only whitespace; <c>false</c> otherwise.</param>
        /// <param name="maxLength">The maximum length.</param>
        /// <param name="minLength">The minimum length.</param>
        /// <param name="checkNoLeadingWhiteSpace"><c>true</c> to check that <paramref name="value"/> does not have leading whitespace; <c>false</c> otherwise.</param>
        /// <param name="checkNoTrailingWhiteSpace"><c>true</c> to check that <paramref name="value"/> does not have trailing whitespace; <c>false</c> otherwise.</param>
        /// <returns>A valid value.</returns>
        /// <exception cref="ModelValidationException">If value of <paramref name="value"/> is not valid.</exception>
        public static string ValidateMandatoryString(this IModel model, string? value, string paramName, bool allowEmpty = false, bool allowWhiteSpace = false, int? maxLength = null, int? minLength = null, bool checkNoLeadingWhiteSpace = false, bool checkNoTrailingWhiteSpace = false)
        {
            if (value == null)
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, "The value cannot be null."));

            if (!allowEmpty && !allowWhiteSpace && string.IsNullOrWhiteSpace(value))
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, "The value cannot be null, empty or only white-space."));

            if (!allowEmpty && allowWhiteSpace && string.IsNullOrEmpty(value))
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, "The value cannot be null or empty."));

            if (allowEmpty && !allowWhiteSpace && value.Length > 0 && value.Trim().Length == 0)
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, "The value contains only white-space."));

            if (allowWhiteSpace && value.Length > 0 && value.Trim().Length == 0)
                return value;

            if (checkNoLeadingWhiteSpace && value.Length != value.TrimStart().Length)
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, "The value cannot start with white-space."));

            if (checkNoTrailingWhiteSpace && value.Length != value.TrimEnd().Length)
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, "The value cannot end with white-space."));

            if (minLength.HasValue && minLength.Value >= 0 && value.Length < minLength.Value)
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, $"The value cannot be shorter than '{minLength.Value}' characters."));

            if (maxLength.HasValue && maxLength.Value >= 0 && value.Length > maxLength.Value)
                throw new ModelValidationException(string.Format(ParameterMessageFormat, paramName), model, new ModelError(paramName, $"The value cannot be longer than '{maxLength.Value}' characters."));

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
        /// <exception cref="ModelValidationException">If value of <paramref name="value"/> is not valid.</exception>
        public static string? ValidateOptionalString(this IModel model, string? value, string paramName, bool allowWhiteSpace = false, int? maxLength = null, int? minLength = null, bool emptyAsNull = true, bool checkNoLeadingWhiteSpace = false, bool checkNoTrailingWhiteSpace = false)
        {
            if (value == null || (emptyAsNull && value.Length == 0))
                return null;

            if (value.Length == 0 || (allowWhiteSpace && value.Trim().Length == 0))
                return value;

            return ValidateMandatoryString(model, value, paramName, true, allowWhiteSpace, maxLength, minLength, checkNoLeadingWhiteSpace, checkNoTrailingWhiteSpace);

        }
    }
}
