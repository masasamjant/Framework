namespace Masasamjant.IO
{
    [TestClass]
    public class MimeTypeUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Assert.ThrowsException<ArgumentNullException>(() => new MimeType("", ".html", "HTML"));
            Assert.ThrowsException<ArgumentNullException>(() => new MimeType("  ", ".html", "HTML"));
            Assert.ThrowsException<ArgumentNullException>(() => new MimeType("text/html", "", "HTML"));
            Assert.ThrowsException<ArgumentNullException>(() => new MimeType("text/html", "  ", "HTML"));
            var mimeType = new MimeType("text/html", ".html", "HTML");
            Assert.AreEqual("text/html", mimeType.Value);
            Assert.AreEqual(".html", mimeType.FileExtension);
            Assert.AreEqual("HTML", mimeType.Description);
            mimeType = new MimeType("text/html", "html", "HTML");
            Assert.AreEqual("text/html", mimeType.Value);
            Assert.AreEqual(".html", mimeType.FileExtension);
            Assert.AreEqual("HTML", mimeType.Description);
        }

        [TestMethod]
        public void Test_Equality()
        {
            var mimeType = new MimeType("text/html", ".html", "HTML");
            
            // Null references
            MimeType? other = null;
            Assert.IsFalse(mimeType.Equals(other));
            object? obj = null;
            Assert.IsFalse(mimeType.Equals(obj));

            // Not correct type.
            obj = DateTime.Now;
            Assert.IsFalse(mimeType.Equals(obj));

            // Description not matter.
            other = new MimeType("text/html", "html", "Foo");
            Assert.IsTrue(mimeType.Equals(other));
            
            // Different extension.
            other = new MimeType("text/html", "htm", "HTML");
            Assert.IsFalse(mimeType.Equals(other));

            // Different value.
            other = new MimeType("text/plain", "html", "HTML");
            Assert.IsFalse(mimeType.Equals(other));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var mimeType = new MimeType("text/html", "html", "");
            var other = new MimeType("text/html", ".html", "Foo");
            Assert.IsTrue(mimeType.Equals(other));
            Assert.AreEqual(mimeType.GetHashCode(), other.GetHashCode());   
        }

        [TestMethod]
        public void Test_ToString()
        {
            var mimeType = new MimeType("text/html", ".html", "");
            Assert.AreEqual("text/html", mimeType.ToString());
            mimeType = new MimeType("text/html", ".html", "HTML");
            Assert.AreEqual("HTML (text/html)", mimeType.ToString());
        }
    }
}
