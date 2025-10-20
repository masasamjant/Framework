using Masasamjant.IO;

namespace Masasamjant.Security
{
    [TestClass]
    public class AesFileCryptographyUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_File_Cryptography()
        {
            var provider = new Base64SHA256Provider();
            var cryptography = new AesFileCryptography(iterations: 1000); // minimum iteration
            var password = "Good4Life!";
            var salt = new Salt("KickMe", provider);
            var content = "Mikki Hiiri";
            var sourceFile = FileHelper.CreateTempTextFile(content);
            var destinationFile = Path.GetTempFileName();
            await cryptography.EncryptAsync(sourceFile, destinationFile, password, salt, true);
            var content2 = File.ReadAllText(destinationFile);
            Assert.AreNotEqual(content, content2);

            cryptography = new AesFileCryptography(iterations: 1000);
            await cryptography.DecryptAsync(destinationFile, sourceFile, password, salt, true);
            content2 = File.ReadAllText(sourceFile);
            Assert.AreEqual(content, content2);

            var cryptoKey = new AesCryptoKey(password, salt);
            cryptography = new AesFileCryptography(iterations: 1000);
            await cryptography.EncryptAsync(sourceFile, destinationFile, cryptoKey, true);
            content2 = File.ReadAllText(destinationFile);
            Assert.AreNotEqual(content, content2);

            cryptography = new AesFileCryptography(iterations: 1000);
            await cryptography.DecryptAsync(destinationFile, sourceFile, cryptoKey, true);
            content2 = File.ReadAllText(sourceFile);
            Assert.AreEqual(content, content2);

            File.Delete(sourceFile);
            File.Delete(destinationFile);
        }
    }
}
