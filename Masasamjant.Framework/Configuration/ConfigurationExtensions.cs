using Masasamjant.Resources;
using Microsoft.Extensions.Configuration;

namespace Masasamjant.Configuration
{
    /// <summary>
    /// Provides extensions methods to <see cref="IConfiguration"/> interface.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Gets configuration value specified by key at configuration section specified by section key.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
        /// <param name="key">The configuration key.</param>
        /// <param name="sectionKey">The configuration section key.</param>
        /// <returns>A configuration value.</returns>
        /// <exception cref="ConfigurationException">If configuration contains errors or section does not exist.</exception>
        public static string? GetValue(this IConfiguration configuration, string key, string sectionKey)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            try
            {
                var configurationSection = configuration.GetRequiredSection(sectionKey);
                return configurationSection[key];
            }
            catch (Exception exception)
            {
                throw new ConfigurationException(key, [sectionKey], "The configuration contains errors or configuration section does not exist.", exception);
            }
        }

        /// <summary>
        /// Gets configuration value specified by key either from configuration or from configuration section specified by section name.
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/>.</param>
        /// <param name="key">The configuration keys.</param>
        /// <param name="sectionKeys">The configuration section keys.</param>
        /// <returns>A configuration value.</returns>
        /// <exception cref="ConfigurationException">If configuration contains errors, section key is invalid or section does not exist.</exception>
        public static string? GetValue(this IConfiguration configuration, string key, IEnumerable<string>? sectionKeys = null)
        {
            ArgumentNullException.ThrowIfNull(configuration);

            try
            {
                if (sectionKeys != null && sectionKeys.Any())
                {
                    IConfigurationSection? configurationSection = null;

                    foreach (var sectionKey in sectionKeys)
                    {
                        if (string.IsNullOrWhiteSpace(sectionKey))
                            throw new InvalidOperationException("The section key is empty or contains only whitespace characters.");

                        configurationSection = configurationSection != null
                            ? configurationSection.GetRequiredSection(sectionKey)
                            : configuration.GetRequiredSection(sectionKey);
                    }

                    if (configurationSection == null)
                        throw new InvalidOperationException("The configuration section could not be resolved.");

                    return configurationSection[key];
                }
                else
                    return configuration[key];
            }
            catch (Exception exception)
            {
                throw new ConfigurationException(key, sectionKeys ?? [], "The configuration contains errors or configuration section does not exist.", exception);
            }
        }

        /// <summary>
        /// Gets the display name of configuration item. That is either localized resource value, if available, or name of the item.
        /// </summary>
        /// <param name="configurationItem">The configuration item.</param>
        /// <param name="resourceProvider">The string resource provider used to resolve localized or named string resources.</param>
        /// <returns>A display name of the configuration item.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configurationItem"/> is <c>null</c>.</exception>
        public static string GetDisplayName(this IConfigurationItem configurationItem, IStringResourceProvider? resourceProvider)
        {
            ArgumentNullException.ThrowIfNull(configurationItem);

            var nameResourceKey = configurationItem.NameResourceKey;

            if (resourceProvider == null || string.IsNullOrWhiteSpace(nameResourceKey))
                return configurationItem.Name;

            var nameResource = resourceProvider.GetString(nameResourceKey);
            return string.IsNullOrWhiteSpace(nameResource) ? configurationItem.Name : nameResource;
        }

        /// <summary>
        /// Gets the display description of configuration item. That is either localized resource value, if available, or description of the item.
        /// </summary>
        /// <param name="configurationItem">The configuration item.</param>
        /// <param name="resourceProvider">The string resource provider used to resolve localized or named string resources.</param>
        /// <returns>A display description of the configuration item.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="configurationItem"/> is <c>null</c>.</exception>
        public static string GetDisplayDescription(this IConfigurationItem configurationItem, IStringResourceProvider? resourceProvider)
        {
            ArgumentNullException.ThrowIfNull(configurationItem);

            var descriptionResourceKey = configurationItem.DescriptionResourceKey;
            
            if (resourceProvider == null || string.IsNullOrWhiteSpace(descriptionResourceKey))
                return configurationItem.Description;
         
            var descriptionResource = resourceProvider.GetString(descriptionResourceKey);
            return string.IsNullOrWhiteSpace(descriptionResource) ? configurationItem.Description : descriptionResource;
        }
    }
}
