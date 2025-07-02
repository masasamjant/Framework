using System.Reflection;

namespace Masasamjant.Configuration
{
    /// <summary>
    /// Provides helper methods to get types decorated with <see cref="ConfigurationEnumerationAttribute"/>.
    /// </summary>
    public static class ConfigurationEnumarationHelper
    {
        /// <summary>
        /// Find <see cref="Type"/> decorated with <see cref="ConfigurationEnumerationAttribute"/> from current app domain assemblies.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="assemblySelector">The predicate to limit searched assemblies or <c>null</c>.</param>
        /// <returns>A specified <see cref="Type"/> with <see cref="ConfigurationEnumerationAttribute"/> or <c>null</c>.</returns>
        public static Type? FindConfigurationEnumerationTypeFromCurrentAppDomain(string typeName, Func<Assembly, bool>? assemblySelector = null)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            
            if (assemblySelector != null)
                assemblies = [.. assemblies.Where(assembly => assemblySelector(assembly))];
            
            return FindConfigurationEnumerationType(typeName, assemblies);
        }

        /// <summary>
        /// Find <see cref="Type"/> decorated with <see cref="ConfigurationEnumerationAttribute"/> from specified assemblies.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="assemblies">The assemblies to seach for type.</param>
        /// <returns>A specified <see cref="Type"/> with <see cref="ConfigurationEnumerationAttribute"/> or <c>null</c>.</returns>
        public static Type? FindConfigurationEnumerationType(string typeName, IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                var configurationEnumerationType = FindConfigurationEnumerationType(typeName, assembly);
                
                if (configurationEnumerationType != null)
                    return configurationEnumerationType;
            }

            return null;
        }

        /// <summary>
        /// Find <see cref="Type"/> decorated with <see cref="ConfigurationEnumerationAttribute"/> from specified assembly.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <param name="assembly">The assembly to search for type.</param>
        /// <returns>A specified <see cref="Type"/> with <see cref="ConfigurationEnumerationAttribute"/> or <c>null</c>.</returns>
        public static Type? FindConfigurationEnumerationType(string typeName, Assembly assembly)
        {
            return GetConfigurationEnumerationTypes(assembly)
                .Where(type => type.Name == typeName)
                .FirstOrDefault();
        }

        /// <summary>
        /// Gets types decorated with <see cref="ConfigurationEnumerationAttribute"/> from current app domain assemblies.
        /// </summary>
        /// <param name="assemblySelector">The predicate to limit searched assemblies or <c>null</c>.</param>
        /// <returns>A types with <see cref="ConfigurationEnumerationAttribute"/>.</returns>
        public static IEnumerable<Type> GetConfigurationEnumerationTypesFromCurrentAppDomain(Func<Assembly, bool>? assemblySelector = null)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            if (assemblySelector != null)
                assemblies = [.. assemblies.Where(assembly => assemblySelector(assembly))];
            return GetConfigurationEnumerationTypes(assemblies);
        }

        /// <summary>
        /// Gets types decorated with <see cref="ConfigurationEnumerationAttribute"/> from specified assemblies.
        /// </summary>
        /// <param name="assemblies">The assemblies to search for types.</param>
        /// <returns>A types with <see cref="ConfigurationEnumerationAttribute"/>.</returns>
        public static IEnumerable<Type> GetConfigurationEnumerationTypes(IEnumerable<Assembly> assemblies)
        {
            var types = new List<Type>();

            foreach (var assembly in assemblies)
                types.AddRange(GetConfigurationEnumerationTypes(assembly));

            return types;
        }

        /// <summary>
        /// Gets types decorated with <see cref="ConfigurationEnumerationAttribute"/> from specified assembly.
        /// </summary>
        /// <param name="assembly">The assembly to search for types.</param>
        /// <returns>A types with <see cref="ConfigurationEnumerationAttribute"/>.</returns>
        public static IEnumerable<Type> GetConfigurationEnumerationTypes(Assembly assembly)
        {
            return assembly.GetTypes()
                .Where(type => type.IsEnum && type.GetCustomAttribute<ConfigurationEnumerationAttribute>(false) != null);
        }
    }
}
