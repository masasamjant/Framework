using Masasamjant.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to ensure that at least one property in a specified group has a value.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple =  false, Inherited = false)]
    public sealed class GroupRequireAnyAttribute : GroupValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var groupName = GetGroupName(validationContext);

            // If group name is null or empty, then no group validation is required.
            if (string.IsNullOrWhiteSpace(groupName))
                return ValidationResult.Success;

            // Provided value is set, then no need to check other properties in the group.
            if (value != null)
                return ValidationResult.Success;

            var properties = validationContext.ObjectType.GetPropertiesInGroup(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty, groupName);

            // Current is only one in group and it is null, then validation fails.
            if (properties.Count == 1)
                return new ValidationResult(ErrorMessage);

            // Check other properties in the group for non-null values.
            foreach (var property in properties)
            {
                var groupAttribute = property.GetCustomAttribute<PropertyGroupAttribute>(false);

                if (groupAttribute == null || groupAttribute.Name != groupName || property.Name == validationContext.MemberName)
                    continue;

                var propertyValue = property.GetValue(validationContext.ObjectInstance);

                if (propertyValue != null)
                    return ValidationResult.Success;
            }

            // If none of the properties in the group have a value, return a validation error.
            return new ValidationResult(ErrorMessage);
        }
    }
}
