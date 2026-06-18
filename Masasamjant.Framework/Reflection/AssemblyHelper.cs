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
            ArgumentNullException.ThrowIfNull(assembly);
            return Path.GetDirectoryName(assembly.Location);
        }

        /// <summary>
        /// Gets the file extension of specified <see cref="AssemblyType"/>.
        /// </summary>
        /// <param name="assemblyType">The type of the assembly.</param>
        /// <returns>The file extension of the specified assembly type.</returns>
        /// <exception cref="NotSupportedException">If value of <paramref name="assemblyType"/> is not supported.</exception>
        public static string GetAssemblyTypeFileExtension(AssemblyType assemblyType)
        {
            switch (assemblyType)
            {
                case AssemblyType.Library:
                    return AssemblyFile.LibraryExtension;
                case AssemblyType.Executable:
                    return AssemblyFile.ExecutableExtension;
                default:
                    throw new NotSupportedException($"The {assemblyType} is not supported assembly type.");
            }
        }

        /// <summary>
        /// Search for assemblies in specified directory and return file paths of found assemblies.
        /// </summary>
        /// <param name="folder">The folder to search for assemblies.</param>
        /// <param name="assemblyType">The type of the assembly.</param>
        /// <param name="assemblyName">The name of the assembly.</param>
        /// <param name="assemblyVersion">The version or part of the version of the assembly.</param>
        /// <param name="includeSubFolders"><c>true</c> to include subfolders in the search; otherwise, <c>false</c>.</param>
        /// <returns>A read-only collection of file paths of found assemblies.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="assemblyName"/> is null, empty, or whitespace.</exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="assemblyType"/> is not supported.</exception>
        public static IReadOnlyCollection<string> SearchAssembly(string folder, AssemblyType assemblyType, string assemblyName, string? assemblyVersion = null, bool includeSubFolders = false)
        {
            if (string.IsNullOrWhiteSpace(assemblyName))
                throw new ArgumentNullException(nameof(assemblyName), "Assembly name is null, empty or only whitespace.");

            string assemblyFileExtension = GetAssemblyTypeFileExtension(assemblyType);
            string assemblyFileName = assemblyName + assemblyFileExtension;

            var result = new List<string>();

            if (!Directory.Exists(folder))
                return result.AsReadOnly();

            foreach (var file in Directory.EnumerateFiles(folder, $"*{assemblyFileExtension}", includeSubFolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly))
            {
                if (IsSearchedAssemblyFile(file, assemblyFileName, assemblyVersion))
                    result.Add(file);
            }

            return result.AsReadOnly();
        }

        private static bool IsSearchedAssemblyFile(string file, string assemblyFileName, string? assemblyVersion)
        {
            if (file.EndsWith(assemblyFileName, StringComparison.OrdinalIgnoreCase))
            {
                if (string.IsNullOrWhiteSpace(assemblyVersion))
                {
                    return true;
                }
                else if (IsSameAssemblyVersion(file, assemblyVersion))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool IsSameAssemblyVersion(string file, string assemblyVersion)
        {
            try
            {
                var currentAssemblyName = AssemblyName.GetAssemblyName(file);
                var currentAssemblyVersion = currentAssemblyName.Version?.ToString();

                if (!string.IsNullOrWhiteSpace(currentAssemblyVersion) && currentAssemblyVersion.Contains(assemblyVersion, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            catch (BadImageFormatException)
            {
                return false;
            }

            return false;
        }
    }
}
