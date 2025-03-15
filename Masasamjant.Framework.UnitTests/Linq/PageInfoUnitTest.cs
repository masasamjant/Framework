namespace Masasamjant.Linq
{
    [TestClass]
    public class PageInfoUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Constructor_With_Negative_Index()
        {
            var index = -1;
            var size = 0;
            new PageInfo(index, size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Constructor_With_Negative_Size()
        {
            var index = 0;
            var size = -1;
            new PageInfo(index, size);
        }

        [TestMethod]
        public void Test_Constructor()
        {
            var page = new PageInfo(0, 0);
            Assert.AreEqual(0, page.Index);
            Assert.AreEqual(0, page.Size);
            page = new PageInfo(3, 20);
            Assert.AreEqual(3, page.Index);
            Assert.AreEqual(20, page.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Test_Set_Negative_TotalCount()
        {
            var page = new PageInfo(1, 20);
            page.TotalCount = -1;
        }

        [TestMethod]
        public void Test_TotalCount()
        {
            var page = new PageInfo(1, 20);
            Assert.IsTrue(page.TotalCount < 0);
            page.TotalCount = 100;
            Assert.AreEqual(100, page.TotalCount);
        }

        [TestMethod]
        public void Test_PageCount()
        {
            var page = new PageInfo(1, 0);
            page.TotalCount = 100;
            Assert.AreEqual(0, page.PageCount);
            page = new PageInfo(1, 20);
            Assert.AreEqual(0, page.PageCount);
            page.TotalCount = 100;
            Assert.AreEqual(5, page.PageCount);
        }

        [TestMethod]
        public void Test_Previous()
        {
            var page = new PageInfo(0, 10);
            var prev = page.Previous();
            Assert.IsTrue(ReferenceEquals(page, prev));
            page = new PageInfo(1, 10);
            prev = page.Previous();
            Assert.AreEqual(0, prev.Index);
            Assert.AreEqual(10, prev.Size);
        }

        [TestMethod]
        public void Test_Next()
        {
            var page = new PageInfo(0, 10);
            var next = page.Next();
            Assert.AreEqual(1, next.Index);
            Assert.AreEqual(10, next.Size);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Next_Max()
        {
            var page = new PageInfo(int.MaxValue, 10);
            page.Next();
        }
    }
}
