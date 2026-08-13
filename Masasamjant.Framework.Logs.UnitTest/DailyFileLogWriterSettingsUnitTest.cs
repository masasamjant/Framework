namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class DailyFileLogWriterSettingsUnitTest : UnitTest
    {
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_DirectoryPath_Is_Invalid()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new DailyFileLogWriterSettings(null!, 10, 1000));
            Assert.ThrowsExactly<ArgumentNullException>(() => new DailyFileLogWriterSettings(string.Empty, 10, 1000));
            Assert.ThrowsExactly<ArgumentNullException>(() => new DailyFileLogWriterSettings("   ", 10, 1000));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentException_When_Directory_Does_Not_Exist()
        {
            var nonExistentDirectory = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Assert.ThrowsExactly<ArgumentException>(() => new DailyFileLogWriterSettings(nonExistentDirectory, 10, 1000));
        }

        [TestMethod]
        public void FilePathProvider_Should_Return_Correct_File_Path()
        {
            var tempDirectory = Path.GetTempPath();
            var settings = new DailyFileLogWriterSettings(tempDirectory, 10, 1000);
            var expectedFileName = $"{DateTime.Now:yyyyMMdd}-LOG.log";
            var expectedFilePath = Path.Combine(tempDirectory, expectedFileName);
            Assert.AreEqual(expectedFilePath, settings.FilePathProvider());
        }
    }
}
