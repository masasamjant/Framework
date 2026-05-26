namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class FileLogWriterSettingsUnitTest : UnitTest
    {
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_FilePathProvider_Is_Null()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new FileLogWriterSettings(null!, 10, 1000));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentOutOfRangeException_When_BatchSize_Is_Less_Than_1()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new FileLogWriterSettings(() => "log.txt", 0, 1000));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentOutOfRangeException_When_BatchSize_Is_Greater_Than_1000()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new FileLogWriterSettings(() => "log.txt", 1001, 1000));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentOutOfRangeException_When_FlushInterval_Is_Less_Than_100()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new FileLogWriterSettings(() => "log.txt", 10, 99));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentOutOfRangeException_When_FlushInterval_Is_Greater_Than_60000()
        {
            Assert.ThrowsExactly<ArgumentOutOfRangeException>(() => new FileLogWriterSettings(() => "log.txt", 10, 60001));
        }

        [TestMethod]
        public void Constructor_Should_Initialize_Properties_Correctly()
        {
            var settings = new FileLogWriterSettings(() => "log.txt", 10, 1000);
            Assert.AreEqual("log.txt", settings.FilePathProvider());
            Assert.AreEqual(10, settings.BatchSize);
            Assert.AreEqual(1000, settings.FlushIntervalMilliseconds);
        }
    }
}
