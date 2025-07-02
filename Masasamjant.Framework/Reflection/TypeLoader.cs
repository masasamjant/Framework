using System.Reflection;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents a type loader that can load type from an assembly.
    /// </summary>
    public class TypeLoader
    {
        /// <summary>
        /// Try load type specified by name from the assembly.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="typeName">The type name.</param>
        /// <returns>A <see cref="TypeLoadResult"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="typeName"/> is empty or only white-space.</exception>
        public virtual TypeLoadResult TryLoadType(Assembly assembly, string typeName)
        {
            ValidateTypeName(typeName);

            try
            {
                var type = assembly.GetType(typeName);

                if (type == null)
                    return new TypeLoadResult();

                return new TypeLoadResult(type);
            }
            catch (Exception exception)
            {
                return new TypeLoadResult(exception);
            }
        }

        /// <summary>
        /// Try load type specified by name from the assembly specified by <paramref name="assemblyFile"/>.
        /// </summary>
        /// <param name="assemblyFile">The <see cref="IAssemblyFile"/>.</param>
        /// <param name="typeName">The type name.</param>
        /// <returns>A <see cref="TypeLoadResult"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="typeName"/> is empty or only white-space.</exception>
        public virtual TypeLoadResult TryLoadType(IAssemblyFile assemblyFile, string typeName)
        {
            ValidateTypeName(typeName);

            var assemblyLoadResult = assemblyFile.TryLoad();

            if (assemblyLoadResult.IsLoaded && assemblyLoadResult.Assembly != null)
                return TryLoadType(assemblyLoadResult.Assembly, typeName);

            return new TypeLoadResult(new InvalidOperationException("Loading assembly from specified file failed.", assemblyLoadResult.Exception));
        }

        /// <summary>
        /// Try load type specified by name from all assemblies loaded in the specified application domain.
        /// </summary>
        /// <param name="domain">The application domain.</param>
        /// <param name="typeName">The type name.</param>
        /// <returns>A <see cref="TypeLoadResult"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="typeName"/> is empty or only white-space.</exception>
        public virtual TypeLoadResult TryLoadType(AppDomain domain, string typeName)
        {
            ValidateTypeName(typeName);

            try
            {
                var assemblies = domain.GetAssemblies();

                foreach (var assembly in assemblies)
                {
                    var result = TryLoadType(assembly, typeName);

                    if (result.IsLoaded || result.IsFaulted)
                        return result;
                }

                return new TypeLoadResult();
            }
            catch (Exception exception)
            {
                return new TypeLoadResult(exception);
            }
        }

        /// <summary>
        /// Try load type specified by name from all assemblies loaded in the current application domain.
        /// </summary>
        /// <param name="typeName">The type name.</param>
        /// <returns>A <see cref="TypeLoadResult"/>.</returns>
        /// <exception cref="ArgumentException">If <paramref name="typeName"/> is empty or only white-space.</exception>
        public TypeLoadResult TryLoadType(string typeName)
        {
            return TryLoadType(AppDomain.CurrentDomain, typeName);
        }

        /// <summary>
        /// Validates that type name is not null, empty or only white-space.
        /// </summary>
        /// <param name="typeName"></param>
        /// <exception cref="ArgumentException"></exception>
        protected static void ValidateTypeName(string typeName)
        {
            if (string.IsNullOrWhiteSpace(typeName))
                throw new ArgumentException("The type name cannot be empty or only white-space.", nameof(typeName));
        }
    }
}
