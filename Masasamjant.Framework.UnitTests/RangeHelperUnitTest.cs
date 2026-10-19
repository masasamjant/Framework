using System.Collections;

namespace Masasamjant
{
    [TestClass]
    public class RangeHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Range_Integer()
        {
            var integerRange = RangeHelper.GetRange(0, 10, 2);
            Assert.AreEqual(0, integerRange.Min);
            Assert.AreEqual(10, integerRange.Max);
            Assert.AreEqual(2, integerRange.Step);
            int[] expected = [0, 2, 4, 6, 8, 10];
            int[] actual = integerRange.Values.ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Range_Fractional()
        {
            var integerRange = RangeHelper.GetRange(0.0, 0.10, 0.02);
            Assert.AreEqual(0.0, integerRange.Min);
            Assert.AreEqual(0.10, integerRange.Max);
            Assert.AreEqual(0.02, integerRange.Step);
            double[] expected = [0.00, 0.02, 0.04, 0.06, 0.08, 0.10];
            double[] actual = integerRange.Values.ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Range_Empty()
        {
            var integerRange = RangeHelper.GetRange(0, 0, 2);
            Assert.AreEqual(0, integerRange.Min);
            Assert.AreEqual(0, integerRange.Max);
            Assert.AreEqual(2, integerRange.Step);
            int[] expected = [];
            int[] actual = integerRange.Values.ToArray();
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Range_Equality()
        {
            var range = RangeHelper.GetRange(0, 10, 2);
            Assert.AreEqual(range, range.Clone());
            Assert.AreEqual(range, RangeHelper.GetRange(0, 10, 2));
            Assert.AreNotEqual(range, RangeHelper.GetRange(0, 10, 3));
            Assert.AreNotEqual(range, RangeHelper.GetRange(1, 10, 2));
            Assert.AreNotEqual(range, RangeHelper.GetRange(1, 12, 2));
            Assert.AreEqual(range.GetHashCode(), RangeHelper.GetRange(0, 10, 2).GetHashCode());
            object? obj = RangeHelper.GetRange(0, 10, 2);
            Assert.IsTrue(range.Equals(obj));
        }

        [TestMethod]
        public void Test_ToString()
        {
            var range = RangeHelper.GetRange(0, 10, 2);
            var expected = "0 2 4 6 8 10";
            var actual = range.ToString();
            Assert.AreEqual(expected, actual);
            expected = "0,2,4,6,8,10";
            actual = range.ToString(',');
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_GetRanges()
        {
            AssertRange(RangeHelper.GetRange((byte)0, (byte)10, (byte)2));
            AssertRange(RangeHelper.GetRange((short)0, (short)10, (short)2));
            AssertRange(RangeHelper.GetRange((ushort)0, (ushort)10, (ushort)2));
            AssertRange(RangeHelper.GetRange((uint)0, (uint)10, (uint)2));
            AssertRange(RangeHelper.GetRange(0L, 10L, 2L));
            AssertRange(RangeHelper.GetRange(0UL, 10UL, 2UL));
            AssertRange(RangeHelper.GetRange(0M, 10M, 2M));
            AssertRange(RangeHelper.GetRange(0F, 10F, 2F));
        }

        private static void AssertRange<T>(Range<T> range) where T : IComparable<T>
        {
            var expected = "0 2 4 6 8 10";
            var actual = range.ToString();
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Clone()
        {
            ICloneable clonable = RangeHelper.GetRange(1, 2, 1);
            var range = RangeHelper.GetRange(1, 2, 1);
            Assert.AreEqual(range, clonable.Clone());
        }

        [TestMethod]
        public void Test_GetEnumerator()
        {
            IEnumerable enumerable1 = RangeHelper.GetRange(1, 2, 1);
            IEnumerable<int> enumerable2 = RangeHelper.GetRange(1, 2, 1);
            var enumerator1 = enumerable1.GetEnumerator();
            var enumerator2 = enumerable2.GetEnumerator();
            Assert.AreEqual(enumerator1.GetType(), enumerator2.GetType());
        }
    }
}
