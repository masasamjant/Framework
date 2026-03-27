using NSubstitute;
using NSubstitute.ExceptionExtensions;

namespace Masasamjant.IO
{
    [TestClass]
    public class IOHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetBufferSize()
        {
            Assert.AreEqual(IOHelper.SmallBufferSize, IOHelper.GetBufferSize(-1));
            Assert.AreEqual(IOHelper.SmallBufferSize, IOHelper.GetBufferSize(IOHelper.SmallBufferSizeLimit - 1));
            Assert.AreEqual(IOHelper.MediumBufferSize, IOHelper.GetBufferSize(IOHelper.SmallBufferSizeLimit + 1));
            Assert.AreEqual(IOHelper.LargeBufferSize, IOHelper.GetBufferSize(IOHelper.MediumBufferSizeLimit + 1));
        }

        [TestMethod]
        public void Test_GetBufferSize_WhenStreamNull_ThenThrows()
        {
            Assert.ThrowsException<ArgumentNullException>(() => IOHelper.GetBufferSize((Stream)null!));
        }

        [TestMethod]
        public void Test_GetBufferSize_WhenStreamWithoutLength_ThenReturnsSmallBufferSize()
        {
            using var stream = new MemoryStream();
            Assert.AreEqual(IOHelper.SmallBufferSize, IOHelper.GetBufferSize(stream));
        }

        [TestMethod]
        public void Test_GetBufferSize_WhenStreamLengthThrow_ThenReturnsSmallBufferSize()
        {
            var stream = Substitute.For<Stream>();
            stream.Length.Throws(new NotSupportedException());
            Assert.AreEqual(IOHelper.SmallBufferSize, IOHelper.GetBufferSize(stream));
        }

        [TestMethod]
        public void Test_GetBufferSize_WhenStreamWithLength_ThenReturnsBufferSizeBasedOnLength()
        {
            using var stream = new MemoryStream(new byte[IOHelper.SmallBufferSizeLimit + 1]);
            Assert.AreEqual(IOHelper.MediumBufferSize, IOHelper.GetBufferSize(stream));
        }

        [TestMethod]
        public void Test_GetBufferSize_WhenFileInfoNull_ThenThrows()
        {
            Assert.ThrowsException<ArgumentNullException>(() => IOHelper.GetBufferSize((FileInfo)null!));
        }

        [TestMethod]
        public void Test_GetBufferSize_WhenFileInfoNotExist_ThenReturnsSmallBufferSize()
        {
            var fileInfo = new FileInfo(NotExistFilePath);
            Assert.AreEqual(IOHelper.SmallBufferSize, IOHelper.GetBufferSize(fileInfo));
        }

        [TestMethod]
        public void Test_GetBufferSize_ValidateFilePath()
        {
            Assert.ThrowsException<ArgumentNullException>(() => IOHelper.GetBufferSize((string)null!));
            Assert.ThrowsException<ArgumentNullException>(() => IOHelper.GetBufferSize(""));
            Assert.ThrowsException<ArgumentNullException>(() => IOHelper.GetBufferSize("  "));
        }

        [TestMethod]
        public void Test_GetBufferSize_WhenFileExist_ThenReturnsBufferSizeBasedOnLength()
        {
            var smallFilePath = GenerateLargeTextFile(IOHelper.SmallBufferSizeLimit - 1024);
            var smallFileInfo = new FileInfo(smallFilePath);
            var mediumFilePath = GenerateLargeTextFile(IOHelper.MediumBufferSizeLimit - 1024);
            var mediumFileInfo = new FileInfo(mediumFilePath);
            var largeFilePath = GenerateLargeTextFile(IOHelper.MediumBufferSizeLimit + 1024);
            var largeFileInfo = new FileInfo(largeFilePath);
            Assert.AreEqual(IOHelper.SmallBufferSize, IOHelper.GetBufferSize(smallFileInfo));
            Assert.AreEqual(IOHelper.MediumBufferSize, IOHelper.GetBufferSize(mediumFileInfo));
            Assert.AreEqual(IOHelper.LargeBufferSize, IOHelper.GetBufferSize(largeFileInfo));
            FileHelper.DeleteFiles(smallFilePath, mediumFilePath, largeFilePath);
        }
    }
}
