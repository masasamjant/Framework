using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    [TestClass]
    public class EncryptedStringPropertyManagerUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_EncryptedString_Cryptography()
        {
            var instance = new EncryptedStringContainer("Mickey Mouse");
            IDataCryptography cryptography = new AesDataCryptography(iterations: 1000);
            string password = "Good4Life!";
            Salt salt = new Salt("Test", new Base64SHA1Provider());
            var manager = new EncryptedStringPropertyManager(cryptography, true);
            
            await manager.EncryptPropertiesAsync(instance, password, salt);
            Assert.AreNotEqual("Mickey Mouse", instance.Value);
            Assert.IsTrue(instance.Value.Length > 0);
            
            cryptography = new AesDataCryptography(iterations: 1000);
            manager = new EncryptedStringPropertyManager(cryptography, true);
            await manager.DecryptPropertiesAsync(instance, password, salt);
            Assert.AreEqual("Mickey Mouse", instance.Value);

            cryptography = new AesDataCryptography(iterations: 1000);
            manager = new EncryptedStringPropertyManager(cryptography, false);
            await manager.EncryptPropertiesAsync(instance, password, salt);
            Assert.AreEqual("Mickey Mouse", instance.Value);
        }

        [TestMethod]
        public async Task Test_EncryptedString_Cryptography_With_CryptoKey()
        {
            var instance = new EncryptedStringContainer("Mickey Mouse");
            IDataCryptography<AesCryptoKey> cryptography = new AesDataCryptography(iterations: 1000);
            string password = "Good4Life!";
            Salt salt = new Salt("Test", new Base64SHA1Provider());
            var cryptoKey = new AesCryptoKey(password, salt);
            var manager = new EncryptedStringPropertyManager<AesCryptoKey>(cryptography, true);

            await manager.EncryptPropertiesAsync(instance, cryptoKey);
            Assert.AreNotEqual("Mickey Mouse", instance.Value);
            Assert.IsTrue(instance.Value.Length > 0);

            cryptography = new AesDataCryptography(iterations: 1000);
            manager = new EncryptedStringPropertyManager<AesCryptoKey>(cryptography, true);
            await manager.DecryptPropertiesAsync(instance, cryptoKey);
            Assert.AreEqual("Mickey Mouse", instance.Value);

            cryptography = new AesDataCryptography(iterations: 1000);
            manager = new EncryptedStringPropertyManager<AesCryptoKey>(cryptography, false);
            await manager.EncryptPropertiesAsync(instance, cryptoKey);
            Assert.AreEqual("Mickey Mouse", instance.Value);
        }
    }

    public class EncryptedStringContainer
    {
        public EncryptedStringContainer(string value)
        {
            Value = value;
        }

        [EncryptedStringProperty]
        public string Value { get; internal set; } = string.Empty;
    }
}
