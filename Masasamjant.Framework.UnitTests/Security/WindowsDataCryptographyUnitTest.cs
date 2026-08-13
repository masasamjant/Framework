using System.Runtime.Versioning;
using System.Security.Cryptography;
using System.Text;

namespace Masasamjant.Security
{
    [TestClass]
    [SupportedOSPlatform("windows")]
    public class WindowsDataCryptographyUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var cryptography = new WindowsDataCryptography(DataProtectionScope.CurrentUser);
            Assert.AreEqual(DataProtectionScope.CurrentUser, cryptography.Scope);
            cryptography = new WindowsDataCryptography(DataProtectionScope.LocalMachine);
            Assert.AreEqual(DataProtectionScope.LocalMachine, cryptography.Scope);
            Assert.ThrowsException<ArgumentException>(() => new WindowsDataCryptography((DataProtectionScope)999));
        }

        [TestMethod]
        public void Test_EncryptDecryptString()
        {
            var cryptography = new WindowsDataCryptography(DataProtectionScope.CurrentUser);
            var clearData = "Hello, World!";
            var encryptedData = cryptography.EncryptString(clearData);
            var decryptedData = cryptography.DecryptString(encryptedData);
            Assert.AreEqual(clearData, decryptedData);

            encryptedData = cryptography.EncryptString(clearData, Encoding.UTF8);
            decryptedData = cryptography.DecryptString(encryptedData, Encoding.UTF8);
            Assert.AreEqual(clearData, decryptedData);

            byte[]? entropy = Encoding.UTF8.GetBytes("Cat5!");
            encryptedData = cryptography.EncryptString(clearData, Encoding.UTF8, entropy);
            decryptedData = cryptography.DecryptString(encryptedData, Encoding.UTF8, entropy);
            Assert.AreEqual(clearData, decryptedData);
        }

        [TestMethod]
        public void Test_EncryptDecryptData()
        {
            var cryptography = new WindowsDataCryptography(DataProtectionScope.CurrentUser);
            var clearData = Encoding.UTF8.GetBytes("Hello, World!");
            var encryptedData = cryptography.EncryptData(clearData);
            var decryptedData = cryptography.DecryptData(encryptedData);
            Assert.IsTrue(clearData.AsSpan().SequenceEqual(decryptedData));

            byte[]? entropy = Encoding.UTF8.GetBytes("Cat5!");
            encryptedData = cryptography.EncryptData(clearData, entropy);
            decryptedData = cryptography.DecryptData(encryptedData, entropy);
            Assert.IsTrue(clearData.AsSpan().SequenceEqual(decryptedData));
        }

        [TestMethod]
        public void Test_DecryptString_WhenNull_ThenReturnEmpty()
        {
            var cryptography = new WindowsDataCryptography(DataProtectionScope.CurrentUser);
            string? cipherData = null;
            var decryptedData = cryptography.DecryptString(cipherData!);
            Assert.AreEqual(string.Empty, decryptedData);
        }

        [TestMethod]
        public void Test_DecryptString_WhenEmpty_ThenReturnEmpty()
        {
            var cryptography = new WindowsDataCryptography(DataProtectionScope.CurrentUser);
            string? cipherData = "";
            var decryptedData = cryptography.DecryptString(cipherData!);
            Assert.AreEqual(string.Empty, decryptedData);
        }

        [TestMethod]
        public void Test_EncryptString_WhenNull_ThenReturnEmpty()
        {
            var cryptography = new WindowsDataCryptography(DataProtectionScope.CurrentUser);
            string? clearData = null;
            var encryptedData = cryptography.EncryptString(clearData!);
            Assert.AreEqual(string.Empty, encryptedData);
        }

        [TestMethod]
        public void Test_EncryptString_WhenEmpty_ThenReturnEmpty()
        {
            var cryptography = new WindowsDataCryptography(DataProtectionScope.CurrentUser);
            string? clearData = "";
            var encryptedData = cryptography.EncryptString(clearData!);
            Assert.AreEqual(string.Empty, encryptedData);
        }
    }
}
