namespace Masasamjant.Collections.Adapters
{
    [TestClass]
    public class ReadOnlyDictionaryAdapterUnitTest : UnitTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_Constructor_When_Same_Type()
        {
            var source = new ReadOnlyDictionaryAdapter<int, int>(DictionaryHelper.CreateReadOnly<int, int>());
            new ReadOnlyDictionaryAdapter<int, int>(source);
        }

        [TestMethod]
        public void Test_Contains_Items_From_Source()
        {
            var dictionary = new Dictionary<int, int>() { { 1, 1 }, { 2, 2 }, { 3, 3 } };
            var adapter = new ReadOnlyDictionaryAdapter<int, int>(dictionary.AsReadOnly());
            Assert.IsTrue(adapter.IsReadOnly);
            Assert.IsTrue(adapter.Count == 3);
            Assert.IsTrue(adapter[1] == 1);
            Assert.IsTrue(adapter[2] == 2);
            Assert.IsTrue(adapter[3] == 3);
            CollectionAssert.AreEquivalent(new int[] { 1, 2, 3 }, adapter.Keys.ToArray());
            CollectionAssert.AreEquivalent(new int[] { 1, 2, 3 }, adapter.Values.ToArray());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void Test_Set_Key_Throws()
        {
            var dictionary = new Dictionary<int, int>() { { 1, 1 }, { 2, 2 }, { 3, 3 } };
            var adapter = new ReadOnlyDictionaryAdapter<int, int>(dictionary.AsReadOnly());
            adapter[1] = 4;
        }
    }
}
