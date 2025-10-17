namespace Masasamjant.IO
{
    [TestClass]
    public class MimeTypesUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetMimeTypes()
        {
            var result = MimeTypes.GetMimeTypes().ToList();
            Assert.AreEqual(77, result.Count);

            result = MimeTypes.GetMimeTypes("text/html").ToList();
            Assert.AreEqual(2, result.Count);
            Assert.IsTrue(result.Contains(new MimeType("text/html", ".html", "")));
            Assert.IsTrue(result.Contains(new MimeType("text/html", ".htm", "")));

            result = MimeTypes.GetMimeTypes("foo/bar").ToList();
            Assert.AreEqual(0, result.Count);
            result = MimeTypes.GetMimeTypes("").ToList();
            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Test_GetMimeType()
        {
            var mime = MimeTypes.GetMimeType("");
            Assert.IsNull(mime);

            mime = MimeTypes.GetMimeType("html");
            Assert.IsNotNull(mime);
            Assert.AreEqual(".html", mime.FileExtension);
            Assert.AreEqual("text/html", mime.Value);

            mime = MimeTypes.GetMimeType(".html");
            Assert.IsNotNull(mime);
            Assert.AreEqual(".html", mime.FileExtension);
            Assert.AreEqual("text/html", mime.Value);

            mime = MimeTypes.GetMimeType("foo");
            Assert.IsNull(mime);

            mime = MimeTypes.GetMimeType("htm");
            Assert.IsNotNull(mime);
            Assert.AreEqual(".htm", mime.FileExtension);
            Assert.AreEqual("text/html", mime.Value);

            mime = MimeTypes.GetMimeType(".htm");
            Assert.IsNotNull(mime);
            Assert.AreEqual(".htm", mime.FileExtension);
            Assert.AreEqual("text/html", mime.Value);
        }

        [TestMethod]
        public void Test_GetExtensionValueMap()
        {
            var result = MimeTypes.GetMimeTypes().ToList();
            var map = MimeTypes.GetExtensionValueMap();
            Assert.IsTrue(result.All(x => map.ContainsKey(x.FileExtension)));
            Assert.IsTrue(result.All(x => map[x.FileExtension] == x.Value));
        }
    }
}
