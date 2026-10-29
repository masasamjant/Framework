using Masasamjant.Resources;

namespace Masasamjant.Configuration
{
    /// <summary>
    /// Represents abstract configuration.
    /// </summary>
    public abstract class ConfigurationBase
    {
        private IReadOnlyCollection<ConfigurationProperty>? properties;

        /// <summary>
        /// Initializes new instance of the <see cref="ConfigurationBase"/> class.
        /// </summary>
        /// <param name="groups">The collection of configuration groups.</param>
        /// <param name="resourceProvider">The string resource provider used to resolve localized or named string resources.</param>
        protected ConfigurationBase(IEnumerable<ConfigurationGroup> groups, IStringResourceProvider? resourceProvider = null)
        {
            ArgumentNullException.ThrowIfNull(groups);
            Groups = groups.ToList().AsReadOnly();
            ResourceProvider = resourceProvider;
        }

        /// <summary>
        /// Gets read-only collection of all configuration groups in this configuration.
        /// </summary>
        public IReadOnlyCollection<ConfigurationGroup> Groups { get; }

        /// <summary>
        /// Gets read-only collection of all configuration properties in all groups of this configuration.
        /// </summary>
        public IReadOnlyCollection<ConfigurationProperty> Properties
        {
            get
            {
                if (properties == null)
                    properties = Groups.SelectMany(g => g.Properties).ToList().AsReadOnly();

                return properties;
            }
        }

        /// <summary>
        /// Provides access to the string resource provider used to resolve localized or named string resources.
        /// </summary>
        public IStringResourceProvider? ResourceProvider { get; }
    }
}
