namespace Masasamjant.Configuration
{
    /// <summary>
    /// Represents group of configuration properties. This class is container of configuration properties
    /// that relates somehow to same area of configuration.
    /// </summary>
    public class ConfigurationGroup : IConfigurationItem
    {
        /// <summary>
        /// Initializes new instance of the <see cref="ConfigurationGroup"/> class.
        /// </summary>
        /// <param name="name">The name of the group.</param>
        /// <param name="description">The description of the group.</param>
        /// <param name="properties">The collection of configuration properties.</param>
        /// <exception cref="ArgumentNullException">
        /// If <paramref name="name"/> or <paramref name="description"/> is <c>null</c>, empty or only whitespace.
        /// -or-
        /// If <paramref name="properties"/> is <c>null</c>.
        /// </exception>
        public ConfigurationGroup(string name, string description, IEnumerable<ConfigurationProperty> properties)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "The name is null, empty or contains only whitespace characters.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description), "The description is null, empty or contains only whitespace characters.");

            ArgumentNullException.ThrowIfNull(properties);

            Name = name;
            Description = description;
            Properties = properties.Where(x => x is not null).ToList().AsReadOnly();
        }

        /// <summary>
        /// Gets the name of the group.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the description of the group.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets or sets the resource key of localized name.
        /// </summary>
        public string? NameResourceKey { get; set; }

        /// <summary>
        /// Gets or sets the resource key of localized description.
        /// </summary>
        public string? DescriptionResourceKey { get; set; }

        /// <summary>
        /// Gets the configuration properties.
        /// </summary>
        public IReadOnlyCollection<ConfigurationProperty> Properties { get; }
    }
}
