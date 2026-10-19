using Masasamjant.ComponentModel;

namespace Masasamjant.IO
{
    /// <summary>
    /// A disposable component that deletes a specified file when disposed.
    /// </summary>
    public sealed class FileDeleteDisposable : DisposableComponent
    { 
        /// <summary>
        /// Initializes a new instance of the <see cref="FileDeleteDisposable"/> class with a specified file path.
        /// </summary>
        /// <param name="filePath">The path of the file to be deleted when the instance is disposed.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="filePath"/> is <c>null</c>, empty or only whitespace.</exception>
        public FileDeleteDisposable(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "The file path cannot be null, empty or only whitespace.");

            FilePath = filePath;
        }

        /// <summary>
        /// Gets the path of the file that will be deleted when the instance is disposed.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileDeleteDisposable"/> class with a specified <see cref="FileInfo"/> object.
        /// </summary>
        /// <param name="fileInfo">The <see cref="FileInfo"/> object representing the file to be deleted when the instance is disposed.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="fileInfo"/> is <c>null</c>.</exception>
        public FileDeleteDisposable(FileInfo fileInfo)
        {
            ArgumentNullException.ThrowIfNull(fileInfo);
            FilePath = fileInfo.FullName;
        }

        protected override void OnDisposing()
        {
            FileHelper.TryDeleteFile(FilePath);
        }
    }
}
