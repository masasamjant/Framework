using System.Xml;

namespace Masasamjant.Xml
{
    [TestClass]
    public class XmlHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsElementOf()
        {
            var doc = GetXmlDocument();
            var node = doc.SelectSingleNode("/persons/person[1]");
            Assert.IsTrue(XmlHelper.IsElementOf(node!, "person"));
            Assert.IsFalse(XmlHelper.IsElementOf(node!, "firstName"));
        }

        [TestMethod]
        public void Test_IsEndElementOf()
        {
            var doc = GetXmlDocument();
            var nodes = doc.SelectNodes("/persons/person");

            foreach (XmlNode node in nodes!)
            {
                if (node.NodeType == XmlNodeType.EndElement)
                {
                    Assert.IsTrue(XmlHelper.IsEndElementOf(node, node.Name));
                }
                else
                {
                    Assert.IsFalse(XmlHelper.IsEndElementOf(node, node.Name));
                }
            }
        }

        [TestMethod]
        public void Test_IsAttributeOf()
        {
            var doc = GetXmlDocument();
            var attr = doc.SelectSingleNode("/persons/person[1]")?.Attributes?["age"];
            Assert.IsTrue(XmlHelper.IsAttributeOf(attr!, "age"));
            Assert.IsFalse(XmlHelper.IsAttributeOf(attr!, "firstName"));
        }

        [TestMethod]
        public void Test_GetAttribute()
        {
            var doc = GetXmlDocument();
            var node = doc.SelectSingleNode("/persons/person[1]");
            var attr = XmlHelper.GetAttribute(node!, "age", true);
            Assert.AreEqual("30", attr!.Value);
            attr = XmlHelper.GetAttribute(node!, "parent", false);
            Assert.IsNull(attr);
            Assert.ThrowsException<XmlException>(() => XmlHelper.GetAttribute(node!, "parent", true));
        }

        [TestMethod]
        public void Test_TryGetAttribute()
        {
            var doc = GetXmlDocument();
            var node = doc.SelectSingleNode("/persons/person[1]");
            Assert.IsTrue(XmlHelper.TryGetAttribute(node!, "age", out var attr));
            Assert.AreEqual("30", attr!.Value);
            Assert.IsFalse(XmlHelper.TryGetAttribute(node!, "parent", out attr));
            Assert.IsNull(attr);
        }

        [TestMethod]
        public void Test_ChecksumAttribute()
        {
            var doc = GetXmlDocument();
            XmlHelper.AppendChecksumAttribute(doc, "persons", "checksum");
            var root = doc.DocumentElement;
            Assert.IsTrue(XmlHelper.TryGetAttribute(root!, "checksum", out var attr));
            bool result = XmlHelper.VerifyChecksumAttribute(doc, "persons", "checksum");
            Assert.IsTrue(result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_AppendChecksumAttribute_Attribute_Exist()
        {
            var doc = GetXmlDocument();
            XmlHelper.AppendChecksumAttribute(doc, "persons", "count");
        }

        [TestMethod]
        [ExpectedException(typeof(XmlException))]
        public void Test_VerifyChecksumAttribute_No_Checksum_Attribute()
        {
            var doc = GetXmlDocument();
            XmlHelper.VerifyChecksumAttribute(doc, "persons", "checksum");
        }

        [TestMethod]
        public void Test_ToXml()
        {
            var doc = GetXmlDocument();
            var xml1 = XmlHelper.ToXml(doc);
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml1));
            var settings = new XmlWriterSettings { OmitXmlDeclaration = false, Indent = false };
            var xml2 = XmlHelper.ToXml(doc, settings);
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml2));
            Assert.AreEqual(xml1, xml2);
            settings .OmitXmlDeclaration = true;
            settings.Indent = true;
            var xml3 = XmlHelper.ToXml(doc, settings);
            Assert.IsFalse(string.IsNullOrWhiteSpace(xml3));
            Assert.AreNotEqual(xml1, xml3);
        }

        private const string Xml = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
            <persons count=""2"">
                <person age=""30"">
                    <firstName>John</firstName>
                    <lastName>Doe</lastName>
                </person>
                <person age=""25"">
                    <firstName>Jane</firstName>
                    <lastName>Smith</lastName>
                </person>
            </persons>";

        private static XmlDocument GetXmlDocument()
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(Xml);
            return xmlDocument;
        }
    }
}
