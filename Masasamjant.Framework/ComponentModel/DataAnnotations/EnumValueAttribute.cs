using Masasamjant.Resources.Strings;
using System.ComponentModel.DataAnnotations;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Validation attribute to validate that enumeration value is defined.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public sealed class EnumValueAttribute : ValidationAttribute
    {
        /// <summary>
        /// Initializes new instance of the <see cref="EnumValueAttribute"/> class.
        /// </summary>
        /// <param name="enumType">The type of the enumeration.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="enumType"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="enumType"/> is not enumeration type.</exception>
        public EnumValueAttribute(Type enumType)
        {
            ArgumentNullException.ThrowIfNull(enumType);

            if (!enumType.IsEnum)
                throw new ArgumentException("The type is not an enumeration type.", nameof(enumType));

            EnumType = enumType;
            ErrorMessageResourceType = typeof(ValidationResource);
            ErrorMessageResourceName = nameof(ValidationResource.EnumValueAttribute);
        }

        /// <summary>
        /// Gets the enumeration type.
        /// </summary>
        public Type EnumType { get; }

        /// <summary>
        /// Gets if requires validation context.
        /// </summary>
        public override bool RequiresValidationContext => false;

        /// <summary>
        /// Validates that the value is a valid enumeration value.
        /// </summary>
        /// <param name="value">The value to validate or <c>null</c>.</param>
        /// <returns><c>true</c> if the value is <c>null</c> or valid enumeration value; otherwise, <c>false</c>.</returns>
        public override bool IsValid(object? value)
        {
            if (value == null)
                return true;

            return EnumType.Equals(value.GetType())
                && Enum.IsDefined(EnumType, value);
        }
    }
}
