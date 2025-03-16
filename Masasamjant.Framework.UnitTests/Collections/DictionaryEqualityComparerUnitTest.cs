namespace Masasamjant.Collections
{
    [TestClass]
    public class DictionaryEqualityComparerUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Equals()
        {
            var comparer = DictionaryEqualityComparer.CreateDefaultComparer<int, int>();
            Dictionary<int, int>? x = null;
            Dictionary<int, int>? y = null;
            Assert.IsTrue(comparer.Equals(x, y));
            x = new Dictionary<int, int>();
            Assert.IsFalse(comparer.Equals(x, y));
            x = null;
            y = new Dictionary<int, int>();
            Assert.IsFalse(comparer.Equals(x, y));
            x = y;
            Assert.IsTrue(comparer.Equals(x, y));
            x = new Dictionary<int, int>();
            y = new Dictionary<int, int>();
            Assert.IsTrue(comparer.Equals(x, y));
            x.Add(1, 1);
            Assert.IsFalse(comparer.Equals(x, y));
            y.Add(1, 1);
            Assert.IsTrue(comparer.Equals(x, y));
            x.Add(2, 2);
            y.Add(2, 1);
            Assert.IsFalse(comparer.Equals(x, y));
        }

        [TestMethod]
        public void Test_GetHashCode()
        {
            var dictionary = new Dictionary<int, int>();
            var comparer = DictionaryEqualityComparer.CreateDefaultComparer<int, int>();
            int expected = 0;
            int actual = comparer.GetHashCode(dictionary);
            Assert.AreEqual(expected, actual);
            var other = new Dictionary<int, int>();
            dictionary.Add(1, 1);
            dictionary.Add(2, 4);
            other.Add(1, 1);
            other.Add(2, 4);
            Assert.AreEqual(comparer.GetHashCode(dictionary), comparer.GetHashCode(other));
        }
    }
}
