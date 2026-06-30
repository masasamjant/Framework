namespace Masasamjant.Web
{
    [TestClass]
    public class UrlHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsValidUrl()
        {
            Assert.IsFalse(UrlHelper.IsValidAbsoluteUrl("~/controller/action"));
            Assert.IsFalse(UrlHelper.IsValidAbsoluteUrl("/controller/action"));
            Assert.IsTrue(UrlHelper.IsValidAbsoluteUrl("http://address/controller/action"));
            Assert.IsFalse(UrlHelper.IsValidAbsoluteUrl("http://address/controller/action", [Uri.UriSchemeFtp]));
        }

        [TestMethod]
        public void Test_IsValidHttpUrl()
        {
            Assert.IsFalse(UrlHelper.IsValidHttpUrl("~/controller/action"));
            Assert.IsFalse(UrlHelper.IsValidHttpUrl("/controller/action"));
            Assert.IsTrue(UrlHelper.IsValidHttpUrl("http://address/controller/action"));
            Assert.IsTrue(UrlHelper.IsValidHttpUrl("https://address/controller/action"));
            Assert.IsFalse(UrlHelper.IsValidHttpUrl("ftp://address/controller/action"));
        }
    }
}
