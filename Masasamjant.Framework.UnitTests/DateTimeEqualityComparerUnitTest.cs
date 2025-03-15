namespace Masasamjant
{
    [TestClass]
    public class DateTimeEqualityComparerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructors()
        {
            var comparer = new DateTimeEqualityComparer();
            Assert.IsTrue(comparer.IgnoreDateTimeKind);
            comparer = new DateTimeEqualityComparer(true);
            Assert.IsTrue(comparer.IgnoreDateTimeKind);
            comparer = new DateTimeEqualityComparer(false);
            Assert.IsFalse(comparer.IgnoreDateTimeKind);
        }

        [TestMethod]
        public void Test_Equals()
        {
            var ignoringComparer = new DateTimeEqualityComparer(true);
            var comparer = new DateTimeEqualityComparer(false);
            var x = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Local);
            var y = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Utc);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
            y = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Local);
            Assert.IsTrue(ignoringComparer.Equals(x, y));
            Assert.IsTrue(comparer.Equals(x, y));
            y = new DateTime(2022, 3, 2, 16, 23, 23, 43, DateTimeKind.Local);
            Assert.IsFalse(ignoringComparer.Equals(x, y));
            Assert.IsFalse(comparer.Equals(x, y));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var ignoringComparer = new DateTimeEqualityComparer(true);
            var comparer = new DateTimeEqualityComparer(false);
            var x = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Local);
            var y = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Utc);
            Assert.AreEqual(ignoringComparer.GetHashCode(x), ignoringComparer.GetHashCode(y));
            Assert.AreNotEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
            y = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Local);
            Assert.AreEqual(ignoringComparer.GetHashCode(x), ignoringComparer.GetHashCode(y));
            Assert.AreEqual(comparer.GetHashCode(x), comparer.GetHashCode(y));
        }

        [TestMethod]
        public void Test_EqualTimes()
        {
            var comparer = new DateTimeEqualityComparerStub(true);
            var x = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Local);
            var y = new DateTime(2000, 6, 1, 12, 23, 23, 43, DateTimeKind.Local);
            Assert.IsTrue(comparer.EqualTimesStub(x, y));
            x = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Local);
            y = new DateTime(2022, 3, 2, 12, 23, 23, 23, DateTimeKind.Local);
            Assert.IsFalse(comparer.EqualTimesStub(x, y));
        }

        [TestMethod]
        public void Test_GetTimeHashCode()
        {
            var comparer = new DateTimeEqualityComparerStub(true);
            var x = new DateTime(2022, 3, 2, 12, 23, 23, 43, DateTimeKind.Local);
            var y = new DateTime(2000, 6, 1, 12, 23, 23, 43, DateTimeKind.Local);
            Assert.AreEqual(comparer.GetTimeHashCodeStub(x), comparer.GetTimeHashCodeStub(y));
        }

        private class DateTimeEqualityComparerStub : DateTimeEqualityComparer
        {
            public DateTimeEqualityComparerStub(bool ignoreDateTimeKind)
                : base(ignoreDateTimeKind)
            { }

            public bool EqualTimesStub(DateTime x, DateTime y) => EqualTimes(x, y);

            public int GetTimeHashCodeStub(DateTime obj) => GetTimeHashCode(obj);
        }
    }
}
