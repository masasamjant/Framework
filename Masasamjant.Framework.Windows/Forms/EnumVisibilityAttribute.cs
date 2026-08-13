namespace Masasamjant.Windows.Forms
{
    /// <summary>
    /// Attribute applied to field to explicitly set value visibility in controls like <see cref="EnumComboBox"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    public sealed class EnumVisibilityAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets whether or not the field, where this attribute is applied, is visible to the control.
        /// </summary>
        public bool IsVisible { get; set; } = true;
    }
}
