namespace Masasamjant.Security
{
    [TestClass]
    public class HexSHA1ProviderUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_CreateHash()
        {
            var provider = new HexSHA1Provider();
            string value = string.Empty;
            string hash = provider.CreateHash(value);
            Assert.AreEqual(value, hash);
            value = "Testing";
            hash = provider.CreateHash(value);
            Assert.AreNotEqual(value, hash);
            string hash2 = provider.CreateHash(value);
            Assert.AreEqual(hash, hash2);
            value = "Resting";
            hash2 = provider.CreateHash(value);
            Assert.AreNotEqual(hash, hash2);
        }

        [TestMethod]
        public async Task Test_CreateHashAsync()
        {
            var provider = new HexSHA1Provider();
            string value = string.Empty;
            string hash = await provider.CreateHashAsync(value);
            Assert.AreEqual(value, hash);
            value = "Testing";
            hash = provider.CreateHash(value);
            Assert.AreNotEqual(value, hash);
            string hash2 = await provider.CreateHashAsync(value);
            Assert.AreEqual(hash, hash2);
            value = "Resting";
            hash2 = await provider.CreateHashAsync(value);
            Assert.AreNotEqual(hash, hash2);
        }
    }
}
