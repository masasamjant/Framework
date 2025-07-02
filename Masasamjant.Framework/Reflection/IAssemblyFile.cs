namespace Masasamjant.Reflection
{
    /// <summary>
    /// Represents a file that points to an assembly.
    /// </summary>
    public interface IAssemblyFile
    {
        /// <summary>
        /// Gets the type of the assembly determined from file extension.
        /// </summary>
        AssemblyType AssemblyType { get; }

        /// <summary>
        /// Gets the path of the directory where the assembly file is located.
        /// </summary>
        string DirectoryPath { get; }

        /// <summary>
        /// Gets the name of the assembly file.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the full path to the assembly file.
        /// </summary>
        string FullAssemblyPath { get; }

        /// <summary>
        /// Gets whether or not assembly file exists.
        /// </summary>
        bool Exists { get; }

        /// <summary>
        /// Gets or sets whether or not possibly loaded <see cref="Assembly"/> is cached.
        /// </summary>
        bool UseCache { get; set; }

        /// <summary>
        /// Tries to load assembly from file specified by <see cref="FullAssemblyPath"/>.
        /// </summary>
        /// <returns>A <see cref="AssemblyLoadResult"/>.</returns>
        AssemblyLoadResult TryLoad();
    }
}
