namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents an attribute that indicates that a property should be ignored in reflection.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class IgnorePropertyAttribute : Attribute
    {
    }
}
