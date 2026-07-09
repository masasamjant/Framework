using Masasamjant.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to ensure that all properties in a specified group have values.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public sealed class GroupRequireAllAttribute : GroupValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var groupName = GetGroupName(validationContext);

            // If group name is null or empty, then no group validation is required.
            if (string.IsNullOrWhiteSpace(groupName))
                return ValidationResult.Success;

            // Provided value is set, then no need to check other properties in the group.
            if (value == null)
                return new ValidationResult(ErrorMessage);

            var properties = validationContext.ObjectType.GetPropertiesInGroup(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, groupName);

            // Current is only one in group and it is not null, then validation succeeds.
            if (properties.Count == 1)
                return ValidationResult.Success;

            // Check of the properties in the group for non-null values.
            foreach (var property in properties)
            {
                var groupAttribute = property.GetCustomAttribute<PropertyGroupAttribute>(false);

                if (groupAttribute == null || groupAttribute.Name != groupName || property.Name == validationContext.MemberName)
                    continue;

                var propertyValue = property.GetValue(validationContext.ObjectInstance);

                // One of the property values is null, then validation fails.
                if (propertyValue == null)
                    return new ValidationResult(ErrorMessage);
            }

            // All properties in group have values, then validation succeeds.
            return ValidationResult.Success;
        }
    }
}
