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
        public static string CreateTempFile(string? text)
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
                if (failed && File.Exists(tempFilePath))
                    TryDeleteFile(tempFilePath);
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
                if (failed && File.Exists(tempFilePath))
                    TryDeleteFile(tempFilePath);
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

                foreach (var sourceFile in sourceFiles)
                {
                    var destinationFile = Path.Combine(tempDirectoryPath, Path.GetFileName(sourceFile));
                    File.Copy(sourceFile, destinationFile, true);
                }

                var childDirectories = Directory.GetDirectories(sourceDirectory);

                foreach (var childDirectory in childDirectories)
                    CopyDirectory(tempDirectoryPath, childDirectory);

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

        private static void CopyDirectory(string parentDirectory, string sourceDirectory)
        {
            var dir = new DirectoryInfo(sourceDirectory);
            var destinationDirectory = Path.Combine(parentDirectory, dir.Name);
            if (!Directory.Exists(destinationDirectory))
                Directory.CreateDirectory(destinationDirectory);
            var sourceFiles = Directory.GetFiles(sourceDirectory);

            foreach (var sourceFile in sourceFiles)
            {
                var destinationFile = Path.Combine(destinationDirectory, Path.GetFileName(sourceFile));
                File.Copy(sourceFile, destinationFile, true);
            }

            var childDirectories = Directory.GetDirectories(sourceDirectory);

            foreach (var childDirectory in childDirectories)
                CopyDirectory(destinationDirectory, childDirectory);
        }

        private static void ValidateSourceFile(string sourceFile)
        {
            if (string.IsNullOrWhiteSpace(sourceFile))
                throw new ArgumentNullException(nameof(sourceFile), "The source file path is empty or only whitespace.");

            if (!File.Exists(sourceFile))
                throw new FileNotFoundException("The source file not exist.", sourceFile);
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
