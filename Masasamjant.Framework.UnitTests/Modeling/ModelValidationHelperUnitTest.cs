using Masasamjant.Modeling.Abstractions;

namespace Masasamjant.Modeling
{
    [TestClass]
    public class ModelValidationHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ValidateMandatoryString()
        {
            var model = new UserModel(Guid.NewGuid(), "Test");
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, null, "test"));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, string.Empty, "test"));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, string.Empty, "test"));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, string.Empty, "test", allowWhiteSpace: true));
            Assert.AreEqual(string.Empty, ModelValidationHelper.ValidateMandatoryString(model, string.Empty, "test", allowEmpty: true));
            Assert.AreEqual("  ", ModelValidationHelper.ValidateMandatoryString(model, "  ", "test", allowWhiteSpace: true));
            Assert.AreEqual("  ", ModelValidationHelper.ValidateMandatoryString(model, "  ", "test", allowWhiteSpace: true, checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: true));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, " Hello ", "test", checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: false));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, " Hello ", "test", checkNoLeadingWhiteSpace: false, checkNoTrailingWhiteSpace: true));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateMandatoryString(model, "Hello", "test", checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: true));
            Assert.AreEqual("  Hello", ModelValidationHelper.ValidateMandatoryString(model, "  Hello", "test", checkNoLeadingWhiteSpace: false, checkNoTrailingWhiteSpace: true));
            Assert.AreEqual("Hello  ", ModelValidationHelper.ValidateMandatoryString(model, "Hello  ", "test", checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: false));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, "Hello", "test", minLength: 6));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateMandatoryString(model, "Hello", "test", maxLength: 4));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateMandatoryString(model, "Hello", "test", minLength: 5));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateMandatoryString(model, "Hello", "test", maxLength: 5));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateMandatoryString(model, "Hello", "test", minLength: -1));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateMandatoryString(model, "Hello", "test", maxLength: -1));
        }

        [TestMethod]
        public void Test_ValidateOptionalString()
        {
            var model = new UserModel(Guid.NewGuid(), "Test");
            Assert.AreEqual(null, ModelValidationHelper.ValidateOptionalString(model, null, "test"));
            Assert.AreEqual(null, ModelValidationHelper.ValidateOptionalString(model, string.Empty, "test"));
            Assert.AreEqual(string.Empty, ModelValidationHelper.ValidateOptionalString(model, string.Empty, "test", emptyAsNull: false));
            Assert.AreEqual("  ", ModelValidationHelper.ValidateOptionalString(model, "  ", "test", allowWhiteSpace: true));
            Assert.AreEqual("  ", ModelValidationHelper.ValidateOptionalString(model, "  ", "test", allowWhiteSpace: true));
            Assert.AreEqual("  ", ModelValidationHelper.ValidateOptionalString(model, "  ", "test", allowWhiteSpace: true, checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: true));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateOptionalString(model, " Hello ", "test", checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: false));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateOptionalString(model, " Hello ", "test", checkNoLeadingWhiteSpace: false, checkNoTrailingWhiteSpace: true));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateOptionalString(model, "Hello", "test", checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: true));
            Assert.AreEqual("  Hello", ModelValidationHelper.ValidateOptionalString(model, "  Hello", "test", checkNoLeadingWhiteSpace: false, checkNoTrailingWhiteSpace: true));
            Assert.AreEqual("Hello  ", ModelValidationHelper.ValidateOptionalString(model, "Hello  ", "test", checkNoLeadingWhiteSpace: true, checkNoTrailingWhiteSpace: false));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateOptionalString(model, "Hello", "test", minLength: 6));
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateOptionalString(model, "Hello", "test", maxLength: 4));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateOptionalString(model, "Hello", "test", minLength: 5));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateOptionalString(model, "Hello", "test", maxLength: 5));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateOptionalString(model, "Hello", "test", minLength: -1));
            Assert.AreEqual("Hello", ModelValidationHelper.ValidateOptionalString(model, "Hello", "test", maxLength: -1));
        }

        [TestMethod]
        public void Test_ValidateReference()
        {
            var model = new UserModel(Guid.NewGuid(), "Test");
            var referenced = new UserModel("Test");
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateReference(model, referenced, "reference", mandatoryIdentity: true));
            var result = ModelValidationHelper.ValidateReference(model, referenced, "reference", mandatoryIdentity: false);
            Assert.IsTrue(ReferenceEquals(result, referenced));
            referenced = new UserModel(Guid.NewGuid(), "Test");
            result = ModelValidationHelper.ValidateReference(model, referenced, "reference", mandatoryIdentity: true);
            Assert.IsTrue(ReferenceEquals(result, referenced));
            referenced = new UserModel(Guid.NewGuid(), string.Empty);
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateReference(model, referenced, "reference", mandatoryIdentity: true));
        }

        [TestMethod]
        public void Test_ValidateOptionalReference()
        {
            var model = new UserModel(Guid.NewGuid(), "Test");
            UserModel? referenced = null;
            var result = ModelValidationHelper.ValidateOptionalReference(model, referenced, "reference");
            Assert.IsNull(result);
            referenced = new UserModel("Test");
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateOptionalReference(model, referenced, "reference", mandatoryIdentity: true));
            result = ModelValidationHelper.ValidateOptionalReference(model, referenced, "reference", mandatoryIdentity: false);
            Assert.IsTrue(ReferenceEquals(result, referenced));
            referenced = new UserModel(Guid.NewGuid(), "Test");
            result = ModelValidationHelper.ValidateOptionalReference(model, referenced, "reference", mandatoryIdentity: true);
            Assert.IsTrue(ReferenceEquals(result, referenced));
            referenced = new UserModel(Guid.NewGuid(), string.Empty);
            Assert.ThrowsException<ModelValidationException>(() => ModelValidationHelper.ValidateOptionalReference(model, referenced, "reference", mandatoryIdentity: true));
        }
    }
}
