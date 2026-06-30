using Masasamjant.Web.Stubs;
using Microsoft.AspNetCore.Http;

namespace Masasamjant.Web
{
    [TestClass]
    public class HttpContextHelperUnitTest : UnitTest
    {
#pragma warning disable ASP0019 // Suggest using IHeaderDictionary.Append or the indexer

        [TestMethod]
        public void Test_GetHeaderValues()
        {
            var context = CreateHttpContext();
            var values = HttpContextHelper.GetHeaderValues(context.Request.Headers, "key");
            Assert.IsFalse(values.Any());
            context.Request.Headers.Add("key", "value");
            values = HttpContextHelper.GetHeaderValues(context.Request.Headers, "key");
            Assert.AreEqual(1, values.Count());
            Assert.AreEqual("value", values.First());
        }

        [TestMethod]
        public void Test_GetRequestHeaderValues()
        {
            var context = CreateHttpContext();
            var values = HttpContextHelper.GetRequestHeaderValues(context, "key");
            Assert.IsFalse(values.Any());
            context.Request.Headers.Add("key", "value");
            values = HttpContextHelper.GetRequestHeaderValues(context, "key");
            Assert.AreEqual(1, values.Count());
            Assert.AreEqual("value", values.First());
        }

        [TestMethod]
        public void Test_GetResponseHeaderValues()
        {
            var context = CreateHttpContext();
            var values = HttpContextHelper.GetResponseHeaderValues(context, "key");
            Assert.IsFalse(values.Any());
            context.Response.Headers.Add("key", "value");
            values = HttpContextHelper.GetResponseHeaderValues(context, "key");
            Assert.AreEqual(1, values.Count());
            Assert.AreEqual("value", values.First());
        }

        [TestMethod]
        public void Test_TryGetHeaderValue()
        {
            var context = CreateHttpContext();
            IReadOnlyCollection<string> values;
            bool result = HttpContextHelper.TryGetHeaderValue(context.Request.Headers, "key", out values);
            Assert.IsFalse(result);
            Assert.IsFalse(values.Any());
            context.Request.Headers.Add("key", "value");
            result = HttpContextHelper.TryGetHeaderValue(context.Request.Headers, "key", out values);
            Assert.IsTrue(result);
            Assert.AreEqual(1, values.Count());
            Assert.AreEqual("value", values.First());
        }

        [TestMethod]
        public void Test_TryGetRequestHeaderValue()
        {
            var context = CreateHttpContext();
            IReadOnlyCollection<string> values;
            bool result = HttpContextHelper.TryGetRequestHeaderValue(context, "key", out values);
            Assert.IsFalse(result);
            Assert.IsFalse(values.Any());
            context.Request.Headers.Add("key", "value");
            result = HttpContextHelper.TryGetRequestHeaderValue(context, "key", out values);
            Assert.IsTrue(result);
            Assert.AreEqual(1, values.Count());
            Assert.AreEqual("value", values.First());
        }

        [TestMethod]
        public void Test_TryGetResponseHeaderValue()
        {
            var context = CreateHttpContext();
            IReadOnlyCollection<string> values;
            bool result = HttpContextHelper.TryGetResponseHeaderValue(context, "key", out values);
            Assert.IsFalse(result);
            Assert.IsFalse(values.Any());
            context.Response.Headers.Add("key", "value");
            result = HttpContextHelper.TryGetResponseHeaderValue(context, "key", out values);
            Assert.IsTrue(result);
            Assert.AreEqual(1, values.Count());
            Assert.AreEqual("value", values.First());
        }

        [TestMethod]
        public void Test_GetCookieValue()
        {
            var context = CreateHttpContext();
            var value = HttpContextHelper.GetCookieValue(context, "cookie");
            Assert.IsNull(value);

            var collection = new RequestCookieCollectionStub(new Dictionary<string, string>() { { "cookie", "value" } });
            context.Request.Cookies = collection;
            value = HttpContextHelper.GetCookieValue(context, "cookie");
            Assert.AreEqual("value", value);

            value = HttpContextHelper.GetCookieValue(collection, "none");
            Assert.IsNull(value);
            value = HttpContextHelper.GetCookieValue(collection, "cookie");
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void Test_TryGetCookieValue()
        {
            var context = CreateHttpContext();
            var result = HttpContextHelper.TryGetCookieValue(context, "cookie", out var value);
            Assert.IsFalse(result);
            Assert.IsNull(value);

            var collection = new RequestCookieCollectionStub(new Dictionary<string, string>() { { "cookie", "value" } });
            context.Request.Cookies = collection;
            result = HttpContextHelper.TryGetCookieValue(context, "cookie", out value);
            Assert.IsTrue(result);
            Assert.AreEqual("value", value);

            result = HttpContextHelper.TryGetCookieValue(collection, "none", out value);
            Assert.IsFalse(result);
            Assert.IsNull(value);
            result = HttpContextHelper.TryGetCookieValue(collection, "cookie", out value);
            Assert.IsTrue(result);
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void Test_SetCookieValue()
        {
            var collection = new ResponseCookiesStub();
            Assert.ThrowsExactly<ArgumentException>(() => HttpContextHelper.SetCookieValue(collection, "cookie", "value", sameSiteMode: (SameSiteMode)999));
            var expires = DateTimeOffset.Now.AddHours(2);
            var maxAge = TimeSpan.FromMinutes(240);
            HttpContextHelper.SetCookieValue(collection, "cookie", "value", "path", expires, maxAge, true, true, true, "domain", SameSiteMode.Strict);
            var cookie = collection.Cookies.Where(x => x.Key == "cookie").FirstOrDefault();
            Assert.IsNotNull(cookie);
            Assert.AreEqual("value", cookie.Value);
            Assert.IsNotNull(cookie.Options);
            Assert.AreEqual("path", cookie.Options.Path);
            Assert.AreEqual(expires, cookie.Options.Expires);
            Assert.AreEqual(maxAge, cookie.Options.MaxAge);
            Assert.IsTrue(cookie.Options.HttpOnly);
            Assert.IsTrue(cookie.Options.Secure);
            Assert.IsTrue(cookie.Options.IsEssential);
            Assert.AreEqual("domain", cookie.Options.Domain);
            Assert.AreEqual(SameSiteMode.Strict, cookie.Options.SameSite);
        }

#pragma warning restore ASP0019 // Suggest using IHeaderDictionary.Append or the indexer
    }
}
