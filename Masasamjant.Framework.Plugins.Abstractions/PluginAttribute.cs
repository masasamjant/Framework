using System;

namespace Masasamjant.Plugins
{
    /// <summary>
    /// Attribute to define plugin type in assembly.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
    public sealed class PluginAttribute : Attribute
    {
        public PluginAttribute(Type pluginType)
        {
            if (pluginType == null)
                throw new ArgumentNullException(nameof(pluginType));

            if (!typeof(IPlugin).IsAssignableFrom(pluginType))
                throw new ArgumentException($"Type '{pluginType.FullName}' does not implement '{typeof(IPlugin).FullName}'.", nameof(pluginType));

            PluginType = pluginType;
        }

        /// <summary>
        /// Gets the plugin type.
        /// </summary>
        public Type PluginType { get; }
    }
}
