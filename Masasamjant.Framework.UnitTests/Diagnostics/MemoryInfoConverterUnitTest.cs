namespace Masasamjant.Diagnostics
{
    [TestClass]
    public class MemoryInfoConverterUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CanConvertFrom()
        {
            var converter = new MemoryInfoConverter();
            Assert.IsTrue(converter.CanConvertFrom(null, typeof(MemoryInfo)));
            Assert.IsTrue(converter.CanConvertFrom(null, typeof(string)));
            Assert.IsFalse(converter.CanConvertFrom(null, typeof(DateTime)));
        }

        [TestMethod]
        public void Test_CanConvertTo()
        {
            var converter = new MemoryInfoConverter();
            Assert.IsFalse(converter.CanConvertTo(null, null));
            Assert.IsFalse(converter.CanConvertTo(null, typeof(DateTime)));
            Assert.IsTrue(converter.CanConvertTo(null, typeof(MemoryInfo)));
            Assert.IsTrue(converter.CanConvertTo(null, typeof(string)));
        }

        [TestMethod]
        public void Test_ConvertFrom()
        {
            var converter = new MemoryInfoConverter();
            object value = new MemoryInfo(1, 10);
            object? result = converter.ConvertFrom(null, null, value);
            Assert.AreEqual(value, result);
            value = "1|10";
            result = converter.ConvertFrom(null, null, value);
            Assert.AreEqual(new MemoryInfo(1, 10), result);
            value = DateTime.Now;
            result = converter.ConvertFrom(null, null, value);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_ConvertTo()
        {
            var converter = new MemoryInfoConverter();
            object? value = null;
            object? result = converter.ConvertTo(null, null, value, typeof(string));
            Assert.IsNull(result);
            value = DateTime.Now;
            result = converter.ConvertTo(null, null, value, typeof(string));
            Assert.IsNull(result);
            value = new MemoryInfo(1, 10);
            result = converter.ConvertTo(null, null, value, typeof(DateTime));
            Assert.IsNull(result);
            result = converter.ConvertTo(null, null, value, typeof(MemoryInfo));
            Assert.AreEqual(value, result);
            result = converter.ConvertTo(null, null, value, typeof(string));
            Assert.AreEqual("1|10", result);
        }
    }
}
