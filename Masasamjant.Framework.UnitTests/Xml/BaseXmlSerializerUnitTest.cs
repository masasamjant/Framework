using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class BaseXmlSerializerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_DeserializeXmlDocument()
        {
            object? result = null;
            var serializer = new TestBaseXmlSerializer(result);
            var document = new XmlDocument();
            object? actual = serializer.Deserialize<DateTime>(document);
            Assert.AreEqual(DateTime.MinValue, actual);

            actual = serializer.Deserialize<XmlDocument>(document);
            Assert.IsNull(actual);

            result = DateTime.Now;
            serializer = new TestBaseXmlSerializer(result);
            actual = serializer.Deserialize<DateTime>(document);
            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void Test_DeserializeString_To_Type()
        {
            object? result = null;
            string xml = "<xml>test</xml>";
            var serializer = new TestBaseXmlSerializer(result);
            object? actual = serializer.Deserialize<DateTime>(xml);
            Assert.AreEqual(DateTime.MinValue, actual);

            actual = serializer.Deserialize<XmlDocument>(xml);
            Assert.IsNull(actual);

            result = DateTime.Now;
            serializer = new TestBaseXmlSerializer(result);
            actual = serializer.Deserialize<DateTime>(xml);
            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void Test_DeserializeString_To_Object()
        {
            object? result = null;
            string xml = "<xml>test</xml>";
            var serializer = new TestBaseXmlSerializer(result);
            object? actual = serializer.Deserialize(xml);
            Assert.IsNull(actual);
            
            result = DateTime.Now;
            serializer = new TestBaseXmlSerializer(result);
            actual = serializer.Deserialize(xml);
            Assert.AreEqual(result, actual);
        }

        [TestMethod]
        public void Test_DeserializeString_Throws_Exception()
        {
            object? result = null;
            string xml = "<xml>test</xml>";
            var inner = new InvalidOperationException();
            var serializer = new TestBaseXmlSerializer(result, inner);
            var actual = Assert.ThrowsException<XmlDeserializationException>(() => serializer.Deserialize(xml));
            Assert.AreEqual("Error during deserialization of XML markup.", actual.Message);
            Assert.AreSame(inner, actual.InnerException);
            Assert.AreEqual(xml, actual.Xml);

            serializer = new TestBaseXmlSerializer(result, new XmlDeserializationException(xml, "testing"));
            actual = Assert.ThrowsException<XmlDeserializationException>(() => serializer.Deserialize(xml));
            Assert.AreEqual("testing", actual.Message);
            Assert.IsNull(actual.InnerException);
            Assert.AreEqual(xml, actual.Xml);
        }

        private class TestBaseXmlSerializer : BaseXmlSerializer
        {
            private readonly object? result;
            private readonly Exception? exception;

            public TestBaseXmlSerializer(object? result, Exception? exception = null)
            {
                this.result = result;
                this.exception = exception;
            }

            public override object? Deserialize(XmlDocument document)
            {
                if (exception != null)
                    throw exception;

                return result;
            }

            public override string Serialize(object instance)
            {
                return instance.ToString() ?? string.Empty;
            }
        }
    }
}
