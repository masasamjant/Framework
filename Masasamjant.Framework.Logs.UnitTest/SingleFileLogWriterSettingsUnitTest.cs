namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class SingleFileLogWriterSettingsUnitTest : UnitTest
    {
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_FilePath_Is_Invalid()
        {
            Assert.ThrowsExactly<ArgumentNullException>(() => new SingleFileLogWriterSettings(null!, 10, 1000));
            Assert.ThrowsExactly<ArgumentNullException>(() => new SingleFileLogWriterSettings(string.Empty, 10, 1000));
            Assert.ThrowsExactly<ArgumentNullException>(() => new SingleFileLogWriterSettings("   ", 10, 1000));
        }
    }
}
