namespace Masasamjant.Linq
{
    [TestClass]
    public class PageInfoConverterUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CanConvertFrom()
        {
            var converter = new PageInfoConverter();
            Assert.IsTrue(converter.CanConvertFrom(null, typeof(PageInfo)));
            Assert.IsTrue(converter.CanConvertFrom(null, typeof(string)));
            Assert.IsFalse(converter.CanConvertFrom(null, typeof(DateTime)));
        }

        [TestMethod]
        public void Test_CanConvertTo()
        {
            var converter = new PageInfoConverter();
            Assert.IsFalse(converter.CanConvertTo(null, null));
            Assert.IsFalse(converter.CanConvertTo(null, typeof(DateTime)));
            Assert.IsTrue(converter.CanConvertTo(null, typeof(PageInfo)));
            Assert.IsTrue(converter.CanConvertTo(null, typeof(string)));
        }

        [TestMethod]
        public void Test_ConvertFrom()
        {
            var converter = new PageInfoConverter();
            object value = new PageInfo(1, 10);
            object? result = converter.ConvertFrom(null, null, value);
            Assert.AreEqual(value, result);
            value = "1|10|-1";
            result = converter.ConvertFrom(null, null, value);
            Assert.AreEqual(new PageInfo(1, 10), result);
            value = DateTime.Now;
            result = converter.ConvertFrom(null, null, value);
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Test_ConvertTo()
        {
            var converter = new PageInfoConverter();
            object? value = null;
            object? result = converter.ConvertTo(null, null, value, typeof(string));
            Assert.IsNull(result);
            value = DateTime.Now;
            result = converter.ConvertTo(null, null, value, typeof(string));
            Assert.IsNull(result);
            value = new PageInfo(1, 10);
            result = converter.ConvertTo(null, null, value, typeof(DateTime));
            Assert.IsNull(result);
            result = converter.ConvertTo(null, null, value, typeof(PageInfo));
            Assert.AreEqual(value, result);
            result = converter.ConvertTo(null, null, value, typeof(string));
            Assert.AreEqual("1|10|-1", result);
        }
    }
}
