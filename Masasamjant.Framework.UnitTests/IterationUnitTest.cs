namespace Masasamjant
{
    [TestClass]
    public class IterationUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Validate_Index_Not_Negative()
        {
            int item = 0;
            int index = -1;
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => new Iteration<int>(item, index));
        }

        [TestMethod]
        public void Test_Constructor()
        {
            int item = 0;
            int index = 0;
            var iteration = new Iteration<int>(item, index);
            Assert.AreEqual(item, iteration.Item);
            Assert.AreEqual(index, iteration.Index);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var iteration = new Iteration<int>(0, 0);
            Assert.IsFalse(iteration.Equals(null));
            Assert.IsFalse(iteration.Equals(DateTime.Now));
            Iteration<int>? other = null;
            Assert.IsFalse(iteration.Equals(other));
            other = new Iteration<int>(0, 0);
            Assert.IsTrue(iteration.Equals(other));
            Assert.IsFalse(iteration.Equals(new Iteration<int>(1, 0)));
            Assert.IsFalse(iteration.Equals(new Iteration<int>(0, 1)));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var a = new Iteration<int>(0, 0);
            var b = new Iteration<int>(0, 0);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Test_ToString()
        {
            int item = 0;
            int index = 0;
            var expected = $"[{index}] {item}";
            var actual = new Iteration<int>(item, index).ToString();
            Assert.AreEqual(expected, actual);
        }
    }
}
