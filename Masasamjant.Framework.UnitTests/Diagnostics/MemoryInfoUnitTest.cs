namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class MemoryInfoUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryInfo(-1, 2));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new MemoryInfo(1, -2));
            var info = new MemoryInfo(0, 0);
            Assert.IsTrue(info.StartBytes == 0);
            Assert.IsTrue(info.EndBytes == 0);
            Assert.IsTrue(info.TotalBytes == 0);

            info = new MemoryInfo(10, 1);
            Assert.IsTrue(info.StartBytes == 1);
            Assert.IsTrue(info.EndBytes == 10);
            Assert.IsTrue(info.TotalBytes == 9);

            info = new MemoryInfo(1, 10);
            Assert.IsTrue(info.StartBytes == 1);
            Assert.IsTrue(info.EndBytes == 10);
            Assert.IsTrue(info.TotalBytes == 9);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var info = new MemoryInfo(1, 10);
            Assert.IsFalse(info.Equals(DateTime.Now));
            Assert.IsFalse(info.Equals(new MemoryInfo(1, 8)));
            Assert.IsFalse(info.Equals(new MemoryInfo(2, 10)));
            Assert.IsTrue(info.Equals(new MemoryInfo(1, 10)));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            Assert.AreEqual(new MemoryInfo(1, 10).GetHashCode(), new MemoryInfo(1, 10).GetHashCode());
        }

        [TestMethod]
        public void Test_ToString()
        {
            var info = new MemoryInfo(100, 1000);
            string expected = "1000 Total (+ 900)";
            string actual = info.ToString(new TestMemoryInfoFormatter());
            Assert.AreEqual(expected, actual);
            expected = "0,98 KB Total (+ 900 B)";
            actual = info.ToString();
            Assert.AreEqual(expected, actual);
        }

        private class TestMemoryInfoFormatter : IMemoryInfoFormatter
        {
            public string FormatByteCount(long bytes)
            {
                return bytes.ToString();
            }
        }
    }
}
