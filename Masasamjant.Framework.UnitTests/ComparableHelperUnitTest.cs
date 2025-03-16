namespace Masasamjant
{
    [TestClass]
    public class ComparableHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Min()
        {
            Assert.AreEqual(10, ComparableHelper.Min(10, 10));
            Assert.AreEqual(10, ComparableHelper.Min(10, 100));
            Assert.AreEqual(10, ComparableHelper.Min(100, 10));
        }

        [TestMethod]
        public void Test_Max()
        {
            Assert.AreEqual(10, ComparableHelper.Max(10, 10));
            Assert.AreEqual(100, ComparableHelper.Max(10, 100));
            Assert.AreEqual(100, ComparableHelper.Max(100, 10));
        }

        [TestMethod]
        public void Test_IsGreaterThan()
        {
            Assert.IsFalse(ComparableHelper.IsGreaterThan(10, 100));
            Assert.IsFalse(ComparableHelper.IsGreaterThan(100, 100));
            Assert.IsTrue(ComparableHelper.IsGreaterThan(110, 100));
        }

        [TestMethod]
        public void Test_IsGreaterThanOrEqual()
        {
            Assert.IsFalse(ComparableHelper.IsGreaterThanOrEqual(10, 100));
            Assert.IsTrue(ComparableHelper.IsGreaterThanOrEqual(100, 100));
            Assert.IsTrue(ComparableHelper.IsGreaterThanOrEqual(110, 100));
        }

        [TestMethod]
        public void Test_IsLessThan()
        {
            Assert.IsTrue(ComparableHelper.IsLessThan(10, 100));
            Assert.IsFalse(ComparableHelper.IsLessThan(100, 100));
            Assert.IsFalse(ComparableHelper.IsLessThan(110, 100));
        }

        [TestMethod]
        public void Test_IsLessThanOrEqual()
        {
            Assert.IsTrue(ComparableHelper.IsLessThanOrEqual(10, 100));
            Assert.IsTrue(ComparableHelper.IsLessThanOrEqual(100, 100));
            Assert.IsFalse(ComparableHelper.IsLessThanOrEqual(110, 100));
        }

        [TestMethod]
        public void Test_IsEqual()
        {
            Assert.IsFalse(ComparableHelper.IsEqual(10, 100));
            Assert.IsTrue(ComparableHelper.IsEqual(100, 100));
            Assert.IsFalse(ComparableHelper.IsEqual(110, 100));
        }

        [TestMethod]
        public void Test_IsNotEqual()
        {
            Assert.IsTrue(ComparableHelper.IsNotEqual(10, 100));
            Assert.IsFalse(ComparableHelper.IsNotEqual(100, 100));
            Assert.IsTrue(ComparableHelper.IsNotEqual(110, 100));
        }

        [TestMethod]
        public void Test_Comparable_IsGreaterThan()
        {
            Assert.IsFalse(ComparableHelper.IsGreaterThan((IComparable)10, 100));
            Assert.IsFalse(ComparableHelper.IsGreaterThan((IComparable)100, 100));
            Assert.IsTrue(ComparableHelper.IsGreaterThan((IComparable)110, 100));
        }

        [TestMethod]
        public void Test_Comparable_IsGreaterThanOrEqual()
        {
            Assert.IsFalse(ComparableHelper.IsGreaterThanOrEqual((IComparable)10, 100));
            Assert.IsTrue(ComparableHelper.IsGreaterThanOrEqual((IComparable)100, 100));
            Assert.IsTrue(ComparableHelper.IsGreaterThanOrEqual((IComparable)110, 100));
        }

        [TestMethod]
        public void Test_Comparable_IsLessThan()
        {
            Assert.IsTrue(ComparableHelper.IsLessThan((IComparable)10, 100));
            Assert.IsFalse(ComparableHelper.IsLessThan((IComparable)100, 100));
            Assert.IsFalse(ComparableHelper.IsLessThan((IComparable)110, 100));
        }

        [TestMethod]
        public void Test_Comparable_IsLessThanOrEqual()
        {
            Assert.IsTrue(ComparableHelper.IsLessThanOrEqual((IComparable)10, 100));
            Assert.IsTrue(ComparableHelper.IsLessThanOrEqual((IComparable)100, 100));
            Assert.IsFalse(ComparableHelper.IsLessThanOrEqual((IComparable)110, 100));
        }

        [TestMethod]
        public void Test_Comparable_IsEqual()
        {
            Assert.IsFalse(ComparableHelper.IsEqual((IComparable)10, 100));
            Assert.IsTrue(ComparableHelper.IsEqual((IComparable)100, 100));
            Assert.IsFalse(ComparableHelper.IsEqual((IComparable)110, 100));
        }

        [TestMethod]
        public void Test_Comparable_IsNotEqual()
        {
            Assert.IsTrue(ComparableHelper.IsNotEqual((IComparable)10, 100));
            Assert.IsFalse(ComparableHelper.IsNotEqual((IComparable)100, 100));
            Assert.IsTrue(ComparableHelper.IsNotEqual((IComparable)110, 100));
        }

        [TestMethod]
        public void Test_TryCompare()
        {
            int? x = null;
            int? y = null;
            int? result;
            Assert.IsTrue(ComparableHelper.TryCompare(x, y, out result) && result == 0);
            x = 0;
            y = 0;
            Assert.IsTrue(ComparableHelper.TryCompare(x, y, out result) && result == 0);
            Comparable cx = new Comparable(0);
            Comparable cy = new Comparable(0);
            Assert.IsTrue(ComparableHelper.TryCompare(cx, cy, out result) && result == 0);
        }

        [TestMethod]
        public void Test_TryCompare_Objects()
        {
            int? x = null;
            int? y = null;
            int? result;
            Assert.IsTrue(ComparableHelper.TryCompare((object?)x, y, out result) && result == 0);
            x = 0;
            y = 0;
            Assert.IsTrue(ComparableHelper.TryCompare((object?)x, y, out result) && result == 0);
            Comparable cx = new Comparable(0);
            Comparable cy = new Comparable(0);
            Assert.IsTrue(ComparableHelper.TryCompare((object?)cx, cy, out result) && result == 0);
        }

        private class Comparable : IComparable
        {
            public Comparable(int value)
            {
                Value = value;
            }

            public int Value { get; }

            public int CompareTo(object? obj)
            {
                if (obj is Comparable other)
                    return Value.CompareTo(other.Value);
                else
                    throw new ArgumentException("Object is not comparable.");
            }
        }
    }
}
