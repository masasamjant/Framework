using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    [TestClass]
    public class ModelHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_IsValid()
        {
            Guid identifier = Guid.NewGuid();
            Model model = new UserModel(identifier, "Test");
            ModelValidationException? validationException;
            Assert.IsTrue(ModelHelper.IsValid(model, out validationException));
            Assert.IsNull(validationException);
            model = new UserModel(identifier, string.Empty);
            Assert.IsFalse(ModelHelper.IsValid(model, out validationException));
            Assert.IsNotNull(validationException);
            Assert.IsTrue(ReferenceEquals(model, validationException.Model));
            Assert.IsTrue(validationException.Errors.Any());
            var error = validationException.Errors.First();
            Assert.AreEqual("Name", error.PropertyName);
            Assert.AreEqual("Name of the user is mandatory and cannot be empty string.", error.ErrorMessage);
        }

        [TestMethod]
        public void Test_TryPrepareModel()
        {
            Guid identifier = Guid.NewGuid();
            var model = new UserModel(identifier, "Test");
            Assert.IsFalse(model.IsPrepared);
            model = ModelHelper.TryPrepareModel(model);
            Assert.IsTrue(model.IsPrepared);
        }

        [TestMethod]
        public void Test_HasVersion()
        {
            Assert.IsFalse(ModelHelper.HasVersion(new UserModel("Test")));
            Assert.IsTrue(ModelHelper.HasVersion(new UserModel(Guid.NewGuid(), "Test")));
        }

        [TestMethod]
        public void Test_Same()
        {
            Guid identifier = Guid.NewGuid();
            var model = new UserModel(identifier, "Test", identifier.ToByteArray());
            var other = model;
            Assert.IsTrue(ModelHelper.Same(model, other));
            other = new UserModel(identifier, "Test", identifier.ToByteArray());
            Assert.IsTrue(ModelHelper.Same(model, other));
            other = new UserModel(identifier, "User", identifier.ToByteArray());
            Assert.IsTrue(ModelHelper.Same(model, other));
            other = new UserModel(identifier, "Test", Guid.NewGuid().ToByteArray());
            Assert.IsFalse(ModelHelper.Same(model, other));
            other = new UserModel(Guid.NewGuid(), "Test", identifier.ToByteArray());
            Assert.IsFalse(ModelHelper.Same(model, other));
        }
    }
}
