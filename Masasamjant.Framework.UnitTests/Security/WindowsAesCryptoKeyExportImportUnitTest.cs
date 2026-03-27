using System.Runtime.Versioning;

namespace Masasamjant.Security
{
    [TestClass]
    [SupportedOSPlatform("windows")]
    public class WindowsAesCryptoKeyExportImportUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_Export_Import_Async()
        {
            var key = new AesCryptoKey("Good4Life!", Salt.SHA1("Foo"));
            var export = new WindowsAesCryptoKeyExport();
            var import = new WindowsAesCryptoKeyImport();
            Stream stream = new MemoryStream();
            using (stream)
            {
                await export.ExportAsync(key, stream);
                stream.Position = 0L;
                var key2 = await import.ImportAsync(stream);
                CollectionAssert.AreEqual(key.Key, key2.Key);
                CollectionAssert.AreEqual(key.IV, key2.IV);
            }
        }
    }
}
