using Masasamjant.IO;

namespace Masasamjant.Security
{
    [TestClass]
    public class AesCryptoKeyExportImportUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_Export_Import_Async()
        {
            var key = new AesCryptoKey("Good4Life!", new Salt("Foo", new Base64SHA1Provider()));
            var export = new AesCryptoKeyExport();
            var import = new AesCryptoKeyImport();
            Stream stream = new MemoryStream();
            using (stream)
            {
                await export.ExportAsync(key, stream);
                stream.Position = 0L;
                var key2 = await import.ImportAsync(stream);
                CollectionAssert.AreEqual(key.Key, key2.Key);
                CollectionAssert.AreEqual(key.IV, key2.IV);
            }

            stream = new ReadOnlyStream(new MemoryStream());
        
            using (stream)
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => export.ExportAsync(key, stream));

            stream = new WriteOnlyStream(new MemoryStream());

            using (stream)
                await Assert.ThrowsExceptionAsync<ArgumentException>(() => import.ImportAsync(stream));
        }
    }
}
