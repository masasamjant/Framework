using System.Reflection;

namespace Masasamjant.Plugins.Runtime
{
    /// <summary>
    /// Factory class to create runtime plugins.
    /// </summary>
    public static class RuntimePluginFactory
    {
        /// <summary>
        /// Creates instance of <see cref="IRuntimePlugin"/> implemeentation using specified plugin information.
        /// </summary>
        /// <param name="info">The plugin information.</param>
        /// <returns>An instance of <see cref="IRuntimePlugin"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="info"/> is <c>null</c>.</exception>
        /// <exception cref="InvalidOperationException">
        /// If the plugin type specified in <paramref name="info"/> cannot be found in the assembly.
        /// -or-
        /// If the plugin instance cannot be created.
        /// </exception>
        public static IRuntimePlugin CreatePlugin(PluginInfo info)
        {
            ArgumentNullException.ThrowIfNull(info);
            var context = new PluginLoadContext(info);
            var assembly = context.Load();
            var pluginType = FindPluginType(assembly, info.PluginTypeName);
            var plugin = Activator.CreateInstance(pluginType) as IPlugin;
            
            if (plugin == null)
                throw new InvalidOperationException($"Failed to create an instance of plugin type '{info.PluginTypeName}'.");

            return new RuntimePlugin(context, plugin);
        }

        private static Type FindPluginType(Assembly assembly, string pluginTypeName)
        {
            try
            {
                var pluginType = typeof(IPlugin);

                var actualType = assembly.GetTypes()
                    .FirstOrDefault(type => !type.IsAbstract && !type.IsInterface && pluginType.IsAssignableFrom(type) && type.FullName == pluginTypeName);

                if (actualType == null)
                    throw new InvalidOperationException($"Plugin type '{pluginTypeName}' not found in assembly '{assembly.FullName}'.");

                return actualType;
            }
            catch (ReflectionTypeLoadException ex)
            {
                var loaderExceptions = ex.LoaderExceptions.Select(e => e?.Message);
                var errorMessage = $"Failed to load types from assembly '{assembly.FullName}'. Loader exceptions: {string.Join("; ", loaderExceptions)}";
                throw new InvalidOperationException(errorMessage, ex);
            }
        }   
    }
}
