using System.Runtime.Versioning;
using System.Security.Cryptography;

namespace Masasamjant.Security
{
    [TestClass]
    [SupportedOSPlatform("windows")]
    public class WindowsAesCryptoKeyProtectorUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ProtectUnprotect()
        {
            var key = CreateCryptoKey();
            byte[] data = WindowsAesCryptoKeyProtector.Protect(key, DataProtectionScope.CurrentUser);
            var other = WindowsAesCryptoKeyProtector.Unprotect(data, DataProtectionScope.CurrentUser);
            CollectionAssert.AreEqual(key.Key, other.Key);
            CollectionAssert.AreEqual(key.IV, other.IV);
        }

        private static AesCryptoKey CreateCryptoKey()
        {
            var random = RandomHelper.CreateRandom();
            var password = RandomHelper.GetString(random, 12);
            var salt = Salt.SHA1(RandomHelper.GetString(random, 6));
            return new AesCryptoKey(password, salt, 100);
        }
    }
}
