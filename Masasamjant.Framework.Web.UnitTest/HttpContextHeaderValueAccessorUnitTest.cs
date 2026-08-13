namespace Masasamjant.Web
{
    [TestClass]
    public class HttpContextHeaderValueAccessorUnitTest : UnitTest
    {
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer

        [TestMethod]
        public void Test_GetHttpValue()
        {
            string key = "key";
            var context = CreateHttpContext();
            var accessor = new HttpContextHeaderValueAccessor();
            var value = accessor.GetHttpValue(context, key);
            Assert.IsNull(value);
            context.Request.Headers.Add(key, "value");
            value = accessor.GetHttpValue(context, key);
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void Test_SetHttpValue()
        {
            string key = "key";
            var context = CreateHttpContext();
            var accessor = new HttpContextHeaderValueAccessor();
            var actual = context.Response.Headers[key];
            Assert.AreEqual(0, actual.Count);
            var value = "value";
            accessor.SetHttpValue(context, key, value);
            actual = context.Response.Headers[key];
            Assert.AreEqual(1, actual.Count);
            Assert.AreEqual(value, actual.First());
        }

#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
    }
}
