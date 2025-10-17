using System.Text;

namespace Masasamjant.IO
{
    [TestClass]
    public class StreamHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_ToMemoryStream()
        {
            Stream stream;
            MemoryStream ms;

            using (var context = new DisposableContext())
            {
                stream = context.Add(new MemoryStream());
                ms = context.Add(StreamHelper.ToMemoryStream(stream));
                Assert.IsTrue(ReferenceEquals(stream, ms));
            }

            var tempFilePath = FileHelper.CreateTempFile("Testing");

            using (var context = new DisposableContext())
            {
                stream = context.Add(File.OpenRead(tempFilePath));
                ms = context.Add(StreamHelper.ToMemoryStream(stream));
                Assert.IsFalse(ReferenceEquals(stream, ms));
                var text = Encoding.UTF8.GetString(ms.ToArray());
                Assert.AreEqual("Testing", text);
            }

            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task Test_ToMemoryStreamAsync()
        {
            Stream stream;
            MemoryStream ms;

            using (var context = new DisposableContext())
            {
                stream = context.Add(new MemoryStream());
                ms = context.Add(await StreamHelper.ToMemoryStreamAsync(stream));
                Assert.IsTrue(ReferenceEquals(stream, ms));
            }

            var tempFilePath = FileHelper.CreateTempFile("Testing");

            using (var context = new DisposableContext())
            {
                stream = context.Add(File.OpenRead(tempFilePath));
                ms = context.Add(await StreamHelper.ToMemoryStreamAsync(stream));
                Assert.IsFalse(ReferenceEquals(stream, ms));
                var text = Encoding.UTF8.GetString(ms.ToArray());
                Assert.AreEqual("Testing", text);
                stream.Dispose();
                ms.Dispose();
            }

            File.Delete(tempFilePath);
        }

        [TestMethod]
        public void Test_ToBytes()
        {
            var tempFilePath = FileHelper.CreateTempFile("Testing");
            
            using (var context = new DisposableContext())
            {
                Stream stream1 = context.Add(new MemoryStream(Encoding.UTF8.GetBytes("Testing")));
                Stream stream2 = context.Add(File.OpenRead(tempFilePath));
                byte[] bytes1 = StreamHelper.ToBytes(stream1);
                byte[] bytes2 = StreamHelper.ToBytes(stream2);
                CollectionAssert.AreEqual(bytes1, bytes2);
            }

            File.Delete(tempFilePath);
        }

        [TestMethod]
        public async Task Test_ToBytesAsync()
        {
            var tempFilePath = FileHelper.CreateTempFile("Testing");

            using (var context = new DisposableContext())
            {
                Stream stream1 = context.Add(new MemoryStream(Encoding.UTF8.GetBytes("Testing")));
                Stream stream2 = context.Add(File.OpenRead(tempFilePath));
                byte[] bytes1 = await StreamHelper.ToBytesAsync(stream1);
                byte[] bytes2 = await StreamHelper.ToBytesAsync(stream2);
                CollectionAssert.AreEqual(bytes1, bytes2);
            }

            File.Delete(tempFilePath);
        }

        [TestMethod]
        public void Test_CopyStream()
        {
            var data = GetLoremIpsumData();
            var source = new MemoryStream(data);
            var destination = new MemoryStream();
            bool first = true;
            Action<StreamCopyProgress> progressHandler = x => 
            {
                Assert.IsTrue(ReferenceEquals(source, x.Source));
                Assert.IsTrue(ReferenceEquals(destination, x.Destination));
                if (first)
                    Assert.AreEqual(32, x.CopiedBytes);
                first = false;
            };
            StreamHelper.CopyStream(source, destination, progressHandler, 32);
            var result = destination.ToArray();
            CollectionAssert.AreEqual(data, result);
        }

        [TestMethod]
        public async Task Test_CopyStreamAsync()
        {
            var data = GetLoremIpsumData();
            var source = new MemoryStream(data);
            var destination = new MemoryStream();
            bool first = true;
            Func<StreamCopyProgress, Task> progressHandler = (x) =>
            {
                Assert.IsTrue(ReferenceEquals(source, x.Source));
                Assert.IsTrue(ReferenceEquals(destination, x.Destination));
                if (first)
                    Assert.AreEqual(32, x.CopiedBytes);
                first = false;
                return Task.CompletedTask;
            };
            await StreamHelper.CopyStreamAsync(source, destination, progressHandler, 32);
            var result = destination.ToArray();
            CollectionAssert.AreEqual(data, result);
        }

        [TestMethod]
        public void Test_ValidateStreamCopy()
        {
            Stream source;
            Stream destination;

            using (var context = new DisposableContext())
            {
                source = context.Add(new MemoryStream());
                destination = source;
                Assert.ThrowsException<ArgumentException>(() => StreamHelper.CopyStream(source, destination));
            }

            using (var context = new DisposableContext())
            {
                source = context.Add(new WriteOnlyStream(new MemoryStream()));
                destination = context.Add(new MemoryStream());
                Assert.ThrowsException<ArgumentException>(() => StreamHelper.CopyStream(source, destination));
            }

            using (var context = new DisposableContext())
            {
                source = context.Add(new MemoryStream());
                destination = context.Add(new ReadOnlyStream(new MemoryStream()));
                Assert.ThrowsException<ArgumentException>(() => StreamHelper.CopyStream(source, destination));
            }
        }
    }
}
