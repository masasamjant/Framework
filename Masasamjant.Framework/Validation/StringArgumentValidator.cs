namespace Masasamjant.Validation
{
    /// <summary>
    /// Represents validator of string arguments.
    /// </summary>
    public static class StringArgumentValidator
    {
        /// <summary>
        /// Validate mandatory string argument value.
        /// </summary>
        /// <param name="value">The argument value.</param>
        /// <param name="paramName">The name of parameter.</param>
        /// <param name="validationContext">The <see cref="StringArgumentValidationContext"/> or <c>null</c> to use <see cref="StringArgumentValidationContext.Default"/>.</param>
        /// <returns>A valid value.</returns>
        /// <exception cref="StringArgumentException">If value of <paramref name="value"/> is not valid.</exception>
        public static string ValidateMandatoryStringArgument(string? value, string paramName, StringArgumentValidationContext? validationContext = null)
        {
            validationContext ??= StringArgumentValidationContext.Default;

            if (value == null)
                throw new StringArgumentException("The value cannot be null.", value, paramName);

            if (!validationContext.AllowEmpty && !validationContext.AllowWhiteSpace && string.IsNullOrWhiteSpace(value))
                throw new StringArgumentException("The value cannot be null, empty or only white-space.", value, paramName);

            if (!validationContext.AllowEmpty && validationContext.AllowWhiteSpace && string.IsNullOrEmpty(value))
                throw new StringArgumentException("The value cannot be null or empty.", value, paramName);

            if (validationContext.AllowEmpty && !validationContext.AllowWhiteSpace && value.Length > 0 && value.Trim().Length == 0)
                throw new StringArgumentException("The value contains only white-space.", value, paramName);

            if (validationContext.AllowWhiteSpace && value.Length > 0 && value.Trim().Length == 0)
                return value;

            if (validationContext.CheckNoLeadingWhiteSpace && value.Length != value.TrimStart().Length)
                throw new StringArgumentException("The value cannot start with whitespace.", value, paramName);

            if (validationContext.CheckNoTrailingWhiteSpace && value.Length != value.TrimEnd().Length)
                throw new StringArgumentException("The value cannot end with whitespace.", value, paramName);

            if (validationContext.MinLength.HasValue && validationContext.MinLength.Value >= 0 && value.Length < validationContext.MinLength.Value)
                throw new StringArgumentException(paramName, value, $"The value cannot be shorter than '{validationContext.MinLength.Value}' characters.");

            if (validationContext.MaxLength.HasValue && validationContext.MaxLength.Value >= 0 && value.Length > validationContext.MaxLength.Value)
                throw new StringArgumentException(paramName, value, $"The value cannot be longer than '{validationContext.MaxLength.Value}' characters.");

            return value;
        }

        /// <summary>
        /// Validate optional string argument value.
        /// </summary>
        /// <param name="value">The argument value.</param>
        /// <param name="paramName">The name of parameter.</param>
        /// <param name="validationContext">The <see cref="StringArgumentValidationContext"/> or <c>null</c> to use <see cref="StringArgumentValidationContext.Default"/>.</param>
        /// <returns>A valid value.</returns>
        /// <exception cref="StringArgumentException">If value of <paramref name="value"/> is not valid.</exception>
        public static string? ValidateOptionalStringArgument(string? value, string paramName, StringArgumentValidationContext? validationContext = null)
        {
            validationContext ??= StringArgumentValidationContext.Default;

            if (value == null || (validationContext.EmptyAsNull && value.Length == 0))
                return null;

            if (value.Length == 0 || (validationContext.AllowWhiteSpace && value.Trim().Length == 0))
                return value;

            return ValidateMandatoryStringArgument(value, paramName, validationContext);
        }
    }
}
