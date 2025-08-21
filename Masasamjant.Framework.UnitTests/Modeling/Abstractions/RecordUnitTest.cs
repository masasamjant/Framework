using Masasamjant.Linq;
using Masasamjant.Modeling.Services.Abstractions;
using System.Linq.Expressions;

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
            var repository = new UserRepository(userIdentityProvider);
            var model = new UserModel(Guid.NewGuid(), "Test");
            Assert.IsNull(model.CreatedBy);
            Assert.AreEqual(DateTimeOffset.MinValue, model.CreatedAt);
            await repository.AddAsync(model);
            Assert.AreEqual("User", model.CreatedBy);
            Assert.AreNotEqual(DateTimeOffset.MinValue, model.CreatedAt);
        }

        [TestMethod]
        public async Task Test_Update()
        {
            var userIdentityProvider = new UserIdentityProvider(() => new UserIdentity("User"));
            var repository = new UserRepository(userIdentityProvider);
            var model = new UserModel(Guid.NewGuid(), "Test");
            Assert.IsNull(model.ModifiedBy);
            Assert.IsFalse(model.ModifiedAt.HasValue);
            await repository.UpdateAsync(model);
            Assert.AreEqual("User", model.ModifiedBy);
            Assert.IsTrue(model.ModifiedAt.HasValue);
        }

        private class UserRepository : Repository<UserModel, Guid>
        {
            public UserRepository(IUserIdentityProvider userIdentityProvider) 
                : base(userIdentityProvider)
            { }

            public override Task<UserModel> AddAsync(UserModel model)
            {
                OnAdd(model);
                return Task.FromResult(model);
            }

            public override Task<UserModel> DeleteAsync(UserModel model)
            {
                OnRemove(model);
                return Task.FromResult(model);
            }

            public override Task<UserModel?> FindAsync(Guid identifier)
            {
                throw new NotImplementedException();
            }

            public override Task<UserModel?> FindAsync(object key)
            {
                throw new NotImplementedException();
            }

            public override IQueryable<UserModel> Query()
            {
                throw new NotImplementedException();
            }

            public override IQueryable<UserModel> Query(IQuery<UserModel> query)
            {
                throw new NotImplementedException();
            }

            public override Task SaveAsync()
            {
                throw new NotImplementedException();
            }

            public override Task<UserModel> UpdateAsync(UserModel model)
            {
                OnUpdate(model);
                return Task.FromResult(model);
            }
        }
    }
}
