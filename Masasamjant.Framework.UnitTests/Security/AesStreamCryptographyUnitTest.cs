using Masasamjant.IO;
using System.Security.Cryptography;
using System.Text;

namespace Masasamjant.Security
{
    [TestClass]
    public class AesStreamCryptographyUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_Stream_Cryptography()
        {
            var provider = new Base64SHA256Provider();
            var cryptography = new AesStreamCryptography(iterations: 1000); // minimum iteration
            var password = "Good4Life!";
            var salt = new Salt("KickMe", provider);
            byte[] clearData = Encoding.Unicode.GetBytes("Mikki Hiiri");
            var sourceStream = new MemoryStream(clearData);
            var destinationStream = new MemoryStream();
            await cryptography.EncryptAsync(sourceStream, destinationStream, password, salt);
            byte[] cipherData = destinationStream.ToArray();
            CollectionAssert.AreNotEqual(clearData, cipherData);
            sourceStream = new MemoryStream(cipherData);
            destinationStream = new MemoryStream();
            await cryptography.DecryptAsync(sourceStream, destinationStream, password, salt);
            byte[] clearData2 = destinationStream.ToArray();
            CollectionAssert.AreEqual(clearData, clearData2);
        }

        [TestMethod]
        public async Task Test_Validate_Password()
        {
            var provider = new Base64SHA256Provider();
            var cryptography = new AesStreamCryptography(iterations: 1000); // minimum iteration
            var salt = new Salt("KickMe", provider);
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => cryptography.DecryptAsync(new MemoryStream(), new MemoryStream(), "  ", salt));
            await Assert.ThrowsExceptionAsync<ArgumentNullException>(() => cryptography.DecryptAsync(new MemoryStream(), new MemoryStream(), "", salt));
        }

        [TestMethod]
        public async Task Test_Validate_Streams()
        {
            var provider = new Base64SHA256Provider();
            var cryptography = new AesStreamCryptography(iterations: 1000); // minimum iteration
            var password = "Good4Life!";
            var salt = new Salt("KickMe", provider);
            Stream sourceStream;
            Stream destinationStream;
            
            using (sourceStream = new MemoryStream())
            {
                destinationStream = sourceStream;
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => cryptography.DecryptAsync(sourceStream, destinationStream, password, salt));
            }

            using (sourceStream = new CryptoStream(new MemoryStream(), Aes.Create().CreateDecryptor(), CryptoStreamMode.Read))
            using (destinationStream = new MemoryStream())
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => cryptography.DecryptAsync(sourceStream, destinationStream, password, salt));

            using (sourceStream = new MemoryStream())
            using (destinationStream = new CryptoStream(new MemoryStream(), Aes.Create().CreateDecryptor(), CryptoStreamMode.Write))
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => cryptography.DecryptAsync(sourceStream, destinationStream, password, salt));

            using (sourceStream = new WriteOnlyStream(new  MemoryStream()))
            using (destinationStream = new MemoryStream())
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => cryptography.DecryptAsync(sourceStream, destinationStream, password, salt));

            using (sourceStream = new MemoryStream())
            using (destinationStream = new ReadOnlyStream(new MemoryStream()))
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => cryptography.DecryptAsync(sourceStream, destinationStream, password, salt));
        }
    }
}
