using Masasamjant.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to validate that value is required if other property value match specified comparison value.
    /// </summary>
    public class RequiredWhenAttribute : PropertyValidationAttribute
    {
        /// <summary>
        /// Initializes new instance of the <see cref="RequiredWhenAttribute"/> class.
        /// </summary>
        /// <param name="otherPropertyName">The name of the other property.</param>
        /// <param name="otherPropertyDisplayName">The display name of the other property.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="otherPropertyName"/> is null, empty, or only white-space.</exception>
        public RequiredWhenAttribute(string otherPropertyName, string? otherPropertyDisplayName = null)
            : base(otherPropertyName, otherPropertyDisplayName)
        { }

        /// <summary>
        /// Gets or sets the comparison operator. Default value is <see cref="EqualityOperator.Equal"/>.
        /// </summary>
        public EqualityOperator ComparisonOperator { get; set; } = EqualityOperator.Equal;

        /// <summary>
        /// Gets or sets the comparison value.
        /// </summary>
        /// <remarks>Use this when comparing other property value to static value. Not used if <see cref="ComparisonPropertyName"/> is defined.</remarks>
        public object? ComparisonValue { get; set; }

        /// <summary>
        /// Gets or sets name of property to provide comparison value. 
        /// </summary>
        /// <remarks>Use this when comparing other property value to value of a third property. If not set, then uses <see cref="ComparisonValue"/>.</remarks>
        public string? ComparisonPropertyName { get; set; }

        /// <summary>
        /// Validates the specified value with respect to the current validation attribute.
        /// </summary>
        /// <param name="value">The value of the property being validated.</param>
        /// <param name="validationContext">The context information about the validation operation.</param>
        /// <returns>A <see cref="ValidationResult"/> indicating whether validation succeeded.</returns>
        /// <exception cref="InvalidOperationException">If the comparison value type does not match the other property type.</exception>
        /// <exception cref="NotSupportedException">If the other property type is not supported for comparison.</exception>
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var otherValue = GetOtherPropertyValue(validationContext);
            var comparisonValue = GetComparisonValue(validationContext);
            var comparisonOperator = ComparisonOperator;

            if (otherValue == null)
            {
                switch (comparisonOperator)
                {
                    case EqualityOperator.Equal:
                        if (comparisonValue == null && value == null)
                            return new ValidationResult(ErrorMessageString);
                        break;
                    case EqualityOperator.NotEqual:
                        if (comparisonValue != null && value == null)
                            return new ValidationResult(ErrorMessageString);
                        break;
                }
            }
            else 
            { 
                Type otherValueType = otherValue.GetType();

                if (comparisonValue != null && !otherValueType.Equals(comparisonValue.GetType()))
                    throw new InvalidOperationException("The comparison value type is not same as other property type.");

                if (otherValueType.Equals(typeof(string)))
                {
                    string x = (string)otherValue;
                    string? y = comparisonValue != null ? (string)comparisonValue : null;

                    if (IsMatch(x, y, comparisonOperator) && value == null)
                        return new ValidationResult(ErrorMessageString);
                }
                else if (TypeHelper.Implements(otherValueType, typeof(IComparable)))
                {
                    IComparable x = (IComparable)otherValue;
                   
                    if (IsMatch(x, comparisonValue, comparisonOperator) && value == null)
                        return new ValidationResult(ErrorMessageString);
                }
                else
                {
                    switch (comparisonOperator)
                    {
                        case EqualityOperator.Equal:
                            if (Equals(otherValue, comparisonValue) && value == null)
                                return new ValidationResult(ErrorMessageString);
                            break;
                        case EqualityOperator.NotEqual:
                            if (!Equals(otherValue, comparisonValue) && value == null)
                                return new ValidationResult(ErrorMessageString);
                            break;
                        default:
                            throw new NotSupportedException($"Comparison operator '{comparisonOperator}' is not supported if compared objects are not strings or implement IComparable.");
                    }
                }
            }

            return ValidationResult.Success;
        }

        /// <summary>
        /// Gets the values to index parameters. Invoked if <see cref="PropertyValidationAttribute.IsIndexPropertySupported"/> is override and 
        /// returns <c>true</c> and other property is index property.
        /// </summary>
        /// <param name="comparisonProperty">The comparison property.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The values to be used for the index parameters.</returns>
        protected virtual object?[]? GetComparisonPropertyIndexParameters(PropertyInfo comparisonProperty, ValidationContext validationContext)
        {
            return null;
        }

        private static bool IsMatch(string x, string? y, EqualityOperator equalityOperator)
        {
            switch (equalityOperator)
            {
                case EqualityOperator.Equal:
                    return string.Equals(x, y, StringComparison.Ordinal);
                case EqualityOperator.NotEqual:
                    return !string.Equals(x, y, StringComparison.Ordinal);
                case EqualityOperator.Contains:
                    return y != null && x.Contains(y, StringComparison.Ordinal);
                case EqualityOperator.EndsWith:
                    return y != null && x.EndsWith(y, StringComparison.Ordinal);
                case EqualityOperator.StartsWith:
                    return y != null && x.StartsWith(y, StringComparison.Ordinal);
                default:
                    return false;
            }
        }

        private static bool IsMatch(IComparable x, object? y, EqualityOperator equalityOperator)
        {
            if (y == null)
                return false;

            switch (equalityOperator)
            {
                case EqualityOperator.Equal:
                    return ComparableHelper.IsEqual(x, y);
                case EqualityOperator.NotEqual:
                    return ComparableHelper.IsNotEqual(x, y);
                case EqualityOperator.LessThan:
                    return ComparableHelper.IsLessThan(x, y);
                case EqualityOperator.LessThanOrEqual:
                    return ComparableHelper.IsLessThanOrEqual(x, y);
                case EqualityOperator.GreaterThan:
                    return ComparableHelper.IsGreaterThan(x, y);
                case EqualityOperator.GreaterThanOrEqual:
                    return ComparableHelper.IsGreaterThanOrEqual(x, y);
                default:
                    return false;
            }
        }

        private object? GetComparisonValue(ValidationContext validationContext)
        {
            var comparisonPropertyName = ComparisonPropertyName;

            if (!string.IsNullOrWhiteSpace(comparisonPropertyName))
            { 
                if (comparisonPropertyName.Equals(OtherPropertyName))
                    throw new InvalidOperationException("The comparison property cannot be same as other property.");

                if (comparisonPropertyName.Equals(validationContext.MemberName))
                    throw new InvalidOperationException("The comparison property cannot be same as the property being validated.");

                Type type = validationContext.ObjectType;
                
                var comparisonProperty = type.GetProperty(comparisonPropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

                if (comparisonProperty == null)
                    throw new InvalidOperationException($"The validated type does not contain a public instance get property of '{comparisonPropertyName}'.");

                if (comparisonProperty.IsIndexProperty())
                {
                    var indexValues = GetComparisonPropertyIndexParameters(comparisonProperty, validationContext);
                    return comparisonProperty.GetValue(validationContext.ObjectInstance, indexValues);
                }
                else
                    return comparisonProperty.GetValue(validationContext.ObjectInstance);
            }

            return ComparisonValue;
        }
    }
}
