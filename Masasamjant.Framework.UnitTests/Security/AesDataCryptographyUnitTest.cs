using System.Text;

namespace Masasamjant.Security
{
    [TestClass]
    public class AesDataCryptographyUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_String_Cryptography()
        {
            var provider = new Base64SHA256Provider();
            var cryptography = new AesDataCryptography(iterations: 1000); // minimum iteration
            var password = "Good4Life!";
            var salt = new Salt("KickMe", provider);
            var clearText = "Mikki Hiiri";
            
            var cipherText = await cryptography.EncryptStringAsync(clearText, password, salt);
            Assert.AreNotEqual(clearText, cipherText);
            cryptography = new AesDataCryptography(iterations: 1000);
            var clearText2 = await cryptography.DecryptStringAsync(cipherText, password, salt);
            Assert.AreEqual(clearText, clearText2);

            cryptography = new AesDataCryptography(iterations: 1000);
            var cryptoKey = new AesCryptoKey("Good4Life!", salt);
            cipherText = await cryptography.EncryptStringAsync(clearText, cryptoKey);
            Assert.AreNotEqual(clearText, cipherText);
            cryptography = new AesDataCryptography(iterations: 1000);
            clearText2 = await cryptography.DecryptStringAsync(cipherText, cryptoKey);
            Assert.AreEqual(clearText, clearText2);

        }

        [TestMethod]
        public async Task Test_Data_Cryptography()
        {
            var provider = new Base64SHA256Provider();
            var cryptography = new AesDataCryptography(iterations: 1000); // minimum iteration
            var password = "Good4Life!";
            var salt = new Salt("KickMe", provider);

            byte[] clearData = Encoding.Unicode.GetBytes("Mikki Hiiri");
            byte[] cipherData = await cryptography.EncryptDataAsync(clearData, password, salt);
            CollectionAssert.AreNotEqual(clearData, cipherData);
            cryptography = new AesDataCryptography(iterations: 1000);
            byte[] clearData2 = await cryptography.DecryptDataAsync(cipherData, password, salt);
            CollectionAssert.AreEqual(clearData, clearData2);

            cryptography = new AesDataCryptography(iterations: 1000);
            var cryptoKey = new AesCryptoKey(password, salt);
            cipherData = await cryptography.EncryptDataAsync(clearData, cryptoKey);
            CollectionAssert.AreNotEqual(clearData, cipherData);
            cryptography = new AesDataCryptography(iterations: 1000);
            clearData2 = await cryptography.DecryptDataAsync(cipherData, cryptoKey);
            CollectionAssert.AreEqual(clearData, clearData2);
        }
    }
}
