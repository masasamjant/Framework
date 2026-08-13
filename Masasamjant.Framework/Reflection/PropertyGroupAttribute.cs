namespace Masasamjant.Reflection
{
    /// <summary>
    /// Attribute used to group properties.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = false)]
    public sealed class PropertyGroupAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PropertyGroupAttribute"/> class.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="name"/> is <c>null</c>, empty or only white-space.</exception>
        public PropertyGroupAttribute(string name) 
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "The group name cannot be null, empty or only white-space.");

            Name = name;
        }

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        public string Name { get; }
    }
}
