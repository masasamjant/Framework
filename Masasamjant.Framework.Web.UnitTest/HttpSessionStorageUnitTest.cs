using Masasamjant.Web.Stubs;

namespace Masasamjant.Web
{
    [TestClass]
    public class HttpSessionStorageUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var session = new SessionStub();
            var context = CreateHttpContext(session);
            var storage = new HttpSessionStorage(session);
            Assert.IsNotNull(storage);
        }

        [TestMethod]
        public void Test_Clear()
        {
            var session = new SessionStub();
            var expectedsessionIdentifier = session.Id;
            var storage = new HttpSessionStorage(session);
            string? actualSessionIdentifier = null;
            storage.Cleared += (s, e) => {
                actualSessionIdentifier = e.SessionIdentifier;
            };
            storage.SetString("key", "value");
            var x = storage.GetString("key");
            Assert.AreEqual("value", x);
            storage.Clear();
            x = storage.GetString("key");
            Assert.IsNull(x);
            Assert.AreEqual(expectedsessionIdentifier, actualSessionIdentifier);
        }

        [TestMethod]
        public void Test_GetString()
        {
            var storage = new HttpSessionStorage(new SessionStub());
            var value = storage.GetString("key");
            Assert.IsNull(value);
            storage.SetString("key", "value");
            value = storage.GetString("key");
            Assert.AreEqual("value", value);
        }

        [TestMethod]
        public void Test_Remove()
        {
            var storage = new HttpSessionStorage(new SessionStub());
            storage.SetString("1", "1");
            storage.SetString("2", "2");
            var value = storage.GetString("2");
            Assert.AreEqual("2", value);
            storage.Remove("2");
            value = storage.GetString("2");
            Assert.IsNull(value);
            Assert.AreEqual("1", storage.GetString("1"));
        }

        [TestMethod]
        public void Test_SetString()
        {
            var storage = new HttpSessionStorage(new SessionStub());
            storage.SetString("1", "1");
            Assert.AreEqual("1", storage.GetString("1"));
            storage.SetString("1", "2");
            Assert.AreEqual("2", storage.GetString("1"));
        }

        [TestMethod]
        public void Test_GetSessionIdentifier()
        {
            var session = new SessionStub();
            var storage = new HttpSessionStorage(session);
            Assert.AreEqual(session.Id, storage.GetSessionIdentifier());
        }
    }
}
