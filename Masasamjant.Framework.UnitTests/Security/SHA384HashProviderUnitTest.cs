using System.Text;

namespace Masasamjant.Security
{
    [TestClass]
    public class SHA384HashProviderUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_HashData()
        {
            var provider = new SHA384HashProvider();
            byte[] data = Encoding.UTF8.GetBytes("Mickey Mouse");
            byte[] hash =  provider.HashData(data);
            Assert.AreNotEqual(data, hash);
            Assert.AreEqual(HashLength.SHA384BytesLength, hash.Length);
        }

        [TestMethod]
        public async Task Test_HashDataAsync()
        {
            var provider = new SHA384HashProvider();
            byte[] data = Encoding.UTF8.GetBytes("Mickey Mouse");
            byte[] hash = await provider.HashDataAsync(data);
            Assert.AreNotEqual(data, hash);
            Assert.AreEqual(HashLength.SHA384BytesLength, hash.Length);
        }
    }
}
