namespace Masasamjant.IO
{
    [TestClass]
    public class DirectoryDeleteDisposableUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Dispose_ShouldDeleteDirectory()
        {
            string tempDirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempDirectoryPath);

            using (var disposable = new DirectoryDeleteDisposable(tempDirectoryPath))
            {
                Assert.IsTrue(Directory.Exists(tempDirectoryPath));
            }

            Assert.IsFalse(Directory.Exists(tempDirectoryPath));
        }

        [TestMethod]
        public void Test_Dispose_ShouldNotThrowIfDirectoryDoesNotExist()
        {
            string tempDirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            using (var disposable = new DirectoryDeleteDisposable(tempDirectoryPath))
            {
                Assert.IsFalse(Directory.Exists(tempDirectoryPath));
            }
            Assert.IsFalse(Directory.Exists(tempDirectoryPath));
        }

        [TestMethod]
        public void Test_Constructor()
        {
            string tempDirectoryPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            using (var disposable = new DirectoryDeleteDisposable(tempDirectoryPath))
            {
                Assert.AreEqual(tempDirectoryPath, disposable.DirectoryPath);
            }

            Assert.ThrowsException<ArgumentNullException>(() => new DirectoryDeleteDisposable((string)null!));
            Assert.ThrowsException<ArgumentNullException>(() => new DirectoryDeleteDisposable((DirectoryInfo)null!));
            Assert.ThrowsException<ArgumentNullException>(() => new DirectoryDeleteDisposable(string.Empty));
            Assert.ThrowsException<ArgumentNullException>(() => new DirectoryDeleteDisposable("   "));
        }
    }
}
