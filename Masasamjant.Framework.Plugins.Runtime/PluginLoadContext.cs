using System.Reflection;
using System.Runtime.Loader;

namespace Masasamjant.Plugins.Runtime
{
    /// <summary>
    /// Represents custom <see cref="AssemblyLoadContext"/> for loading plugin assemblies.
    /// </summary>
    internal sealed class PluginLoadContext : AssemblyLoadContext
    {
        private const string PluginAbstractionsAssemblyName = "Masasamjant.Framework.Plugins.Abstractions";
        private readonly AssemblyDependencyResolver dependencyResolver;
        private readonly AssemblyName assemblyName;

        /// <summary>
        /// Initializes new instance of the <see cref="PluginLoadContext"/> class.
        /// </summary>
        /// <param name="pluginInfo">The plugin information.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="pluginInfo"/> is <c>null</c>.</exception>
        public PluginLoadContext(PluginInfo pluginInfo)
            : base(true)
        {
            ArgumentNullException.ThrowIfNull(pluginInfo);
            dependencyResolver = new AssemblyDependencyResolver(pluginInfo.AssemblyFilePath);
            assemblyName = pluginInfo.GetAssemblyName();
        }

        /// <summary>
        /// Load plugin assembly.
        /// </summary>
        /// <returns>A plugin assembly.</returns>
        public Assembly Load()
        {
            return LoadFromAssemblyName(assemblyName);
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            if (assemblyName.Name == PluginAbstractionsAssemblyName)
                return null;

            var assemblyPath = dependencyResolver.ResolveAssemblyToPath(assemblyName);

            if (!string.IsNullOrWhiteSpace(assemblyPath) && File.Exists(assemblyPath))
            {
                byte[] assemblyBytes = File.ReadAllBytes(assemblyPath);

                using (var stream = new MemoryStream(assemblyBytes))
                {
                    return LoadFromStream(stream);
                }
            }

            return null;
        }

        protected override nint LoadUnmanagedDll(string unmanagedDllName)
        {
            var nativePath = dependencyResolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            return nativePath != null ? LoadUnmanagedDllFromPath(nativePath) : nint.Zero;
        }
    }
}
