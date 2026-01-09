namespace Masasamjant
{
    [TestClass]
    public class TimingConverterUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CanConvertFrom()
        {
            var converter = new TimingConverter();
            Assert.IsTrue(converter.CanConvertFrom(typeof(string)));
            Assert.IsTrue(converter.CanConvertFrom(typeof(Timing)));
            Assert.IsFalse(converter.CanConvertFrom(typeof(DateTime)));
        }

        [TestMethod]
        public void Test_CanConvertTo() 
        {
            var converter = new TimingConverter();
            Assert.IsFalse(converter.CanConvertTo(null));
            Assert.IsTrue(converter.CanConvertTo(typeof(string)));
            Assert.IsTrue(converter.CanConvertTo(typeof(Timing)));
            Assert.IsFalse(converter.CanConvertTo(typeof(DateTime)));
        }

        [TestMethod]
        public void Test_ConvertFrom_ConvertTo()
        {
            var converter = new TimingConverter();
            object value = DateTime.Now;
            var result = converter.ConvertFrom(value);
            Assert.IsNull(result);
            
            value = new Timing(new DateOnly(2000, 1, 1), new TimeOnly(10, 0), new TimeOnly(11, 0));
            result = converter.ConvertFrom(value);
            Assert.AreEqual(value, result);

            result = converter.ConvertTo(value, typeof(string));
            result = converter.ConvertFrom(result!);
            Assert.AreEqual(value, result);

            result = converter.ConvertFrom("Foo");
            Assert.IsNull(result);

            result = converter.ConvertTo(value, typeof(Timing));
            Assert.AreEqual(value, result);

            result = converter.ConvertTo(null, null, value, typeof(string));
            result = converter.ConvertFrom(null, null, result!);
            Assert.AreEqual(value, result);

            result = converter.ConvertTo(null, null, DateTime.Now, typeof(string));
            Assert.IsNull(result);
        }
    }
}
