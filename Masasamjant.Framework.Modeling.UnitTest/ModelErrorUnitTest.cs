using System.Text.Json;

namespace Masasamjant.Modeling
{
    [TestClass]
    public class ModelErrorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Default_Constructor()
        {
            var error = new ModelError();
            Assert.AreEqual(string.Empty, error.MemberName);
            Assert.AreEqual(string.Empty, error.ErrorMessage);
        }

        [TestMethod]
        public void Test_Constructor()
        {
            var error = new ModelError("Member", "Message");
            Assert.AreEqual("Member", error.MemberName);
            Assert.AreEqual("Message", error.ErrorMessage);
        }

        [TestMethod]
        public void Test_Serialization()
        {
            var error = new ModelError("Member", "Message");
            var json = JsonSerializer.Serialize(error);
            var other = JsonSerializer.Deserialize<ModelError>(json);
            Assert.IsNotNull(other);
            Assert.AreEqual(error.MemberName, other.MemberName);
            Assert.AreEqual(error.ErrorMessage, other.ErrorMessage);
        }
    }
}
