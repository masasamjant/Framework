using Masasamjant.Web.Stubs;

namespace Masasamjant.Web
{
    [TestClass]
    public class SessionStorageHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetGuid_SetGuid()
        {
            string key = "key";
            ISessionStorage storage = new SessionStorageStub();
            Guid? result = SessionStorageHelper.GetGuid(storage, key);
            Assert.IsFalse(result.HasValue);
            Guid expected = Guid.NewGuid();
            SessionStorageHelper.SetGuid(storage, key, expected);
            result = SessionStorageHelper.GetGuid(storage, key);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Test_GetInt32_SetInt32()
        {
            string key = "key";
            ISessionStorage storage = new SessionStorageStub();
            int? result = SessionStorageHelper.GetInt32(storage, key);
            Assert.IsFalse(result.HasValue);
            int expected = 100;
            SessionStorageHelper.SetInt32(storage, key, expected);
            result = SessionStorageHelper.GetInt32(storage, key);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Test_GetInt64_SetInt64()
        {
            string key = "key";
            ISessionStorage storage = new SessionStorageStub();
            long? result = SessionStorageHelper.GetInt64(storage, key);
            Assert.IsFalse(result.HasValue);
            long expected = 100L;
            SessionStorageHelper.SetInt64(storage, key, expected);
            result = SessionStorageHelper.GetInt64(storage, key);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Test_GetEnum_SetEnum()
        {
            string key = "key";
            ISessionStorage storage = new SessionStorageStub();
            DateTimeKind? result = SessionStorageHelper.GetEnum<DateTimeKind>(storage, key);
            Assert.IsFalse(result.HasValue);
            DateTimeKind expected = DateTimeKind.Utc;
            SessionStorageHelper.SetEnum(storage, key, expected);
            result = SessionStorageHelper.GetEnum<DateTimeKind>(storage, key);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Test_GetDouble_SetDouble()
        {
            string key = "key";
            ISessionStorage storage = new SessionStorageStub();
            double? result = SessionStorageHelper.GetDouble(storage, key);
            Assert.IsFalse(result.HasValue);
            double expected = 0.01D;
            SessionStorageHelper.SetDouble(storage, key, expected);
            result = SessionStorageHelper.GetDouble(storage, key);
            Assert.IsTrue(result.HasValue);
            Assert.AreEqual(expected, result.Value);
        }

        [TestMethod]
        public void Test_JsonDeserialize_JsonSerialize()
        {
            string key = "key";
            ISessionStorage storage = new SessionStorageStub();
            Pet? actual = SessionStorageHelper.JsonDeserialize<Pet>(storage, key);
            Assert.IsNull(actual);
            Pet expected = new Pet()
            {
                Name = "Peter the Rabbit",
                Age = 4
            };
            SessionStorageHelper.JsonSerialize(storage, key, expected);
            var json = storage.GetString(key);
            Assert.IsFalse(string.IsNullOrWhiteSpace(json));
            actual = SessionStorageHelper.JsonDeserialize<Pet>(storage, key);
            Assert.IsNotNull(actual);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Age, actual.Age);
        }
    }
}
