namespace Masasamjant.IO
{
    [TestClass]
    public class FileDeleteDisposableUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Dispose_ShouldDeleteFile()
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            File.WriteAllText(tempFilePath, "Test content");

            using (var disposable = new FileDeleteDisposable(tempFilePath))
            {
                Assert.IsTrue(File.Exists(tempFilePath));
            }

            Assert.IsFalse(File.Exists(tempFilePath));
        }

        [TestMethod]
        public void Test_Dispose_ShouldNotThrowIfFileDoesNotExist()
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            using (var disposable = new FileDeleteDisposable(tempFilePath))
            {
                Assert.IsFalse(File.Exists(tempFilePath));
            }
            Assert.IsFalse(File.Exists(tempFilePath));
        }

        [TestMethod]
        public void Test_Constructor()
        {
            string tempFilePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            using (var disposable = new FileDeleteDisposable(tempFilePath))
            {
                Assert.AreEqual(tempFilePath, disposable.FilePath);
            }
            
            Assert.ThrowsException<ArgumentNullException>(() => new FileDeleteDisposable((string)null!));
            Assert.ThrowsException<ArgumentNullException>(() => new FileDeleteDisposable((FileInfo)null!));
            Assert.ThrowsException<ArgumentNullException>(() => new FileDeleteDisposable(string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => new FileDeleteDisposable("   "));
        }
    }
}
