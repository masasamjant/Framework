using System.Text;

namespace Masasamjant.IO
{
    [TestClass]
    public class WriteOnlyStreamUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var stream = new MemoryStream();
            var rs = new WriteOnlyStream(stream);
            Assert.IsNotNull(rs);
            rs.Dispose();
            stream = new MemoryStream();
            Assert.ThrowsException<ArgumentException>(() => new WriteOnlyStream(new ReadOnlyStream(stream)));
            stream.Dispose();
        }

        [TestMethod]
        public void Test_WriteOnlyStream()
        {
            var stream = new MemoryStream();
            var rs = new WriteOnlyStream(stream);
            Assert.IsFalse(rs.CanRead);
            Assert.IsFalse(rs.CanSeek);
            Assert.IsTrue(rs.CanWrite);
            Assert.AreEqual(0L, rs.Length);
            Assert.AreEqual(0L, rs.Position);
            Assert.ThrowsException<NotSupportedException>(() => rs.Position = 0L);
            byte[] buffer = new byte[512];
            Assert.ThrowsException<NotSupportedException>(() => rs.Read(buffer, 0, buffer.Length));
            Assert.ThrowsException<NotSupportedException>(() => rs.Seek(0L, SeekOrigin.Begin));
            Assert.ThrowsException<NotSupportedException>(() => rs.SetLength(0L));
            buffer = Encoding.UTF8.GetBytes("Testing");
            rs.Write(buffer, 0, buffer.Length);
            Assert.AreEqual(rs.Length, buffer.Length);
            rs.Dispose();
        }
    }
}
