using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlDataContractSerializerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var serializer = new XmlDataContractSerializer(typeof(XmlData));
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
            var serializer = new XmlDataContractSerializer(typeof(XmlData));
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
            var serializer = new XmlDataContractSerializer(typeof(Stream));
            Assert.ThrowsException<XmlSerializationException>(() => serializer.Serialize(new XmlData()));
        }

        [TestMethod]
        public void Test_Deserialize()
        {
            var serializer = new XmlDataContractSerializer(typeof(Stream));
            Assert.ThrowsException<XmlDeserializationException>(() => serializer.Deserialize(new XmlDocument()));
        }
    }
}
