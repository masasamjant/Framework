namespace Masasamjant.Configuration
{
    /// <summary>
    /// Attribute to mark enumeration that can be used in configuration.
    /// </summary>
    [AttributeUsage(AttributeTargets.Enum, AllowMultiple = false, Inherited = false)]
    public sealed class ConfigurationEnumerationAttribute : Attribute
    {
    }
}
