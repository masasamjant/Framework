using Masasamjant.Linq;
using System.ComponentModel;

namespace Masasamjant.ComponentModel
{
    [TestClass]
    public class TypeConverterHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_HasTypeConverter()
        {
            TypeConverterAttribute? attribute;
            Assert.IsFalse(TypeConverterHelper.HasTypeConverter(typeof(DateRange), out attribute));
            Assert.IsTrue(TypeConverterHelper.HasTypeConverter(typeof(PageInfo), out attribute));
        }

        [TestMethod]
        public void Test_CreateTypeConverter()
        {
            TypeConverterAttribute? attribute;
            Assert.IsTrue(TypeConverterHelper.HasTypeConverter(typeof(PageInfo), out attribute));
            var converter = TypeConverterHelper.CreateTypeConverter(attribute);
            Assert.IsNotNull(converter);
            Assert.IsInstanceOfType<PageInfoConverter>(converter);
        }
    }
}
