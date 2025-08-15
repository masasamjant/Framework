namespace Masasamjant.Modeling
{
    [TestClass]
    public class UserIdentityUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Identity()
        {
            var identity = new UserIdentity();
            Assert.AreEqual(string.Empty, identity.Identity);
            identity = new UserIdentity("      ");
            Assert.AreEqual(string.Empty, identity.Identity);
            identity = new UserIdentity("Test");
            Assert.AreEqual("Test", identity.Identity);
        }
    }
}
