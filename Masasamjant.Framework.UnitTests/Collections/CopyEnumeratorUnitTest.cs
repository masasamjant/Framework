namespace Masasamjant.Collections
{
    [TestClass]
    public class CopyEnumeratorUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_When_Source_Is_CopyEnumerable()
        {
            var values = new List<int>() { 1, 2, 3 };
            var source = new CopyEnumerable<int>(values);
            new CopyEnumerator<int>(source);
        }

        [TestMethod]
        public void Test_CopyEnumerator()
        {
            var values = new List<int>() { 1, 2, 3 };
            var actual = new List<int>();
            var enumerator = new CopyEnumerator<int>(values);
            int n = 4;

            while (enumerator.MoveNext())
            {
                actual.Add(enumerator.Current);
                values.Add(n++);
            }

            var currentValues = new List<int>();

            enumerator.Reset();

            while (enumerator.MoveNext())
                currentValues.Add(enumerator.Current);

            CollectionAssert.AreEqual(new[] { 1, 2, 3 }, actual);
            Assert.AreEqual(6, currentValues.Count);
            CollectionAssert.AreEqual(currentValues, values);
        }
    }
}
