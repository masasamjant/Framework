namespace Masasamjant.Linq
{
    [TestClass]
    public class PageInfoUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var page = new PageInfo(0, 0);
            Assert.AreEqual(0, page.Index);
            Assert.AreEqual(0, page.Size);
            page = new PageInfo(3, 20);
            Assert.AreEqual(3, page.Index);
            Assert.AreEqual(20, page.Size);
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PageInfo(-1, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PageInfo(0, -1));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new PageInfo(1, 20) { TotalCount = -1 });
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
            Assert.ThrowsException<InvalidOperationException>(() => {
                page = new PageInfo(int.MaxValue, 10);
                page.Next();
            });
        }

        [TestMethod]
        public void Test_Equals()
        {
            Assert.IsTrue(new PageInfo(1, 10).Equals(new PageInfo(1, 10)));
            Assert.IsTrue((new PageInfo(1, 10) { TotalCount = 10 }).Equals(new PageInfo(1, 10) { TotalCount = 10 }));
            Assert.IsFalse((new PageInfo(1, 10) { TotalCount = 1 }).Equals(new PageInfo(1, 10) { TotalCount = 10 }));
            Assert.IsFalse((new PageInfo(10, 10) { TotalCount = 10 }).Equals(new PageInfo(1, 10) { TotalCount = 10 }));
            Assert.IsFalse((new PageInfo(1, 1) { TotalCount = 10 }).Equals(new PageInfo(1, 10) { TotalCount = 10 }));
            Assert.IsFalse(new PageInfo(1, 10).Equals(DateTime.Now));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            Assert.AreEqual(new PageInfo(1, 10) { TotalCount = 20 }.GetHashCode(), new PageInfo(1, 10) { TotalCount = 20 }.GetHashCode());
        }
    }
}
