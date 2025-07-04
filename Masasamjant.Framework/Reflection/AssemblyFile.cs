using System.Reflection;
using System.Runtime.Loader;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents a file that points to an assembly.
    /// </summary>
    public class AssemblyFile : IAssemblyFile, IEquatable<AssemblyFile>
    {
        /// <summary>
        /// Extension of library assembly file.
        /// </summary>
        public const string LibraryExtension = ".dll";

        /// <summary>
        /// Extension of executable assembly file.
        /// </summary>
        public const string ExecutableExtension = ".exe";

        /// <summary>
        /// Initializes new instance of the <see cref="AssemblyFile"/> class.
        /// </summary>
        /// <param name="filePath">The full absolute path to the assembly file.</param>
        /// <exception cref="ArgumentNullException">If value of <paramref name="filePath"/> is empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">
        /// If value of <paramref name="filePath"/> does not include directory path.
        /// -or-
        /// If value of <paramref name="filePath"/> does not include file name.
        /// </exception>
        /// <exception cref="NotSupportedException">If value of <paramref name="filePath"/> points to file that has not supported extension.</exception>
        public AssemblyFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "The file path cannot be empty or only whitespace.");

            var directoryPath = Path.GetDirectoryName(filePath);
            var fileName = Path.GetFileName(filePath);

            if (string.IsNullOrEmpty(directoryPath))
                throw new ArgumentException("The directory path not included.", nameof(filePath));

            if (string.IsNullOrEmpty(fileName))
                throw new ArgumentException("The file name not included.", nameof(filePath));

            DirectoryPath = directoryPath;
            FileName = fileName;
            var extension = Path.GetExtension(fileName);
            if (LibraryExtension.Equals(extension, StringComparison.OrdinalIgnoreCase))
                AssemblyType = AssemblyType.Library;
            else if (ExecutableExtension.Equals(extension, StringComparison.OrdinalIgnoreCase))
                AssemblyType = AssemblyType.Executable;
            else
                throw new NotSupportedException($"The {fileName} is not supported assembly file.");
            FullAssemblyPath = filePath;
            UseCache = false;
        }

        /// <summary>
        /// Gets the type of the assembly determined from file extension.
        /// </summary>
        public AssemblyType AssemblyType { get; }

        /// <summary>
        /// Gets the path of the directory where the assembly file is located.
        /// </summary>
        public string DirectoryPath { get; }

        /// <summary>
        /// Gets the name of the assembly file.
        /// </summary>
        public string FileName { get; }

        /// <summary>
        /// Gets the full path to the assembly file.
        /// </summary>
        public string FullAssemblyPath { get; }

        /// <summary>
        /// Gets whether or not assembly file exists.
        /// </summary>
        public bool Exists
        {
            get { return File.Exists(FullAssemblyPath); }
        }

        /// <summary>
        /// Gets or sets whether or not possibly loaded <see cref="Assembly"/> is cached.
        /// </summary>
        public bool UseCache { get; set; }

        /// <summary>
        /// Check if other <see cref="AssemblyFile"/> is equal to this.
        /// </summary>
        /// <param name="other">The other <see cref="AssemblyFile"/>.</param>
        /// <returns><c>true</c> if <paramref name="other"/> is equal to this; <c>false</c> otherwise.</returns>
        public bool Equals(AssemblyFile? other)
        {
            return other != null &&
                FullAssemblyPath.Equals(other.FullAssemblyPath, StringComparison.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Check if object instance is <see cref="AssemblyFile"/> and equal to this.
        /// </summary>
        /// <param name="obj">The object instance.</param>
        /// <returns><c>true</c> if <paramref name="obj"/> is <see cref="AssemblyFile"/> and equal to this; <c>false</c> otherwise.</returns>
        public override bool Equals(object? obj)
        {
            return Equals(obj as AssemblyFile);
        }

        /// <summary>
        /// Gets hash code for this instance.
        /// </summary>
        /// <returns>A hash code.</returns>
        public override int GetHashCode()
        {
            return FullAssemblyPath.ToLowerInvariant().GetHashCode();
        }

        /// <summary>
        /// Gets the string presentation, see <see cref="FullAssemblyPath"/>.
        /// </summary>
        /// <returns>A <see cref="FullAssemblyPath"/>.</returns>
        public override string ToString()
        {
            return FullAssemblyPath;
        }

        /// <summary>
        /// Tries to load assembly from file specified by <see cref="FullAssemblyPath"/>.
        /// </summary>
        /// <returns>A <see cref="AssemblyLoadResult"/>.</returns>
        public virtual AssemblyLoadResult TryLoad()
        {
            try
            {
                if (UseCache)
                    return AssemblyCache.TryGetAssembly(this, Load);
                else
                {
                    var assembly = Load();
                    return new AssemblyLoadResult(assembly);
                }
            }
            catch (Exception exception)
            {
                return new AssemblyLoadResult(new InvalidOperationException($"Failed to lad assembly from {FullAssemblyPath}.", exception));
            }
        }

        /// <summary>
        /// Gets the <see cref="AssemblyLoadContext"/> for loading assemblies.
        /// </summary>
        /// <returns>A <see cref="AssemblyLoadContext"/>.</returns>
        protected virtual AssemblyLoadContext GetAssemblyLoadContext() => AssemblyLoadContext.Default;

        /// <summary>
        /// Load assembly pointed by <see cref="FullAssemblyPath"/>.
        /// </summary>
        /// <returns>A <see cref="Assembly"/>.</returns>
        protected virtual Assembly Load()
        {
            var context = GetAssemblyLoadContext();
            var assembly = context.LoadFromAssemblyPath(FullAssemblyPath);
            return assembly;
        }

        /// <summary>
        /// Represents cache used by <see cref="AssemblyFile"/> to cache loaded assemblies.
        /// </summary>
        public static class AssemblyCache
        {
            private static readonly Lock mutex;
            private static readonly Dictionary<string, WeakReference<Assembly>> cache;

            static AssemblyCache()
            {
                mutex = new Lock();
                cache = new Dictionary<string, WeakReference<Assembly>>();
            }

            /// <summary>
            /// Tries to get cached assembly.
            /// </summary>
            /// <param name="assemblyFile">The <see cref="IAssemblyFile"/>.</param>
            /// <param name="getAssembly">The function to get <see cref="Assembly"/>, if not in cache.</param>
            /// <returns>A <see cref="AssemblyLoadResult"/>.</returns>
            public static AssemblyLoadResult TryGetAssembly(IAssemblyFile assemblyFile, Func<Assembly> getAssembly)
            {
                try
                {
                    lock (mutex)
                    {
                        bool hasReference = cache.TryGetValue(assemblyFile.FullAssemblyPath, out WeakReference<Assembly>? reference);

                        if (hasReference && reference != null && reference.TryGetTarget(out Assembly? assembly))
                            return new AssemblyLoadResult(assembly);

                        assembly = getAssembly();

                        if (hasReference && reference != null)
                            reference.SetTarget(assembly);
                        else
                            reference = new WeakReference<Assembly>(assembly);

                        cache[assemblyFile.FullAssemblyPath] = reference;

                        return new AssemblyLoadResult(assembly);
                    }
                }
                catch (Exception exception)
                { 
                    return new AssemblyLoadResult(new InvalidOperationException($"Failed to lad assembly from {assemblyFile.FullAssemblyPath}.", exception));
                }
            }
        }
    }
}
