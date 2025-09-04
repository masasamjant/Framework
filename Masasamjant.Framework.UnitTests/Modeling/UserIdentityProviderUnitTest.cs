using Masasamjant.Modeling.Abstractions;
using Masasamjant.Repositories;

namespace Masasamjant.Modeling
{
    [TestClass]
    public class UserIdentityProviderUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Provider()
        {
            var userIdentity = new UserIdentity("Test");
            var provider = new UserIdentityProvider(userIdentity);
            var identity = provider.GetUserIdentity();
            Assert.IsTrue(ReferenceEquals(userIdentity, identity));
            Func<IUserIdentity?>? getUserIdentity = () => null;
            provider = new UserIdentityProvider(getUserIdentity);
            identity = provider.GetUserIdentity();
            Assert.IsFalse(ReferenceEquals(userIdentity, identity));
            Assert.AreEqual(string.Empty, identity.Identity);
            getUserIdentity = () => userIdentity;
            provider = new UserIdentityProvider(getUserIdentity);
            identity = provider.GetUserIdentity();
            Assert.IsTrue(ReferenceEquals(userIdentity, identity));
        }
    }
}
