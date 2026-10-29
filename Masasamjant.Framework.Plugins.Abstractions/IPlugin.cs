namespace Masasamjant.Plugins
{
    /// <summary>
    /// Represents plugin.
    /// </summary>
    public interface IPlugin
    {
        /// <summary>
        /// Gets the plugin name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the plugin description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Executes plugin with specified request.
        /// </summary>
        /// <param name="request">The plugin request.</param>
        /// <returns>A plugin response.</returns>
        IPluginResponse Execute(IPluginRequest request);
    }
}
