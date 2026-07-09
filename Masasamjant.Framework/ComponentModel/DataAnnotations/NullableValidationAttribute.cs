using System.ComponentModel.DataAnnotations;

namespace Masasamjant.ComponentModel.DataAnnotations
{
    /// <summary>
    /// Represents validation attribute that validates nullable values.
    /// </summary>
    public abstract class NullableValidationAttribute : ValidationAttribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether nullable values without value are allowed as valid.
        /// Default value is <c>false</c>.
        /// </summary>
        public bool AllowNullable { get; set; } = false;
    }
}
