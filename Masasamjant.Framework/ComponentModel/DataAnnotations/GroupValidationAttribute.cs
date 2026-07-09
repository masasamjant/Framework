using Masasamjant.Reflection;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Represents a abstract validation attribute that validates a group of properties in a class.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public abstract class GroupValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes new instance of the <see cref="GroupValidationAttribute"/> class.
        /// </summary>
        protected GroupValidationAttribute() 
        { }

        /// <summary>
        /// Gets whether or not validation context is required.
        /// </summary>
        public sealed override bool RequiresValidationContext => true;

        /// <summary>
        /// Gets the group name from the validation context.
        /// </summary>
        /// <param name="validationContext">The validation context.</param>
        /// <returns>The group name if available; otherwise, null.</returns>
        protected static string? GetGroupName(ValidationContext validationContext)
        {
            if (string.IsNullOrWhiteSpace(validationContext.MemberName))
                return null;

            var type = validationContext.ObjectType;
            var property = type.GetProperty(validationContext.MemberName, BindingFlags.Public | BindingFlags.Instance);

            if (property == null)
                return null;

            var attribute = property.GetCustomAttribute<PropertyGroupAttribute>(false);

            return attribute?.Name;
        }
    }
}
