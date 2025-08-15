using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    [TestClass]
    public class ModelIdentityUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Guid identifier = Guid.NewGuid();
            IModel model = new UserModel(identifier, "User");
            Assert.ThrowsException<ArgumentException>(() => new ModelIdentity(model, Array.Empty<object>()));
            var identity = new ModelIdentity(model, new object[] { identifier });
            Assert.IsFalse(string.IsNullOrWhiteSpace(identity.Value));
        }

        [TestMethod]
        public void Test_Equals()
        {
            Guid identifier = Guid.NewGuid();
            IModel model = new UserModel(identifier, "User");
            var expected = new ModelIdentity(model, new object[] { identifier });
            var actual = model.GetIdentity();
            Assert.IsTrue(expected.Equals(actual));
            Assert.IsTrue(expected.Equals((object?)actual));

            model = new UserModel(Guid.NewGuid(), "User");
            actual = model.GetIdentity();
            Assert.IsFalse(expected.Equals(actual));
            Assert.IsFalse(expected.Equals((object?)actual));
            Assert.IsFalse(expected.Equals(DateTime.Now));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            Guid identifier = Guid.NewGuid();
            IModel model = new UserModel(identifier, "User");
            var expected = new ModelIdentity(model, new object[] { identifier });
            var actual = model.GetIdentity();
            Assert.IsTrue(expected.GetHashCode() == actual?.GetHashCode());
        }
    }
}
