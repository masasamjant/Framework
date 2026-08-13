using Masasamjant.Web.Stubs;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Masasamjant.Web
{
    [TestClass]
    public class HttpContextSessionValueAccessorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_SetHttpValue_GetHttpValue()
        {
            string key = "key";
            string value = "value";
            var context = CreateHttpContext(new SessionStub());
            var accessor = new HttpContextSessionValueAccessor();
            accessor.SetHttpValue(context, key, value);
            var actual = accessor.GetHttpValue(context, key);
            Assert.AreEqual(value, actual);
        }

        [TestMethod]
        public void Test_GetHttpValue()
        {
            string key = "key";
            var context = CreateHttpContext(new SessionStub());
            var accessor = new HttpContextSessionValueAccessor();
            var value = accessor.GetHttpValue(context, key);
            Assert.IsNull(value);
            context.Session.SetString(key, "value");
            value = accessor.GetHttpValue(context, key);
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void Test_SetHttpValue()
        {
            string key = "key";
            string value = "value";
            var context = CreateHttpContext(new SessionStub());
            var accessor = new HttpContextSessionValueAccessor();
            accessor.SetHttpValue(context, key, value);
            var actual = context.Session.GetString(key);
            Assert.AreEqual(value, actual);
        }
    }
}
