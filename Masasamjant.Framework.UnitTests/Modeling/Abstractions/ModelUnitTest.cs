namespace Masasamjant.Modeling.Abstractions
{
    [TestClass]
    public class ModelUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Version()
        {
            Guid identifier = Guid.NewGuid();
            Model model = new UserModel(identifier, "Test");
            byte[] expected = identifier.ToByteArray();
            byte[] actual = model.Version;
            CollectionAssert.AreEqual(expected, actual);

            model = new UserModel("Test");
            Assert.IsTrue(model.Version.Length == 0);
        }

        [TestMethod]
        public void Test_GetVersionString()
        {
            Guid identifier = Guid.NewGuid();
            Model model = new UserModel(identifier, "Test");
            string expected = Convert.ToBase64String(identifier.ToByteArray()).ToUpperInvariant();
            string actual = model.GetVersionString();
            Assert.AreEqual(expected, actual);
            model = new UserModel("Test");
            Assert.IsTrue(string.IsNullOrEmpty(model.GetVersionString()));
        }

        [TestMethod]
        public void Test_GetIdentity()
        {
            Model model = new UserModel("Test");
            var identity = model.GetIdentity();
            Assert.IsNull(identity);
            Guid identifier = Guid.NewGuid();
            model = new UserModel(identifier, "Test");
            Model other = new UserModel(identifier, "Test");
            identity = model.GetIdentity();
            var otherIdentity = other.GetIdentity();
            Assert.IsNotNull(identity);
            Assert.IsNotNull(otherIdentity);
            Assert.AreEqual(identity.Value, otherIdentity.Value);
        }

        [TestMethod]
        public void Test_Validate()
        {
            Model model = new UserModel("");
            bool invalid = false;
            try
            {
                model.Validate();
            }
            catch (ModelValidationException exception)
            {
                invalid = true;
                Assert.IsTrue(ReferenceEquals(model, exception.Model));
                Assert.IsTrue(exception.Errors.Any());
                var error = exception.Errors.First();
                Assert.AreEqual("Name", error.PropertyName);
                Assert.AreEqual("Name of the user is mandatory and cannot be empty string.", error.ErrorMessage);
            }
            finally
            {
                Assert.IsTrue(invalid);
            }
        }
    }
}
