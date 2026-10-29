namespace Masasamjant.Configuration
{
    /// <summary>
    /// Represents configuration property.
    /// </summary>
    public class ConfigurationProperty : IConfigurationItem
    {
        private string? value;

        /// <summary>
        /// Initializes new instance of the <see cref="ConfigurationProperty"/> class.
        /// </summary>
        /// <param name="name">The name of the property.</param>
        /// <param name="description">The description of the property.</param>
        /// <param name="value">The value of the property.</param>
        /// <param name="readOnly"><c>true</c> if property is read-only and value cannot be changed; otherwise, <c>false</c>.</param>
        public ConfigurationProperty(string name, string description, string? value = null, bool readOnly = false)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name), "The name is null, empty or contains only whitespace characters.");

            if (string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException(nameof(description), "The description is null, empty or contains only whitespace characters.");

            Name = name;
            Description = description;
            Value = value;
            IsReadOnly = readOnly;
        }

        /// <summary>
        /// Gets the property name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the property description.
        /// </summary>
        public string Description { get; }

        /// <summary>
        /// Gets or sets property value.
        /// </summary>
        public string? Value
        {
            get { return value; }
            set
            { 
                if (IsReadOnly)
                    throw new InvalidOperationException("Property is read-only and value cannot be changed.");

                this.value = value;
            }
        }

        public bool IsReadOnly { get; private set; }

        /// <summary>
        /// Gets or sets the resource key of localized name.
        /// </summary>
        public string? NameResourceKey { get; set; }

        /// <summary>
        /// Gets or sets the resource key of localized description.
        /// </summary>
        public string? DescriptionResourceKey { get; set; }

        /// <summary>
        /// Sets the instance to <c>true</c>.
        /// </summary>
        public void SetReadOnly()
        {
            IsReadOnly = true;
        }
    }
}
