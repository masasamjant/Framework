using System.Text;

namespace Masasamjant.IO
{
    [TestClass]
    public class FileHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsEmptyFile()
        {
            var filePath = Path.GetTempFileName();
            Assert.IsTrue(FileHelper.IsEmptyFile(filePath));
            File.WriteAllText(filePath, "Test");
            Assert.IsFalse(FileHelper.IsEmptyFile(filePath));
            File.Delete(filePath);
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.IsEmptyFile(""));
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.IsEmptyFile("  "));
            Assert.ThrowsException<FileNotFoundException>(() => FileHelper.IsEmptyFile(NotExistFilePath));
        }

        [TestMethod]
        public async Task Test_IsEmptyFileAsync()
        {
            var filePath = Path.GetTempFileName();
            Assert.IsTrue(await FileHelper.IsEmptyFileAsync(filePath));
            File.WriteAllText(filePath, "Test");
            Assert.IsFalse(await FileHelper.IsEmptyFileAsync(filePath));
            File.Delete(filePath);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => FileHelper.IsEmptyFileAsync(""));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => FileHelper.IsEmptyFileAsync("  "));
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => FileHelper.IsEmptyFileAsync(NotExistFilePath));
        }

        [TestMethod]
        public void Test_CreateTempFile()
        {
            var tempFilePath = FileHelper.CreateTempTextFile((string?)null);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempTextFile("");
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempTextFile("Text");
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.AreEqual("Text", File.ReadAllText(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempFile("Text", Encoding.Unicode);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.AreEqual("Text", File.ReadAllText(tempFilePath, Encoding.Unicode));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempFile(null, Encoding.Unicode);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempFile("", Encoding.Unicode);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempFile([]);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempFile((byte[]?)null);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            byte[]? data = Encoding.UTF8.GetBytes("Test");
            tempFilePath = FileHelper.CreateTempFile(data);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsFalse(FileHelper.IsEmptyFile(tempFilePath));
            byte[]? res = File.ReadAllBytes(tempFilePath);
            CollectionAssert.AreEqual(data, res);
            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task Test_CreateTempFileAsync()
        {
            var tempFilePath = await FileHelper.CreateTempFileAsync((string?)null);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(await FileHelper.IsEmptyFileAsync(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = await FileHelper.CreateTempFileAsync("");
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(await FileHelper.IsEmptyFileAsync(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = await FileHelper.CreateTempFileAsync("Text");
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.AreEqual("Text", File.ReadAllText(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = await FileHelper.CreateTempFileAsync("Text", Encoding.Unicode);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.AreEqual("Text", File.ReadAllText(tempFilePath, Encoding.Unicode));
            File.Delete(tempFilePath);

            tempFilePath = await FileHelper.CreateTempFileAsync(null, Encoding.Unicode);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(await FileHelper.IsEmptyFileAsync(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = await FileHelper.CreateTempFileAsync("", Encoding.Unicode);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(await FileHelper.IsEmptyFileAsync(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = await FileHelper.CreateTempFileAsync([]);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(await FileHelper.IsEmptyFileAsync(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = await FileHelper.CreateTempFileAsync((byte[]?)null);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(await FileHelper.IsEmptyFileAsync(tempFilePath));
            File.Delete(tempFilePath);

            byte[]? data = Encoding.UTF8.GetBytes("Test");
            tempFilePath = await FileHelper.CreateTempFileAsync(data);
            Assert.IsTrue(File.Exists(tempFilePath));
            byte[]? res = File.ReadAllBytes(tempFilePath);
            CollectionAssert.AreEqual(data, res);
            File.Delete(tempFilePath);
        }

        [TestMethod]
        public void Test_CopyToTempFile()
        {
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.CopyToTempFile(""));
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.CopyToTempFile("  "));
            Assert.ThrowsException<FileNotFoundException>(() => FileHelper.CopyToTempFile(NotExistFilePath));
            var sourceFile = FileHelper.CreateTempTextFile("Content");
            var tempFilePath = FileHelper.CopyToTempFile(sourceFile);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsFalse(FileHelper.IsEmptyFile(tempFilePath));
            Assert.AreEqual("Content", File.ReadAllText(tempFilePath));
            File.Delete(tempFilePath);
            File.Delete(sourceFile);
        }

        [TestMethod]
        public async Task Test_CopyToTempFileAsync()
        {
            var sourceFile = FileHelper.CreateTempTextFile("Content");
            var tempFilePath = await FileHelper.CopyToTempFileAsync(sourceFile);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsFalse(FileHelper.IsEmptyFile(tempFilePath));
            Assert.AreEqual("Content", File.ReadAllText(tempFilePath));
            File.Delete(tempFilePath);
            File.Delete(sourceFile);
        }

        [TestMethod]
        public void Test_CreateTempDirectory()
        {
            var tempDirPath = FileHelper.CreateTempDirectory();
            Assert.IsTrue(Directory.Exists(tempDirPath));
            Directory.Delete(tempDirPath);

            var sourceDirectory = FileHelper.CreateTempDirectory();
            var childDirectory = Path.Combine(sourceDirectory, "Child");
            var grandChildDirectory = Path.Combine(childDirectory, "Grand");
            Directory.CreateDirectory(childDirectory);
            Directory.CreateDirectory(grandChildDirectory);
            File.WriteAllText(Path.Combine(grandChildDirectory, "innermost.txt"), "inner most");
            File.WriteAllText(Path.Combine(childDirectory, "inner.txt"), "inner");
            File.WriteAllText(Path.Combine(sourceDirectory, "outer.txt"), "outer");

            tempDirPath = FileHelper.CreateTempDirectory(sourceDirectory);
            Assert.IsTrue(Directory.Exists(tempDirPath));
            Assert.IsTrue(Directory.Exists(Path.Combine(tempDirPath, "Child")));
            Assert.IsTrue(Directory.Exists(Path.Combine(tempDirPath, "Child", "Grand")));
            Assert.IsTrue(File.Exists(Path.Combine(tempDirPath, "outer.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(tempDirPath, "Child", "inner.txt")));
            Assert.IsTrue(File.Exists(Path.Combine(tempDirPath, "Child", "Grand", "innermost.txt")));
            Directory.Delete(tempDirPath, true);

            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.CreateTempDirectory(""));
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.CreateTempDirectory("   "));
            Assert.ThrowsException<DirectoryNotFoundException>(() => FileHelper.CreateTempDirectory(@"C:\NOTEXISTS"));
        }

        [TestMethod]
        public void Test_CreateTempFilePath()
        {
            var tempFilePath = FileHelper.CreateTempFilePath();
            Assert.IsFalse(File.Exists(tempFilePath));
        }

        [TestMethod]
        public void Test_GetFileSize()
        {
            Assert.AreEqual(100L, FileHelper.GetFileSize(100L, FileSizeUnit.Bytes));
            Assert.AreEqual(10000L / 1024L, FileHelper.GetFileSize(10000L, FileSizeUnit.Kilobytes));
            Assert.AreEqual(100000L / (1024 * 1024), FileHelper.GetFileSize(100000L, FileSizeUnit.Megabytes));
            Assert.AreEqual(1000000000L / (1024 * 1024 * 1024), FileHelper.GetFileSize(1000000000L, FileSizeUnit.Gigabytes));
            Assert.ThrowsException<NotSupportedException>(() => FileHelper.GetFileSize(100L, (FileSizeUnit)100));
        }

        [TestMethod]
        public void Test_GetFileSize_WhenFileNull_ThenThrows()
        {
            FileInfo? file = null;
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.GetFileSize(file!, FileSizeUnit.Bytes));
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.GetFileSize((string?)null!, FileSizeUnit.Bytes));
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.GetFileSize("", FileSizeUnit.Bytes));
            Assert.ThrowsException<ArgumentNullException>(() => FileHelper.GetFileSize("   ", FileSizeUnit.Bytes));
        }

        [TestMethod]
        public void Test_GetFileSize_WhenFileNotExists_ThenThrows()
        {
            var file = new FileInfo(NotExistFilePath);
            Assert.ThrowsException<FileNotFoundException>(() => FileHelper.GetFileSize(file, FileSizeUnit.Bytes));
        }

        [TestMethod]
        public void Test_GetFileSize_WhenUnitInvalid_ThenThrows()
        {
            var file = FileHelper.CreateTempTextFile("Test");
            Assert.ThrowsException<ArgumentException>(() => FileHelper.GetFileSize(file, (FileSizeUnit)100));
            File.Delete(file);
        }

        [TestMethod]
        public void Test_TryDelete()
        {
            Assert.IsFalse(FileHelper.TryDelete(null!));
            Assert.IsFalse(FileHelper.TryDelete(""));
            Assert.IsFalse(FileHelper.TryDelete("   "));
            Assert.IsFalse(FileHelper.TryDelete(NotExistFilePath));
            var tempFile = FileHelper.CreateTempFile(null, Encoding.UTF8);
            var attributes = File.GetAttributes(tempFile);
            File.SetAttributes(tempFile, FileAttributes.ReadOnly);
            Assert.IsFalse(FileHelper.TryDelete(tempFile));
            File.SetAttributes(tempFile, attributes);
            Assert.IsTrue(FileHelper.TryDelete(tempFile));
        }

        [TestMethod]
        public void Test_DeleteFiles()
        {
            var result = FileHelper.DeleteFiles();
            Assert.IsTrue(result.Count == 0);
            var tempFile1 = FileHelper.CreateTempFile(null, Encoding.UTF8);
            var tempFile2 = FileHelper.CreateTempFile(null, Encoding.UTF8);
            var tempFile3 = FileHelper.CreateTempFile(null, Encoding.UTF8);
            var attibutes = File.GetAttributes(tempFile2);
            File.SetAttributes(tempFile2, FileAttributes.ReadOnly);
            result = FileHelper.DeleteFiles(tempFile1, tempFile2, tempFile3, NotExistFilePath, "", "   ", null!);
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(tempFile1));
            Assert.IsTrue(result.Contains(tempFile3));
            Assert.IsTrue(File.Exists(tempFile2));
            Assert.IsFalse(File.Exists(tempFile1));
            Assert.IsFalse(File.Exists(tempFile3));

            File.SetAttributes(tempFile2, attibutes);
            result = FileHelper.DeleteFiles(tempFile2);
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Contains(tempFile2));
            Assert.IsFalse(File.Exists(tempFile2));
        }
    }
}
