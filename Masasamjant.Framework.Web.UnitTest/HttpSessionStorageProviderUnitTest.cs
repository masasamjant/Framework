using Masasamjant.Web.Stubs;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace Masasamjant.Web
{
    [TestClass]
    public class HttpSessionStorageProviderUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetSessionStorage()
        {
            var session = new SessionStub();
            session.SetString("1", "1");
            var context = CreateHttpContext(session);
            var provider = new HttpSessionStorageProvider(context);
            var result = provider.GetSessionStorage();
            Assert.IsNotNull(result);
            Assert.AreEqual("1", session.GetString("1"));

            var accessor = new HttpContextAccessor();
            accessor.HttpContext = context;
            provider = new HttpSessionStorageProvider(accessor);
            result = provider.GetSessionStorage();
            Assert.IsNotNull(result);
            Assert.AreEqual("1", session.GetString("1"));

            accessor = new HttpContextAccessor();
            accessor.HttpContext = null;
            provider = new HttpSessionStorageProvider(accessor);
            Assert.ThrowsExactly<InvalidOperationException>(() => provider.GetSessionStorage());
        }
    }
}
