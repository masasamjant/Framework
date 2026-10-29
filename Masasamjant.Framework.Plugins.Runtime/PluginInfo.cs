using System.Reflection;

namespace Masasamjant.Plugins.Runtime
{
    /// <summary>
    /// Provides information about plugin located in assembly stored in file system.
    /// </summary>
    public sealed class PluginInfo
    {
        /// <summary>
        /// Initializes new instance of the <see cref="PluginInfo"/> class.
        /// </summary>
        /// <param name="assemblyFilePath">The full path of the assembly file.</param>
        /// <param name="pluginTypeName">The full name of the plugin type.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="assemblyFilePath"/> or <paramref name="pluginTypeName"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="FileNotFoundException">If the assembly file specified by <paramref name="assemblyFilePath"/> does not exist.</exception>
        public PluginInfo(string assemblyFilePath, string pluginTypeName)
        {
            if (string.IsNullOrWhiteSpace(assemblyFilePath))
                throw new ArgumentNullException(nameof(assemblyFilePath), "Assembly file path cannot be null, empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(pluginTypeName))
                throw new ArgumentNullException(nameof(pluginTypeName), "Plugin type name cannot be null, empty or only whitespace.");

            if (!File.Exists(assemblyFilePath))
                throw new FileNotFoundException($"Assembly file '{assemblyFilePath}' does not exist.", assemblyFilePath);

            AssemblyFilePath = assemblyFilePath;
            PluginTypeName = pluginTypeName;
        }


        /// <summary>
        /// Gets the full path of the assembly file.
        /// </summary>
        public string AssemblyFilePath { get; }
    
        /// <summary>
        /// Gets the full name of the plugin type.
        /// </summary>
        public string PluginTypeName { get; }

        /// <summary>
        /// Gets the assembly name.
        /// </summary>
        /// <returns>A assembly name.</returns>
        /// <exception cref="InvalidOperationException">If failed to get assembly name from file specified by <see cref="AssemblyFilePath"/>.</exception>
        public AssemblyName GetAssemblyName()
        {
            try
            { 
                return AssemblyName.GetAssemblyName(AssemblyFilePath);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to get assembly name for '{AssemblyFilePath}'.", ex);
            }
        }
    }
}
