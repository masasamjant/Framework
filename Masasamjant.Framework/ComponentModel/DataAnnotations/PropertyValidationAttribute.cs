using Masasamjant.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Represents abstract property validation attribute to perform validation between two properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public abstract class PropertyValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PropertyValidationAttribute"/> class.
        /// </summary>
        /// <remarks><paramref name="otherPropertyName"/> is set to display name, if <paramref name="otherPropertyDisplayName"/> is <c>null</c>, empty or only white-space.</remarks>
        /// <param name="otherPropertyName">The name of the other property.</param>
        /// <param name="otherPropertyDisplayName">The display name of the other property.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="otherPropertyName"/> is null, empty, or only white-space.</exception>
        protected PropertyValidationAttribute(string otherPropertyName, string? otherPropertyDisplayName = null)
        { 
            if (string.IsNullOrWhiteSpace(otherPropertyName))
                throw new ArgumentNullException(nameof(otherPropertyName), "The name of other property cannot be null, empty, or only white-space.");

            OtherPropertyName = otherPropertyName;
            OtherPropertyDisplayName = string.IsNullOrWhiteSpace(otherPropertyDisplayName) ? otherPropertyName : otherPropertyDisplayName;
        }

        /// <summary>
        /// Gets the name of the other property.
        /// </summary>
        protected string OtherPropertyName { get; }

        /// <summary>
        /// Gets the display name of the other property.
        /// </summary>
        protected string OtherPropertyDisplayName { get; }

        /// <summary>
        /// Gets whether or not <see cref="ValidationContext"/> is required.
        /// </summary>
        public sealed override bool RequiresValidationContext => true;

        /// <summary>
        /// Gets whether or not index properties are supported.
        /// Default value is <c>false</c>.
        /// </summary>
        protected virtual bool IsIndexPropertySupported
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the values to index parameters. Invoked if <see cref="IsIndexPropertySupported"/> is override and 
        /// returns <c>true</c> and other property is index property.
        /// </summary>
        /// <param name="otherProperty">The other property.</param>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The values to be used for the index parameters.</returns>
        protected virtual object?[]? GetOtherPropertyIndexParameters(PropertyInfo otherProperty, ValidationContext validationContext)
        {
            return null;
        }

        /// <summary>
        /// Gets the value of the other property.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The value of the other property.</returns>
        /// <exception cref="InvalidOperationException">
        /// If name of other property is same as validated property name.
        /// -or-
        /// If validated type does not contain a public instance get property with other property name.
        /// </exception>
        /// <exception cref="NotSupportedException">
        /// If the other property is an index property and index properties are not supported.
        /// </exception>
        protected object? GetOtherPropertyValue(ValidationContext validationContext)
        {
            if (OtherPropertyName.Equals(validationContext.MemberName))
                throw new InvalidOperationException("The other property name cannot be the same as the validated property name.");

            Type type = validationContext.ObjectType;

            var otherProperty = type.GetProperty(OtherPropertyName, BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
        
            if (otherProperty == null)
                throw new InvalidOperationException($"The validated type does not contain a public instance get property of '{OtherPropertyName}'.");

            if (otherProperty.IsIndexProperty())
            {
                if (IsIndexPropertySupported)
                {
                    var indexValues = GetOtherPropertyIndexParameters(otherProperty, validationContext);
                    return otherProperty.GetValue(validationContext.ObjectInstance, indexValues);
                }
                else
                    throw new NotSupportedException("Properties with index parameters are not supported.");

            }
            else
                return otherProperty.GetValue(validationContext.ObjectInstance);
        }
    }
}
