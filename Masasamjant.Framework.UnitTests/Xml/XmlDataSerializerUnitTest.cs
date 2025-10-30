using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlDataSerializerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var serializer = new XmlDataSerializer(typeof(XmlData));
            Assert.IsNotNull(serializer);
        }

        [TestMethod]
        public void Test_Serialize_Deserialize()
        {
            var data = new XmlData()
            {
                Key = "key",
                Value = "value"
            };
            var serializer = new XmlDataSerializer(typeof(XmlData));
            var xml = serializer.Serialize(data);
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml));
            var document = new XmlDocument();
            document.LoadXml(xml);
            var other = (XmlData?)serializer.Deserialize(document);
            Assert.IsNotNull(other);
            Assert.IsFalse(ReferenceEquals(data, other));
            Assert.AreEqual(data.Key, other.Key);
            Assert.AreEqual(data.Value, other.Value);
        }

        [TestMethod]
        public void Test_Serialize()
        {
            var serializer = new XmlDataSerializer(typeof(Stream));
            Assert.ThrowsException<XmlSerializationException>(() => serializer.Serialize(new XmlData()));
        }
    }
}
