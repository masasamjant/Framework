using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Masasamjant.Security
{
    [TestClass]
    public class AesCryptoKeyExportUnitTest : UnitTest
    {
        [TestMethod]
        public async Task Test_ExportAsync()
        {
            var key = new AesCryptoKey("Good4Life!", new Salt("Foo", new Base64SHA1Provider()));
            var export = new AesCryptoKeyExport();
            var stream = new MemoryStream();
            await export.ExportAsync(key, stream);
            stream.Position = 0L;
            var import = new AesCryptoKeyImport();
            var key2 = await import.ImportAsync(stream);
            CollectionAssert.AreEqual(key.Key, key2.Key);
            CollectionAssert.AreEqual(key.IV, key2.IV);
        }
    }
}
