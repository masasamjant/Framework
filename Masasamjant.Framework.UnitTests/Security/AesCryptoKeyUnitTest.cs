using Masasamjant.IO;
using Masasamjant.Security.Abstractions;

namespace Masasamjant.Security
{
    [TestClass]
    public class AesCryptoKeyUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var salt = new Salt("Test", new Base64SHA256Provider());
            Assert.ThrowsException<ArgumentNullException>(() => new AesCryptoKey("", salt));
            Assert.ThrowsException<ArgumentNullException>(() => new AesCryptoKey("  ", salt));
            var cryptoKey = new AesCryptoKey("Testing", salt, iterations: CryptoKey.MinIterations);
            Assert.AreEqual(32, cryptoKey.Key.Length);
            Assert.AreEqual(16, cryptoKey.IV.Length);
            var cryptoKey2 = new AesCryptoKey("Testing", salt, iterations: CryptoKey.MinIterations);
            CollectionAssert.AreEqual(cryptoKey.Key, cryptoKey2.Key);
            CollectionAssert.AreEqual(cryptoKey.IV, cryptoKey2.IV);
        }

        [TestMethod]
        public async Task Test_Export_Import()
        {
            var tempFilePath = FileHelper.CreateTempFilePath();
            var salt = new Salt("Test", new Base64SHA256Provider());
            var cryptoKey = new AesCryptoKey("Testing", salt, iterations: CryptoKey.MinIterations);
            await AesCryptoKey.ExportAsync(cryptoKey, tempFilePath);
            Assert.IsTrue(File.Exists(tempFilePath));
            var cryptoKey2 = await AesCryptoKey.ImportAsync(tempFilePath);
            CollectionAssert.AreEqual(cryptoKey.Key, cryptoKey2.Key);
            CollectionAssert.AreEqual(cryptoKey.IV, cryptoKey2.IV);
            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task Test_ExportAsync()
        {
            var tempFilePath = FileHelper.CreateTempTextFile(null);
            var salt = new Salt("Test", new Base64SHA256Provider());
            var cryptoKey = new AesCryptoKey("Testing", salt, iterations: CryptoKey.MinIterations);
            await Assert.ThrowsExceptionAsync<ArgumentException>(() => AesCryptoKey.ExportAsync(cryptoKey, tempFilePath));
            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task Test_ImportAsync()
        {
            var tempFilePath = FileHelper.CreateTempFilePath();
            await Assert.ThrowsExceptionAsync<FileNotFoundException>(() => AesCryptoKey.ImportAsync(tempFilePath));
        }
    }
}
