namespace Masasamjant
{
    [TestClass]
    public class DateTimeComparisonEqualityComparerUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_With_Undefined_Comparison()
        {
            DateTimeComparison comparison = (DateTimeComparison)999;
            new DateTimeComparisonEqualityComparer(comparison);
        }

        [TestMethod]
        public void Test_Constructor()
        {
            var comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Date);
            Assert.IsTrue(comparer.IgnoreDateTimeKind);
            Assert.AreEqual(DateTimeComparison.Date, comparer.Comparison);
            comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Time, false);
            Assert.IsFalse(comparer.IgnoreDateTimeKind);
            Assert.AreEqual(DateTimeComparison.Time, comparer.Comparison);
            comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.DateTime, true);
            Assert.IsTrue(comparer.IgnoreDateTimeKind);
            Assert.AreEqual(DateTimeComparison.DateTime, comparer.Comparison);
        }

        [TestMethod]
        public void Test_Equals_Date()
        {
            var ignoringComparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Date, true);
            var comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Date, false);
            var x = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            var y = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Utc);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
            x = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, y));
            x = new DateTime(2021, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 0, 0, 0, DateTimeKind.Local);
            Assert.IsFalse(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
        }

        [TestMethod]
        public void Test_Equals_Time()
        {
            var ignoringComparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Time, true);
            var comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Time, false);
            var x = new DateTime(2000, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            var y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Utc);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
            x = new DateTime(2000, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, y));
            x = new DateTime(2000, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 36, DateTimeKind.Local);
            Assert.IsFalse(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
        }

        [TestMethod]
        public void Test_Equals_DateTime()
        {
            var ignoringComparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.DateTime, true);
            var comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.DateTime, false);
            var x = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            var y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Utc);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
            
            x = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, y));
            
            x = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 36, DateTimeKind.Local);
            Assert.IsFalse(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));

            x = new DateTime(2020, 3, 2, 12, 31, 28, 0, DateTimeKind.Local);
            y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            Assert.IsFalse(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var ignoringComparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.DateTime, true);
            var comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.DateTime, false);
            var x = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            var y = new DateTime(2020, 3, 1, 12, 31, 28, 0, DateTimeKind.Local);
            Assert.AreNotEqual(ignoringComparer.GetHashCode(x), comparer.GetHashCode(x));
            Assert.AreEqual(ignoringComparer.GetHashCode(x), ignoringComparer.GetHashCode(y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));

            ignoringComparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Date, true);
            comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Date, false);
            Assert.AreNotEqual(ignoringComparer.GetHashCode(x), comparer.GetHashCode(x));
            Assert.AreEqual(ignoringComparer.GetHashCode(x), ignoringComparer.GetHashCode(y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));

            ignoringComparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Time, true);
            comparer = new DateTimeComparisonEqualityComparer(DateTimeComparison.Time, false);
            Assert.AreNotEqual(ignoringComparer.GetHashCode(x), comparer.GetHashCode(x));
            Assert.AreEqual(ignoringComparer.GetHashCode(x), ignoringComparer.GetHashCode(y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }
    }
}
