using Masasamjant.Web.Stubs;

namespace Masasamjant.Web
{
    [TestClass]
    public class SessionStorageUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetSessionIdentifier()
        {
            SessionStorageStub sessionStorage = new SessionStorageStub();
            var a = sessionStorage.GetSessionIdentifier();
            var b = sessionStorage.GetString(sessionStorage.GetDefaultSessionIdentifierKey());
            Assert.AreEqual(a, b);

            string sessionIdentifierKey = "session-identifier";
            Guid expected = Guid.NewGuid();
            sessionStorage = new SessionStorageStub(expected, sessionIdentifierKey);
            Guid actual = Guid.Parse(sessionStorage.GetSessionIdentifier());
            Assert.AreEqual(expected, actual);
            var value = sessionStorage.GetString(sessionIdentifierKey);
            Assert.AreEqual(actual.ToString(), value);
        }
    }
}
