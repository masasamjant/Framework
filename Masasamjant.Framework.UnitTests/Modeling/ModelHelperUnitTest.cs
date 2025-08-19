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
            Assert.AreEqual("Name", error.MemberName);
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

        [TestMethod]
        public void Test_TryUpdate()
        {
            var a = new A(1, "A");
            var b = new B(1, "B");
            Assert.IsTrue(ModelHelper.TryUpdate(a, b) && a.Value == "B");
            b = new B(2, "C");
            Assert.IsFalse(ModelHelper.TryUpdate(a, b));
            Assert.IsTrue(a.Value == "B");
        }

        private class A : Model, IUpdateable<B>
        {
            public A(int id, string value)
            {
                Id = id;
                Value = value;
            }

            public int Id { get; private set; }

            public string Value { get; private set; } = string.Empty;

            protected override object[] GetKeyProperties()
            {
                return [Id];
            }

            private bool CanUpdateFrom(B source) => Id == source.Id;

            private void UpdateFrom(B source)
            {
                if (!CanUpdateFrom(source))
                    throw new ArgumentException("Cannot update from source instance.", nameof(source));

                Value = source.Value;
            }
            
            bool IUpdateable<B>.CanUpdateFrom(B source)
            {
                return this.CanUpdateFrom(source);
            }

            void IUpdateable<B>.UpdateFrom(B source)
            {
                this.UpdateFrom(source);
            }
        }

        private class B : Model
        {
            public B(int id, string value)
            {
                Id = id;
                Value = value;
            }

            public int Id { get; private set; }

            public string Value { get; private set; } = string.Empty;

            protected override object[] GetKeyProperties()
            {
                return [Id];
            }
        }
    }
}
