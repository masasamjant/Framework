namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class DefaultMemoryInfoFormatterUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_FormatByteCount()
        {
            var formatter = new DefaultMemoryInfoFormatter();
            Assert.AreEqual("0 B", formatter.FormatByteCount(-1));
            Assert.AreEqual("0 B", formatter.FormatByteCount(0));
            Assert.AreEqual("999 B", formatter.FormatByteCount(999));
            Assert.AreEqual("0,98 KB", formatter.FormatByteCount(1000));
            Assert.AreEqual("97,66 KB", formatter.FormatByteCount(99999));
            Assert.AreEqual("976,56 KB", formatter.FormatByteCount(999999));
            Assert.AreEqual("0,95 MB", formatter.FormatByteCount(1000000));
            Assert.AreEqual("9,54 MB", formatter.FormatByteCount(10000000));
        }
    }
}
