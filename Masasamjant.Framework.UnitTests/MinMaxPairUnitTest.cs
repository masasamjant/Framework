namespace Masasamjant
{
    [TestClass]
    public class MinMaxPairUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var pair = new MinMaxPair<int>(1, 1);
            Assert.AreEqual(1, pair.Min);
            Assert.AreEqual(1, pair.Max);
            pair = new MinMaxPair<int>(23, 4);
            Assert.AreEqual(23, pair.Max);
            Assert.AreEqual(4, pair.Min);
            pair = new MinMaxPair<int>(4, 23);
            Assert.AreEqual(23, pair.Max);
            Assert.AreEqual(4, pair.Min);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var pair = new MinMaxPair<int>(23, 45);
            Assert.IsFalse(pair.Equals(null));
            Assert.IsFalse(pair.Equals(DateTime.Now));
            Assert.IsTrue(pair.Equals(new MinMaxPair<int>(23, 45)));
            Assert.IsTrue(pair.Equals(new MinMaxPair<int>(45, 23)));
            Assert.IsFalse(pair.Equals(new MinMaxPair<int>(22, 45)));
            Assert.IsFalse(pair.Equals(new MinMaxPair<int>(23, 44)));
            Assert.IsTrue(pair == new MinMaxPair<int>(23, 45));
            Assert.IsTrue(pair == new MinMaxPair<int>(45, 23));
            Assert.IsFalse(pair == new MinMaxPair<int>(4, 5));
            Assert.IsTrue(pair != new MinMaxPair<int>(4, 5));
            Assert.IsFalse(pair != new MinMaxPair<int>(23, 45));
            Assert.IsFalse(pair != new MinMaxPair<int>(45, 23));
            object? obj = new MinMaxPair<int>(23, 45);
            Assert.IsTrue(pair.Equals(obj));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var a = new MinMaxPair<int>(23, 45);
            var b = new MinMaxPair<int>(45, 23);
            Assert.AreEqual(a.GetHashCode(), b.GetHashCode());
        }

        [TestMethod]
        public void Test_ToString()
        {
            int min = 3;
            int max = 5;
            var expected = $"Min={min}, Max={max}";
            var actual = new MinMaxPair<int>(min, max).ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Factory()
        {
            var pair = new MinMaxPair<int>(23, 45);
            var other = MinMaxPair.Create(23, 45);
            Assert.AreEqual(pair, other);
        }
    }
}
