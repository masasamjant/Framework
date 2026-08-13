namespace Masasamjant
{
    [TestClass]
    public class NumberHelperUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_GetHighestDigits()
        {
            var values1 = new List<double>
            {
                1.0,
                1.1,
                1.11,
                1.111,
                1.1111
            };
            Assert.AreEqual(4, NumberHelper.GetHighestDigits(values1));
            values1.RemoveAt(values1.Count - 1);
            Assert.AreEqual(3, NumberHelper.GetHighestDigits(values1));
            var values2 = new List<decimal>
            {
                1.0m,
                1.1m,
                1.11m,
                1.111m,
                1.1111m
            };
            Assert.AreEqual(4, NumberHelper.GetHighestDigits(values2));
            values2.RemoveAt(values2.Count - 1);
            Assert.AreEqual(3, NumberHelper.GetHighestDigits(values2));
        }

        [TestMethod]
        public void Test_GetPositives()
        {
            CollectionAssert.AreEquivalent(new int[] { 1, 2, 3 }, NumberHelper.GetPositives(new int[] { 1, 2, 3, -1, -2, -3 }).ToArray());
            CollectionAssert.AreEquivalent(new long[] { 1L, 2L, 3L }, NumberHelper.GetPositives(new long[] { 1L, 2L, 3L, -1L, -2L, -3L }).ToArray());
        }

        [TestMethod]
        public void Test_GetNegatives()
        {
            CollectionAssert.AreEquivalent(new int[] { -1, -2, -3 }, NumberHelper.GetNegatives(new int[] { 1, 2, 3, -1, -2, -3 }).ToArray());
            CollectionAssert.AreEquivalent(new long[] { -1L, -2L, -3L }, NumberHelper.GetNegatives(new long[] { 1L, 2L, 3L, -1L, -2L, -3L }).ToArray());
        }

        [TestMethod]
        public void Test_ParseInt32()
        {
            Assert.AreEqual(new int?(), NumberHelper.ParseInt32(string.Empty));
            Assert.AreEqual(new int?(), NumberHelper.ParseInt32("   "));
            Assert.AreEqual(new int?(25), NumberHelper.ParseInt32("25"));
            Assert.AreEqual(new int?(-25), NumberHelper.ParseInt32("-25"));
            Assert.AreEqual(new int?(25), NumberHelper.ParseInt32("> 25"));
            Assert.AreEqual(new int?(-25), NumberHelper.ParseInt32("> -25"));
            Assert.AreEqual(new int?(-25), NumberHelper.ParseInt32("> -> -25"));
            Assert.AreEqual(new int?(-25), NumberHelper.ParseInt32("> ->>25"));
            Assert.AreEqual(new int?(-25), NumberHelper.ParseInt32("> --25"));
            Assert.AreEqual(new int?(-255), NumberHelper.ParseInt32("> -25-5"));
            Assert.IsNull(NumberHelper.ParseInt32("Foo"));
        }

        [TestMethod]
        public void Test_ParseInt64()
        {
            Assert.AreEqual(new long?(), NumberHelper.ParseInt64(string.Empty));
            Assert.AreEqual(new long?(), NumberHelper.ParseInt64("   "));
            Assert.AreEqual(new long?(25), NumberHelper.ParseInt64("25"));
            Assert.AreEqual(new long?(-25), NumberHelper.ParseInt64("-25"));
            Assert.AreEqual(new long?(25), NumberHelper.ParseInt64("> 25"));
            Assert.AreEqual(new long?(-25), NumberHelper.ParseInt64("> -25"));
            Assert.AreEqual(new long?(-25), NumberHelper.ParseInt64("> -> -25"));
            Assert.AreEqual(new long?(-25), NumberHelper.ParseInt64("> ->>25"));
            Assert.AreEqual(new long?(-25), NumberHelper.ParseInt64("> --25"));
            Assert.AreEqual(new long?(-255), NumberHelper.ParseInt64("> -25-5"));
            Assert.IsNull(NumberHelper.ParseInt64("Foo"));
        }
    }
}
