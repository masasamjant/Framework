using System.Reflection;

namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents result of assembly loading.
    /// </summary>
    public sealed class AssemblyLoadResult
    {
        /// <summary>
        /// Initializes new instance of the <see cref="AssemblyLoadResult"/> class that indicates that assembly was loaded successfully.
        /// </summary>
        /// <param name="assembly">The loaded assembly.</param>
        public AssemblyLoadResult(Assembly assembly)
        {
            Assembly = assembly;
            Exception = null;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="AssemblyLoadResult"/> class that indicates that assembly load faulted.
        /// </summary>
        /// <param name="exception">The occurred exception.</param>
        public AssemblyLoadResult(Exception exception)
        {
            Exception = exception;
            Assembly = null;
        }

        /// <summary>
        /// Initializes new instance of the <see cref="AssemblyLoadResult"/> class that indicates that assembly not exist.
        /// </summary>
        public AssemblyLoadResult()
        { }

        /// <summary>
        /// Gets the loaded assembly if <see cref="IsLoaded"/> is <c>true</c>.
        /// </summary>
        public Assembly? Assembly { get; }

        /// <summary>
        /// Gets the exception occurred in assembly loading if <see cref="IsFaulted"/> is <c>true</c>.
        /// </summary>
        public Exception? Exception { get; }

        /// <summary>
        /// Gets whether or not assembly load faulted; meaning that <see cref="Exception"/> is not <c>null</c>.
        /// </summary>
        public bool IsFaulted
        {
            get { return Exception != null; }
        }

        /// <summary>
        /// Gets whether or not assembly was loaded; meaning that <see cref="Assembly"/> is not <c>null</c>.
        /// </summary>
        public bool IsLoaded 
        { 
            get { return Assembly != null; }
        }

        /// <summary>
        /// Gets whether or not assembly was not found; meaning that such assembly does not exist
        /// </summary>
        public bool NotFound
        {
            get { return !IsLoaded && !IsFaulted; }
        }
    }
}
