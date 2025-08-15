using Masasamjant.Modeling.Abstractions.Services;

namespace Masasamjant.Modeling.Abstractions
{
    [TestClass]
    public class RecordUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Delete()
        {
            var model = new UserModel(Guid.NewGuid(), "Test");
            Assert.IsFalse(model.IsDeleted);
            Assert.IsFalse(model.DeletedAt.HasValue);
            Assert.IsNull(model.DeletedBy);
            model.Delete("User");
            Assert.IsTrue(model.IsDeleted);
            Assert.IsTrue(model.DeletedAt.HasValue);
            Assert.AreEqual("User", model.DeletedBy);
        }

        [TestMethod]
        public async Task Test_Add()
        {
            var userIdentityProvider = new UserIdentityProvider(() => new UserIdentity("User"));
            var manager = new UserManager(userIdentityProvider);
            var model = new UserModel(Guid.NewGuid(), "Test");
            Assert.IsNull(model.CreatedBy);
            Assert.AreEqual(DateTimeOffset.MinValue, model.CreatedAt);
            await manager.AddAsync(model);
            Assert.AreEqual("User", model.CreatedBy);
            Assert.AreNotEqual(DateTimeOffset.MinValue, model.CreatedAt);
        }

        [TestMethod]
        public async Task Test_Update()
        {
            var userIdentityProvider = new UserIdentityProvider(() => new UserIdentity("User"));
            var manager = new UserManager(userIdentityProvider);
            var model = new UserModel(Guid.NewGuid(), "Test");
            Assert.IsNull(model.ModifiedBy);
            Assert.IsFalse(model.ModifiedAt.HasValue);
            await manager.UpdateAsync(model);
            Assert.AreEqual("User", model.ModifiedBy);
            Assert.IsTrue(model.ModifiedAt.HasValue);
        }

        private class UserManager : Manager<UserModel>
        {
            public UserManager(IUserIdentityProvider userIdentityProvider) 
                : base(userIdentityProvider)
            { }

            public override Task<UserModel> AddAsync(UserModel model)
            {
                OnAdd(model);
                return Task.FromResult(model);
            }

            public override Task<UserModel> RemoveAsync(UserModel model)
            {
                OnRemove(model);
                return Task.FromResult(model);
            }

            public override Task<UserModel> UpdateAsync(UserModel model)
            {
                OnUpdate(model);
                return Task.FromResult(model);
            }
        }
    }
}
