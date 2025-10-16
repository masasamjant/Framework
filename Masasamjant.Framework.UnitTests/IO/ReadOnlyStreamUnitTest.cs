using System.Text;

namespace Masasamjant.IO
{
    [TestClass]
    public class ReadOnlyStreamUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var stream = new MemoryStream();
            var rs = new ReadOnlyStream(stream);
            Assert.IsNotNull(rs);
            rs.Dispose();
            stream = new MemoryStream();
            Assert.ThrowsException<ArgumentException>(() => new ReadOnlyStream(new WriteOnlyStream(stream)));
            stream.Dispose();
        }

        [TestMethod]
        public void Test_ReadOnlyStream()
        {
            var data = Encoding.UTF8.GetBytes("Testing");
            var stream = new MemoryStream(data);
            var rs = new ReadOnlyStream(stream);
            Assert.IsTrue(rs.CanRead);
            Assert.IsTrue(rs.CanSeek);
            Assert.IsFalse(rs.CanWrite);
            Assert.AreEqual(rs.Length, data.Length);
            Assert.AreEqual(0L, rs.Position);
            Assert.ThrowsException<NotSupportedException>(() => rs.Flush());
            byte[] buffer = new byte[512];
            Assert.AreEqual(1, rs.Read(buffer, 0, 1));
            Assert.AreEqual(0L, rs.Seek(0L, SeekOrigin.Begin));
            Assert.ThrowsException<NotSupportedException>(() => rs.SetLength(0L));
            Assert.ThrowsException<NotSupportedException>(() => rs.Write(buffer, 0, buffer.Length));
            rs.Dispose();
        }
    }
}
