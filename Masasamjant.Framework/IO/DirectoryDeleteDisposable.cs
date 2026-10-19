using Masasamjant.ComponentModel;

namespace Masasamjant.IO
{
    /// <summary>
    /// A disposable component that deletes a specified directory when disposed.
    /// </summary>
    public sealed class DirectoryDeleteDisposable : DisposableComponent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryDeleteDisposable"/> class with a specified directory path.
        /// </summary>
        /// <param name="directoryPath">The path of the directory to be deleted when the instance is disposed.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="directoryPath"/> is <c>null</c>, empty or only whitespace.</exception>
        public DirectoryDeleteDisposable(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
                throw new ArgumentNullException(nameof(directoryPath), "The directory path cannot be null, empty or only whitespace.");
            
            DirectoryPath = directoryPath;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirectoryDeleteDisposable"/> class with a specified <see cref="DirectoryInfo"/> object.
        /// </summary>
        /// <param name="directoryInfo">The <see cref="DirectoryInfo"/> object representing the directory to be deleted when the instance is disposed.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="directoryInfo"/> is <c>null</c>.</exception>
        public DirectoryDeleteDisposable(DirectoryInfo directoryInfo)
        {
            ArgumentNullException.ThrowIfNull(directoryInfo);
            DirectoryPath = directoryInfo.FullName;
        }

        /// <summary>
        /// Gets the path of the directory that will be deleted when the instance is disposed.
        /// </summary>
        public string DirectoryPath { get; }

        protected override void OnDisposing()
        {
            FileHelper.TryDeleteDirectory(DirectoryPath);
        }
    }
}
