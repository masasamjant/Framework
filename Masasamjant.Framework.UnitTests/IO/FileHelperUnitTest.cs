using System.Text;

namespace Masasamjant.IO
{
    [TestClass]
    public  class FileHelperUnitTest : UnitTest
    {
        private const string NotExistFile = @"C:\NOTEXISTS\NOTEXISTS.txt";

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
            Assert.ThrowsException<FileNotFoundException>(() => FileHelper.IsEmptyFile(NotExistFile));
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
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => FileHelper.IsEmptyFileAsync(NotExistFile));
        }

        [TestMethod]
        public void Test_CreateTempFile()
        {
            var tempFilePath = FileHelper.CreateTempFile((string?)null);
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempFile("");
            Assert.IsTrue(File.Exists(tempFilePath));
            Assert.IsTrue(FileHelper.IsEmptyFile(tempFilePath));
            File.Delete(tempFilePath);

            tempFilePath = FileHelper.CreateTempFile("Text");
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
            Assert.ThrowsException<FileNotFoundException>(() => FileHelper.CopyToTempFile(NotExistFile));
            var sourceFile = FileHelper.CreateTempFile("Content");
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
            var sourceFile = FileHelper.CreateTempFile("Content");
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
    }
}
