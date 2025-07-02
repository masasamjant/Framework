using System.Reflection;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Provides helper methods to work with assemblies.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Gets path of directory of an specified <see cref="Assembly"/>.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <returns>A path to directory of <paramref name="assembly"/>.</returns>
        public static string? GetAssemblyDirectory(this Assembly assembly)
        {
            return Path.GetDirectoryName(assembly.Location);
        }
    }
}
