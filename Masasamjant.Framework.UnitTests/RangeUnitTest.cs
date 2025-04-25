namespace Masasamjant
{
    [TestClass]
    public class RangeUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Range_Integer()
        {
            var integerRange = Range.GetRange(0, 10, 2);
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
            var integerRange = Range.GetRange(0.0, 0.10, 0.02);
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
            var integerRange = Range.GetRange(0, 0, 2);
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
            var range = Range.GetRange(0, 10, 2);
            Assert.AreEqual(range, range.Clone());
            Assert.AreEqual(range, Range.GetRange(0, 10, 2));
            Assert.AreNotEqual(range, Range.GetRange(0, 10, 3));
            Assert.AreNotEqual(range, Range.GetRange(1, 10, 2));
            Assert.AreNotEqual(range, Range.GetRange(1, 12, 2));
            Assert.AreEqual(range.GetHashCode(), Range.GetRange(0, 10, 2).GetHashCode());
        }

        [TestMethod]
        public void Test_ToString()
        {
            var range = Range.GetRange(0, 10, 2);
            var expected = "0 2 4 6 8 10";
            var actual = range.ToString();
            Assert.AreEqual(expected, actual);
            expected = "0,2,4,6,8,10";
            actual = range.ToString(',');
            Assert.AreEqual(expected, actual);
        }
    }
}
