using System.Text;

namespace Masasamjant.IO
{
    /// <summary>
    /// Provides helper methods related to files.
    /// </summary>
    public static class FileHelper
    {
        /// <summary>
        /// Check if specified file is empty.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns><c>true</c> if file specified by <paramref name="filePath"/> is empty; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="filePath"/> is empty or only whitespace.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="filePath"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If checking file fails.</exception>
        public static bool IsEmptyFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "The file path is empty or only whitespace.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file not exist.", filePath);

            try
            {
                byte[] buffer = new byte[1];

                using (var stream = File.OpenRead(filePath))
                    return stream.Read(buffer, 0, buffer.Length) == 0;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("File read failed. See inner exception.", exception);
            }
        }

        /// <summary>
        /// Check if specified file is empty.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns><c>true</c> if file specified by <paramref name="filePath"/> is empty; <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="filePath"/> is empty or only whitespace.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="filePath"/> not exist.</exception>
        /// <exception cref="InvalidOperationException">If checking file fails.</exception>
        public static async Task<bool> IsEmptyFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "The file path is empty or only whitespace.");

            if (!File.Exists(filePath))
                throw new FileNotFoundException("The file not exist.", filePath);

            try
            {
                byte[] buffer = new byte[1];

                using (var stream = File.OpenRead(filePath))
                    return await stream.ReadAsync(buffer) == 0;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException("File read failed. See inner exception.", exception);
            }
        }

        /// <summary>
        /// Create temporary file and write specified text using UTF-8 encoding.
        /// </summary>
        /// <param name="text">The text to write to file.</param>
        /// <returns>A full path to temporary file.</returns>
        public static string CreateTempTextFile(string? text)
        {
            var filePath = Path.GetTempFileName();

            if (text != null && text.Length > 0)
            {
                using (var writer = File.CreateText(filePath))
                {
                    writer.Write(text);
                    writer.Flush();
                }
            }

            return filePath;
        }

        /// <summary>
        /// Create path to temp file that does not exist.
        /// </summary>
        /// <returns>A full path to temporary file that does not exist.</returns>
        public static string CreateTempFilePath()
        {
            var filePath = Path.GetTempFileName();
            TryDeleteFile(filePath);
            return filePath;
        }

        /// <summary>
        /// Create temporary file and write specified text using UTF-8 encoding.
        /// </summary>
        /// <param name="text">The text to write to file.</param>
        /// <returns>A full path to temporary file.</returns>
        public static async Task<string> CreateTempFileAsync(string? text)
        {
            var filePath = Path.GetTempFileName();

            if (text != null && text.Length > 0)
            {
                using (var writer = File.CreateText(filePath))
                {
                    await writer.WriteAsync(text);
                    await writer.FlushAsync();
                }
            }

            return filePath;
        }

        /// <summary>
        /// Create temporary file and write specified text using specified encoding.
        /// </summary>
        /// <param name="text">The text to write to file.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>A full path to temporary file.</returns>
        public static string CreateTempFile(string? text, Encoding encoding)
        {
            byte[]? data = text != null && text.Length > 0 ? text.GetByteArray(encoding) : null;
            return CreateTempFile(data);
        }

        /// <summary>
        /// Create temporary file and write specified text using specified encoding.
        /// </summary>
        /// <param name="text">The text to write to file.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns>A full path to temporary file.</returns>
        public static async Task<string> CreateTempFileAsync(string? text, Encoding encoding)
        {
            byte[]? data = text != null && text.Length > 0 ? text.GetByteArray(encoding) : null;
            return await CreateTempFileAsync(data);
        }

        /// <summary>
        /// Create temporary file and write specified data.
        /// </summary>
        /// <param name="data">The data to write to file.</param>
        /// <returns>A full path to temporary file.</returns>
        public static string CreateTempFile(byte[]? data)
        {
            var filePath = Path.GetTempFileName();

            if (data != null && data.Length > 0)
            {
                using (var writer = File.OpenWrite(filePath))
                {
                    writer.Write(data);
                    writer.Flush();
                }
            }

            return filePath;
        }

        /// <summary>
        /// Create temporary file and write specified data.
        /// </summary>
        /// <param name="data">The data to write to file.</param>
        /// <returns>A full path to temporary file.</returns>
        public static async Task<string> CreateTempFileAsync(byte[]? data)
        {
            var filePath = Path.GetTempFileName();

            if (data != null && data.Length > 0)
            {
                using (var writer = File.OpenWrite(filePath))
                {
                    await writer.WriteAsync(data);
                    await writer.FlushAsync();
                }
            }

            return filePath;
        }

        /// <summary>
        /// Create and copy file specified by <paramref name="sourceFile"/> to temporary file.
        /// </summary>
        /// <param name="sourceFile">The path to source file to copy.</param>
        /// <returns>A full path to temporary file.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sourceFile"/> is empty or only whitespace.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="sourceFile"/> does not exist.</exception>
        /// <exception cref="InvalidOperationException">If copying source file to temporary file fails.</exception>
        public static string CopyToTempFile(string sourceFile)
        {
            ValidateSourceFile(sourceFile);

            bool failed = false;
            string? tempFilePath = null;

            try
            {
                tempFilePath = Path.GetTempFileName();

                using (var sourceStream = File.OpenRead(sourceFile))
                using (var destinationStream = File.OpenWrite(tempFilePath))
                {
                    sourceStream.CopyTo(destinationStream);
                    destinationStream.Flush();
                }

                return tempFilePath;
            }
            catch (Exception exception)
            {
                failed = true;
                throw new InvalidOperationException($"Copying source file '{sourceFile}' to temporary file failed. See inner exception.", exception);
            }
            finally
            {
                TryDeleteFile(failed, tempFilePath);
            }
        }

        /// <summary>
        /// Create and copy file specified by <paramref name="sourceFile"/> to temporary file.
        /// </summary>
        /// <param name="sourceFile">The path to source file to copy.</param>
        /// <returns>A full path to temporary file.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sourceFile"/> is empty or only whitespace.</exception>
        /// <exception cref="FileNotFoundException">If file specified by <paramref name="sourceFile"/> does not exist.</exception>
        /// <exception cref="InvalidOperationException">If copying source file to temporary file fails.</exception>
        public static async Task<string> CopyToTempFileAsync(string sourceFile)
        {
            ValidateSourceFile(sourceFile);

            bool failed = false;
            string? tempFilePath = null;

            try
            {
                tempFilePath = Path.GetTempFileName();

                using (var sourceStream = File.OpenRead(sourceFile))
                using (var destinationStream = File.OpenWrite(tempFilePath))
                {
                    await sourceStream.CopyToAsync(destinationStream);
                    await destinationStream.FlushAsync();
                }

                return tempFilePath;
            }
            catch (Exception exception)
            {
                throw new InvalidOperationException($"Copying source file '{sourceFile}' to temporary file failed. See inner exception.", exception);
            }
            finally
            {
                TryDeleteFile(failed, tempFilePath);
            }
        }

        /// <summary>
        /// Create temporary directory.
        /// </summary>
        /// <returns>A full path to temporary directory.</returns>
        public static string CreateTempDirectory()
        {
            var tempDirPath = Path.GetTempPath();
            var dirName = Guid.NewGuid().ToString(GuidFormat.N);
            var dirPath = Path.Combine(tempDirPath, dirName);
            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
            return dirPath;
        }

        /// <summary>
        /// Create temporary directory and copy content of specified source directory to temporary directory.
        /// </summary>
        /// <param name="sourceDirectory">The path to source directory to copy.</param>
        /// <returns>A full path to tempoary directory.</returns>
        /// <exception cref="ArgumentNullException">If value of <paramref name="sourceDirectory"/> is empty or only whitespace.</exception>
        /// <exception cref="DirectoryNotFoundException">If directory specified by <paramref name="sourceDirectory"/> does not exist.</exception>
        /// <exception cref="InvalidOperationException">If copying content of source directory fails.</exception>
        public static string CreateTempDirectory(string sourceDirectory)
        {
            if (string.IsNullOrWhiteSpace(sourceDirectory))
                throw new ArgumentNullException(nameof(sourceDirectory), "The source directory is empty or only whitespace.");

            if (!Directory.Exists(sourceDirectory))
                throw new DirectoryNotFoundException($"The source directory '{sourceDirectory}' not exist.");

            bool failed = false;
            string? tempDirectoryPath = null;

            try
            {
                tempDirectoryPath = CreateTempDirectory();

                var sourceFiles = Directory.GetFiles(sourceDirectory);
                CopyFiles(sourceFiles, tempDirectoryPath);

                var childDirectories = Directory.GetDirectories(sourceDirectory);
                CopyDirectories(childDirectories, tempDirectoryPath);

                return tempDirectoryPath;
            }
            catch (Exception exception)
            {
                failed = true;
                throw new InvalidOperationException($"Creating temporary directory from '{sourceDirectory}' failed. See inner exception.", exception);
            }
            finally
            {
                if (failed && Directory.Exists(tempDirectoryPath))
                    TryDeleteDirectory(tempDirectoryPath);
            }
        }

        /// <summary>
        /// Gets the size of the specified file in the given unit.
        /// </summary>
        /// <param name="filePath">The full path to the file whose size is to be determined.</param>
        /// <param name="unit">The unit in which to return the file size.</param>
        /// <returns>A size of file in specified unit.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="filePath"/> is null, empty, or consists only of white-space characters.</exception>
        /// <exception cref="FileNotFoundException">If the file does not exist.</exception>
        /// <exception cref="NotSupportedException">If the specified file size unit is not supported.</exception>
        public static long GetFileSize(string filePath, FileSizeUnit unit)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                throw new ArgumentNullException(nameof(filePath), "The file path is null, empty or only whitespace.");

            var file = new FileInfo(filePath);
            return GetFileSize(file, unit);
        }


        /// <summary>
        /// Gets the size of specified file in specified unit.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="unit">The unit in which to return the file size.</param>
        /// <returns>A size of file in specified unit.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="file"/> is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">If <paramref name="unit"/> is not defined.</exception>
        /// <exception cref="FileNotFoundException">If the file does not exist.</exception>
        /// <exception cref="NotSupportedException">If the specified file size unit is not supported.</exception>
        public static long GetFileSize(this FileInfo file, FileSizeUnit unit)
        {
            ArgumentNullException.ThrowIfNull(file);

            if (!file.Exists)
                throw new FileNotFoundException("The file not exist.", file.FullName);

            if (!Enum.IsDefined(unit))
                throw new ArgumentException("The value is not defined.", nameof(unit));

            return GetFileSize(file.Length, unit);
        }

        /// <summary>
        /// Tries to delete all the specified files.
        /// </summary>
        /// <param name="filePaths">The paths of files to delete.</param>
        /// <returns>A read-only collection of successfully deleted file paths.</returns>
        public static IReadOnlyCollection<string> DeleteFiles(params string[] filePaths)
        {
            var result = new List<string>();

            foreach (var filePath in filePaths)
            {
                if (TryDelete(filePath))
                    result.Add(filePath);
            }

            return result.AsReadOnly();
        }

        /// <summary>
        /// Tries to delete file specified by path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns><c>true</c> if file existed and was deleted; <c>false</c> otherwise.</returns>
        public static bool TryDelete(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return false;

            try
            {
                if (!File.Exists(filePath))
                    return false;

                File.Delete(filePath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static long GetFileSize(long length, FileSizeUnit unit)
        {
            return unit switch
            {
                FileSizeUnit.Bytes => length,
                FileSizeUnit.Kilobytes => length / 1024,
                FileSizeUnit.Megabytes => length / (1024 * 1024),
                FileSizeUnit.Gigabytes => length / (1024 * 1024 * 1024),
                _ => throw new NotSupportedException("File size unit not supported.")
            };
        }

        private static void CopyDirectory(string parentDirectory, string sourceDirectory)
        {
            var dir = new DirectoryInfo(sourceDirectory);
            var destinationDirectory = Path.Combine(parentDirectory, dir.Name);

            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);

            var sourceFiles = Directory.GetFiles(sourceDirectory);
            CopyFiles(sourceFiles, destinationDirectory);

            var childDirectories = Directory.GetDirectories(sourceDirectory);
            CopyDirectories(childDirectories, destinationDirectory);
        }

        /// <summary>
        /// Validates that the specified file paths are not null, or empty, or consist only of white-space characters and 
        /// that destination file path is not same as source file path and that source file exist.
        /// </summary>
        /// <param name="sourceFile">The source file path.</param>
        /// <param name="destinationFile">The destination file path.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="sourceFile"/> or <paramref name="destinationFile"/> is <c>null</c>, empty or only whitespace.</exception>
        /// <exception cref="ArgumentException">If <paramref name="destinationFile"/> is the same as <paramref name="sourceFile"/>.</exception>
        /// <exception cref="FileNotFoundException">If <paramref name="sourceFile"/> does not exist.</exception>
        public static void ValidateFilePaths(string sourceFile, string destinationFile)
        {
            if (string.IsNullOrWhiteSpace(sourceFile))
                throw new ArgumentNullException(nameof(sourceFile), "The source file path is empty or only whitespace.");

            if (string.IsNullOrWhiteSpace(destinationFile))
                throw new ArgumentNullException(nameof(destinationFile), "The destination file path is empty or only whitespace.");

            if (string.Equals(sourceFile, destinationFile, StringComparison.OrdinalIgnoreCase))
                throw new ArgumentException("The destination file path is same as source file path.", nameof(destinationFile));

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException("The source file not exist.", sourceFile);
        }

        private static void CopyFiles(string[] sourceFiles, string directoryPath)
        {
            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile = Path.Combine(directoryPath, Path.GetFileName(sourceFile));
                File.Copy(sourceFile, destinationFile, true);
            }
        }

        private static void CopyDirectories(string[] directories, string parentDirectory)
        {
            foreach (var directory in directories)
                CopyDirectory(parentDirectory, directory);
        }

        private static void ValidateSourceFile(string sourceFile)
        {
            if (string.IsNullOrWhiteSpace(sourceFile))
                throw new ArgumentNullException(nameof(sourceFile), "The source file path is empty or only whitespace.");

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException("The source file not exist.", sourceFile);
        }

        private static void TryDeleteFile(bool failed, string? filePath)
        {
            if (failed && filePath != null && File.Exists(filePath))
                TryDeleteFile(filePath);
        }

        private static void TryDeleteFile(string filePath)
        {
            try
            {
                File.Delete(filePath);
            }
            catch
            {
                return;
            }
        }

        private static void TryDeleteDirectory(string directoryPath)
        {
            try
            {
                Directory.Delete(directoryPath, true);
            }
            catch
            {
                return;
            }
        }
    }
}
