using System.IO.Compression;
using System.Text;

namespace Masasamjant.IO
{
    [TestClass]
    public class CompressionHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Compress_Decompress()
        {
            byte[] data = [];
            var compressed = CompressionHelper.Compress(data);
            Assert.IsTrue(compressed.Length == 0);
            CollectionAssert.AreEqual(data, compressed);

            data = [128];
            compressed = CompressionHelper.Compress(data);
            Assert.IsFalse(ReferenceEquals(data, compressed));
            Assert.IsTrue(compressed.Length == 1);
            CollectionAssert.AreEqual(data, compressed);

            data = GetLoremIpsumData();
            Assert.ThrowsException<ArgumentException>(() => CompressionHelper.Compress(data, (CompressionLevel)999));
            compressed = CompressionHelper.Compress(data);
            CollectionAssert.AreNotEqual(data, compressed);
            var result = CompressionHelper.Decompress(compressed);
            CollectionAssert.AreEqual(result, data);
        }

        [TestMethod]
        public async Task Test_Compress_Decompress_Async()
        {
            byte[] data = [];
            var compressed = await CompressionHelper.CompressAsync(data);
            Assert.IsTrue(compressed.Length == 0);
            CollectionAssert.AreEqual(data, compressed);

            data = [128];
            compressed = await CompressionHelper.CompressAsync(data);
            Assert.IsFalse(ReferenceEquals(data, compressed));
            Assert.IsTrue(compressed.Length == 1);
            CollectionAssert.AreEqual(data, compressed);

            data = GetLoremIpsumData();
            Assert.ThrowsException<ArgumentException>(() => CompressionHelper.Compress(data, (CompressionLevel)999));
            compressed = await CompressionHelper.CompressAsync(data);
            CollectionAssert.AreNotEqual(data, compressed);
            var result = await CompressionHelper.DecompressAsync(compressed);
            CollectionAssert.AreEqual(result, data);
        }

        [TestMethod]
        public void Test_Compress_Decompress_String()
        {
            string data = "";
            var compressed = CompressionHelper.Compress(data);
            Assert.IsTrue(compressed.Length == 0);
            data = GetLoremIpsumText();
            var dataBytes = data.GetByteArray(Encoding.UTF8);
            compressed = CompressionHelper.Compress(data, encoding: Encoding.UTF8);
            CollectionAssert.AreNotEqual(dataBytes, compressed);
            var result = CompressionHelper.Decompress(compressed, encoding: Encoding.UTF8);
            Assert.AreEqual(result, data);
        }

        [TestMethod]
        public async Task Test_Compress_Decompress_String_Async()
        {
            string data = "";
            var compressed = await CompressionHelper.CompressAsync(data);
            Assert.IsTrue(compressed.Length == 0);
            data = GetLoremIpsumText();
            var dataBytes = data.GetByteArray(Encoding.UTF8);
            compressed = await CompressionHelper.CompressAsync(data, encoding: Encoding.UTF8);
            CollectionAssert.AreNotEqual(dataBytes, compressed);
            var result = await CompressionHelper.DecompressAsync(compressed, encoding: Encoding.UTF8);
            Assert.AreEqual(result, data);
        }
    }
}
