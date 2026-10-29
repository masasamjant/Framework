namespace Masasamjant.Configuration
{
    /// <summary>
    /// Represents configuration item.
    /// </summary>
    public interface IConfigurationItem
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the resource key of localized name.
        /// </summary>
        string? NameResourceKey { get; }

        /// <summary>
        /// Gets the resource key of localized description.
        /// </summary>
        string? DescriptionResourceKey { get; }
    }
}
