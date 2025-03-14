namespace Masasamjant
{
    [TestClass]
    public class ObjectHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Swap()
        {
            int left = 10;
            int right = 100;
            ObjectHelper.Swap(ref left, ref right);
            Assert.AreEqual(100, left);
            Assert.AreEqual(10, right);
        }

        [TestMethod]
        public void Test_When_No_Match_Then_SwapIf_Returns_False_And_No_Swap()
        {
            int left = 10;
            int right = 100;
            Func<int, int, bool> match = (x, y) => x > y;
            Assert.IsFalse(ObjectHelper.SwapIf(ref left, ref right, match));
            Assert.AreEqual(10, left);
            Assert.AreEqual(100, right);
        }

        [TestMethod]
        public void Test_When_Match_Then_SwapIf_Returns_True_And_Swap()
        {
            int left = 10;
            int right = 100;
            Func<int, int, bool> match = (x, y) => x < y;
            Assert.IsTrue(ObjectHelper.SwapIf(ref left, ref right, match));
            Assert.AreEqual(100, left);
            Assert.AreEqual(10, right);
        }
    }
}
