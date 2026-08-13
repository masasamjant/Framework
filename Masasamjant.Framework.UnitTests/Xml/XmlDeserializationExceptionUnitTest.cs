using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlDeserializationExceptionUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor_With_Document()
        {
            var document = new XmlDocument();
            var inner = new InvalidOperationException();
            var message = "Testing";
            var exception = new XmlDeserializationException(document, message, inner);
            Assert.AreSame(document, exception.Document);
            Assert.AreSame(inner, exception.InnerException);
            Assert.AreEqual("Testing", exception.Message);
            Assert.IsNull(exception.Xml);

            exception = new XmlDeserializationException(document, message);
            Assert.AreSame(document, exception.Document);
            Assert.IsNull(exception.InnerException);
            Assert.AreEqual("Testing", exception.Message);
            Assert.IsNull(exception.Xml);
        }

        [TestMethod]
        public void Test_Constructor_With_Xml()
        {
            var xml = "xml";
            var inner = new InvalidOperationException();
            var message = "Testing";
            var exception = new XmlDeserializationException(xml, message, inner);
            Assert.AreEqual(xml, exception.Xml);
            Assert.AreSame(inner, exception.InnerException);
            Assert.AreEqual("Testing", exception.Message);
            Assert.IsNull(exception.Document);

            exception = new XmlDeserializationException(xml, message);
            Assert.AreEqual(xml, exception.Xml);
            Assert.IsNull(exception.InnerException);
            Assert.AreEqual("Testing", exception.Message);
            Assert.IsNull(exception.Document);
        }
    }
}
