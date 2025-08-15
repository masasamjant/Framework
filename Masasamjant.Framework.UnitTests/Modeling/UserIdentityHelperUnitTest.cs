namespace Masasamjant.Modeling
{
    [TestClass]
    public class UserIdentityHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsAnonymous()
        {
            Assert.IsTrue(UserIdentityHelper.IsAnonymous(new UserIdentity()));
            Assert.IsFalse(UserIdentityHelper.IsAnonymous(new UserIdentity("Test")));
        }
    }
}
