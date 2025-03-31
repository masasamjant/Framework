namespace Masasamjant.Collections.Adapters
{
    [TestClass]
    public class CollectionUnitTest : UnitTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            var collection = new TestCollection<int>();
            Assert.IsFalse(collection.IsReadOnly);
            Assert.AreEqual(0, collection.Count);
            IEnumerable<int> items = new int[] { 1, 2, 3 };
            collection = new TestCollection<int>(items);
            Assert.IsFalse(collection.IsReadOnly);
            Assert.AreEqual(3, collection.Count);
            ICollection<int> itemsCollection = new List<int>(items);
            collection = new TestCollection<int>(itemsCollection);
            Assert.IsFalse(collection.IsReadOnly);
            Assert.IsTrue(ReferenceEquals(collection.Items, itemsCollection));
        }

        [TestMethod]
        public void Test_SetReadOnly()
        {
            var collection = new TestCollection<int>();
            Assert.IsFalse(collection.IsReadOnly);
            collection.SetReadOnly();
            Assert.IsTrue(collection.IsReadOnly);
        }

        [TestMethod]
        public void Test_Add()
        {
            var collection = new TestCollection<int>();
            collection.Add(1);
            Assert.AreEqual(1, collection.Count);
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                collection.SetReadOnly();
                collection.Add(1);
            });
            Assert.AreEqual(1, collection.Count);
        }

        [TestMethod]
        public void Test_Remove()
        {
            var collection = new TestCollection<int>();
            collection.Add(1);
            collection.Add(2);
            Assert.IsTrue(collection.Remove(1));
            Assert.IsFalse(collection.Remove(1));
            Assert.IsTrue(collection.Contains(2));
            Assert.IsTrue(collection.Count == 1);
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                collection.SetReadOnly();
                collection.Remove(2);
            });
            Assert.IsTrue(collection.Contains(2));
            Assert.IsTrue(collection.Count == 1);
        }

        [TestMethod]
        public void Test_Clear()
        {
            var collection = new TestCollection<int>();
            collection.Add(1);
            collection.Add(2);
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
            collection.Add(1);
            Assert.ThrowsException<InvalidOperationException>(() =>
            {
                collection.SetReadOnly();
                collection.Clear();
            });
            Assert.AreEqual(1, collection.Count);
        }

        private class TestCollection<T> : Collection<T>
        {
            public TestCollection()
                : base()
            { }
            public TestCollection(IEnumerable<T> items)
                : base(items)
            { }

            public TestCollection(ICollection<T> items)
                : base(items)
            { }

            public new ICollection<T> Items => base.Items;
        }
    }
}
