namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlSerializerFactoryUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentException>(() => new XmlSerializerFactory((XmlSerialization)999));
            var factory1 = new XmlSerializerFactory(XmlSerialization.Contract);
            var factory2 = new XmlSerializerFactory(XmlSerialization.Xml);
            Assert.IsNotNull(factory1);
            Assert.IsNotNull(factory2);
        }

        [TestMethod]
        public void Test_CreateSerializer()
        {
            var factory1 = new XmlSerializerFactory(XmlSerialization.Contract);
            var factory2 = new XmlSerializerFactory(XmlSerialization.Xml);
            var serializer1 = factory1.CreateSerializer(typeof(List<string>));
            var serializer2 = factory2.CreateSerializer(typeof(List<string>));
            Assert.IsTrue(serializer1 is XmlDataContractSerializer);
            Assert.IsTrue(serializer2 is XmlDataSerializer);
        }
    }
}
