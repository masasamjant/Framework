namespace Masasamjant
{
    [TestClass]
    public class MinMaxPairUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_MinMaxPairs()
        {
            var a = new MinMaxPair<int>(1, 1);
            var b = MinMaxPair.Create(1, 1);
            Assert.AreEqual(a, b);
            Assert.AreEqual(1, a.Min);
            Assert.AreEqual(1, a.Max);
            var c = MinMaxPair.Create(1, 2);
            var d = MinMaxPair.Create(2, 1);
            Assert.AreEqual(c, d);
            Assert.AreEqual(1, c.Min);
            Assert.AreEqual(2, c.Max);
            Assert.AreEqual(c.GetHashCode(), d.GetHashCode());
            Assert.IsTrue(c == d);
            Assert.IsTrue(c != a);
            Assert.IsTrue(c.Equals(d));
            Assert.IsTrue(d.Equals(c));
            Assert.IsFalse(c.Equals(a));
            Assert.IsFalse(a.Equals(c));
            Assert.AreEqual(c.ToString(), d.ToString());
            Assert.AreNotEqual(c.ToString(), a.ToString());
        }
    }
}
