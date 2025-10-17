namespace Masasamjant.IO
{
    [TestClass]
    public class StreamCopyProgressUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            Stream source;
            Stream destination;

            using (var context = new DisposableContext())
            {
                source = context.Add(new MemoryStream());
                destination = source;
                Assert.ThrowsException<ArgumentException>(() => new StreamCopyProgress(source, destination, 0L));
            }

            using (var context = new DisposableContext()) 
            {
                source = context.Add(new MemoryStream());
                destination = context.Add(new MemoryStream());
                Assert.ThrowsException<ArgumentOutOfRangeException>(() => new StreamCopyProgress(source, destination, -1L));
            }

            using (var context = new DisposableContext()) 
            {
                source = context.Add(new MemoryStream());
                destination = context.Add(new MemoryStream());
                var copiedBytes = 32L;
                var progress = new StreamCopyProgress(source, destination, copiedBytes);
                Assert.IsTrue(ReferenceEquals(source, progress.Source));
                Assert.IsTrue(ReferenceEquals(destination, progress.Destination));
                Assert.AreEqual(copiedBytes, progress.CopiedBytes);
            }
        }
    }
}
