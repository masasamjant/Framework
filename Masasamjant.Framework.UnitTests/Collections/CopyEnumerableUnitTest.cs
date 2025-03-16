namespace Masasamjant.Collections
{
    [TestClass]
    public class CopyEnumerableUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_When_Source_Is_CopyEnumerable()
        {
            var values = new List<int>() { 1, 2, 3 };
            var source = new CopyEnumerable<int>(values);
            new CopyEnumerable<int>(source);
        }

        [TestMethod]
        public void Test_CopyEnumerable()
        {
            var values = new List<int>() { 1, 2, 3 };
            var actual = new List<int>();
            var enumerable = new CopyEnumerable<int>(values);
            int n = 4;

            foreach (var item in enumerable)
            {
                actual.Add(item);
                values.Add(n++);
            }

            var currentValues = new List<int>();

            foreach (var item in enumerable)
                currentValues.Add(item);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, actual);
            Assert.AreEqual(6, currentValues.Count);
            CollectionAssert.AreEqual(currentValues, values);
        }
    }
}
